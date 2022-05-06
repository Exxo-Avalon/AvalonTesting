using System;
using System.Collections.Generic;
using AvalonTesting.Common;
using AvalonTesting.Tiles;
using IL.Terraria;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Hooks;

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
        var instructions = new Instruction[4];
        var delegates = new Func<int>[4]
        {
            () => Terraria.WorldGen.SavedOreTiers.Copper, () => Terraria.WorldGen.SavedOreTiers.Iron,
            () => Terraria.WorldGen.SavedOreTiers.Silver, () => Terraria.WorldGen.SavedOreTiers.Gold,
        };
        var c = new ILCursor(il);

        for (int i = 0; i < instructions.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (!c.TryGotoNext(i => i.MatchCall<Terraria.WorldGen>(nameof(Terraria.WorldGen.TileRunner))))
                {
                    return;
                }
            }

            if (!c.TryGotoPrev(i => i.MatchLdfld(out _)))
            {
                return;
            }

            instructions[i] = c.Next;
        }

        for (int i = 0; i < instructions.Length; i++)
        {
            SoftInterceptOreGen(il, instructions[i], delegates[i]);
        }
    }

    private static void SoftInterceptOreGen(ILContext il, Instruction i1, Func<int> del)
    {
        var c = new ILCursor(il);

        while (c.TryGotoNext(i => i.OpCode == i1.OpCode && i.Operand == i1.Operand))
        {
            c.Index++;
            c.Emit(OpCodes.Pop);
            c.EmitDelegate(del);
        }
    }

    private static void ILCheck3X2(ILContext il)
    {
        var c = new ILCursor(il);
        try
        {
            ILLabel altarLabel = null;
            c.GotoNext(i => i.MatchLdcI4(533));
            int beginIndex = c.Index;
            c.GotoNext(i => i.MatchLdcI4(TileID.DemonAltar));
            c.GotoNext(i => i.MatchBeq(out altarLabel));
            c.Index = beginIndex;
            c.EmitDelegate<Func<int, bool>>(type => Data.Sets.Tile.Altar[type]);
            c.Emit(OpCodes.Brtrue, altarLabel);
            c.Emit(OpCodes.Ldarg_2);
        }
        catch (KeyNotFoundException e)
        {
            AvalonTesting.Mod.Logger.Error("Failed to apply hook!", e);
        }
    }
}
