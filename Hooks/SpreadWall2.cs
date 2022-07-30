using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Common;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Both)]
internal class SpreadWall2 : ModHook
{
    protected override void Apply()
    {
        On.Terraria.WorldGen.Spread.Wall2 += OnSpreadWall2;
    }
    private static void OnSpreadWall2(On.Terraria.WorldGen.Spread.orig_Wall2 orig, int x, int y, int wallType)
    {
        if (!WorldGen.InWorld(x, y))
        {
            return;
        }
        ushort num = (ushort)wallType;
        int num2 = 0;
        int maxWallOut = WorldGen.maxWallOut2;
        List<Point> list = new List<Point>();
        List<Point> list2 = new List<Point>();
        HashSet<Point> hashSet = new HashSet<Point>();
        list2.Add(new Point(x, y));
        Point item2 = default(Point);
        while (list2.Count > 0)
        {
            list.Clear();
            list.AddRange(list2);
            list2.Clear();
            while (list.Count > 0)
            {
                Point item = list[0];
                if (!WorldGen.InWorld(item.X, item.Y, 1))
                {
                    list.Remove(item);
                    continue;
                }
                hashSet.Add(item);
                list.Remove(item);
                Tile tile = Main.tile[item.X, item.Y];
                if (!WorldGen.SolidTile(item.X, item.Y) && tile.WallType != num && tile.WallType != 4 && tile.WallType != 40 && tile.WallType != 3 && tile.WallType != 87 && tile.WallType != 34 && tile.WallType != (ushort)ModContent.WallType<Walls.ChunkstoneWall>())
                {
                    bool flag = num is WallID.GrassUnsafe or 62;
                    if (flag && tile.WallType == 0)
                    {
                        list.Remove(item);
                        continue;
                    }
                    num2++;
                    if (num2 >= maxWallOut)
                    {
                        list.Remove(item);
                        continue;
                    }
                    tile.WallType = num;
                    item2 = new(item.X - 1, item.Y);
                    if (!hashSet.Contains(item2))
                    {
                        list2.Add(item2);
                    }
                    item2 = new(item.X + 1, item.Y);
                    if (!hashSet.Contains(item2))
                    {
                        list2.Add(item2);
                    }
                    item2 = new(item.X, item.Y - 1);
                    if (!hashSet.Contains(item2))
                    {
                        list2.Add(item2);
                    }
                    item2 = new(item.X, item.Y + 1);
                    if (!hashSet.Contains(item2))
                    {
                        list2.Add(item2);
                    }
                    if (flag)
                    {
                        item2 = new(item.X - 1, item.Y - 1);
                        if (!hashSet.Contains(item2))
                        {
                            list2.Add(item2);
                        }
                        item2 = new(item.X + 1, item.Y - 1);
                        if (!hashSet.Contains(item2))
                        {
                            list2.Add(item2);
                        }
                        item2 = new(item.X - 1, item.Y + 1);
                        if (!hashSet.Contains(item2))
                        {
                            list2.Add(item2);
                        }
                        item2 = new(item.X + 1, item.Y + 1);
                        if (!hashSet.Contains(item2))
                        {
                            list2.Add(item2);
                        }
                        item2 = new(item.X - 2, item.Y);
                        if (!hashSet.Contains(item2))
                        {
                            list2.Add(item2);
                        }
                        item2 = new(item.X + 2, item.Y);
                        if (!hashSet.Contains(item2))
                        {
                            list2.Add(item2);
                        }
                    }
                }
                else if (tile.HasTile && tile.WallType != num && tile.WallType != 4 && tile.WallType != 40 && tile.WallType != 3 && tile.WallType != 87 && tile.WallType != 34 && tile.WallType != (ushort)ModContent.WallType<Walls.ChunkstoneWall>())
                {
                    tile.WallType = num;
                }
            }
        }
    }
}
