using System;
using System.Collections.Generic;
using AvalonTesting.Items.Armor;
using AvalonTesting.Items.Material;
using AvalonTesting.Items.Placeable.Seed;
using AvalonTesting.Systems;
using AvalonTesting.Tiles;
using AvalonTesting.Tiles.Ores;
using AvalonTesting.World.Structures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace AvalonTesting;

public class AvalonTestingWorld : ModSystem
{
    public static int WallOfSteel { get; set; } = -1;
    public static int WallOfSteelB { get; set; }
    public static int WallOfSteelF { get; set; }
    public static int WallOfSteelT { get; set; }
    public bool SuperHardmode { get; private set; }

    public static void ChangeRain()
    {
        if (Main.cloudBGActive >= 1f || Main.numClouds > 150.0)
        {
            if (Main.rand.NextBool(3))
            {
                Main.maxRaining = Main.rand.Next(20, 90) * 0.01f;
                return;
            }

            Main.maxRaining = Main.rand.Next(40, 90) * 0.01f;
        }
        else if (Main.numClouds > 100.0)
        {
            if (Main.rand.NextBool(3))
            {
                Main.maxRaining = Main.rand.Next(10, 70) * 0.01f;
                return;
            }

            Main.maxRaining = Main.rand.Next(20, 60) * 0.01f;
        }
        else
        {
            if (Main.rand.NextBool(3))
            {
                Main.maxRaining = Main.rand.Next(5, 40) * 0.01f;
                return;
            }

            Main.maxRaining = Main.rand.Next(5, 30) * 0.01f;
        }
    }

    public static void CheckLargeHerb(int x, int y, int type)
    {
        if (WorldGen.destroyObject)
        {
            return;
        }

        Tile t = Main.tile[x, y];
        int style = t.TileFrameX / 18;
        bool destroy = false;
        int fixedY = y;
        int yFrame = Main.tile[x, y].TileFrameY;
        fixedY -= yFrame / 18;
        if (!WorldGen.SolidTile2(x, fixedY + 3) || !Main.tile[x, fixedY].HasTile ||
            !Main.tile[x, fixedY + 1].HasTile || !Main.tile[x, fixedY + 2].HasTile)
        {
            destroy = true;
        }

        if (destroy)
        {
            WorldGen.destroyObject = true;
            for (int u = fixedY; u < fixedY + 3; u++)
            {
                WorldGen.KillTile(x, u);
            }

            // 469 through 471 are the immature tiles of the large herb; 472 is the mature version
            if (type == (ushort)ModContent.TileType<LargeHerbsStage1>() ||
                type == (ushort)ModContent.TileType<LargeHerbsStage2>() ||
                type == (ushort)ModContent.TileType<LargeHerbsStage3>())
            {
                int item = 0;
                switch (style)
                {
                    case 0:
                        item = ModContent.ItemType<LargeDaybloomSeed>();
                        break;
                    case 1:
                        item = ModContent.ItemType<LargeMoonglowSeed>();
                        break;
                    case 2:
                        item = ModContent.ItemType<LargeBlinkrootSeed>();
                        break;
                    case 3:
                        item = ModContent.ItemType<LargeDeathweedSeed>();
                        break;
                    case 4:
                        item = ModContent.ItemType<LargeWaterleafSeed>();
                        break;
                    case 5:
                        item = ModContent.ItemType<LargeFireblossomSeed>();
                        break;
                    case 6:
                        item = ModContent.ItemType<LargeShiverthornSeed>();
                        break;
                    case 7:
                        item = ModContent.ItemType<LargeBloodberrySeed>();
                        break;
                    case 8:
                        item = ModContent.ItemType<LargeSweetstemSeed>();
                        break;
                    case 9:
                        item = ModContent.ItemType<LargeBarfbushSeed>();
                        break;
                    case 10:
                        item = ModContent.ItemType<LargeHolybirdSeed>();
                        break;
                } // 3710 through 3719 are the seeds

                if (item > 0)
                {
                    Item.NewItem(WorldGen.GetItemSource_FromTileBreak(x, y), x * 16, fixedY * 16, 16, 48, item);
                }
            }
            else
            {
                int item = 0;
                switch (style)
                {
                    case 0:
                        item = ModContent.ItemType<LargeDaybloom>();
                        break;
                    case 1:
                        item = ModContent.ItemType<LargeMoonglow>();
                        break;
                    case 2:
                        item = ModContent.ItemType<LargeBlinkroot>();
                        break;
                    case 3:
                        item = ModContent.ItemType<LargeDeathweed>();
                        break;
                    case 4:
                        item = ModContent.ItemType<LargeWaterleaf>();
                        break;
                    case 5:
                        item = ModContent.ItemType<LargeFireblossom>();
                        break;
                    case 6:
                        item = ModContent.ItemType<LargeShiverthorn>();
                        break;
                    case 7:
                        item = ModContent.ItemType<LargeBloodberry>();
                        break;
                    case 8:
                        item = ModContent.ItemType<LargeSweetstem>();
                        break;
                    case 9:
                        item = ModContent.ItemType<LargeBarfbush>();
                        break;
                    case 10:
                        item = ModContent.ItemType<LargeHolybird>();
                        break;
                }

                if (item > 0)
                {
                    Item.NewItem(WorldGen.GetItemSource_FromTileBreak(x, y), x * 16, fixedY * 16, 16, 48, item);
                }

                // 3700 through 3709 are the large herbs
            }

            WorldGen.destroyObject = false;
        }
    }

