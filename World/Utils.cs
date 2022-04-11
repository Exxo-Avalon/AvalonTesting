using AvalonTesting.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.World;

class Utils
{
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
            if ((tile.TileType == TileID.Dirt || tile.TileType == TileID.ClayBlock || tile.TileType == TileID.Stone || tile.TileType == TileID.Sand || tile.TileType == ModContent.TileType<Snotsand>() || tile.TileType == ModContent.TileType<TropicalMud>() || tile.TileType == TileID.Mud || tile.TileType == TileID.SnowBlock || tile.TileType == TileID.IceBlock) && tile.HasTile)
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
