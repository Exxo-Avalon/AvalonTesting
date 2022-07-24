using System;
using Avalon.Common;
using Avalon.Tiles;
using IL.Terraria;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.Utils;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Both)]
public class WorldGenEdits : ModHook
{
    protected override ModHook[] HookDependencies => new ModHook[] { ModContent.GetInstance<GenPasses>() };

    public static void AddEvilAltarAlternativeChecks(ILContext il) =>
        Utilities.AddAlternativeIdChecks(il, TileID.DemonAltar, id => id == ModContent.TileType<IckyAltar>());

    public static void AddAllAltarAlternativeChecks(ILContext il) =>
        Utilities.AddAlternativeIdChecks(il, TileID.DemonAltar, id => Data.Sets.Tile.Altar[id]);

    protected override void Apply()
    {
        ModContent.GetInstance<GenPasses>().HookGenPassShinies += ILGenPassShinies;
        WorldGen.AddBuriedChest_int_int_int_bool_int_bool_ushort += AddAllAltarAlternativeChecks;
        WorldGen.Place3x2 += AddAllAltarAlternativeChecks;
        WorldGen.Check3x2 += ILCheck3X2;
        WorldGen.badOceanCaveTiles += AddAllAltarAlternativeChecks;
        WorldGen.AllowsSandfall += AddAllAltarAlternativeChecks;
        Player.ItemCheck_UseMiningTools_ActuallyUseMiningTool += AddEvilAltarAlternativeChecks;
    }

    private static void ILGenPassShinies(ILContext il)
    {
        var c = new ILCursor(il);
        FieldReference? copperField = default;
        c.GotoNext(i => i.MatchStfld(out copperField));
        Type methodType = copperField?.DeclaringType.ResolveReflection()!;

        SoftInterceptOreGen(il, i => i.MatchLdfld(methodType.GetField("copper")),
            () => Terraria.WorldGen.SavedOreTiers.Copper);
        SoftInterceptOreGen(il, i => i.MatchLdfld(methodType.GetField("iron")),
            () => Terraria.WorldGen.SavedOreTiers.Iron);
        SoftInterceptOreGen(il, i => i.MatchLdfld(methodType.GetField("silver")),
            () => Terraria.WorldGen.SavedOreTiers.Silver);
        SoftInterceptOreGen(il, i => i.MatchLdfld(methodType.GetField("gold")),
            () => Terraria.WorldGen.SavedOreTiers.Gold);
    }

    private static void SoftInterceptOreGen(ILContext il, Func<Instruction, bool> predicate, Func<int> valueProvider)
    {
        var c = new ILCursor(il);

        while (c.TryGotoNext(predicate))
        {
            c.Index++;
            c.Emit(OpCodes.Pop)
                .EmitDelegate(valueProvider);
        }
    }

    private static void ILCheck3X2(ILContext il)
    {
        var c = new ILCursor(il);
        ILLabel? altarLabel = null;
        c.GotoNext(i => i.MatchLdcI4(533));
        int beginIndex = c.Index;
        c.GotoNext(i => i.MatchLdcI4(TileID.DemonAltar))
            .GotoNext(i => i.MatchBeq(out altarLabel));
        c.Index = beginIndex;

        c.EmitDelegate<Func<int, bool>>(type => Data.Sets.Tile.Altar[type]);
        c.Emit(OpCodes.Brtrue, altarLabel)
            .Emit(OpCodes.Ldarg_2);
    }
}
