using System;
using System.Collections.Generic;
using Avalon.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.World;

class Utils
{
    public static void TileRunnerCrystalMines(int i, int j, double strength, int steps, int type, bool addTile = false, float speedX = 0f, float speedY = 0f, bool noYChange = false, bool overRide = true, int ignoreTileType = -1)
    {
        if (WorldGen.drunkWorldGen)
        {
            strength *= (double)(1f + WorldGen.genRand.Next(-80, 81) * 0.01f);
            steps = (int)(steps * (1f + WorldGen.genRand.Next(-80, 81) * 0.01f));
        }
        if (WorldGen.getGoodWorldGen && type != 57)
        {
            strength *= (double)(1f + WorldGen.genRand.Next(-80, 81) * 0.015f);
            steps += WorldGen.genRand.Next(3);
        }
        double num = strength;
        float num2 = steps;
        Vector2 vector = default;
        vector.X = i;
        vector.Y = j;
        Vector2 vector2 = default;
        vector2.X = WorldGen.genRand.Next(-10, 11) * 0.1f;
        vector2.Y = WorldGen.genRand.Next(-10, 11) * 0.1f;
        if (speedX != 0f || speedY != 0f)
        {
            vector2.X = speedX;
            vector2.Y = speedY;
        }
        bool flag = type == 368;
        bool flag2 = type == 367;
        bool lava = false;
        if (WorldGen.getGoodWorldGen && WorldGen.genRand.NextBool(4))
        {
            lava = true;
        }
        while (num > 0.0 && num2 > 0f)
        {
            if (WorldGen.drunkWorldGen && WorldGen.genRand.NextBool(30))
            {
                vector.X += WorldGen.genRand.Next(-100, 101) * 0.05f;
                vector.Y += WorldGen.genRand.Next(-100, 101) * 0.05f;
            }
            if (vector.Y < 0f && num2 > 0f && type == 59)
            {
                num2 = 0f;
            }
            num = strength * (double)(num2 / steps);
            num2--;
            int num3 = (int)(vector.X - num * 0.5);
            int num4 = (int)(vector.X + num * 0.5);
            int num5 = (int)(vector.Y - num * 0.5);
            int num6 = (int)(vector.Y + num * 0.5);
            if (num3 < 1)
            {
                num3 = 1;
            }
            if (num4 > Main.maxTilesX - 1)
            {
                num4 = Main.maxTilesX - 1;
            }
            if (num5 < 1)
            {
                num5 = 1;
            }
            if (num6 > Main.maxTilesY - 1)
            {
                num6 = Main.maxTilesY - 1;
            }
            for (int k = num3; k < num4; k++)
            {
                if (k < WorldGen.beachDistance + 50 || k >= Main.maxTilesX - WorldGen.beachDistance - 50)
                {
                    lava = false;
                }
                for (int l = num5; l < num6; l++)
                {
                    if ((WorldGen.drunkWorldGen && l < Main.maxTilesY - 300 && type == 57) || (ignoreTileType >= 0 && Main.tile[k, l].HasTile && Main.tile[k, l].TileType == ignoreTileType) || !((double)(Math.Abs(k - vector.X) + Math.Abs(l - vector.Y)) < strength * 0.5 * (1.0 + WorldGen.genRand.Next(-10, 11) * 0.015)))
                    {
                        continue;
                    }
                    //if (Main.tileFrameImportant[Main.tile[k, l - 1].TileType] && Main.tile[k, l].HasTile ||
                    //    Main.tileFrameImportant[Main.tile[k, l].TileType] && Main.tile[k, l + 1].HasTile)
                    //{
                    //    continue;
                    //}
                    if (type < 0)
                    {
                        if (Main.tile[k, l].TileType == 53)
                        {
                            continue;
                        }
                        if (type == -2 && Main.tile[k, l].HasTile && (l < WorldGen.waterLine || l > WorldGen.lavaLine))
                        {
                            Main.tile[k, l].LiquidAmount = byte.MaxValue;
                            Tile t = Main.tile[k, l];
                            if (lava)
                            {
                                t.LiquidType = LiquidID.Lava;
                            }
                            if (l > WorldGen.lavaLine)
                            {
                                t.LiquidType = LiquidID.Lava;
                            }
                        }
                        Tile t2 = Main.tile[k, l];
                        t2.HasTile = false;
                        continue;
                    }
                    if (flag && (double)(Math.Abs(k - vector.X) + Math.Abs(l - vector.Y)) < strength * 0.3 * (1.0 + WorldGen.genRand.Next(-10, 11) * 0.01))
                    {
                        WorldGen.PlaceWall(k, l, 180, mute: true);
                    }
                    if (flag2 && (double)(Math.Abs(k - vector.X) + Math.Abs(l - vector.Y)) < strength * 0.3 * (1.0 + WorldGen.genRand.Next(-10, 11) * 0.01))
                    {
                        WorldGen.PlaceWall(k, l, 178, mute: true);
                    }

                    if (overRide || !Main.tile[k, l].HasTile)
                    {
                        Tile tile = Main.tile[k, l];
                        bool flag3 = Main.tileStone[type] && tile.TileType != 1;
                        bool flag4 = Main.tileStone[type] && tile.TileType != 1;
                        if (!TileID.Sets.CanBeClearedDuringGeneration[tile.TileType])
                        {
                            flag3 = true;
                        }
                        if (tile.TileType is TileID.Granite or TileID.Marble)
                            flag3 = false;
                        switch (tile.TileType)
                        {
                            case TileID.Sand:
                                if (type == 59 && WorldGen.UndergroundDesertLocation.Contains(k, l))
                                {
                                    flag3 = true;
                                }
                                if (type == 40)
                                {
                                    flag3 = true;
                                }
                                if (l < Main.worldSurface && type != 59)
                                {
                                    flag3 = true;
                                }
                                break;
                            case TileID.GoldBrick:
                            case TileID.Cloud:
                            case TileID.MushroomBlock:
                            case TileID.RainCloud:
                            case TileID.SnowCloud:
                            case TileID.Containers:
                            case TileID.Containers2:
                            case TileID.Cobweb:
                                flag3 = true;
                                break;
                            case 1:
                                if (type == 59 && l < Main.worldSurface + WorldGen.genRand.Next(-50, 50))
                                {
                                    flag3 = true;
                                }
                                break;
                        }
                        if (Main.tileDungeon[tile.TileType] || Main.wallDungeon[tile.WallType] || TileID.Sets.IsVine[tile.TileType] ||
                            TileID.Sets.IsATreeTrunk[tile.TileType] || TileID.Sets.CountsAsGemTree[tile.TileType] ||
                            tile.TileType == TileID.LihzahrdBrick || Main.tileFrameImportant[tile.TileType])
                        {
                            flag3 = true;
                        }
                        if (!flag3)
                        {
                            tile.TileType = (ushort)type;
                            tile.WallType = (ushort)ModContent.WallType<Walls.CrystalStoneWall>();
                            WorldGen.SquareTileFrame(k, l);
                            WorldGen.SquareWallFrame(k, l);
                        }
                        if (tile.TileType == TileID.Cobweb || Main.tileFrameImportant[tile.TileType])
                        {
                            tile.WallType = (ushort)ModContent.WallType<Walls.CrystalStoneWall>();
                            WorldGen.SquareWallFrame(k, l);
                        }
                    }
                    if (addTile)
                    {
                        Tile t = Main.tile[k, l];
                        t.HasTile = true;
                        t.LiquidAmount = 0;
                        t.LiquidType = LiquidID.Water;
                    }
                    if (noYChange && l < Main.worldSurface && type != 59)
                    {
                        Main.tile[k, l].WallType = 2;
                    }
                    if (type == 59 && l > WorldGen.waterLine && Main.tile[k, l].LiquidAmount > 0)
                    {
                        Tile t = Main.tile[k, l];
                        t.LiquidType = LiquidID.Water;
                        t.LiquidAmount = 0;
                    }
                }
            }
            vector += vector2;
            if ((!WorldGen.drunkWorldGen || WorldGen.genRand.Next(3) != 0) && num > 50.0)
            {
                vector += vector2;
                num2 -= 1f;
                vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                if (num > 100.0)
                {
                    vector += vector2;
                    num2 -= 1f;
                    vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                    vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                    if (num > 150.0)
                    {
                        vector += vector2;
                        num2 -= 1f;
                        vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                        vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                        if (num > 200.0)
                        {
                            vector += vector2;
                            num2 -= 1f;
                            vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                            vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                            if (num > 250.0)
                            {
                                vector += vector2;
                                num2 -= 1f;
                                vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                if (num > 300.0)
                                {
                                    vector += vector2;
                                    num2 -= 1f;
                                    vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                    vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                    if (num > 400.0)
                                    {
                                        vector += vector2;
                                        num2 -= 1f;
                                        vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                        vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                        if (num > 500.0)
                                        {
                                            vector += vector2;
                                            num2 -= 1f;
                                            vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                            vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                            if (num > 600.0)
                                            {
                                                vector += vector2;
                                                num2 -= 1f;
                                                vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                if (num > 700.0)
                                                {
                                                    vector += vector2;
                                                    num2 -= 1f;
                                                    vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                    vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                    if (num > 800.0)
                                                    {
                                                        vector += vector2;
                                                        num2 -= 1f;
                                                        vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                        vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                        if (num > 900.0)
                                                        {
                                                            vector += vector2;
                                                            num2 -= 1f;
                                                            vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                            vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
            if (WorldGen.drunkWorldGen)
            {
                vector2.X += WorldGen.genRand.Next(-10, 11) * 0.25f;
            }
            if (vector2.X > 1f)
            {
                vector2.X = 1f;
            }
            if (vector2.X < -1f)
            {
                vector2.X = -1f;
            }
            if (!noYChange)
            {
                vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                if (vector2.Y > 1f)
                {
                    vector2.Y = 1f;
                }
                if (vector2.Y < -1f)
                {
                    vector2.Y = -1f;
                }
            }
            else if (type != 59 && num < 3.0)
            {
                if (vector2.Y > 1f)
                {
                    vector2.Y = 1f;
                }
                if (vector2.Y < -1f)
                {
                    vector2.Y = -1f;
                }
            }
            if (type == 59 && !noYChange)
            {
                if (vector2.Y > 0.5)
                {
                    vector2.Y = 0.5f;
                }
                if (vector2.Y < -0.5)
                {
                    vector2.Y = -0.5f;
                }
                if (vector.Y < Main.rockLayer + 100.0)
                {
                    vector2.Y = 1f;
                }
                if (vector.Y > Main.maxTilesY - 300)
                {
                    vector2.Y = -1f;
                }
            }
        }
    }

    /// <summary>
    /// Helper method to shift the Sky Fortress to the left/right if there are tiles/liquid/walls in the way.
    /// </summary>
    /// <param name="x">The X coordinate of the Sky Fortress origin point.</param>
    /// <param name="y">The Y coordinate of the Sky Fortress origin point.</param>
    /// <param name="xLength">The width of the Sky Fortress.</param>
    /// <param name="ylength">The height of the Sky Fortress.</param>
    /// <param name="xCoord">The X coordinate of the Sky Fortress origin point, passed in again to be modified (the original X coordinate needs to remain the same I think).</param>
    public static void GetSkyFortressXCoord(int x, int y, int xLength, int ylength, ref int xCoord)
    {
        bool leftSideActive = false;
        bool rightSideActive = false;
        for (int i = y; i < y + ylength; i++)
        {
            if (Main.tile[x, i].HasTile || Main.tile[x, i].LiquidAmount > 0 || Main.tile[x, i].WallType > 0)
            {
                leftSideActive = true;
                break;
            }
        }
        for (int i = y; i < y + ylength; i++)
        {
            if (Main.tile[x + xLength, i].HasTile || Main.tile[x + xLength, i].LiquidAmount > 0 || Main.tile[x + xLength, i].WallType > 0)
            {
                rightSideActive = true;
                break;
            }
        }
        if (leftSideActive || rightSideActive)
        {
            if (xCoord > Main.maxTilesX / 2)
                xCoord--;
            else xCoord++;
            if (xCoord < 100)
            {
                xCoord = 100;
                return;
            }
            if (xCoord > Main.maxTilesX - 100)
            {
                xCoord = Main.maxTilesX - 100;
                return;
            }
            GetSkyFortressXCoord(xCoord, y, xLength, ylength, ref xCoord);
        }
    }

    /// <summary>
    /// Generic version of the Sky Fortress shift method. Does not currently work - will crash the game when used.
    /// </summary>
    /// <param name="x">The X coordinate of the structure's origin point.</param>
    /// <param name="y">The Y coordinate of the structure's origin point.</param>
    /// <param name="xLength">The width of the structure.</param>
    /// <param name="ylength">The height of the structure.</param>
    /// <param name="xCoord">The X coordinate of the structure's origin point, passed in again as ref to be modified.</param>
    /// <param name="typesToCheck">A List of the tile types to shift the structure if found.</param>
    /// <param name="liquid">Whether or not to check for liquids.</param>
    /// <param name="walls">Whether or not to check for walls.</param>
    public static void GetXCoordGeneric(int x, int y, int xLength, int ylength, ref int xCoord, List<int> typesToCheck, bool liquid = true, bool walls = true)
    {
        bool leftSideActive = false;
        bool rightSideActive = false;

        for (int i = y; i < y + ylength; i++)
        {
            if ((Main.tile[x, i].HasTile && typesToCheck.Contains(Main.tile[x, i].TileType)) || (Main.tile[x, i].LiquidAmount > 0 && liquid) || (Main.tile[x, i].WallType > 0 && walls))
            {
                leftSideActive = true;
                break;
            }
        }
        for (int i = y; i < y + ylength; i++)
        {
            if ((Main.tile[x + xLength, i].HasTile && typesToCheck.Contains(Main.tile[x + xLength, i].TileType)) || (Main.tile[x + xLength, i].LiquidAmount > 0 && liquid) || (Main.tile[x + xLength, i].WallType > 0 && walls))
            {
                rightSideActive = true;
                break;
            }
        }
        if (leftSideActive || rightSideActive)
        {
            if (xCoord > Main.maxTilesX / 2)
                xCoord--;
            else
                xCoord++;
            if (xCoord < 100)
            {
                xCoord = 100;
                return;
            }
            if (xCoord > Main.maxTilesX - 100)
            {
                xCoord = Main.maxTilesX - 100;
                return;
            }
            GetXCoordGeneric(xCoord, y, xLength, ylength, ref xCoord, typesToCheck, liquid, walls);
        }
    }

    public static void PlaceHallowedAltar(int x, int y, int style = 0)
    {
        if (x < 5 || x > Main.maxTilesX - 5 || y < 5 || y > Main.maxTilesY - 5)
        {
            return;
        }
        bool placeOrNot = true;
        int num = y - 1;
        for (int i = x - 1; i < x + 2; i++)
        {
            for (int j = num; j < y + 1; j++)
            {
                if (Main.tileCut[Main.tile[i, j].TileType] || Main.tile[i, j].TileType is TileID.SmallPiles or TileID.LargePiles or TileID.LargePiles2 or TileID.Stalactite)
                {
                    WorldGen.KillTile(i, j, noItem: true);
                }
            }
            for (int j2 = num; j2 < y + 1; j2++)
            {
                if (Main.tile[i, j2].HasTile) placeOrNot = false;
            }
        }
        if (placeOrNot)
        {
            short num2 = (short)(54 * style);
            Tile t0 = Main.tile[x - 1, y - 1];
            t0.HasTile = true;
            Main.tile[x - 1, y - 1].TileFrameY = 0;
            Main.tile[x - 1, y - 1].TileFrameX = num2;
            Main.tile[x - 1, y - 1].TileType = (ushort)ModContent.TileType<HallowedAltar>();
            Tile t = Main.tile[x, y - 1];
            t.HasTile = true;
            Main.tile[x, y - 1].TileFrameY = 0;
            Main.tile[x, y - 1].TileFrameX = (short)(num2 + 18);
            Main.tile[x, y - 1].TileType = (ushort)ModContent.TileType<HallowedAltar>();
            Tile t2 = Main.tile[x + 1, y - 1];
            t2.HasTile = true;
            Main.tile[x + 1, y - 1].TileFrameY = 0;
            Main.tile[x + 1, y - 1].TileFrameX = (short)(num2 + 36);
            Main.tile[x + 1, y - 1].TileType = (ushort)ModContent.TileType<HallowedAltar>();
            Tile t3 = Main.tile[x - 1, y];
            t3.HasTile = true;
            Main.tile[x - 1, y].TileFrameY = 18;
            Main.tile[x - 1, y].TileFrameX = num2;
            Main.tile[x - 1, y].TileType = (ushort)ModContent.TileType<HallowedAltar>();
            Tile t4 = Main.tile[x, y];
            t4.HasTile = true;
            Main.tile[x, y].TileFrameY = 18;
            Main.tile[x, y].TileFrameX = (short)(num2 + 18);
            Main.tile[x, y].TileType = (ushort)ModContent.TileType<HallowedAltar>();
            Tile t5 = Main.tile[x + 1, y];
            t5.HasTile = true;
            Main.tile[x + 1, y].TileFrameY = 18;
            Main.tile[x + 1, y].TileFrameX = (short)(num2 + 36);
            Main.tile[x + 1, y].TileType = (ushort)ModContent.TileType<HallowedAltar>();
        }
    }
    public static void ResetSlope(int i, int j)
    {
        Tile t = Main.tile[i, j];
        t.Slope = SlopeType.Solid;
        t.IsHalfBlock = false;
    }
    public static void SquareTileFrame(int i, int j, bool resetFrame = true, bool resetSlope = false, bool largeHerb = false)
    {
        if (resetSlope)
        {
            Tile t = Main.tile[i, j];
            t.Slope = SlopeType.Solid;
            t.IsHalfBlock = false;
        }
        WorldGen.TileFrame(i - 1, j - 1, false, largeHerb);
        WorldGen.TileFrame(i - 1, j, false, largeHerb);
        WorldGen.TileFrame(i - 1, j + 1, false, largeHerb);
        WorldGen.TileFrame(i, j - 1, false, largeHerb);
        WorldGen.TileFrame(i, j, resetFrame, largeHerb);
        WorldGen.TileFrame(i, j + 1, false, largeHerb);
        WorldGen.TileFrame(i + 1, j - 1, false, largeHerb);
        WorldGen.TileFrame(i + 1, j, false, largeHerb);
        WorldGen.TileFrame(i + 1, j + 1, false, largeHerb);
    }
    /// <summary>
    /// A helper method to run WorldGen.SquareTileFrame() over an area.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="r">The radius.</param>
    /// <param name="lh">Whether or not to use Large Herb logic.</param>
    public static void SquareTileFrameArea(int x, int y, int r, bool lh = false)
    {
        for (int i = x - r; i < x + r; i++)
        {
            for (int j = y - r; j < y + r; j++)
            {
                SquareTileFrame(i, j, true, lh);
            }
        }
    }
    /// <summary>
    /// A helper method to run WorldGen.SquareTileFrame() over an area.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="xr">The number of blocks in the X direction.</param>
    /// <param name="yr">The number of blocks in the Y direction.</param>
    /// <param name="lh">Whether or not to use Large Herb logic.</param>
    public static void SquareTileFrameArea(int x, int y, int xr, int yr, bool lh = false)
    {
        for (int i = x; i < x + xr; i++)
        {
            for (int j = y; j < y + yr; j++)
            {
                SquareTileFrame(i, j, true, lh);
            }
        }
    }
    /// <summary>
    /// Swaps two values.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="lhs">Left hand side.</param>
    /// <param name="rhs">Right hand side.</param>
    public static void Swap<T>(ref T lhs, ref T rhs)
    {
        T t = lhs;
        lhs = rhs;
        rhs = t;
    }
    /// <summary>
    /// A helper method to find the actual surface of the world.
    /// </summary>
    /// <param name="positionX">The x position.</param>
    /// <returns></returns>
    public static int TileCheck(int positionX)
    {
        for (int i = (int)(WorldGen.worldSurfaceLow - 30); i < Main.maxTilesY; i++)
        {
            Tile tile = Framing.GetTileSafely(positionX, i);
            if ((tile.TileType == TileID.Dirt || tile.TileType == TileID.ClayBlock || tile.TileType == TileID.Stone || tile.TileType == TileID.Sand || tile.TileType == ModContent.TileType<Snotsand>() || tile.TileType == ModContent.TileType<Loam>() || tile.TileType == TileID.Mud || tile.TileType == TileID.SnowBlock || tile.TileType == TileID.IceBlock) && tile.HasTile)
            {
                return i;
            }
        }
        return 0;
    }
    public static void MakeSquareTemp(int x, int y)
    {
        for (int i = x; i < x + 5; i++)
        {
            for (int j = y; j < y + 5; j++)
            {
                
                WorldGen.KillTile(i, j, noItem: true);
                Main.tile[i, j].TileType = TileID.Stone;
                Tile t = Main.tile[i, j];
                t.HasTile = true;
                WorldGen.SquareTileFrame(i, j);
            }
        }
    }
    public static void MakeCircle(int x, int y, int radius, int tileType, bool walls = false, int wallType = WallID.Dirt)
    {
        for (int k = x - radius; k <= x + radius; k++)
        {
            for (int l = y - radius; l <= y + radius; l++)
            {
                float dist = Vector2.Distance(new Vector2(k, l), new Vector2(x, y));
                if (dist <= radius && dist >= (radius - 29))
                {
                    Tile t = Main.tile[k, l];
                    t.HasTile = false;
                }
                if ((dist <= radius && dist >= radius - 7) || (dist <= radius - 22 && dist >= radius - 29))
                {
                    Tile t = Main.tile[k, l];
                    t.HasTile = false;
                    t.IsHalfBlock = false;
                    t.Slope = SlopeType.Solid;
                    Main.tile[k, l].TileType = (ushort)tileType;
                    WorldGen.SquareTileFrame(k, l);
                }
                if (walls)
                {
                    if (dist <= radius - 6 && dist >= radius - 23)
                    {
                        Main.tile[k, l].WallType = (ushort)wallType;
                    }
                }
            }
        }
    }
}
