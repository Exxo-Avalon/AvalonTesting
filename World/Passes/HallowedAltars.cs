using System;
using AvalonTesting.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.WorldBuilding;
using Terraria.IO;

namespace AvalonTesting.World.Passes;

class HallowedAltars
{
    public static void Generate()
    {
        Main.rand ??= new UnifiedRandom((int)DateTime.Now.Ticks);

        for (int a = 0; a < (int)(Main.maxTilesX * Main.maxTilesY * 0.3); a++)
        {
            int k = Main.rand.Next(100, Main.maxTilesX - 100);
            int l = Main.rand.Next((int)Main.rockLayer, Main.maxTilesY - 150);
            if (Main.tile[k, l].HasTile && (Main.tile[k - 1, l].HasTile && !Main.tile[k - 1, l].IsHalfBlock && Main.tile[k - 1, l].Slope == SlopeType.Solid) &&
                (Main.tile[k + 1, l].HasTile && !Main.tile[k + 1, l].IsHalfBlock && Main.tile[k + 1, l].Slope == SlopeType.Solid) && 
                Main.tile[k, l - 1].TileType != TileID.Containers && Main.tile[k, l - 1].TileType != TileID.Containers2 &&
                (Main.tile[k, l].TileType == TileID.Pearlstone || Main.tile[k, l].TileType == TileID.Pearlsand ||
                Main.tile[k, l].TileType == TileID.HallowHardenedSand || Main.tile[k, l].TileType == TileID.HallowSandstone ||
                Main.tile[k, l].TileType == TileID.HallowedGrass || Main.tile[k, l].TileType == TileID.HallowedIce) &&
                l < Main.maxTilesY - 200 && Main.rand.NextBool(2))
            {
                Utils.PlaceHallowedAltar(k, l - 1);
            }
        }
    }
    public static void Method(GenerationProgress progress, GameConfiguration configuration)
    {
        Main.rand ??= new UnifiedRandom((int)DateTime.Now.Ticks);

        for (int a = 0; a < (int)(Main.maxTilesX * Main.maxTilesY * 0.3); a++)
        {
            int k = Main.rand.Next(100, Main.maxTilesX - 100);
            int l = Main.rand.Next((int)Main.worldSurface, Main.maxTilesY - 150);
            if ((Main.tile[k, l].HasTile && Main.tileSolid[Main.tile[k, l].TileType] && !Main.tile[k, l].IsHalfBlock && Main.tile[k, l].Slope == SlopeType.Solid) &&
                (Main.tile[k - 1, l].HasTile && Main.tileSolid[Main.tile[k - 1, l].TileType] && !Main.tile[k - 1, l].IsHalfBlock && Main.tile[k - 1, l].Slope == SlopeType.Solid) &&
                (Main.tile[k + 1, l].HasTile && Main.tileSolid[Main.tile[k + 1, l].TileType] && !Main.tile[k + 1, l].IsHalfBlock && Main.tile[k + 1, l].Slope == SlopeType.Solid) &&
                Main.tile[k, l - 1].TileType != TileID.Containers && Main.tile[k, l - 1].TileType != TileID.Containers2 &&
                (Main.tile[k, l].TileType == TileID.Pearlstone || Main.tile[k, l].TileType == TileID.Pearlsand ||
                Main.tile[k, l].TileType == TileID.HallowHardenedSand || Main.tile[k, l].TileType == TileID.HallowSandstone ||
                Main.tile[k, l].TileType == TileID.HallowedGrass || Main.tile[k, l].TileType == TileID.HallowedIce) &&
                l < Main.maxTilesY - 200 && Main.rand.NextBool(2))
            {
                Utils.PlaceHallowedAltar(k, l - 1);
            }
        }
    }
}
