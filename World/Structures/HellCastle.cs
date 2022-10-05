using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Avalon.World.Structures;

internal class Hellcastle
{
    public static void GenerateHellcastle(int x, int y)
    {
        Vector2 castleBottomCenterL = new Vector2(x + 199, y + 150);
        Vector2 castleBottomCenterR = new Vector2(x + 200, y + 150);

        MakeBox(x, y, 400, 150, ModContent.TileType<Tiles.ImperviousBrick>(), (ushort)ModContent.WallType<Walls.ImperviousBrickWallUnsafe>());
        MakePath(x, y, -1);
        MakePath(x, y, 1);

        MakeTunnel((int)castleBottomCenterL.X - 25, (int)castleBottomCenterL.Y - 115, Vector2.Zero, Vector2.Zero, 50);
        MakeTunnel((int)castleBottomCenterR.X + 25, (int)castleBottomCenterR.Y - 115, Vector2.Zero, Vector2.Zero, 50);

        RefineStructure(x, y, 400, 150);

        AddPlatforms(x, y, 400, 150);
        AddSpikes(x, y, 400, 150);
        AddFurniture(x, y, 400, 150);
        AddChests(x, y, 400, 150);
        AddPots(x, y, 400, 150);
        MakeEntranceArea(x + 200, y + 135);
    }
    public static void MakeEntranceArea(int x, int y)
    {
        MakeBox(x - 10, y + 13, 20, 25, ModContent.TileType<Tiles.ImperviousBrick>(), (ushort)ModContent.WallType<Walls.ImperviousBrickWallUnsafe>());
        MakeBox(x - 7, y, 14, 40, ushort.MaxValue, (ushort)ModContent.WallType<Walls.ImperviousBrickWallUnsafe>());
        MakeBox(x - 7, y + 34, 14, 4, ModContent.TileType<Tiles.UltraResistantWood>(), (ushort)ModContent.WallType<Walls.ImperviousBrickWallUnsafe>());
    }
    public static void MakePath(int x, int y, int start)
    {
        Vector2 lastPos;
        Vector2 newPos = Vector2.Zero;

        Vector2 castleBottomCenterL = new Vector2(x + 199, y + 150);
        Vector2 castleBottomCenterR = new Vector2(x + 200, y + 150);

        int randDir = start;
        int randHeight = start;
        int lastPath;
        int givePurpose = 0;

        for (int i = 0; i < 60; i++)
        {
            lastPath = givePurpose;
            givePurpose = Main.rand.Next(2);
            int randStairHeight = Main.rand.Next(12, 20);
            randStairHeight = 15;
            int randHallwayLength = 30;

            if (givePurpose == 0 && lastPath == 0)
            {
                givePurpose = 1;
            }

            lastPos = newPos;
            switch (givePurpose)
            {
                case 0:
                    if (lastPos.X - randStairHeight < -200 + randStairHeight && randDir == -1)
                    {
                        randDir = 1;
                    }
                    if (lastPos.X + randStairHeight > 200 - randStairHeight && randDir == 1)
                    {
                        randDir = -1;
                    }
                    if(lastPos.Y + randStairHeight > 0 - randStairHeight && randHeight == 1)
                    {
                        randHeight = -1;
                    }
                    if(lastPos.Y - randStairHeight < -140 + randStairHeight && randHeight == -1)
                    {
                        randHeight = 1;
                    }
                    newPos += new Vector2(randStairHeight * randDir, randStairHeight * randHeight);
                    break;
                case 1:
                    if (lastPos.X - randHallwayLength < -200 + randHallwayLength && randDir == -1)
                    {
                        randDir = 1;
                    }
                    if (lastPos.X + randHallwayLength > 200 - randHallwayLength && randDir == 1)
                    {
                        randDir = -1;
                    }
                    newPos += new Vector2(randHallwayLength * randDir, 0);
                    break;
            }
            MakeTunnel((int)castleBottomCenterL.X, (int)castleBottomCenterL.Y - 15, lastPos, newPos, Main.rand.Next(6, 9));
        }
    }
    public static void MakeBox(float x, float y, int width, int height, int type, ushort walltype)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Tile t = Main.tile[(int)x + i, (int)y + j];
                if (type == ushort.MaxValue)
                {
                    t.TileType = 0;
                    t.LiquidAmount = 0;
                    t.HasTile = false;
                }
                else
                {
                    t.TileType = (ushort)type;
                    t.HasTile = true;
                    t.LiquidAmount = 0;
                    t.LiquidType = LiquidID.Water;
                    t.IsHalfBlock = false;
                    t.Slope = SlopeType.Solid;
                    if (i == 0 || i == width - 1 || j == 0 || j == height - 1)
                    {

                    }
                    else
                    {
                        WorldGen.KillWall((int)x + i, (int)y + j);
                        WorldGen.PlaceWall((int)x + i, (int)y + j, walltype, true);
                    }
                }
            }
        }
    }
    /// <summary>
    /// Destroys a box of tiles.
    /// </summary>
    public static void DestroyBox(int x, int y, int width, int height)
    {
        int a = -(width / 2);
        int b = -(height / 2);
        for (int i = a; i <= width / 2; i++)
        {
            for (int j = b; j <= height / 2; j++)
            {
                WorldGen.KillTile(x + i, y + j, noItem: true);
            }
        }
    }
    public static void MakeBoxFromCenter(int x, int y, int width, int height, int type)
    {
        int a = -(width / 2);
        int b = -(height / 2);
        for (int i = a; i <= width / 2; i++)
        {
            for (int j = b; j <= height / 2; j++)
            {
                WorldGen.PlaceTile(x + i, y + j + 1, type);
            }
        }
    }
    public static void AddChests(int x, int y, int width, int height)
    {
        int maxAmount = Main.rand.Next(5, 8);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (i == 0 || i == width - 1 || j == 0 || j == height - 1)
                {

                }
                else
                {
                    if (Main.tile[x + i, y + j].HasTile && !Main.tile[x + i, y + j - 1].HasTile && !Main.tile[x + i + 1, y + j - 1].HasTile)
                    {
                        if(Main.rand.NextBool(80) && maxAmount > 0)
                        {
                            maxAmount--;
                            WorldGen.PlaceChest(x + i, y + j - 1, (ushort)ModContent.TileType<Tiles.Furniture.ResistantWood.ResistantWoodChest>());
                        }
                    }
                }
            }
        }
    }
    public static void RefineStructure(int x, int y, int width, int height)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if(Main.tile[x + i, y + j].HasTile)
                {
                    if (!Main.tile[x + i, y + j - 1].HasTile && !Main.tile[x + i, y + j + 1].HasTile)
                    {
                        WorldGen.KillTile(x + i, y + j, noItem: true);
                    }
                    if (!Main.tile[x + i, y + j - 1].HasTile && !Main.tile[x + i, y + j + 2].HasTile)
                    {
                        WorldGen.KillTile(x + i, y + j, noItem: true);
                    }
                    if (!Main.tile[x + i, y + j + 1].HasTile && !Main.tile[x + i - 1, y + j].HasTile && !Main.tile[x + i + 1, y + j].HasTile)
                    {
                        int size = 0;
                        for (int k = 0; k <= 8; k++)
                        {
                            if(Main.tile[x + i, y + j - k].HasTile)
                            {
                                size++;
                            }
                            if (!Main.tile[x + i, y + j - k].HasTile)
                            {
                                DestroyBox(x + i, y + j - k + (size / 2), size * 2, size + 1);
                                size = 0;
                                break;
                            }
                        }
                    }
                    if (!Main.tile[x + i, y + j - 1].HasTile && !Main.tile[x + i - 1, y + j].HasTile && !Main.tile[x + i + 1, y + j].HasTile)
                    {
                        int size = 0;
                        for (int k = 0; k <= 8; k++)
                        {
                            if (Main.tile[x + i, y + j + k].HasTile)
                            {
                                size++;
                            }
                            if (!Main.tile[x + i, y + j + k].HasTile)
                            {
                                DestroyBox(x + i, y + j + k - (size / 2), size * 2, size + 1);
                                size = 0;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
    public static void AddPlatforms(int x, int y, int width, int height)
    {
        int randLength;
        int counter = 0;
        int countTo = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (i == 0 || i == width - 1 || j == 0 || j == height - 1)
                {

                }
                else
                {
                    countTo = 5 + Main.rand.Next(5);
                    randLength = Main.rand.Next(2, 8);
                    if(Main.tile[x + i, y + j].HasTile)
                    {
                        if (!Main.tile[x + i - 1, y + j].HasTile && Main.tile[x + i, y + j + 1].HasTile && Main.tile[x + i, y + j - 1].HasTile && Main.tile[x + i + 1, y + j].HasTile)
                        {
                            counter++;
                            if (counter > countTo)
                            {
                                for (int k = 0; k < randLength; k++)
                                {
                                    WorldGen.PlaceTile(x + i - k - 1, y + j, ModContent.TileType<Tiles.Furniture.ResistantWood.ResistantWoodPlatform>(), true, false);
                                    if (k <= 1)
                                    {
                                        WorldGen.PlaceTile(x + i - k - 1, y + j - 1, TileID.Books, true, true);
                                    }
                                    else if (Main.rand.NextBool(3))
                                    {
                                        WorldGen.PlaceTile(x + i - k - 1, y + j - 1, TileID.Books, true, true);
                                    }
                                }
                                counter = 0;
                            }
                        }
                        if (!Main.tile[x + i + 1, y + j].HasTile && Main.tile[x + i, y + j + 1].HasTile && Main.tile[x + i, y + j - 1].HasTile && Main.tile[x + i - 1, y + j].HasTile)
                        {
                            counter++;
                            if (counter > countTo)
                            {
                                for (int k = 0; k < randLength; k++)
                                {
                                    WorldGen.PlaceTile(x + i + k + 1, y + j, ModContent.TileType<Tiles.Furniture.ResistantWood.ResistantWoodPlatform>(), true, false);
                                    if (k <= 1)
                                    {
                                        WorldGen.PlaceTile(x + i + k + 1, y + j - 1, TileID.Books, true, true);
                                    }
                                    else if (Main.rand.NextBool(3))
                                    {
                                        WorldGen.PlaceTile(x + i + k + 1, y + j - 1, TileID.Books, true, true);
                                    }
                                }
                                counter = 0;
                            }
                        }
                        if (!Main.tile[x + i + 1, y + j].HasTile && !Main.tile[x + i, y + j + 1].HasTile && Main.tile[x + i - 1, y + j].HasTile && Main.tile[x + i, y + j - 1].HasTile && !Main.tileSolidTop[Main.tile[x + i, y + j].TileType])
                        {
                            counter++;
                            if (counter > countTo)
                            {
                                for (int k = 0; k < randLength; k++)
                                {
                                    WorldGen.PlaceTile(x + i + k + 1, y + j, ModContent.TileType<Tiles.Furniture.ResistantWood.ResistantWoodPlatform>(), true, false);
                                    if (k <= 1)
                                    {
                                        WorldGen.PlaceTile(x + i + k + 1, y + j - 1, TileID.Books, true, true);
                                    }
                                    else if (Main.rand.NextBool(3))
                                    {
                                        WorldGen.PlaceTile(x + i + k + 1, y + j - 1, TileID.Books, true, true);
                                    }
                                }
                                counter = 0;
                            }
                        }
                        if (Main.tile[x + i + 1, y + j].HasTile && !Main.tile[x + i, y + j + 1].HasTile && !Main.tile[x + i - 1, y + j].HasTile && Main.tile[x + i, y + j - 1].HasTile && !Main.tileSolidTop[Main.tile[x + i, y + j].TileType])
                        {
                            counter++;
                            if (counter > countTo)
                            {
                                for (int k = 0; k < randLength; k++)
                                {
                                    WorldGen.PlaceTile(x + i - k - 1, y + j, ModContent.TileType<Tiles.Furniture.ResistantWood.ResistantWoodPlatform>(), true, false);
                                    if (k <= 1)
                                    {
                                        WorldGen.PlaceTile(x + i - k - 1, y + j - 1, TileID.Books, true, true);
                                    }
                                    else if (Main.rand.NextBool(3))
                                    {
                                        WorldGen.PlaceTile(x + i - k - 1, y + j - 1, TileID.Books, true, true);
                                    }
                                }
                                counter = 0;
                            }
                        }
                    }
                }
            }
        }
    }
    public static void AddSpikes(int x, int y, int width, int height)
    {
        int counter = 0;
        int countTo = 20;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (i == 0 || i == width - 1 || j == 0 || j == height - 1)
                {

                }
                else
                {
                    if(Main.tile[x + i, y + j].HasTile && !Main.tileSolidTop[Main.tile[x + i, y + j].TileType] && Main.tileSolid[Main.tile[x + i, y + j].TileType])
                    {
                        if (!Main.tile[x + i, y + j - 1].HasTile && Main.tile[x + i + 1, y + j].HasTile && Main.tile[x + i - 1, y + j].HasTile)
                        {
                            counter++;
                            if (counter > countTo)
                            {
                                GenerateSpikeTrap(x + i, y + j, Main.rand.Next(9, 21));
                                counter = 0;
                                countTo = 20;
                            }
                        }
                        if (!Main.tile[x + i, y + j + 1].HasTile && Main.tile[x + i + 1, y + j].HasTile && Main.tile[x + i - 1, y + j].HasTile)
                        {
                            counter++;
                            if (counter > countTo)
                            {
                                GenerateSpikeTrap(x + i, y + j, Main.rand.Next(9, 21));
                                counter = 0;
                                countTo = 20;
                            }
                        }
                    }
                }
            }
        }
    }
    public static void GenerateSpikeTrap(int x, int y, int length)
    {
        if(length%2 == 0)
        {
            length++;
        }
        for (int i = 1; i <= length; i++)
        {
            if(!Main.tile[x + i + 2, y].HasTile || Main.tileSolidTop[Main.tile[x + i + 2, y].TileType])
            {
                break;
            }
            if (Main.tile[x + i + 2, y - 1].HasTile && Main.tile[x + i + 2, y + 1].HasTile)
            {
                break;
            }
            if (i%2 == 0)
            {
                WorldGen.PlaceTile(x + i - 1, y, ModContent.TileType<Tiles.VenomSpike>(), true, true);
                if(!Main.tile[x, y - 1].HasTile)
                {
                    WorldGen.PlaceTile(x + i - 1, y - 1, ModContent.TileType<Tiles.VenomSpike>(), true, true);
                    if (Main.rand.NextBool(2) && i > 2 && i < length - 1)
                    {
                        WorldGen.PlaceTile(x + i - 1, y - 2, ModContent.TileType<Tiles.VenomSpike>(), true, true);
                    }
                }
                else
                {
                    WorldGen.PlaceTile(x + i - 1, y + 1, ModContent.TileType<Tiles.VenomSpike>(), true, true);
                    if (Main.rand.NextBool(2) && i > 2 && i < length - 1)
                    {
                        WorldGen.PlaceTile(x + i - 1, y + 2, ModContent.TileType<Tiles.VenomSpike>(), true, true);
                    }
                }
            }
            else
            {
                WorldGen.PlaceTile(x + i - 1, y, ModContent.TileType<Tiles.VenomSpike>(), true, true);
            }
        }
    }
    public static void AddFurniture(int x, int y, int width, int height)
    {
        int counter = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (i == 0 || i == width - 1 || j == 0 || j == height - 1)
                {

                }
                else
                {
                    if(Main.tile[x + i, y + j].HasTile && !Main.tile[x + i, y + j - 1].HasTile && Main.tile[x + i - 1, y + j].HasTile && Main.tile[x + i + 1, y + j].HasTile && !Main.tile[x + i + 1, y + j - 1].HasTile && !Main.tile[x + i - 1, y + j - 1].HasTile)
                    {
                        counter++;
                        if (counter > 10 && Main.rand.NextBool(3))
                        {
                            int randomTile = Main.rand.Next(10);
                            int type = TileID.Bookcases;
                            switch (randomTile)
                            {
                                case 0:
                                    type = ModContent.TileType<Tiles.Furniture.ResistantWood.ResistantWoodBathtub>();
                                    break;
                                case 1:
                                    type = ModContent.TileType<Tiles.Furniture.ResistantWood.ResistantWoodBed>();
                                    break;
                                case 2:
                                    type = ModContent.TileType<Tiles.Furniture.ResistantWood.ResistantWoodClock>();
                                    break;
                                case 3:
                                    type = ModContent.TileType<Tiles.Furniture.ResistantWood.ResistantWoodTable>();
                                    break;
                                case 4:
                                    type = ModContent.TileType<Tiles.Furniture.ResistantWood.ResistantWoodSofa>();
                                    break;
                                case 5:
                                    type = ModContent.TileType<Tiles.Furniture.ResistantWood.ResistantWoodPiano>();
                                    break;
                                case 6:
                                    type = ModContent.TileType<Tiles.Furniture.ResistantWood.ResistantWoodSink>();
                                    break;
                                case 7:
                                    type = ModContent.TileType<Tiles.Furniture.ResistantWood.ResistantWoodDresser>();
                                    break;
                                case 8:
                                    type = ModContent.TileType<Tiles.Furniture.ResistantWood.ResistantWoodBookcase>();
                                    break;
                                case 9:
                                    type = 105;
                                    break;
                            }
                            WorldGen.PlaceTile(x + i, y + j - 1, type, true, true);
                            counter = 0;
                        }
                    }
                }
            }
        }
    }
    public static void AddPots(int x, int y, int width, int height)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (i == 0 || i == width - 1 || j == 0 || j == height - 1)
                {

                }
                else
                {
                    if (Main.tile[x + i, y + j].HasTile && !Main.tile[x + i, y + j - 1].HasTile && !Main.tile[x + i + 1, y + j - 1].HasTile && Main.rand.NextBool(3))
                    {
                        WorldGen.PlacePot(x + i, y + j - 1, TileID.Pots, style: Main.rand.Next(13, 16));
                    }
                }
            }
        }
    }

    public static void MakeTunnel(int x, int y, Vector2 beginPoint, Vector2 endPoint, int thickness = 20)
    {
        BoreTunnel2(x + (int)beginPoint.X, y + (int)beginPoint.Y, x + (int)endPoint.X, y + (int)endPoint.Y, thickness, ushort.MaxValue, 0);
    }
    public static void MakeTunnelHollow(int x, int y, Vector2 beginPoint, Vector2 endPoint, int thicknessTunnel = 20, int thicknessHollow = 10)
    {
        int offset = (thicknessTunnel / 2) - (thicknessHollow / 2);
        BoreTunnel2(x + (int)beginPoint.X + offset, y + (int)beginPoint.Y + offset, x + (int)endPoint.X + offset, y + (int)endPoint.Y + offset, thicknessHollow, ushort.MaxValue, WallID.Wood);
    }
    public static void BoreTunnel2(int x0, int y0, int x1, int y1, float r, ushort type, ushort walltype)
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
                DestroyBox(num5, i, (int)r, (int)r);
            }
            else
            {
                DestroyBox(i, num5, (int)r, (int)r);
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