    public static void GenerateSHMOres()
    {
        Main.rand ??= new UnifiedRandom((int)DateTime.Now.Ticks);

        // oblivion ore
        for (int a = 0; a < (int)(Main.maxTilesX * Main.maxTilesY * 0.00012); a++)
        {
            int x = Main.rand.Next(100, Main.maxTilesX - 100);
            int y = Main.rand.Next((int)Main.rockLayer, Main.maxTilesY - 200);
            WorldGen.OreRunner(x, y, Main.rand.Next(5, 9), Main.rand.Next(4, 6),
                (ushort)ModContent.TileType<OblivionOre>());
        }

        // opals
        for (int a = 0; a < (int)(Main.maxTilesX * Main.maxTilesY * 0.00012); a++)
        {
            int x = Main.rand.Next(100, Main.maxTilesX - 100);
            int y = Main.rand.Next((int)Main.rockLayer, Main.maxTilesY - 200);
            WorldGen.OreRunner(x, y, Main.rand.Next(4, 7), Main.rand.Next(1, 4), (ushort)ModContent.TileType<Opal>());
        }

        // onyx
        for (int a = 0; a < (int)(Main.maxTilesX * Main.maxTilesY * 0.00012); a++)
        {
            int x = Main.rand.Next(100, Main.maxTilesX - 100);
            int y = Main.rand.Next((int)Main.rockLayer, Main.maxTilesY - 200);
            WorldGen.OreRunner(x, y, Main.rand.Next(4, 7), Main.rand.Next(1, 4), (ushort)ModContent.TileType<Onyx>());
        }

        // kunzite
        for (int a = 0; a < (int)(Main.maxTilesX * Main.maxTilesY * 0.00012); a++)
        {
            int x = Main.rand.Next(100, Main.maxTilesX - 100);
            int y = Main.rand.Next((int)Main.rockLayer, Main.maxTilesY - 200);
            WorldGen.OreRunner(x, y, Main.rand.Next(4, 7), Main.rand.Next(1, 4),
                (ushort)ModContent.TileType<Kunzite>());
        }

        // primordial ore
        for (int a = 0; a < (int)(Main.maxTilesX * Main.maxTilesY * 0.00012); a++)
        {
            int x = Main.rand.Next(100, Main.maxTilesX - 100);
            int y = Main.rand.Next((int)Main.rockLayer, Main.maxTilesY - 200);
            WorldGen.OreRunner(x, y, Main.rand.Next(3, 6), Main.rand.Next(5, 8),
                (ushort)ModContent.TileType<PrimordialOre>());
        }
    }

