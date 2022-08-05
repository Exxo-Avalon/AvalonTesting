using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.World.Structures;

public class CaesiumSpikeShape : GenShape
{
    private float _angle;

    private float _startingSize;

    private float _endingSize;

    private float _distance;
    public CaesiumSpikeShape(float angle, float distance = 10f, float startingSize = 4f, float endingSize = 1f)
    {
        _angle = angle;
        _distance = distance;
        _startingSize = startingSize;
        _endingSize = endingSize;
    }
    public override bool Perform(Point origin, GenAction action)
    {
        return DoSpike(origin, action, _angle, _distance, _startingSize);
    }

    private bool DoSpike(Point origin, GenAction action, float angle, float distance, float startingSize)
    {
        float num = origin.X;
        float num2 = origin.Y;
        for (float num3 = 0f; num3 < distance * 0.85f; num3++)
        {
            float num4 = num3 / distance;
            float num5 = MathHelper.Lerp(startingSize, _endingSize, num4);
            //Main.NewText(num5);
            num += (float)Math.Cos(angle);
            num2 += (float)Math.Sin(angle);
            angle += (_random.NextFloat() - _random.NextFloat(0.5f, 10) + _random.NextFloat(-10, 10) * (_angle - (float)Math.PI / 4f) * -0.1f * (1f - num4));
            angle = -angle * 0.2f + 0.85f * MathHelper.Clamp(angle, _angle - 0.5f * (1f - 0.5f * num4), _angle + 2f * (1f - 1f * num4));// + MathHelper.Lerp(_angle, (float)Math.PI / 2f, num4) * 0.15f;

            for (int i = 0; i < (int)num5; i++)
            {
                for (int j = 0; j < (int)num5; j++)
                {
                    if (!UnitApply(action, origin, (int)num + i, (int)num2 + j) && _quitOnFail)
                    {
                        return false;
                    }
                }
            }


            //int num6 = -(int)num5;
            //Main.NewText(num6);
            //if (num6 < 0)
            //{
            //    for (int i = num6; i < 0; i++)
            //    {
            //        for (int j = num6; j < 0; j++)
            //        {
            //            if (!UnitApply(action, origin, (int)num + i, (int)num2 + j) && _quitOnFail)
            //            {
            //                return false;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < (int)num5; i++)
            //    {
            //        for (int j = 0; j < (int)num5; j++)
            //        {
            //            if (!UnitApply(action, origin, (int)num + i, (int)num2 + j) && _quitOnFail)
            //            {
            //                return false;
            //            }
            //        }
            //    }
            //}

        }
        return true;
    }
}

class CaesiumSpike
{
    public static Vector2 CreateSpike2(float X, float Y, float xDir, float yDir, int Steps, int Size)
    {
        float num = X;
        float num2 = Y;
        try
        {
            float num3 = 0f;
            float num4 = 0f;
            float num5 = Size;
            num = MathHelper.Clamp(num, num5 + 1f, Main.maxTilesX - num5 - 1f);
            num2 = MathHelper.Clamp(num2, num5 + 1f, Main.maxTilesY - num5 - 1f);
            for (int i = 0; i < Steps; i++)
            {
                for (int j = (int)(num - num5); j <= num + num5; j++)
                {
                    for (int k = (int)(num2 - num5); k <= num2 + num5; k++)
                    {
                        if ((Math.Abs(j - num) + Math.Abs(k - num2)) < num5 * (1.0 + WorldGen.genRand.Next(-10, 11) * 0.005) && j >= 0 && j < Main.maxTilesX && k >= 0 && k < Main.maxTilesY)
                        {
                            Tile t = Main.tile[j, k];
                            t.HasTile = true;
                            t.TileType = (ushort)ModContent.TileType<Tiles.Ores.CaesiumOre>();
                            WorldGen.SquareTileFrame(j, k);
                        }
                    }
                }
                num5 += WorldGen.genRand.Next(-50, 51) * 0.03f;
                if (num5 < Size * 0.6)
                {
                    num5 = Size * 0.6f;
                }
                if (num5 > (Size * 2))
                {
                    num5 = Size * 2f;
                }
                num3 += WorldGen.genRand.Next(-20, 21) * 0.01f;
                num4 += WorldGen.genRand.Next(-20, 21) * 0.01f;
                if (num3 < -1f)
                {
                    num3 = -1f;
                }
                if (num3 > 1f)
                {
                    num3 = 1f;
                }
                if (num4 < -1f)
                {
                    num4 = -1f;
                }
                if (num4 > 1f)
                {
                    num4 = 1f;
                }
                num += (xDir + num3) * 0.3f;
                num2 += (yDir + num4) * 0.6f;
            }
        }
        catch
        {
        }
        return new Vector2(num, num2);
    }

    public static bool CreateSpikeDown(int x, int y, ushort type, bool down = false)
    {
        if (!WorldUtils.Find(new Point(x, y), Searches.Chain(new Searches.Down(10), new Conditions.IsSolid().AreaAnd(4, 1)), out var result))
        {
            return false;
        }
        float rn = WorldGen.genRand.NextFloat(1.5f, 4.5f);
        float angle = rn / 3f * 2f - 0.57075f;
        WorldUtils.Gen(result, new CaesiumSpikeShape(angle, WorldGen.genRand.Next(45, 55), 8, 1), new Actions.SetTile(type, setSelfFrames: true));
        return true;
    }

    public static bool CreateSpikeUp(int x, int y, ushort type)
    {
        if (!WorldUtils.Find(new Point(x, y), Searches.Chain(new Searches.Down(10), new Conditions.IsSolid().AreaAnd(4, 1)), out var result))
        {
            return false;
        }
        float rn = WorldGen.genRand.NextFloat(1.5f, 4.5f);
        float angle = -(rn / 3f * 2f + 0.57075f * 2);
        WorldUtils.Gen(result, new CaesiumSpikeShape(angle, WorldGen.genRand.Next(45, 55), 8, 1), new Actions.SetTile(type, setSelfFrames: true));
        return true;
    }

    public static void CreateSpike(int x, int y)
    {
        int xStep = 2;
        int yStep = 7;

        while (xStep < 3)
        {
            for (int i = x - xStep + 1; i < x + xStep + 1; i++)
            {
                for (int j = y - yStep + 1; j < y + yStep + 1; j++)
                {
                    Tile t = Main.tile[i, j];
                    t.TileType = (ushort)ModContent.TileType<Tiles.Ores.CaesiumOre>();
                    t.HasTile = true;
                }
                yStep += WorldGen.genRand.Next(1, 3);
            }
            xStep += WorldGen.genRand.Next(1, 3);
        }
        x += xStep - 2;
        if (WorldGen.genRand.Next(2) == 0)
        {
            y += WorldGen.genRand.Next(10, 15);
        }
        else y -= WorldGen.genRand.Next(10, 15);
        int xStep2 = 2;
        int yStep2 = 7;
        while (xStep2 < 3)
        {
            for (int i = x + xStep2 + 1; i > x - xStep2 + 1; i--)
            {
                for (int j = y + yStep2 + 1; j > y - yStep2 + 1; j--)
                {
                    Tile t = Main.tile[i, j];
                    t.TileType = (ushort)ModContent.TileType<Tiles.Ores.CaesiumOre>();
                    t.HasTile = true;
                }
                yStep2 += WorldGen.genRand.Next(1, 3);
            }
            xStep2 += WorldGen.genRand.Next(1, 3);
        }
    }
}
