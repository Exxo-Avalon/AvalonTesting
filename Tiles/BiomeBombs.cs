using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Audio;

namespace AvalonTesting.Tiles;

public class BiomeBombs : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(Color.Gray);
        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
        TileObjectData.addTile(Type);
        Main.tileFrameImportant[Type] = true;
    }

    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        int item = 0;
        switch (frameX / 54)
        {
            case 0:
                item = ModContent.ItemType<Items.Placeable.Tile.PurityBomb>();
                break;
            case 1:
                item = ModContent.ItemType<Items.Placeable.Tile.CorruptionBomb>();
                break;
            case 2:
                item = ModContent.ItemType<Items.Placeable.Tile.JungleBomb>();
                break;
            case 3:
                item = ModContent.ItemType<Items.Placeable.Tile.CrimsonBomb>();
                break;
            case 4:
                item = ModContent.ItemType<Items.Placeable.Tile.ContagionBomb>();
                break;
            case 5:
                item = ModContent.ItemType<Items.Placeable.Tile.MushroomBomb>();
                break;
            case 6:
                item = ModContent.ItemType<Items.Placeable.Tile.HallowBomb>();
                break;
            case 7:
                item = ModContent.ItemType<Items.Placeable.Tile.TropicsBomb>();
                break;
        }
        if (item > 0)
        {
            Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 48, 48, item);
        }
    }

    public override bool RightClick(int i, int j)
    {
        int typeC = 2;
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX <= 52) typeC = 0;
        else if (tile.TileFrameX >= 54 && tile.TileFrameX <= 106) typeC = 1;
        else if (tile.TileFrameX >= 108 && tile.TileFrameX <= 160) typeC = 5;
        else if (tile.TileFrameX >= 162 && tile.TileFrameX <= 214) typeC = 4;
        else if (tile.TileFrameX >= 216 && tile.TileFrameX <= 268) typeC = 6;
        else if (tile.TileFrameX >= 270 && tile.TileFrameX <= 322) typeC = 3;
        else if (tile.TileFrameX >= 324 && tile.TileFrameX <= 376) typeC = 2;
        else if (tile.TileFrameX >= 378 && tile.TileFrameX <= 430) typeC = 7;
        BCBConvert(i, j, typeC, 75);
        //WorldGen.KillTile(i, j, noItem: true);
        for (int x = i - 1; x <= i + 1; x++)
        {
            for (int y = j - 1; y <= j; y++)
            {
                WorldGen.KillTile(x, y, noItem: true);
                if (!Main.tile[x, y].HasTile && Main.netMode != NetmodeID.SinglePlayer)
                {
                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, x, y);
                }
                for (int num369 = 0; num369 < 3; num369++)
                {
                    int num370 = Dust.NewDust(new Vector2(x * 16, y * 16), 8, 8, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
                    Main.dust[num370].velocity *= 1.4f;
                }
                for (int num371 = 0; num371 < 3; num371++)
                {
                    int num372 = Dust.NewDust(new Vector2(x * 16, y * 16), 8, 8, DustID.Torch, 0f, 0f, 100, default, 2.5f);
                    Main.dust[num372].noGravity = true;
                    Main.dust[num372].velocity *= 5f;
                    num372 = Dust.NewDust(new Vector2(x * 16, y * 16), 8, 8, DustID.Torch, 0f, 0f, 100, default, 1.5f);
                    Main.dust[num372].velocity *= 3f;
                }
            }
        }
        SoundEngine.PlaySound(2, i * 16, j * 16, 14);
        return true;
    }
    public override void MouseOver(int i, int j)
    {
        Player player = Main.LocalPlayer;
        player.noThrow = 2;
        player.cursorItemIconEnabled = true;
        int item = 0;
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX <= 52) item = ModContent.ItemType<Items.Placeable.Tile.PurityBomb>();
        else if (tile.TileFrameX >= 54 && tile.TileFrameX <= 106) item = ModContent.ItemType<Items.Placeable.Tile.CorruptionBomb>();
        else if (tile.TileFrameX >= 108 && tile.TileFrameX <= 160) item = ModContent.ItemType<Items.Placeable.Tile.JungleBomb>();
        else if (tile.TileFrameX >= 162 && tile.TileFrameX <= 214) item = ModContent.ItemType<Items.Placeable.Tile.CrimsonBomb>();
        else if (tile.TileFrameX >= 216 && tile.TileFrameX <= 268) item = ModContent.ItemType<Items.Placeable.Tile.ContagionBomb>();
        else if (tile.TileFrameX >= 270 && tile.TileFrameX <= 322) item = ModContent.ItemType<Items.Placeable.Tile.MushroomBomb>();
        else if (tile.TileFrameX >= 324 && tile.TileFrameX <= 376) item = ModContent.ItemType<Items.Placeable.Tile.HallowBomb>();
        else if (tile.TileFrameX >= 378 && tile.TileFrameX <= 430) item = ModContent.ItemType<Items.Placeable.Tile.TropicsBomb>();
        player.cursorItemIconID = item;
    }
    public override void HitWire(int i, int j)
    {
        int typeC = 2;
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX <= 52) typeC = 0;
        else if (tile.TileFrameX >= 54 && tile.TileFrameX <= 106) typeC = 1;
        else if (tile.TileFrameX >= 108 && tile.TileFrameX <= 160) typeC = 5;
        else if (tile.TileFrameX >= 162 && tile.TileFrameX <= 214) typeC = 4;
        else if (tile.TileFrameX >= 216 && tile.TileFrameX <= 268) typeC = 6;
        else if (tile.TileFrameX >= 270 && tile.TileFrameX <= 322) typeC = 3;
        else if (tile.TileFrameX >= 324 && tile.TileFrameX <= 376) typeC = 2;
        else if (tile.TileFrameX >= 378 && tile.TileFrameX <= 430) typeC = 7;
        BCBConvert(i, j, typeC, 75);
        //WorldGen.KillTile(i, j, noItem: true);
        for (int x = i - 1; x <= i + 1; x++)
        {
            for (int y = j - 1; y <= j; y++)
            {
                WorldGen.KillTile(x, y, noItem: true);
                if (!Main.tile[x, y].HasTile && Main.netMode != NetmodeID.SinglePlayer)
                {
                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, x, y);
                }
                for (int num369 = 0; num369 < 3; num369++)
                {
                    int num370 = Dust.NewDust(new Vector2(x * 16, y * 16), 8, 8, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
                    Main.dust[num370].velocity *= 1.4f;
                }
                for (int num371 = 0; num371 < 3; num371++)
                {
                    int num372 = Dust.NewDust(new Vector2(x * 16, y * 16), 8, 8, DustID.Torch, 0f, 0f, 100, default, 2.5f);
                    Main.dust[num372].noGravity = true;
                    Main.dust[num372].velocity *= 5f;
                    num372 = Dust.NewDust(new Vector2(x * 16, y * 16), 8, 8, DustID.Torch, 0f, 0f, 100, default, 1.5f);
                    Main.dust[num372].velocity *= 3f;
                }
            }
        }
        SoundEngine.PlaySound(2, i * 16, j * 16, 14);
    }
    
    public static void BCBConvert(int i, int j, int conversionType, int size = 4)
    {
        for (int k = i - size; k <= i + size; k++)
        {
            for (int l = j - size; l <= j + size; l++)
            {
                if (!WorldGen.InWorld(k, l, 1))
                {
                    continue;
                }
                int type = Main.tile[k, l].TileType;
                int wall = Main.tile[k, l].WallType;
                switch (conversionType)
                {
                    #region crimson (4)
                    case 4: // crimson
                        if (WallID.Sets.Conversion.Grass[wall] && wall != 81)
                        {
                            Main.tile[k, l].WallType = 81;
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (WallID.Sets.Conversion.Stone[wall] && wall != 83)
                        {
                            Main.tile[k, l].WallType = 83;
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (WallID.Sets.Conversion.HardenedSand[wall] && wall != 218)
                        {
                            Main.tile[k, l].WallType = 218;
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (WallID.Sets.Conversion.Sandstone[wall] && wall != 221)
                        {
                            Main.tile[k, l].WallType = 221;
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        if ((Main.tileMoss[type] || TileID.Sets.Conversion.Stone[type]) && type != 203)
                        {
                            Main.tile[k, l].TileType = 203;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Grass[type] && type != 199)
                        {
                            Main.tile[k, l].TileType = 199;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Ice[type] && type != 200)
                        {
                            Main.tile[k, l].TileType = 200;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Sand[type] && type != 234)
                        {
                            Main.tile[k, l].TileType = 234;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.HardenedSand[type] && type != 399)
                        {
                            Main.tile[k, l].TileType = 399;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Sandstone[type] && type != 401)
                        {
                            Main.tile[k, l].TileType = 401;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Thorn[type] && type != 352)
                        {
                            Main.tile[k, l].TileType = 352;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        if (type == 59 && (Main.tile[k - 1, l].TileType == 199 || Main.tile[k + 1, l].TileType == 199 || Main.tile[k, l - 1].TileType == 199 || Main.tile[k, l + 1].TileType == 199))
                        {
                            Main.tile[k, l].TileType = 0;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        continue;
                    #endregion crimson
                    #region hallow (2)
                    case 2:
                        if (WallID.Sets.Conversion.Grass[wall] && wall != 70)
                        {
                            Main.tile[k, l].WallType = 70;
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (WallID.Sets.Conversion.Stone[wall] && wall != 28)
                        {
                            Main.tile[k, l].WallType = 28;
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (WallID.Sets.Conversion.HardenedSand[wall] && wall != 219)
                        {
                            Main.tile[k, l].WallType = 219;
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (WallID.Sets.Conversion.Sandstone[wall] && wall != 222)
                        {
                            Main.tile[k, l].WallType = 222;
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        if ((Main.tileMoss[type] || TileID.Sets.Conversion.Stone[type]) && type != 117)
                        {
                            Main.tile[k, l].TileType = 117;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Grass[type] && type != 109)
                        {
                            Main.tile[k, l].TileType = 109;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Ice[type] && type != 164)
                        {
                            Main.tile[k, l].TileType = 164;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Sand[type] && type != 116)
                        {
                            Main.tile[k, l].TileType = 116;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.HardenedSand[type] && type != 402)
                        {
                            Main.tile[k, l].TileType = 402;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Sandstone[type] && type != 403)
                        {
                            Main.tile[k, l].TileType = 403;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Thorn[type])
                        {
                            WorldGen.KillTile(k, l);
                            if (Main.netMode == 1)
                            {
                                NetMessage.SendData(17, -1, -1, null, 0, k, l);
                            }
                        }
                        if (type == 59 && (Main.tile[k - 1, l].TileType == 109 || Main.tile[k + 1, l].TileType == 109 || Main.tile[k, l - 1].TileType == 109 || Main.tile[k, l + 1].TileType == 109))
                        {
                            Main.tile[k, l].TileType = 0;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        continue;
                    #endregion hallow
                    #region corruption (1)
                    case 1:
                        if (WallID.Sets.Conversion.Grass[wall] && wall != 69)
                        {
                            Main.tile[k, l].WallType = 69;
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (WallID.Sets.Conversion.Stone[wall] && wall != 3)
                        {
                            Main.tile[k, l].WallType = 3;
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (WallID.Sets.Conversion.HardenedSand[wall] && wall != 217)
                        {
                            Main.tile[k, l].WallType = 217;
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (WallID.Sets.Conversion.Sandstone[wall] && wall != 220)
                        {
                            Main.tile[k, l].WallType = 220;
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        if ((Main.tileMoss[type] || TileID.Sets.Conversion.Stone[type]) && type != 25)
                        {
                            Main.tile[k, l].TileType = 25;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Grass[type] && type != 23)
                        {
                            Main.tile[k, l].TileType = 23;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Ice[type] && type != 163)
                        {
                            Main.tile[k, l].TileType = 163;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Sand[type] && type != 112)
                        {
                            Main.tile[k, l].TileType = 112;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.HardenedSand[type] && type != 398)
                        {
                            Main.tile[k, l].TileType = 398;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Sandstone[type] && type != 400)
                        {
                            Main.tile[k, l].TileType = 400;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Thorn[type] && type != 32)
                        {
                            Main.tile[k, l].TileType = 32;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        if (type == 59 && (Main.tile[k - 1, l].TileType == 23 || Main.tile[k + 1, l].TileType == 23 || Main.tile[k, l - 1].TileType == 23 || Main.tile[k, l + 1].TileType == 23))
                        {
                            Main.tile[k, l].TileType = 0;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        continue;
                    #endregion corruption
                    #region mushrooms (3)
                    case 3:
                        if (Main.tile[k, l].WallType == 64 || Main.tile[k, l].WallType == 15)
                        {
                            Main.tile[k, l].WallType = 80;
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 3);
                        }
                        if (Main.tile[k, l].TileType == 60)
                        {
                            Main.tile[k, l].TileType = 70;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 3);
                        }
                        else if (TileID.Sets.Conversion.Thorn[type])
                        {
                            WorldGen.KillTile(k, l);
                            if (Main.netMode == 1)
                            {
                                NetMessage.SendData(17, -1, -1, null, 0, k, l);
                            }
                        }
                        continue;
                    #endregion mushrooms
                    #region jungle (5)
                    case 5:
                        if (WallID.Sets.Conversion.Grass[wall] && wall != WallID.JungleUnsafe2)
                        {
                            Main.tile[k, l].WallType = WallID.JungleUnsafe2;
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (WallID.Sets.Conversion.Stone[wall] && wall != WallID.MudUnsafe)
                        {
                            Main.tile[k, l].WallType = WallID.MudUnsafe;
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (WallID.Sets.Conversion.HardenedSand[wall] && wall != WallID.JungleUnsafe1)
                        {
                            Main.tile[k, l].WallType = WallID.JungleUnsafe1;
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (WallID.Sets.Conversion.Sandstone[wall] && wall != WallID.JungleUnsafe)
                        {
                            Main.tile[k, l].WallType = WallID.JungleUnsafe;
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        if ((Main.tileMoss[type] || TileID.Sets.Conversion.Stone[type]) && type != 1)
                        {
                            Main.tile[k, l].TileType = 1;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == 0 || type == ModContent.TileType<TropicalMud>())
                        {
                            Main.tile[k, l].TileType = 59;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Grass[type] && type != 60)
                        {
                            Main.tile[k, l].TileType = 60;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Ice[type] && type != (ushort)ModContent.TileType<GreenIce>())
                        {
                            Main.tile[k, l].TileType = (ushort)ModContent.TileType<GreenIce>();
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Sand[type] && type != 53)
                        {
                            Main.tile[k, l].TileType = 53;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.HardenedSand[type] && type != TileID.HardenedSand)
                        {
                            Main.tile[k, l].TileType = TileID.HardenedSand;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Sandstone[type] && type != TileID.Sandstone)
                        {
                            Main.tile[k, l].TileType = TileID.Sandstone;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Thorn[type] && type != TileID.JungleThorns)
                        {
                            Main.tile[k, l].TileType = TileID.JungleThorns;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        continue;
                    #endregion jungle
                    #region contagion (6)
                    case 6:
                        if (WallID.Sets.Conversion.Grass[wall] && wall != ModContent.WallType<Walls.ContagionGrassWall>())
                        {
                            Main.tile[k, l].WallType = (ushort)ModContent.WallType<Walls.ContagionGrassWall>();
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (WallID.Sets.Conversion.Stone[wall] && wall != ModContent.WallType<Walls.ContagionNaturalWall1>())
                        {
                            Main.tile[k, l].WallType = (ushort)ModContent.WallType<Walls.ContagionNaturalWall1>();
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (WallID.Sets.Conversion.HardenedSand[wall] && wall != ModContent.WallType<Walls.ContagionNaturalWall1>())
                        {
                            Main.tile[k, l].WallType = (ushort)ModContent.WallType<Walls.ContagionNaturalWall1>();
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (WallID.Sets.Conversion.Sandstone[wall] && wall != ModContent.WallType<Walls.ContagionNaturalWall2>())
                        {
                            Main.tile[k, l].WallType = (ushort)ModContent.WallType<Walls.ContagionNaturalWall2>();
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        if ((Main.tileMoss[type] || TileID.Sets.Conversion.Stone[type]) && type != ModContent.TileType<Chunkstone>())
                        {
                            Main.tile[k, l].TileType = (ushort)ModContent.TileType<Chunkstone>();
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Grass[type] && type != ModContent.TileType<Ickgrass>())
                        {
                            Main.tile[k, l].TileType = (ushort)ModContent.TileType<Ickgrass>();
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Ice[type] && type != (ushort)ModContent.TileType<YellowIce>())
                        {
                            Main.tile[k, l].TileType = (ushort)ModContent.TileType<YellowIce>();
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Sand[type] && type != ModContent.TileType<Snotsand>())
                        {
                            Main.tile[k, l].TileType = (ushort)ModContent.TileType<Snotsand>();
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.HardenedSand[type] && type != ModContent.TileType<HardenedSnotsand>())
                        {
                            Main.tile[k, l].TileType = (ushort)ModContent.TileType<HardenedSnotsand>();
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Sandstone[type] && type != ModContent.TileType<Snotsandstone>())
                        {
                            Main.tile[k, l].TileType = (ushort)ModContent.TileType<Snotsandstone>();
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        continue;
                    #endregion contagion
                    #region tropics (7)
                    case 7:
                        if (WallID.Sets.Conversion.Grass[wall] && wall != ModContent.WallType<Walls.TropicalGrassWall>())
                        {
                            Main.tile[k, l].WallType = (ushort)ModContent.WallType<Walls.TropicalGrassWall>();
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (WallID.Sets.Conversion.Stone[wall] && wall != ModContent.WallType<Walls.TropicalMudWall>())
                        {
                            Main.tile[k, l].WallType = (ushort)ModContent.WallType<Walls.TropicalMudWall>();
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (WallID.Sets.Conversion.HardenedSand[wall] && wall != ModContent.WallType<Walls.TropicalMudWall>())
                        {
                            Main.tile[k, l].WallType = (ushort)ModContent.WallType<Walls.TropicalMudWall>();
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (WallID.Sets.Conversion.Sandstone[wall] && wall != ModContent.WallType<Walls.TropicalMudWall>())
                        {
                            Main.tile[k, l].WallType = (ushort)ModContent.WallType<Walls.TropicalMudWall>();
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        if ((Main.tileMoss[type] || TileID.Sets.Conversion.Stone[type]) && type != TileID.Stone)
                        {
                            Main.tile[k, l].TileType = TileID.Stone;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == 0 || type == 59)
                        {
                            Main.tile[k, l].TileType = (ushort)ModContent.TileType<TropicalMud>();
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Grass[type] && type != ModContent.TileType<TropicalGrass>())
                        {
                            Main.tile[k, l].TileType = (ushort)ModContent.TileType<TropicalGrass>();
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Ice[type] && type != (ushort)ModContent.TileType<BrownIce>())
                        {
                            Main.tile[k, l].TileType = (ushort)ModContent.TileType<BrownIce>();
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Sand[type] && type != TileID.Sand)
                        {
                            Main.tile[k, l].TileType = TileID.Sand;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.HardenedSand[type] && type != TileID.HardenedSand)
                        {
                            Main.tile[k, l].TileType = TileID.HardenedSand;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (TileID.Sets.Conversion.Sandstone[type] && type != TileID.Sandstone)
                        {
                            Main.tile[k, l].TileType = TileID.Sandstone;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        continue;
                    #endregion tropics
                }
                if (Main.tile[k, l].WallType == 69 || Main.tile[k, l].WallType == 70 || Main.tile[k, l].WallType == 81)
                {
                    if ((double)l < Main.worldSurface)
                    {
                        if (WorldGen.genRand.Next(10) == 0)
                        {
                            Main.tile[k, l].WallType = 65;
                        }
                        else
                        {
                            Main.tile[k, l].WallType = 63;
                        }
                    }
                    else
                    {
                        Main.tile[k, l].WallType = 64;
                    }
                    WorldGen.SquareWallFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                else if (Main.tile[k, l].WallType == 3 || Main.tile[k, l].WallType == 28 || Main.tile[k, l].WallType == 83)
                {
                    Main.tile[k, l].WallType = 1;
                    WorldGen.SquareWallFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                else if (Main.tile[k, l].WallType == 80)
                {
                    if ((double)l < Main.worldSurface + 4.0 + (double)WorldGen.genRand.Next(3) || (double)l > ((double)Main.maxTilesY + Main.rockLayer) / 2.0 - 3.0 + (double)WorldGen.genRand.Next(3))
                    {
                        Main.tile[k, l].WallType = 15;
                        WorldGen.SquareWallFrame(k, l);
                        NetMessage.SendTileSquare(-1, k, l, 3);
                    }
                    else
                    {
                        Main.tile[k, l].WallType = 64;
                        WorldGen.SquareWallFrame(k, l);
                        NetMessage.SendTileSquare(-1, k, l, 3);
                    }
                }
                else if (WallID.Sets.Conversion.HardenedSand[wall] && wall != 216)
                {
                    Main.tile[k, l].WallType = 216;
                    WorldGen.SquareWallFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                else if (WallID.Sets.Conversion.Sandstone[wall] && wall != 187)
                {
                    Main.tile[k, l].WallType = 187;
                    WorldGen.SquareWallFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                if (Main.tile[k, l].TileType == 23 || Main.tile[k, l].TileType == 109 || Main.tile[k, l].TileType == 199)
                {
                    Main.tile[k, l].TileType = 2;
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                else if (Main.tile[k, l].TileType == 117 || Main.tile[k, l].TileType == 25 || Main.tile[k, l].TileType == 203)
                {
                    Main.tile[k, l].TileType = 1;
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                else if (Main.tile[k, l].TileType == 112 || Main.tile[k, l].TileType == 116 || Main.tile[k, l].TileType == 234)
                {
                    Main.tile[k, l].TileType = 53;
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                else if (Main.tile[k, l].TileType == 398 || Main.tile[k, l].TileType == 402 || Main.tile[k, l].TileType == 399)
                {
                    Main.tile[k, l].TileType = 397;
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                else if (Main.tile[k, l].TileType == 400 || Main.tile[k, l].TileType == 403 || Main.tile[k, l].TileType == 401)
                {
                    Main.tile[k, l].TileType = 396;
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                else if (Main.tile[k, l].TileType == 164 || Main.tile[k, l].TileType == 163 || Main.tile[k, l].TileType == 200)
                {
                    Main.tile[k, l].TileType = 161;
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                else if (Main.tile[k, l].TileType == 70)
                {
                    Main.tile[k, l].TileType = 60;
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                else if (Main.tile[k, l].TileType == 32 || Main.tile[k, l].TileType == 352)
                {
                    WorldGen.KillTile(k, l);
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendData(17, -1, -1, null, 0, k, l);
                    }
                }
            }
        }
    }
}
