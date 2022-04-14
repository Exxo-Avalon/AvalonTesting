using System;
using AvalonTesting.Systems;
using AvalonTesting.Tiles;
using AvalonTesting.Tiles.Ores;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Hooks;

// TODO:
// "Tile Cleanup" has two references for placing orbs
// FinishGetGoodWorld() world has a reference
// MakeDungeon() for the chest
// SmashAltar for tiles that are added on smash altar
// GERunner for tiles on hardmode evil enter

public static class ContagionWorldGen
{
    public static void ILGenPassReset(ILContext il)
    {
        var c = new ILCursor(il);

        if (!c.TryGotoNext(i => i.MatchStsfld<WorldGen>(nameof(WorldGen.crimson))))
        {
            return;
        }

        if (!c.TryGotoNext(i => i.MatchLdfld(out _)))
        {
            return;
        }

        c.EmitDelegate(() =>
        {
            if (ModContent.GetInstance<ExxoWorldGen>().WorldEvil == ExxoWorldGen.EvilBiome.Crimson)
            {
                WorldGen.crimson = true;
            }
            else
            {
                WorldGen.crimson = false;
            }
        });

        if (!c.TryGotoNext(i => i.MatchRet()))
        {
            return;
        }

        if (!c.TryGotoPrev(i => i.MatchBneUn(out _)))
        {
            return;
        }

        if (!c.TryGotoPrev(i => i.MatchLdcI4(-1)))
        {
            return;
        }

        c.EmitDelegate<Func<int, int>>(dungeonSide =>
        {
            ModContent.GetInstance<ExxoWorldGen>().DungeonSide = dungeonSide;
            return dungeonSide;
        });

        for (int i = 0; i < 2; i++)
        {
            if (!c.TryGotoNext(i => i.MatchRet()))
            {
                return;
            }

            c.Index--;

            c.EmitDelegate<Func<int, int>>(dungeonLocation =>
            {
                ModContent.GetInstance<ExxoWorldGen>().DungeonLocation = dungeonLocation;
                return dungeonLocation;
            });
        }
    }

    public static void ILGenPassShinies(ILContext il)
    {
        var c = new ILCursor(il);

        if (!c.TryGotoNext(i => i.MatchRet()))
        {
            return;
        }


        if (!c.TryGotoPrev(i => i.MatchLdsfld<WorldGen>(nameof(WorldGen.drunkWorldGen))))
        {
            return;
        }

        if (!c.TryGotoNext(i => i.MatchBrfalse(out _)))
        {
            return;
        }

        c.Index++;

        // Drunk world seed baccilite generation
        c.EmitDelegate(() =>
        {
            for (int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 2.25E-05 / 2.0); i++)
            {
                WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX),
                    WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY), WorldGen.genRand.Next(3, 6),
                    WorldGen.genRand.Next(4, 8), ModContent.TileType<BacciliteOre>());
            }
        });

        if (!c.TryGotoNext(i => i.MatchRet()))
        {
            return;
        }

        if (!c.TryGotoNext(i => i.MatchBr(out _)))
        {
            return;
        }

        ILLabel startCorruptionGen = c.DefineLabel();

        c.EmitDelegate(() => ModContent.GetInstance<ExxoWorldGen>().WorldEvil != ExxoWorldGen.EvilBiome.Corruption);
        c.Emit(OpCodes.Brfalse, startCorruptionGen);

        c.EmitDelegate(() =>
        {
            if (ModContent.GetInstance<ExxoWorldGen>().WorldEvil == ExxoWorldGen.EvilBiome.Contagion)
            {
                for (int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 2.25E-05); i++)
                {
                    WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX),
                        WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY), WorldGen.genRand.Next(3, 6),
                        WorldGen.genRand.Next(4, 8), ModContent.TileType<BacciliteOre>());
                }
            }
        });

        c.Emit(OpCodes.Ret);
        c.MarkLabel(startCorruptionGen);
    }

    public static void ILGenPassAltars(ILContext il)
    {
        var c = new ILCursor(il);

        ILLabel endNormalAltar = c.DefineLabel();
        ILLabel startNormalAltar = c.DefineLabel();

        if (!c.TryGotoNext(i => i.MatchLdsfld<WorldGen>(nameof(WorldGen.crimson))))
        {
            return;
        }

        c.EmitDelegate(() => ModContent.GetInstance<ExxoWorldGen>().WorldEvil != ExxoWorldGen.EvilBiome.Corruption);
        c.Emit(OpCodes.Brfalse, startNormalAltar);
        c.Emit(OpCodes.Ldloc, 3);
        c.Emit(OpCodes.Ldloc, 4);
        c.EmitDelegate((int x, int y) =>
        {
            if (ModContent.GetInstance<ExxoWorldGen>().WorldEvil == ExxoWorldGen.EvilBiome.Contagion)
            {
                if (!WorldGen.IsTileNearby(x, y, ModContent.TileType<IckyAltar>(), 3))
                {
                    WorldGen.Place3x2(x, y, (ushort)ModContent.TileType<IckyAltar>());
                }
            }
        });
        c.Emit(OpCodes.Br, endNormalAltar);
        c.MarkLabel(startNormalAltar);

        if (!c.TryGotoNext(i => i.MatchLdloc(5)))
        {
            return;
        }

        if (!c.TryGotoNext(i => i.MatchLdsflda(out _)))
        {
            return;
        }

        c.MarkLabel(endNormalAltar);
    }
}
