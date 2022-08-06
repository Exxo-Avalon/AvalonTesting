using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalon.Walls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.World;
internal class Tests
{

    public static void MakeZigZag(int x, int y, ushort type, ushort wall)
    {
        int dist = Main.rand.Next(20, 30);
        int interiorDist = dist - 8;
        MakeSolidTunnel(type, wall, 20, new Vector2(x, y), new Vector2(x + dist, y + dist));
        MakeSolidTunnel(type, wall, 20, new Vector2(x + dist, y + dist), new Vector2(x, y + dist * 2));



        MakeHollowTunnel(wall, 8, new Vector2(x, y), new Vector2(x + interiorDist, y + interiorDist));
        MakeHollowTunnel(wall, 8, new Vector2(x + interiorDist, y + interiorDist), new Vector2(x, y + interiorDist * 2));
    }
    public static void MakeSolidTunnel(ushort type, ushort wall, int thickness, Vector2 startPoint, Vector2 endPoint)
    {
        //Vector2 endPoint1 = new Vector2(x + distanceX, y + distanceY);

        BoreTunnel((int)startPoint.X, (int)startPoint.Y, (int)endPoint.X, (int)endPoint.Y, thickness, type, wall);
    }
    public static void MakeHollowTunnel(ushort wall, int thickness, Vector2 startPoint, Vector2 endPoint)
    {
        BoreTunnel((int)startPoint.X + thickness / 2, (int)startPoint.Y + thickness / 2, (int)endPoint.X + thickness / 2, (int)endPoint.Y + thickness / 2, thickness, ushort.MaxValue, wall);
    }

    public static void MakeBox(int x, int y, int width, int height, ushort type, ushort wall)
    {
        int num = (int)(x - width / 2);
        int num2 = (int)(y - height / 2);
        int num3 = (int)(x + width / 2);
        int num4 = (int)(y + height / 2);
        for (int i = num; i < num3 + 1; i++)
        {
            for (int j = num2; j < num4 + 1; j++)
            {
                if (type == ushort.MaxValue)
                {
                    Main.tile[i, j].Active(false);
                    Main.tile[i, j].WallType = wall;
                    WorldGen.SquareWallFrame(i, j);
                }
                else
                {
                    Main.tile[i, j].Active(true);
                    Main.tile[i, j].TileType = type;
                    Main.tile[i, j].WallType = wall;
                    WorldGen.SquareTileFrame(i, j);
                    WorldGen.SquareWallFrame(i, j);
                }
            }
        }
    }

    public static void BoreTunnel(int x0, int y0, int x1, int y1, float r, ushort type, ushort wall)
    {
        bool flag = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
        if (flag)
        {
            Utils.Swap(ref x0, ref y0);
            Utils.Swap(ref x1, ref y1);
        }

        if (x0 > x1)
        {
            Utils.Swap(ref x0, ref x1);
            Utils.Swap(ref y0, ref y1);
        }

        int num = x1 - x0;
        int num2 = Math.Abs(y1 - y0);
        int num3 = num / 2;
        int num4 = y0 < y1 ? 1 : -1;
        int num5 = y0;
        for (int i = x0; i <= x1; i++)
        {
            if (flag)
            {
                MakeBox(num5, i, (int)r, (int)r, type, wall);
            }
            else
            {
                MakeBox(i, num5, (int)r, (int)r, type, wall);
            }

            num3 -= num2;
            if (num3 < 0)
            {
                num5 += num4;
                num3 += num;
            }
        }
    }

}
