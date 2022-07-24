using System;
using Avalon.Common;
using Avalon.Systems;
using Avalon.Tiles;
using Avalon.Tiles.Ores;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Hooks;

// TODO:
// "Tile Cleanup" has two references for placing orbs
// FinishGetGoodWorld() world has a reference
// MakeDungeon() for the chest
// SmashAltar for tiles that are added on smash altar
// GERunner for tiles on hardmode evil enter
[Autoload(Side = ModSide.Both)]
public class ContagionWorldGen : ModHook
{
    protected override ModHook[] HookDependencies => new ModHook[] { ModContent.GetInstance<GenPasses>() };

    protected override void Apply()
    {
        ModContent.GetInstance<GenPasses>().HookGenPassReset += ILGenPassReset;
        ModContent.GetInstance<GenPasses>().HookGenPassAltars += ILGenPassAltars;
        ModContent.GetInstance<GenPasses>().HookGenPassShinies += ILGenPassShinies;
    }

    private static void ILGenPassShinies(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(i => i.MatchRet())
            .GotoPrev(i => i.MatchLdsfld<WorldGen>(nameof(WorldGen.drunkWorldGen)))
            .GotoNext(i => i.MatchBrfalse(out _));
        c.Index++;

        // Drunk world seed baccilite generation
        c.EmitDelegate(() =>
        {
            for (int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 2.25E-05 / 2.0); i++)
            {
                WorldGen.TileRunner(
                    WorldGen.genRand.Next(0, Main.maxTilesX),
                    WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY), WorldGen.genRand.Next(3, 6),
                    WorldGen.genRand.Next(4, 8), ModContent.TileType<BacciliteOre>());
            }
        });

        c.GotoNext(i => i.MatchRet())
            .GotoNext(i => i.MatchBr(out _));

        ILLabel startCorruptionGen = c.DefineLabel();

        c.EmitDelegate(() => ModContent.GetInstance<ExxoWorldGen>().WorldEvil != ExxoWorldGen.EvilBiome.Corruption);
        c.Emit(OpCodes.Brfalse, startCorruptionGen);

        c.EmitDelegate(() =>
        {
            if (ModContent.GetInstance<ExxoWorldGen>().WorldEvil == ExxoWorldGen.EvilBiome.Contagion)
            {
                for (int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 2.25E-05); i++)
                {
                    WorldGen.TileRunner(
                        WorldGen.genRand.Next(0, Main.maxTilesX),
                        WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY), WorldGen.genRand.Next(3, 6),
                        WorldGen.genRand.Next(4, 8), ModContent.TileType<BacciliteOre>());
                }
            }
        });

        c.Emit(OpCodes.Ret);
        c.MarkLabel(startCorruptionGen);
    }

    private static void ILGenPassReset(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(i => i.MatchStsfld<WorldGen>(nameof(WorldGen.crimson)))
            .GotoNext(i => i.MatchLdfld(out _));

        c.EmitDelegate<Action>(() =>
            WorldGen.crimson = ModContent.GetInstance<ExxoWorldGen>().WorldEvil == ExxoWorldGen.EvilBiome.Crimson);

        c.GotoNext(i => i.MatchRet())
            .GotoPrev(i => i.MatchBneUn(out _))
            .GotoPrev(i => i.MatchLdcI4(-1));

        c.EmitDelegate<Func<int, int>>(dungeonSide =>
        {
            ModContent.GetInstance<ExxoWorldGen>().DungeonSide = dungeonSide;
            return dungeonSide;
        });

        for (int i = 0; i < 2; i++)
        {
            c.GotoNext(inst => inst.MatchRet());
            c.Index--;

            c.EmitDelegate<Func<int, int>>(dungeonLocation =>
            {
                ModContent.GetInstance<ExxoWorldGen>().DungeonLocation = dungeonLocation;
                return dungeonLocation;
            });

            c.Index += 2;
        }
    }

    private static void ILGenPassAltars(ILContext il)
    {
        var c = new ILCursor(il);

        ILLabel endNormalAltar = c.DefineLabel();
        ILLabel startNormalAltar = c.DefineLabel();

        c.GotoNext(i => i.MatchLdsfld<WorldGen>(nameof(WorldGen.crimson)));

        c.EmitDelegate(() => ModContent.GetInstance<ExxoWorldGen>().WorldEvil != ExxoWorldGen.EvilBiome.Corruption);
        c.Emit(OpCodes.Brfalse, startNormalAltar)
            .Emit(OpCodes.Ldloc, 3)
            .Emit(OpCodes.Ldloc, 4);
        c.EmitDelegate((int x, int y) =>
        {
            if (ModContent.GetInstance<ExxoWorldGen>().WorldEvil == ExxoWorldGen.EvilBiome.Contagion &&
                !WorldGen.IsTileNearby(x, y, ModContent.TileType<IckyAltar>(), 3))
            {
                WorldGen.Place3x2(x, y, (ushort)ModContent.TileType<IckyAltar>());
            }
        });
        c.Emit(OpCodes.Br, endNormalAltar);
        c.MarkLabel(startNormalAltar);

        c.GotoNext(i => i.MatchLdloc(5))
            .GotoNext(i => i.MatchLdsflda(out _));

        c.MarkLabel(endNormalAltar);
    }
}
