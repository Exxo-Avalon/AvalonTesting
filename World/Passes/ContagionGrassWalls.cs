using System;
using AltLibrary.Common.Systems;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Avalon.World.Passes;
internal class ContagionGrassWalls
{
    public static void Method(GenerationProgress progress, GameConfiguration configuration)
    {
        Main.rand ??= new UnifiedRandom((int)DateTime.Now.Ticks);
        if (WorldBiomeManager.WorldEvil == "Avalon/ContagionAlternateBiome")
        {
            for (int i = 300; i < Main.maxTilesX - 300; i++)
            {
                for (int j = 50; j < Main.maxTilesY - 180; j++)
                {
                    if (Main.tile[i, j].WallType == WallID.CorruptGrassUnsafe)
                    {
                        Main.tile[i, j].WallType = (ushort)ModContent.WallType<Walls.ContagionGrassWall>();
                        WorldGen.SquareWallFrame(i, j);
                    }
                    if (Main.tile[i, j].WallType == WallID.CorruptSandstone)
                    {
                        Main.tile[i, j].WallType = (ushort)ModContent.WallType<Walls.SnotsandstoneWall>();
                        WorldGen.SquareWallFrame(i, j);
                    }
                    if (Main.tile[i, j].WallType == WallID.CorruptHardenedSand)
                    {
                        Main.tile[i, j].WallType = (ushort)ModContent.WallType<Walls.HardenedSnotsandWall>();
                        WorldGen.SquareWallFrame(i, j);
                    }
                }
            }
        }
    }
}