    public static void GenerateSkyFortress()
    {
        Main.rand ??= new UnifiedRandom((int)DateTime.Now.Ticks);

        if (!ModContent.GetInstance<DownedBossSystem>().DownedArmageddon)
        {
            return;
        }

        int x = Main.maxTilesX / 3;
        int y = 50;
        if (Main.maxTilesY == 1800)
        {
            y = 60;
        }

        if (Main.maxTilesY == 2400)
        {
            y = 70;
        }

        if (Main.rand.NextBool(2))
        {
            x = Main.maxTilesX - (Main.maxTilesX / 3);
        }

        World.Utils.GetSkyFortressXCoord(x, y, 209, 158, ref x);
        SkyFortress.Generate(x, y);
        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            Main.NewText("The fortress of ages has descended into the sky...", 244, 140, 140);
        }
        else if (Main.netMode == NetmodeID.Server)
        {
            ChatHelper.BroadcastChatMessage(
                NetworkText.FromLiteral("The fortress of ages has descended into the sky..."),
                new Color(244, 140, 140));
        }
    }

    public static void GenerateSulphur()
    {
        Main.rand ??= new UnifiedRandom((int)DateTime.Now.Ticks);

        for (int a = 0; a < (int)(Main.maxTilesX * Main.maxTilesY * 0.00012); a++)
        {
            int x = Main.rand.Next(100, Main.maxTilesX - 100);
            int y = Main.rand.Next((int)Main.rockLayer, Main.maxTilesY - 150);
            WorldGen.OreRunner(x, y, Main.rand.Next(7, 9), Main.rand.Next(5, 7),
                (ushort)ModContent.TileType<SulphurOre>());
        }

        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            Main.NewText("The underground smells like rotten eggs...", 210, 183, 4);
        }
        else if (Main.netMode == NetmodeID.Server)
        {
            ChatHelper.BroadcastChatMessage(
                NetworkText.FromLiteral("The underground smells like rotten eggs..."),
                new Color(210, 183, 4));
        }
    }

    public static void StartRain()
    {
        const int num = 86400;
        const int num2 = num / 24;
        Main.rainTime = Main.rand.Next(num2 * 8, num);
        if (Main.rand.NextBool(3))
        {
            Main.rainTime += Main.rand.Next(0, num2);
        }

        if (Main.rand.NextBool(4))
        {
            Main.rainTime += Main.rand.Next(0, num2 * 2);
        }

        if (Main.rand.NextBool(5))
        {
            Main.rainTime += Main.rand.Next(0, num2 * 2);
        }

        if (Main.rand.NextBool(6))
        {
            Main.rainTime += Main.rand.Next(0, num2 * 3);
        }

        if (Main.rand.NextBool(7))
        {
            Main.rainTime += Main.rand.Next(0, num2 * 4);
        }

        if (Main.rand.NextBool(8))
        {
            Main.rainTime += Main.rand.Next(0, num2 * 5);
        }

        float num3 = 1f;
        if (Main.rand.NextBool(2))
        {
            num3 += 0.05f;
        }

        if (Main.rand.NextBool(3))
        {
            num3 += 0.1f;
        }

        if (Main.rand.NextBool(4))
        {
            num3 += 0.15f;
        }

        if (Main.rand.NextBool(5))
        {
            num3 += 0.2f;
        }

        Main.rainTime = (int)(Main.rainTime * num3);
        ChangeRain();
        Main.raining = true;
    }

    public static void StopRain()
    {
        Main.rainTime = 0;
        Main.raining = false;
        Main.maxRaining = 0f;
    }

    public override void PostUpdateEverything()
    {
        SpectrumHelmet.StaticUpdate();
        CoolGemsparkBlock.StaticUpdate();
        WarmGemsparkBlock.StaticUpdate();
    }
    public static void GrowLargeHerb(int x, int y)
    {
        if (Main.tile[x, y].HasTile)
        {
            if (Main.tile[x, y].TileType == (ushort)ModContent.TileType<Tiles.LargeHerbsStage1>() && WorldGen.genRand.NextBool(8)) // phase 1 to 2
            {
                bool grow = false;
                if (Main.tile[x, y].TileFrameX == 108) // shiverthorn check
                {
                    if (Main.rand.NextBool(2))
                    {
                        grow = true;
                    }
                }
                else
                {
                    grow = true;
                }
                if (grow)
                {
                    Main.tile[x, y].TileType = (ushort)ModContent.TileType<Tiles.LargeHerbsStage2>();
                    if (Main.tile[x, y + 1].TileType == (ushort)ModContent.TileType<Tiles.LargeHerbsStage1>())
                    {
                        Main.tile[x, y + 1].TileType = (ushort)ModContent.TileType<Tiles.LargeHerbsStage2>();
                    }

                    if (Main.tile[x, y + 2].TileType == (ushort)ModContent.TileType<Tiles.LargeHerbsStage1>())
                    {
                        Main.tile[x, y + 2].TileType = (ushort)ModContent.TileType<Tiles.LargeHerbsStage2>();
                    }

                    if (Main.tile[x, y - 1].TileType == (ushort)ModContent.TileType<Tiles.LargeHerbsStage1>())
                    {
                        Main.tile[x, y - 1].TileType = (ushort)ModContent.TileType<Tiles.LargeHerbsStage2>();
                    }

                    if (Main.tile[x, y - 2].TileType == (ushort)ModContent.TileType<Tiles.LargeHerbsStage1>())
                    {
                        Main.tile[x, y - 2].TileType = (ushort)ModContent.TileType<Tiles.LargeHerbsStage2>();
                    }

                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendTileSquare(-1, x, y, 2);
                        NetMessage.SendTileSquare(-1, x, y + 1, 2);
                        NetMessage.SendTileSquare(-1, x, y + 2, 2);
                        NetMessage.SendTileSquare(-1, x, y - 1, 2);
                        NetMessage.SendTileSquare(-1, x, y - 2, 2);
                    }
                    World.Utils.SquareTileFrameArea(x, y, 4, true);
                    return;
                }
            }
            else if (Main.tile[x, y].TileType == (ushort)ModContent.TileType<Tiles.LargeHerbsStage2>() && WorldGen.genRand.NextBool(7)) // phase 2 to 3
            {
                bool grow = false;
                if (Main.tile[x, y].TileFrameX == 108) // shiverthorn check
                {
                    if (Main.rand.NextBool(2))
                    {
                        grow = true;
                    }
                }
                else
                {
                    grow = true;
                }
                if (grow)
                {
                    Main.tile[x, y].TileType = (ushort)ModContent.TileType<Tiles.LargeHerbsStage3>();
                    if (Main.tile[x, y + 1].TileType == (ushort)ModContent.TileType<Tiles.LargeHerbsStage2>())
                    {
                        Main.tile[x, y + 1].TileType = (ushort)ModContent.TileType<Tiles.LargeHerbsStage3>();
                    }

                    if (Main.tile[x, y + 2].TileType == (ushort)ModContent.TileType<Tiles.LargeHerbsStage2>())
                    {
                        Main.tile[x, y + 2].TileType = (ushort)ModContent.TileType<Tiles.LargeHerbsStage3>();
                    }

                    if (Main.tile[x, y - 1].TileType == (ushort)ModContent.TileType<Tiles.LargeHerbsStage2>())
                    {
                        Main.tile[x, y - 1].TileType = (ushort)ModContent.TileType<Tiles.LargeHerbsStage3>();
                    }

                    if (Main.tile[x, y - 2].TileType == (ushort)ModContent.TileType<Tiles.LargeHerbsStage2>())
                    {
                        Main.tile[x, y - 2].TileType = (ushort)ModContent.TileType<Tiles.LargeHerbsStage3>();
                    }

                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendTileSquare(-1, x, y, 2);
                        NetMessage.SendTileSquare(-1, x, y + 1, 2);
                        NetMessage.SendTileSquare(-1, x, y + 2, 2);
                        NetMessage.SendTileSquare(-1, x, y - 1, 2);
                        NetMessage.SendTileSquare(-1, x, y - 2, 2);
                    }
                    World.Utils.SquareTileFrameArea(x, y, 4, true);
                    return;
                }
            }
            else if (Main.tile[x, y].TileType == (ushort)ModContent.TileType<Tiles.LargeHerbsStage3>() && WorldGen.genRand.NextBool(5)) // phase 3 to 4
            {
                Main.tile[x, y].TileType = (ushort)ModContent.TileType<Tiles.LargeHerbsStage4>();
                if (Main.tile[x, y + 1].TileType == (ushort)ModContent.TileType<Tiles.LargeHerbsStage3>())
                {
                    Main.tile[x, y + 1].TileType = (ushort)ModContent.TileType<Tiles.LargeHerbsStage4>();
                }

                if (Main.tile[x, y + 2].TileType == (ushort)ModContent.TileType<Tiles.LargeHerbsStage3>())
                {
                    Main.tile[x, y + 2].TileType = (ushort)ModContent.TileType<Tiles.LargeHerbsStage4>();
                }

                if (Main.tile[x, y - 1].TileType == (ushort)ModContent.TileType<Tiles.LargeHerbsStage3>())
                {
                    Main.tile[x, y - 1].TileType = (ushort)ModContent.TileType<Tiles.LargeHerbsStage4>();
                }

                if (Main.tile[x, y - 2].TileType == (ushort)ModContent.TileType<Tiles.LargeHerbsStage3>())
                {
                    Main.tile[x, y - 2].TileType = (ushort)ModContent.TileType<Tiles.LargeHerbsStage4>();
                }

                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendTileSquare(-1, x, y, 2);
                    NetMessage.SendTileSquare(-1, x, y + 1, 2);
                    NetMessage.SendTileSquare(-1, x, y + 2, 2);
                    NetMessage.SendTileSquare(-1, x, y - 1, 2);
                    NetMessage.SendTileSquare(-1, x, y - 2, 2);
                }
                World.Utils.SquareTileFrameArea(x, y, 4, true);
                return;
            }
        }
    }
    public override void PostUpdateWorld()
    {
        float num2 = 3E-05f * (float)WorldGen.GetWorldUpdateRate();

        // float num3 = 1.5E-05f * (float)Main.worldRate;
        for (int num4 = 0; num4 < Main.maxTilesX * Main.maxTilesY * num2; num4++)
        {
            int num5 = WorldGen.genRand.Next(10, Main.maxTilesX - 10);
            int num6 = WorldGen.genRand.Next(10, /*(int)Main.worldSurface - 1*/ Main.maxTilesY - 20);
            int num7 = num5 - 1;
            int num8 = num5 + 2;
            int num9 = num6 - 1;
            int num10 = num6 + 2;
            if (num7 < 10)
            {
                num7 = 10;
            }

            if (num8 > Main.maxTilesX - 10)
            {
                num8 = Main.maxTilesX - 10;
            }

            if (num9 < 10)
            {
                num9 = 10;
            }

            if (num10 > Main.maxTilesY - 10)
            {
                num10 = Main.maxTilesY - 10;
            }

            #region large herb growth
            if (Main.tile[num5, num6].TileType == (ushort)ModContent.TileType<LargeHerbsStage1>() || Main.tile[num5, num6].TileType == (ushort)ModContent.TileType<LargeHerbsStage2>() ||
                        Main.tile[num5, num6].TileType == (ushort)ModContent.TileType<LargeHerbsStage3>())
            {
                GrowLargeHerb(num5, num6);
            }
            #endregion large herb growth

            #region holybird spawning
            if (Main.tile[num5, num6].TileType == TileID.HallowedGrass || Main.tile[num5, num6].TileType == TileID.Pearlstone)
            {
                int num14 = Main.tile[num5, num6].TileType;
                if (!Main.tile[num5, num9].HasTile && Main.tile[num5, num9].LiquidAmount == 0 && !Main.tile[num5, num6].IsHalfBlock && Main.tile[num5, num6].Slope == SlopeType.Solid && WorldGen.genRand.NextBool(num6 > Main.worldSurface ? 600 : 250) && (num14 == TileID.HallowedGrass || num14 == TileID.Pearlstone))
                {
                    WorldGen.PlaceTile(num5, num9, ModContent.TileType<Tiles.Herbs.Holybird>(), true, false, -1, 0);
                    if (Main.tile[num5, num9].HasTile)
                    {
                        Tile t = Main.tile[num5, num9];
                        t.TileColor = Main.tile[num5, num6].TileColor;
                    }
                    if (Main.netMode == NetmodeID.Server && Main.tile[num5, num9].HasTile)
                    {
                        NetMessage.SendTileSquare(-1, num5, num9, 1);
                    }
                }
            }
            #endregion holybird spawning

            #region sweetstem spawning
            if (Main.tile[num5, num6].TileType == ModContent.TileType<Nest>() || Main.tile[num5, num6].TileType == TileID.Hive)
            {
                int num14 = Main.tile[num5, num6].TileType;
                if (!Main.tile[num5, num9].HasTile && !Main.tile[num5, num6].IsHalfBlock && Main.tile[num5, num6].Slope == SlopeType.Solid && WorldGen.genRand.NextBool(700) && (num14 == ModContent.TileType<Nest>() || num14 == TileID.Hive))
                {
                    WorldGen.PlaceTile(num5, num9, ModContent.TileType<Tiles.Herbs.Sweetstem>(), true, false, -1, 0);
                    if (Main.tile[num5, num9].HasTile)
                    {
                        Tile t = Main.tile[num5, num9];
                        t.TileColor = Main.tile[num5, num6].TileColor;
                    }
                    if (Main.netMode == NetmodeID.Server && Main.tile[num5, num9].HasTile)
                    {
                        NetMessage.SendTileSquare(-1, num5, num9, 1);
                    }
                }
            }
            #endregion sweetstem spawning

            #region bloodberry spawning
            if (Main.tile[num5, num6].TileType == TileID.CrimsonGrass || Main.tile[num5, num6].TileType == TileID.Crimstone)
            {
                int num14 = Main.tile[num5, num6].TileType;
                if (!Main.tile[num5, num9].HasTile && Main.tile[num5, num9].LiquidAmount == 0 && !Main.tile[num5, num6].IsHalfBlock && Main.tile[num5, num6].Slope == SlopeType.Solid && WorldGen.genRand.NextBool(num6 > Main.worldSurface ? 500 : 200) && (num14 == TileID.CrimsonGrass || num14 == TileID.Crimstone))
                {
                    WorldGen.PlaceTile(num5, num9, ModContent.TileType<Tiles.Herbs.Bloodberry>(), true, false, -1, 0);
                    if (Main.tile[num5, num9].HasTile)
                    {
                        Tile t = Main.tile[num5, num9];
                        t.TileColor = Main.tile[num5, num6].TileColor;
                    }
                    if (Main.netMode == NetmodeID.Server && Main.tile[num5, num9].HasTile)
                    {
                        NetMessage.SendTileSquare(-1, num5, num9, 1);
                    }
                }
            }
            #endregion bloodberry spawning

            #region killing things if the block above/below isn't the necessary type
            // kill contagion vines if block above isn't contagion grass
            if (Main.tile[num5, num9].TileType == TileID.Dirt && Main.tile[num5, num6].TileType == ModContent.TileType<ContagionVines>())
            {
                WorldGen.KillTile(num5, num6);
            }
            // kill contagion short grass if block below isn't contagion grass
            if (Main.tile[num5, num6].TileType == TileID.Dirt && Main.tile[num5, num9].TileType == ModContent.TileType<ContagionShortGrass>())
            {
                WorldGen.KillTile(num5, num6);
            }
            // kill barfbush if block below isn't contagion grass
            if (Main.tile[num5, num6].TileType == TileID.Dirt && Main.tile[num5, num9].TileType == ModContent.TileType<Tiles.Herbs.Barfbush>())
            {
                WorldGen.KillTile(num5, num6);
            }
            // kill bloodberry if block below isn't crimson grass
            if (Main.tile[num5, num6].TileType == TileID.Dirt && Main.tile[num5, num9].TileType == ModContent.TileType<Tiles.Herbs.Bloodberry>())
            {
                WorldGen.KillTile(num5, num6);
            }
            // kill holybird if block below isn't hallowed grass
            if (Main.tile[num5, num6].TileType == TileID.Dirt && Main.tile[num5, num9].TileType == ModContent.TileType<Tiles.Herbs.Holybird>())
            {
                WorldGen.KillTile(num5, num6);
            }
            #endregion

            #region contagion shortgrass/barfbush spawning
            if (Main.tile[num5, num6].TileType == ModContent.TileType<Ickgrass>())
            {
                int num14 = Main.tile[num5, num6].TileType;
                if (!Main.tile[num5, num9].HasTile && Main.tile[num5, num9].LiquidAmount == 0 && !Main.tile[num5, num6].IsHalfBlock && Main.tile[num5, num6].Slope == SlopeType.Solid && WorldGen.genRand.NextBool(5) && num14 == ModContent.TileType<Ickgrass>())
                {
                    WorldGen.PlaceTile(num5, num9, ModContent.TileType<ContagionShortGrass>(), true, false, -1, 0);
                    Main.tile[num5, num9].TileFrameX = (short)(WorldGen.genRand.Next(0, 11) * 18);
                    if (Main.tile[num5, num9].HasTile)
                    {
                        Tile t = Main.tile[num5, num9];
                        t.TileColor = Main.tile[num5, num6].TileColor;
                    }
                    if (Main.netMode == NetmodeID.Server && Main.tile[num5, num9].HasTile)
                    {
                        NetMessage.SendTileSquare(-1, num5, num9, 1);
                    }
                }
                if (!Main.tile[num5, num9].HasTile && Main.tile[num5, num9].LiquidAmount == 0 && !Main.tile[num5, num6].IsHalfBlock && Main.tile[num5, num6].Slope == SlopeType.Solid && WorldGen.genRand.NextBool(num6 > Main.worldSurface ? 500 : 200) && num14 == ModContent.TileType<Ickgrass>())
                {
                    WorldGen.PlaceTile(num5, num9, ModContent.TileType<Tiles.Herbs.Barfbush>(), true, false, -1, 0);
                    if (Main.tile[num5, num9].HasTile)
                    {
                        Tile t = Main.tile[num5, num9];
                        t.TileColor = Main.tile[num5, num6].TileColor;
                    }
                    if (Main.netMode == NetmodeID.Server && Main.tile[num5, num9].HasTile)
                    {
                        NetMessage.SendTileSquare(-1, num5, num9, 1);
                    }
                }
                bool flag2 = false;
                for (int m = num7; m < num8; m++)
                {
                    for (int n = num9; n < num10; n++)
                    {
                        if ((num5 != m || num6 != n) && Main.tile[m, n].HasTile)
                        {
                            if (Main.tile[m, n].TileType == 0 || (num14 == ModContent.TileType<Ickgrass>() && Main.tile[m, n].TileType == TileID.Grass))
                            {
                                WorldGen.SpreadGrass(m, n, 0, num14, false, Main.tile[num5, num6].TileColor);
                                if (num14 == ModContent.TileType<Ickgrass>())
                                {
                                    WorldGen.SpreadGrass(m, n, TileID.Grass, num14, false, Main.tile[num5, num6].TileColor);
                                }
                                if (num14 == ModContent.TileType<Ickgrass>())
                                {
                                    WorldGen.SpreadGrass(m, n, TileID.HallowedGrass, num14, false, Main.tile[num5, num6].TileColor);
                                }
                                if (Main.tile[m, n].TileType == num14)
                                {
                                    WorldGen.SquareTileFrame(m, n, true);
                                    flag2 = true;
                                }
                            }
                            if (Main.tile[m, n].TileType == 0 || (num14 == 109 && Main.tile[m, n].TileType == 2) || (num14 == 109 && Main.tile[m, n].TileType == 23) || (num14 == 109 && Main.tile[m, n].TileType == 199))
                            {
                                if (num14 == TileID.HallowedGrass)
                                {
                                    WorldGen.SpreadGrass(m, n, ModContent.TileType<Ickgrass>(), num14, false, Main.tile[num5, num6].TileColor);
                                }
                            }
                        }
                    }
                }
                if (Main.netMode == NetmodeID.Server && flag2)
                {
                    NetMessage.SendTileSquare(-1, num5, num6, 3);
                }
            }

            #endregion contagion shortgrass/barfbush spawning
            // Lazite grass
            if (Main.tile[num5, num6].TileType == ModContent.TileType<LaziteGrass>())
            {
                int num14 = Main.tile[num5, num6].TileType;

                // where lazite tallgrass would grow
                if (!Main.tile[num5, num9].HasTile && Main.tile[num5, num9].LiquidAmount == 0 &&
                    !Main.tile[num5, num6].IsHalfBlock && Main.tile[num5, num6].Slope == SlopeType.Solid &&
                    WorldGen.genRand.NextBool(5) && num14 == ModContent.TileType<LaziteGrass>())
                {
                    WorldGen.PlaceTile(num5, num9, ModContent.TileType<LaziteShortGrass>(), true);
                    Main.tile[num5, num9].TileFrameX = (short)(WorldGen.genRand.Next(0, 10) * 18);
                    if (Main.tile[num5, num9].HasTile)
                    {
                        Tile t = Main.tile[num5, num9];
                        t.TileColor = Main.tile[num5, num6].TileColor;
                    }

                    if (Main.netMode == NetmodeID.Server && Main.tile[num5, num9].HasTile)
                    {
                        NetMessage.SendTileSquare(-1, num5, num9, 1);
                    }
                }

                bool flag2 = false;
                for (int m = num7; m < num8; m++)
                {
                    for (int n = num9; n < num10; n++)
                    {
                        if ((num5 != m || num6 != n) && Main.tile[m, n].HasTile)
                        {
                            if (Main.tile[m, n].TileType == ModContent.TileType<BlastedStone>())
                            {
                                WorldGen.SpreadGrass(m, n, ModContent.TileType<BlastedStone>(),
                                    ModContent.TileType<LaziteGrass>(), false, Main.tile[num5, num6].TileColor);
                            }

                            if (Main.tile[m, n].TileType == num14)
                            {
                                WorldGen.SquareTileFrame(m, n);
                                flag2 = true;
                            }
                        }
                    }
                }

                if (Main.netMode == NetmodeID.Server && flag2)
                {
                    NetMessage.SendTileSquare(-1, num5, num6, 3);
                }
            }

            // impgrass growing
            if (Main.tile[num5, num6].TileType == ModContent.TileType<Impgrass>())
            {
                int num14 = Main.tile[num5, num6].TileType;
                bool flag2 = false;
                for (int m = num7; m < num8; m++)
                {
                    for (int n = num9; n < num10; n++)
                    {
                        if ((num5 != m || num6 != n) && Main.tile[m, n].HasTile)
                        {
                            if (Main.tile[m, n].TileType == TileID.Ash)
                            {
                                WorldGen.SpreadGrass(m, n, TileID.Ash, ModContent.TileType<Impgrass>(), false,
                                    Main.tile[num5, num6].TileColor);
                            }

                            if (Main.tile[m, n].TileType == num14)
                            {
                                WorldGen.SquareTileFrame(m, n);
                                flag2 = true;
                            }
                        }
                    }
                }

                if (Main.netMode == NetmodeID.Server && flag2)
                {
                    NetMessage.SendTileSquare(-1, num5, num6, 3);
                }
            }

            // impvines growing
            if ((Main.tile[num5, num6].TileType == ModContent.TileType<Impgrass>() ||
                 Main.tile[num5, num6].TileType == ModContent.TileType<Impvines>()) &&
                WorldGen.genRand.NextBool(15) && !Main.tile[num5, num6 + 1].HasTile && // change back to NextBool(15)
                Main.tile[num5, num6 + 1].LiquidType != LiquidID.Lava)
            {
                bool flag10 = false;
                for (int num47 = num6; num47 > num6 - 10; num47--)
                {
                    if (Main.tile[num5, num47].BottomSlope)
                    {
                        flag10 = false;
                        break;
                    }

                    if (Main.tile[num5, num47].HasTile &&
                        Main.tile[num5, num47].TileType == ModContent.TileType<Impgrass>() &&
                        !Main.tile[num5, num47].BottomSlope)
                    {
                        flag10 = true;
                        break;
                    }
                }

                if (flag10)
                {
                    int num48 = num5;
                    int num49 = num6 + 1;
                    Main.tile[num48, num49].TileType = (ushort)ModContent.TileType<Impvines>();

                    Tile t = Main.tile[num48, num49];
                    t.HasTile = true;
                    WorldGen.SquareTileFrame(num48, num49);
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendTileSquare(-1, num48, num49, 3);
                    }
                }
            }
        }
    }
    public override void PreWorldGen()
    {
        SuperHardmode = false;
        ModContent.GetInstance<DownedBossSystem>().DownedArmageddon = false;
        ModContent.GetInstance<DownedBossSystem>().DownedBacteriumPrime = false;
        ModContent.GetInstance<DownedBossSystem>().DownedDesertBeak = false;
        ModContent.GetInstance<DownedBossSystem>().DownedDragonLord = false;
        ModContent.GetInstance<DownedBossSystem>().DownedKingSting = false;
        ModContent.GetInstance<DownedBossSystem>().DownedMechasting = false;
        ModContent.GetInstance<DownedBossSystem>().DownedOblivion = false;
        ModContent.GetInstance<DownedBossSystem>().DownedPhantasm = false;
    }
    public void InitiateSuperHardMode()
    {
        if (SuperHardmode)
        {
            return;
        }

        GenerateSHMOres();
        SuperHardmode = true;
        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            Main.NewText("The ancient souls have been disturbed...", 255, 60, 0);
            Main.NewText("The heavens above are rumbling...", 50, 255, 130);
            Main.NewText("Your world has been blessed with the elements!", 0, 255, 0);
        }
        else if (Main.netMode == NetmodeID.Server)
        {
            ChatHelper.BroadcastChatMessage(
                NetworkText.FromLiteral("The ancient souls have been disturbed..."),
                new Color(255, 60, 0));
            ChatHelper.BroadcastChatMessage(
                NetworkText.FromLiteral("The heavens above are rumbling..."),
                new Color(50, 255, 130));
            ChatHelper.BroadcastChatMessage(
                NetworkText.FromLiteral("Your world has been blessed with the elements!"),
                new Color(0, 255, 0));
        }

        if (Main.netMode == NetmodeID.Server)
        {
            Netplay.ResetSections();
        }
    }
}
