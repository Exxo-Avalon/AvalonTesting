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
    public static void Method(GenerationProgress progress, GameConfiguration configuration)
    {
        Main.rand ??= new UnifiedRandom((int)DateTime.Now.Ticks);

        for (int a = 0; a < (int)(Main.maxTilesX * Main.maxTilesY * 0.00012); a++)
        {
            int k = Main.rand.Next(100, Main.maxTilesX - 100);
            int l = Main.rand.Next((int)Main.rockLayer, Main.maxTilesY - 150);
            if (Main.tile[k, l].HasTile && !Main.tile[k, l - 1].HasTile && Main.tile[k, l - 1].LiquidType != LiquidID.Lava &&
                Main.tile[k, l - 1].TileType != TileID.Containers && Main.tile[k, l - 1].TileType != TileID.Containers2 &&
                l < Main.maxTilesY - 200 && Main.rand.NextBool(150) && Main.tile[k, l + 1].TileType == TileID.Pearlstone) // hallowed altar gen
            {
                WorldGen.Place3x2(k, l - 1, (ushort)ModContent.TileType<HallowedAltar>());
            }
        }

        //WorldGen.IsGeneratingHardMode = true;
        //if (Main.rand == null)
        //    Main.rand = new UnifiedRandom((int)DateTime.Now.Ticks);
        //float num1 = Main.rand.Next(300, 400) * (1f / 1000f);
        //float num2 = Main.rand.Next(200, 300) * (1f / 1000f);
        //int i1 = (int)(Main.maxTilesX * (double)num1);
        //int i2 = (int)(Main.maxTilesX * (1.0 - (double)num1));
        //int num3 = 1;
        //if (Main.rand.NextBool(2))
        //{
        //    i2 = (int)(Main.maxTilesX * (double)num1);
        //    i1 = (int)(Main.maxTilesX * (1.0 - (double)num1));
        //    num3 = -1;
        //}
        //int num4 = 1;
        //if (WorldGen.dungeonX < Main.maxTilesX / 2)
        //    num4 = -1;
        //if (num4 < 0)
        //{
        //    if (i2 < i1)
        //        i2 = (int)((double)Main.maxTilesX * num2);
        //    else
        //        i1 = (int)((double)Main.maxTilesX * num2);
        //}
        //else if (i2 > i1)
        //    i2 = (int)(Main.maxTilesX * (1.0 - num2));
        //else
        //    i1 = (int)(Main.maxTilesX * (1.0 - num2));
        //GERunner(i1, 0, (3 * -num3), 5f, true);
        //GERunner(i2, 0, (3 * -num3), 5f, false);
    }
    public static void GERunner(int i, int j, float speedX = 0f, float speedY = 0f, bool good = true)
    {
        Main.rand ??= new UnifiedRandom((int)DateTime.Now.Ticks);

        int num = Main.rand.Next(200, 250);
        float num2 = Main.maxTilesX / 4200;
        num = (int)(num * num2);
        double num3 = num;
        Vector2 vector = new Vector2(i, j);
        Vector2 vector2 = new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11)) * 0.1f;
        if (speedX != 0f || speedY != 0f)
        {
            vector2.X = speedX;
            vector2.Y = speedY;
        }
        bool flag = true;
        while (flag)
        {
            int num4 = (int)(vector.X - num3 * 0.5);
            int num5 = (int)(vector.X + num3 * 0.5);
            int num6 = (int)(vector.Y - num3 * 0.5);
            int num7 = (int)(vector.Y + num3 * 0.5);
            if (num4 < 0)
            {
                num4 = 0;
            }
            if (num5 > Main.maxTilesX)
            {
                num5 = Main.maxTilesX;
            }
            if (num6 < 0)
            {
                num6 = 0;
            }
            if (num7 > Main.maxTilesY)
            {
                num7 = Main.maxTilesY;
            }
            for (int k = num4; k < num5; k++)
            {
                for (int l = num6; l < num7; l++)
                {
                    if (!((double)(Math.Abs(k - vector.X) + Math.Abs(l - vector.Y)) < num * 0.5 * (1.0 + Main.rand.Next(-10, 11) * 0.015)))
                    {
                        continue;
                    }
                    if (good)
                    {
                        if (Main.tile[k, l].WallType == (ushort)ModContent.WallType<Walls.ChunkstoneWall>() || Main.tile[k, l].WallType == WallID.EbonstoneUnsafe || Main.tile[k, l].WallType == WallID.CrimstoneUnsafe)
                        {
                            Main.tile[k, l].WallType = WallID.PearlstoneBrickUnsafe;
                        }
                        if (Main.tile[k, l].WallType is WallID.GrassUnsafe or WallID.FlowerUnsafe or WallID.Grass or WallID.Flower or WallID.CorruptGrassUnsafe or WallID.CrimsonGrassUnsafe)
                        {
                            Main.tile[k, l].WallType = WallID.HallowedGrassUnsafe;
                        }
                        else if (Main.tile[k, l].WallType == WallID.HardenedSand || Main.tile[k, l].WallType == (ushort)ModContent.WallType<Walls.ContagionNaturalWall1>())
                        {
                            Main.tile[k, l].WallType = WallID.HallowHardenedSand;
                        }
                        else if (Main.tile[k, l].WallType == WallID.Sandstone || Main.tile[k, l].WallType == (ushort)ModContent.WallType<Walls.ContagionNaturalWall2>())
                        {
                            Main.tile[k, l].WallType = WallID.HallowSandstone;
                        }
                        if (Main.tile[k, l].HasTile && !Main.tile[k, l - 1].HasTile && Main.tile[k, l - 1].LiquidType != LiquidID.Lava && Main.tile[k, l - 1].TileType != TileID.Containers && Main.tile[k, l - 1].TileType != TileID.Containers2 && l < Main.maxTilesY - 200 && Main.rand.NextBool(150)) // hallowed altar gen
                        {
                            WorldGen.Place3x2(k, l - 1, (ushort)ModContent.TileType<HallowedAltar>());
                        }
                        if (Main.tile[k, l].WallType is WallID.EbonstoneUnsafe or WallID.CrimstoneUnsafe)
                        {
                            Main.tile[k, l].WallType = WallID.PearlstoneBrickUnsafe;
                        }
                        if (Main.tile[k, l].TileType == TileID.Grass || Main.tile[k, l].TileType == TileID.CorruptGrass || Main.tile[k, l].TileType == TileID.CrimsonGrass || Main.tile[k, l].TileType == (ushort)ModContent.TileType<Ickgrass>())
                        {
                            Main.tile[k, l].TileType = TileID.HallowedGrass;
                            Utils.SquareTileFrame(k, l);
                        }
                        else if (Main.tile[k, l].TileType == TileID.Stone || Main.tile[k, l].TileType == TileID.Ebonstone || Main.tile[k, l].TileType == TileID.Crimstone || Main.tile[k, l].TileType == (ushort)ModContent.TileType<Chunkstone>())
                        {
                            Main.tile[k, l].TileType = TileID.Pearlstone;
                            Utils.SquareTileFrame(k, l);
                        }
                        else if (Main.tile[k, l].TileType == TileID.Sand || Main.tile[k, l].TileType == TileID.Ebonsand || Main.tile[k, l].TileType == TileID.Crimsand || Main.tile[k, l].TileType == TileID.Silt || Main.tile[k, l].TileType == (ushort)ModContent.TileType<Snotsand>())
                        {
                            Main.tile[k, l].TileType = TileID.Pearlsand;
                            Utils.SquareTileFrame(k, l);
                        }
                        else if (Main.tile[k, l].TileType == TileID.IceBlock || Main.tile[k, l].TileType == TileID.CorruptIce || Main.tile[k, l].TileType == TileID.FleshIce || Main.tile[k, l].TileType == (ushort)ModContent.TileType<YellowIce>())
                        {
                            Main.tile[k, l].TileType = TileID.HallowedIce;
                            Utils.SquareTileFrame(k, l);
                        }
                        else if (Main.tile[k, l].TileType == TileID.Sandstone || Main.tile[k, l].TileType == (ushort)ModContent.TileType<Snotsandstone>())
                        {
                            Main.tile[k, l].TileType = TileID.HallowSandstone;
                            Utils.SquareTileFrame(k, l);
                        }
                        else if (Main.tile[k, l].TileType == TileID.HardenedSand || Main.tile[k, l].TileType == (ushort)ModContent.TileType<HardenedSnotsand>())
                        {
                            Main.tile[k, l].TileType = TileID.HallowHardenedSand;
                            Utils.SquareTileFrame(k, l);
                        }
                    }
                }
            }
            vector += vector2;
            vector2.X += Main.rand.Next(-10, 11) * 0.05f;
            if (vector2.X > speedX + 1f)
            {
                vector2.X = speedX + 1f;
            }
            if (vector2.X < speedX - 1f)
            {
                vector2.X = speedX - 1f;
            }
            if (vector.X < -num || vector.Y < -num || vector.X > Main.maxTilesX + num || vector.Y > Main.maxTilesX + num)
            {
                flag = false;
            }
        }
    }
}
