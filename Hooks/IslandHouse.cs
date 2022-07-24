using Avalon.Common;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Both)]
public class IslandHouse : ModHook
{
    private static int houseCount;
    protected override void Apply()
    {
        On.Terraria.WorldGen.IslandHouse += OnIslandHouse;
    }
    private static void OnIslandHouse(On.Terraria.WorldGen.orig_IslandHouse orig, int i, int j, int islandStyle)
    {
        ushort type = TileID.Sunplate;
        ushort wallType = WallID.DiscWall;
        if (WorldGen.SavedOreTiers.Gold == TileID.Platinum)
        {
            type = (ushort)ModContent.TileType<Tiles.MoonplateBlock>();
            wallType = (ushort)ModContent.WallType<Walls.MoonWall>();
        }
        if (WorldGen.SavedOreTiers.Gold == ModContent.TileType<Tiles.Ores.BismuthOre>())
        {
            type = (ushort)ModContent.TileType<Tiles.TwiliplateBlock>();
            wallType = (ushort)ModContent.WallType<Walls.TwilightWall>();
        }
        Vector2 vector2 = new Vector2(i, j);
        int num3 = 1;
        if (WorldGen.genRand.NextBool(2))
            num3 = -1;
        int num4 = WorldGen.genRand.Next(7, 12);
        int num5 = WorldGen.genRand.Next(5, 7);
        vector2.X = i + (num4 + 2) * num3;
        for (int y = j - 15; y < j + 30; ++y)
        {
            if (Main.tile[(int)vector2.X, y].HasTile)
            {
                vector2.Y = y - 1;
                break;
            }
        }
        vector2.X = i;
        int num6 = (int)(vector2.X - num4 - 1.0);
        int num7 = (int)(vector2.X + num4 + 1.0);
        int num8 = (int)(vector2.Y - num5 - 1.0);
        int num9 = (int)(vector2.Y + 2.0);
        if (num6 < 0)
            num6 = 0;
        if (num7 > Main.maxTilesX)
            num7 = Main.maxTilesX;
        if (num8 < 0)
            num8 = 0;
        if (num9 > Main.maxTilesY)
            num9 = Main.maxTilesY;
        Tile tile;
        for (int x = num6; x <= num7; ++x)
        {
            for (int y = num8 - 1; y < num9 + 1; ++y)
            {
                if (y != num8 - 1 || x != num6 && x != num7)
                {
                    tile = Main.tile[x, y];
                    tile.HasTile = true;
                    tile = Main.tile[x, y];
                    tile.LiquidAmount = 0;
                    tile = Main.tile[x, y];
                    tile.TileType = type;
                    tile = Main.tile[x, y];
                    tile.WallType = 0;
                    tile = Main.tile[x, y];
                    tile.IsHalfBlock = false;
                    tile = Main.tile[x, y];
                    tile.Slope = SlopeType.Solid;
                }
            }
        }
        int num10 = (int)(vector2.X - num4);
        int num11 = (int)(vector2.X + num4);
        int j1 = (int)(vector2.Y - num5);
        int num12 = (int)(vector2.Y + 1.0);
        if (num10 < 0)
            num10 = 0;
        if (num11 > Main.maxTilesX)
            num11 = Main.maxTilesX;
        if (j1 < 0)
            j1 = 0;
        if (num12 > Main.maxTilesY)
            num12 = Main.maxTilesY;
        for (int x = num10; x <= num11; ++x)
        {
            for (int y = j1; y < num12; ++y)
            {
                if (y != j1 || x != num10 && x != num11)
                {
                    tile = Main.tile[x, y];
                    if (tile.WallType == 0)
                    {
                        tile = Main.tile[x, y];
                        tile.HasTile = false;
                        tile = Main.tile[x, y];
                        tile.WallType = wallType;
                    }
                }
            }
        }
        int i1 = i + (num4 + 1) * num3;
        int y1 = (int)vector2.Y;
        for (int x = i1 - 2; x <= i1 + 2; ++x)
        {
            tile = Main.tile[x, y1];
            tile.HasTile = false;
            tile = Main.tile[x, y1 - 1];
            tile.HasTile = false;
            tile = Main.tile[x, y1 - 2];
            tile.HasTile = false;
        }
        WorldGen.PlaceTile(i1, y1, 10, true, style: 9);
        int x1 = i + (num4 + 1) * -num3 - num3;
        for (int y2 = j1; y2 <= num12 + 1; ++y2)
        {
            tile = Main.tile[x1, y2];
            tile.HasTile = true;
            tile = Main.tile[x1, y2];
            tile.LiquidAmount = 0;
            tile = Main.tile[x1, y2];
            tile.TileType = type;
            tile = Main.tile[x1, y2];
            tile.WallType = 0;
            tile = Main.tile[x1, y2];
            tile.IsHalfBlock = false;
            tile = Main.tile[x1, y2];
            tile.Slope = SlopeType.Solid;
        }
        int contain = 0;
        int num13 = houseCount;
        if (num13 > 2)
            num13 = WorldGen.genRand.Next(3);
        switch (num13)
        {
            case 0:
                contain = ItemID.ShinyRedBalloon;
                break;
            case 1:
                contain = ItemID.Starfury;
                break;
            case 2:
                contain = ItemID.CreativeWings;
                break;
        }
        if (WorldGen.getGoodWorldGen)
            WorldGen.AddBuriedChest(i, y1 - 3, contain, Style: 2);
        else
            WorldGen.AddBuriedChest(i, y1 - 3, contain, Style: 13);
        if (islandStyle > 0)
        {
            for (int index = 0; index < 100000; ++index)
            {
                int num14 = i + WorldGen.genRand.Next(-50, 51);
                int num15 = y1 + WorldGen.genRand.Next(21);
                if (index < 50000)
                {
                    tile = Main.tile[num14, num15];
                    if (tile.TileType == type)
                        continue;
                }
                tile = Main.tile[num14, num15];
                if (!tile.HasTile)
                {
                    WorldGen.Place2xX(num14, num15, 207, islandStyle);
                    tile = Main.tile[num14, num15];
                    if (tile.HasTile)
                    {
                        WorldGen.SwitchFountain(num14, num15);
                        break;
                    }
                }
            }
        }
        ++houseCount;
        int num16 = i - num4 / 2 + 1;
        int num17 = i + num4 / 2 - 1;
        int num18 = 1;
        if (num4 > 10)
            num18 = 2;
        int num19 = (j1 + num12) / 2 - 1;
        for (int x2 = num16 - num18; x2 <= num16 + num18; ++x2)
        {
            for (int y3 = num19 - 1; y3 <= num19 + 1; ++y3)
            {
                tile = Main.tile[x2, y3];
                tile.WallType = 21;
            }
        }
        for (int x3 = num17 - num18; x3 <= num17 + num18; ++x3)
        {
            for (int y4 = num19 - 1; y4 <= num19 + 1; ++y4)
            {
                tile = Main.tile[x3, y4];
                tile.WallType = 21;
            }
        }
        int i2 = i + (num4 / 2 + 1) * -num3;
        WorldGen.PlaceTile(i2, num12 - 1, 14, true, style: 7);
        WorldGen.PlaceTile(i2 - 2, num12 - 1, 15, true, plr: 0, style: 10);
        tile = Main.tile[i2 - 2, num12 - 1];
        tile.TileFrameX += 18;
        tile = Main.tile[i2 - 2, num12 - 2];
        tile.TileFrameX += 18;
        WorldGen.PlaceTile(i2 + 2, num12 - 1, 15, true, plr: 0, style: 10);
        WorldGen.PlaceTile(num10 + 1, j1, 91, true, style: WorldGen.genRand.Next(7, 10));
        WorldGen.PlaceTile(num11 - 1, j1, 91, true, style: WorldGen.genRand.Next(7, 10));
        int i3;
        int j2;
        if (num3 > 0)
        {
            i3 = num10;
            j2 = j1 + 1;
        }
        else
        {
            i3 = num11;
            j2 = j1 + 1;
        }
        WorldGen.PlaceTile(i3, j2, 91, true, style: WorldGen.genRand.Next(7, 10));
        if (islandStyle != 1)
            return;
        int num20 = WorldGen.genRand.Next(3, 6);
        for (int index = 0; index < 100000; ++index)
        {
            int num21 = i + WorldGen.genRand.Next(-50, 51);
            int y5 = y1 + WorldGen.genRand.Next(-10, 21);
            tile = Main.tile[num21, y5];
            if (!tile.HasTile)
            {
                WorldGen.GrowPalmTree(num21, y5 + 1);
                tile = Main.tile[num21, y5];
                if (tile.HasTile)
                    --num20;
            }
            if (num20 <= 0)
                break;
        }
    }
}
