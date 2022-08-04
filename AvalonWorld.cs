using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.WorldBuilding;
using Terraria.ModLoader.IO;
using AltLibrary.Common.Systems;
using System.Threading;
using Avalon.Items.Armor;
using Avalon.Items.Material;
using Avalon.Items.Placeable.Seed;
using Avalon.Logic;
using Avalon.Systems;
using Avalon.Tiles;
using Avalon.Tiles.Ores;
using Avalon.World.Structures;
using Terraria.Graphics.Light;

namespace Avalon;

public class AvalonWorld : ModSystem
{
    public static int WallOfSteel { get; set; } = -1;
    public static int WallOfSteelB { get; set; }
    public static int WallOfSteelF { get; set; }
    public static int WallOfSteelT { get; set; }
    public bool SuperHardmode { get; private set; }
    private static bool caesiumBreak;
    private static bool crackedBrick;



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
    public override void PreUpdateWorld()
    {
        if (Main.time == 16200 && Main.rand.NextBool(4) && NPC.downedMoonlord && SuperHardmode && Main.hardMode)
        {
            DropComet(ModContent.TileType<HydrolythOre>());
        }
        Main.tileSolidTop[ModContent.TileType<FallenStarTile>()] = Main.dayTime;
    }
    public static void GenerateSolarium()
    {
        for (int a = 0; a < (int)(Main.maxTilesX * Main.maxTilesY * 0.00008); a++)
        {
            int x = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
            int y = WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200);
            WorldGen.OreRunner(x, y, WorldGen.genRand.Next(4, 7), WorldGen.genRand.Next(4, 8), (ushort)ModContent.TileType<Tiles.Ores.SolariumOre>());
        }
        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            Main.NewText("Your world has been energized with Solarium!", 244, 167, 0);
        }
        else if (Main.netMode == NetmodeID.Server)
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Your world has been energized with Solarium!"), new Color(244, 167, 0));
        }
    }
    public static void GenerateHallowedOre()
    {
        Main.rand ??= new UnifiedRandom((int)DateTime.Now.Ticks);

        double num5 = Main.rockLayer;
        int xloc = -100 + Main.maxTilesX - 100;
        int yloc = -(int)num5 + Main.maxTilesY - 200;
        int sum = xloc * yloc;
        int amount = (sum / 10000) * 10;
        for (int zz = 0; zz < amount; zz++)
        {
            int i2 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
            double num6 = Main.rockLayer;
            int j2 = WorldGen.genRand.Next((int)num6, Main.maxTilesY - 200);
            WorldGen.OreRunner(i2, j2, WorldGen.genRand.Next(WorldGen.genRand.Next(2, 4), WorldGen.genRand.Next(4, 6)), WorldGen.genRand.Next(WorldGen.genRand.Next(3, 5), WorldGen.genRand.Next(4, 8)), (ushort)ModContent.TileType<Tiles.Ores.HallowedOre>());
        }
        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            Main.NewText("Your world has been blessed with Hallowed Ore!", 220, 170, 0);
        }
        else if (Main.netMode == NetmodeID.Server)
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Your world has been blessed with Hallowed Ore!"), new Color(220, 170, 0));
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
            WorldGen.OreRunner(x, y, Main.rand.Next(4, 7), Main.rand.Next(1, 4), (ushort)ModContent.TileType<Tiles.Ores.Opal>());
        }

        // onyx
        for (int a = 0; a < (int)(Main.maxTilesX * Main.maxTilesY * 0.00012); a++)
        {
            int x = Main.rand.Next(100, Main.maxTilesX - 100);
            int y = Main.rand.Next((int)Main.rockLayer, Main.maxTilesY - 200);
            WorldGen.OreRunner(x, y, Main.rand.Next(4, 7), Main.rand.Next(1, 4), (ushort)ModContent.TileType<Tiles.Ores.Onyx>());
        }

        // kunzite
        for (int a = 0; a < (int)(Main.maxTilesX * Main.maxTilesY * 0.00012); a++)
        {
            int x = Main.rand.Next(100, Main.maxTilesX - 100);
            int y = Main.rand.Next((int)Main.rockLayer, Main.maxTilesY - 200);
            WorldGen.OreRunner(x, y, Main.rand.Next(4, 7), Main.rand.Next(1, 4),
                (ushort)ModContent.TileType<Tiles.Ores.Kunzite>());
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
    public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
    {
        if (Main.LocalPlayer.GetModPlayer<Players.ExxoBiomePlayer>().ZoneContagion)
        {
            //backgroundColor = new Color(95, 140, 108);
            //Main.ColorOfTheSkies = new Color(97, 122, 87);
        }
        if (Main.LocalPlayer.GetModPlayer<Players.ExxoBiomePlayer>().ZoneDarkMatter || Main.LocalPlayer.GetModPlayer<Players.ExxoPlayer>().DarkMatterMonolith)
        {
            backgroundColor = new Color(126, 71, 107) * 0.2f;
        }
    }
    public override void ModifyLightingBrightness(ref float scale)
    {
        if (Main.LocalPlayer.GetModPlayer<Players.ExxoBiomePlayer>().ZoneDarkMatter || Main.LocalPlayer.GetModPlayer<Players.ExxoPlayer>().DarkMatterMonolith)
        {
            scale = 0.8f;
        }
    }
    public override void SaveWorldData(TagCompound tag)
    {
        tag["SuperHardmode"] = SuperHardmode;
    }
    public override void LoadWorldData(TagCompound tag)
    {
        if (tag.ContainsKey("SuperHardmode"))
        {
            SuperHardmode = tag.Get<bool>("SuperHardmode");
        }
    }
    public override void NetSend(BinaryWriter writer)
    {
        var flags = new BitsByte
        {
            [0] = SuperHardmode
        };
        writer.Write(flags);
    }
    public override void NetReceive(BinaryReader reader)
    {
        BitsByte flags = reader.ReadByte();
        SuperHardmode = flags[0];
    }
    public static void AttemptCaesiumOreShattering(int i, int j, Tile tileCache, bool fail)
    {
        if (tileCache.TileType != ModContent.TileType<CaesiumOre>() || Main.netMode == NetmodeID.MultiplayerClient || caesiumBreak || j < Main.maxTilesY - 200)
        {
            return;
        }
        caesiumBreak = true;
        for (int k = i - 1; k <= i + 1; k++)
        {
            for (int l = j - 1; l <= j + 1; l++)
            {
                int maxValue = 15;
                if (!WorldGen.SolidTile(k, l + 1))
                {
                    maxValue = 4;
                }
                else if (k == i && l == j - 1 && !fail)
                {
                    maxValue = 4;
                }
                if ((k != i || l != j) && Main.tile[k, l].HasTile && Main.tile[k, l].TileType == ModContent.TileType<CaesiumOre>() && WorldGen.genRand.NextBool(maxValue))
                {
                    WorldGen.KillTile(k, l, fail: false, effectOnly: false, noItem: true);
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, k, l);
                    }
                }
            }
        }
        caesiumBreak = false;
    }
    public static void ShatterCrackedBricks(int i, int j, Tile tileCache, bool fail)
    {
        if (tileCache.TileType != ModContent.TileType<CrackedOrangeBrick>() && tileCache.TileType != ModContent.TileType<CrackedPurpleBrick>() || Main.netMode == NetmodeID.MultiplayerClient || crackedBrick || j < Main.maxTilesY - 200)
        {
            return;
        }
        crackedBrick = true;
        for (int k = i - 4; k <= i + 4; k++)
        {
            for (int l = j - 4; l <= j + 4; l++)
            {
                int maxValue = 15;
                if (!WorldGen.SolidTile(k, l + 1))
                {
                    maxValue = 4;
                }
                else if (k == i && l == j - 1 && !fail)
                {
                    maxValue = 4;
                }
                if ((k != i || l != j) && Main.tile[k, l].HasTile && (Main.tile[k, l].TileType == ModContent.TileType<CrackedOrangeBrick>() || Main.tile[k, l].TileType == ModContent.TileType<CrackedPurpleBrick>()) && WorldGen.genRand.NextBool(maxValue))
                {
                    WorldGen.KillTile(k, l, fail: false, effectOnly: false, noItem: true);
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, k, l);
                    }
                }
            }
        }
        crackedBrick = false;
    }
    public static void GenerateSkyFortress()
    {
        Main.rand ??= new UnifiedRandom((int)DateTime.Now.Ticks);

        if (!ModContent.GetInstance<DownedBossSystem>().DownedArmageddon)
        {
            return;
        }

        int x = Main.maxTilesX / 3;
        int y = 41;
        if (Main.maxTilesY == 1800)
        {
            y = 51;
        }

        if (Main.maxTilesY == 2400)
        {
            y = 61;
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
    public void GenerateCrystalMines()
    {
        ThreadPool.QueueUserWorkItem(new WaitCallback(CrystalMinesCallback), 1);
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
    public void CrystalMinesCallback(object threadContext)
    {
        if (!SuperHardmode)
            return;
        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            Main.NewText("The otherworldly crystals begin to grow...", 176, 153, 214); // [c/7BBAE4:The ot][c/90ABDD:herwo][c/A3A0D9:rldly] [c/B099D6:cryst][c/BA92D4:als] [c/BA92D4:be][c/C88AD1:gin to] [c/D881CD:grow][c/E37BCB:...]
        }
        else if (Main.netMode == NetmodeID.Server)
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The otherworldly crystals begin to grow..."), new Color(176, 153, 214));
        }
        float num611 = Main.maxTilesX * Main.maxTilesY / 5040000f;
        int amtOfBiomes = 3;
        if (Main.maxTilesX == 6300)
            amtOfBiomes = 4;
        if (Main.maxTilesX == 8400)
            amtOfBiomes = 5;
        //int num612 = (int)(WorldGen.genRand.Next(2, 4) * num611);
        float num613 = (Main.maxTilesX - 160) / amtOfBiomes;
        int num614 = 0;
        while (num614 < amtOfBiomes)
        {
            float num615 = (float)num614 / amtOfBiomes;
            Point point = WorldGen.RandomRectanglePoint((int)(num615 * (Main.maxTilesX - 160)) + 80, (int)Main.rockLayer + 20, (int)num613, Main.maxTilesY - ((int)Main.rockLayer + 40) - 200);
            //CrystalMinesRunner(point.X, point.Y, 150, 150);
            //Biomes<World.Biomes.CrystalMinesHouseBiome>.Place(new Point(point.X, point.Y), null);
            //num614++;
            WorldGenConfiguration config = WorldGenConfiguration.FromEmbeddedPath("Terraria.GameContent.WorldBuilding.Configuration.json");
            World.Biomes.CrystalMines crystalMines = config.CreateBiome<World.Biomes.CrystalMines>();
            if (crystalMines.Place(point, null))
            {
                World.Biomes.CrystalMinesHouseBiome crystalHouse = config.CreateBiome<World.Biomes.CrystalMinesHouseBiome>();
                crystalHouse.Place(new Point(point.X, point.Y + 15), null);
                num614++;
            }
        }
    }
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
    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
    {
        if (WorldBiomeManager.WorldJungle == "Avalon/TropicsAlternateBiome")
        {
            int hives = tasks.FindIndex(genpass => genpass.Name == "Oasis");
            if (hives != -1)
            {
                tasks[hives + 2] = new PassLegacy("Sanctums", World.Passes.TropicsSanctum.Method);
            }
        }
    }
    public override void ModifyHardmodeTasks(List<GenPass> list)
    {
        int index = list.FindIndex(genpass => genpass.Name.Equals("Hardmode Good"));
        list.Insert(index + 1, new PassLegacy("Exxo Avalon Origins: Hardmode Good (Hallowed Altars)", new WorldGenLegacyMethod(World.Passes.HallowedAltars.Method)));
        index = list.FindIndex(genpass => genpass.Name.Equals("Hardmode Evil"));
        list.Insert(index + 1, new PassLegacy("Exxo Avalon Origins: Hardmode Evil (Contagion Grass Walls)", new WorldGenLegacyMethod(World.Passes.ContagionGrassWalls.Method)));
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
    public void SpreadXanthophyte(int x, int y)
    {
        if (Main.tile[x, y].IsActuated)
        {
            return;
        }

        int type = Main.tile[x, y].TileType;

        if (y > (Main.worldSurface + Main.rockLayer) / 2.0)
        {
            if ((type == ModContent.TileType<TropicalGrass>()/* || type == ModContent.TileType<Tiles.BrownIce>()*/) && WorldGen.genRand.NextBool(325))
            {
                int num6 = x + WorldGen.genRand.Next(-10, 11);
                int num7 = y + WorldGen.genRand.Next(-10, 11);
                if (Main.tile[num6, num7].HasTile && (Main.tile[num6, num7].TileType == ModContent.TileType<Loam>()/* || Main.tile[num6, num7].type == ModContent.TileType<Tiles.BrownIce>()*/) && (!Main.tile[num6, num7 - 1].HasTile || (Main.tile[num6, num7 - 1].TileType != 5 && Main.tile[num6, num7 - 1].TileType != 236 && Main.tile[num6, num7 - 1].TileType != 238)) && GrowingOreSpread.GrowingOre(x, y, ModContent.TileType<XanthophyteOre>()))
                {
                    Main.tile[num6, num7].TileType = (ushort)ModContent.TileType<XanthophyteOre>();
                    WorldGen.SquareTileFrame(num6, num7, true);
                }
            }
            if (type == (ushort)ModContent.TileType<XanthophyteOre>() && WorldGen.genRand.Next(3) != 0)
            {
                int num8 = x;
                int num9 = y;
                int num10 = WorldGen.genRand.Next(4);
                if (num10 == 0)
                {
                    num8++;
                }
                if (num10 == 1)
                {
                    num8--;
                }
                if (num10 == 2)
                {
                    num9++;
                }
                if (num10 == 3)
                {
                    num9--;
                }
                if (Main.tile[num8, num9].HasTile && (Main.tile[num8, num9].TileType == ModContent.TileType<Loam>() || Main.tile[num8, num9].TileType == ModContent.TileType<TropicalGrass>()) /*|| Main.tile[num8, num9].type == ModContent.TileType<Tiles.BrownIce>())*/ && GrowingOreSpread.GrowingOre(x, y, ModContent.TileType<XanthophyteOre>()))
                {
                    Main.tile[num8, num9].TileType = (ushort)ModContent.TileType<XanthophyteOre>();
                    WorldGen.SquareTileFrame(num8, num9, true);
                }
            }
        }
    }
    public void SpreadShroomite(int x, int y)
    {
        if (Main.tile[x, y].IsActuated)
        {
            return;
        }

        int type = Main.tile[x, y].TileType;

        if (y > (Main.worldSurface + Main.rockLayer) / 2.0)
        {
            if ((type == TileID.MushroomGrass) && WorldGen.genRand.NextBool(150))
            {
                int num6 = x + WorldGen.genRand.Next(-10, 11);
                int num7 = y + WorldGen.genRand.Next(-10, 11);
                if (Main.tile[num6, num7].HasTile && (Main.tile[num6, num7].TileType == TileID.Mud) && (!Main.tile[num6, num7 - 1].HasTile || (Main.tile[num6, num7 - 1].TileType != 5 && Main.tile[num6, num7 - 1].TileType != 236 && Main.tile[num6, num7 - 1].TileType != 238)) && GrowingOreSpread.GrowingOre(x, y, ModContent.TileType<ShroomiteOre>()))
                {
                    Main.tile[num6, num7].TileType = (ushort)ModContent.TileType<ShroomiteOre>();
                    WorldGen.SquareTileFrame(num6, num7, true);
                }
            }
            if (type == (ushort)ModContent.TileType<ShroomiteOre>() && !WorldGen.genRand.NextBool(3))
            {
                int num8 = x;
                int num9 = y;
                int num10 = WorldGen.genRand.Next(4);
                if (num10 == 0)
                {
                    num8++;
                }
                if (num10 == 1)
                {
                    num8--;
                }
                if (num10 == 2)
                {
                    num9++;
                }
                if (num10 == 3)
                {
                    num9--;
                }
                if (Main.tile[num8, num9].HasTile && (Main.tile[num8, num9].TileType == TileID.Mud || Main.tile[num8, num9].TileType == TileID.MushroomGrass) && GrowingOreSpread.GrowingOre(x, y, ModContent.TileType<ShroomiteOre>()))
                {
                    Main.tile[num8, num9].TileType = (ushort)ModContent.TileType<ShroomiteOre>();
                    WorldGen.SquareTileFrame(num8, num9, true);
                }
            }
        }
    }
    public void DarkMatterSpread(int i, int j)
    {
        if (!Main.hardMode || Main.tile[i, j].IsActuated)
        {
            return;
        }

        int type = Main.tile[i, j].TileType;
        int wall = Main.tile[i, j].WallType;
        if (SuperHardmode)
        {
            if (type == ModContent.TileType<DarkMatter>() || type == ModContent.TileType<DarkMatterSoil>() || type == ModContent.TileType<DarkMatterGrass>() || type == ModContent.TileType<DarkMatterSand>() || type == ModContent.TileType<HardenedDarkSand>() || type == ModContent.TileType<Darksandstone>() || type == ModContent.TileType<BlackIce>())
            {
                bool flag5 = true;
                while (flag5)
                {
                    flag5 = false;
                    int num6 = i + WorldGen.genRand.Next(-3, 4);
                    int num7 = j + WorldGen.genRand.Next(-3, 4);
                    if (Main.tile[num6, num7 - 1].TileType == 27)
                    {
                        continue;
                    }
                    if (Main.tile[num6, num7].TileType == TileID.Grass || Main.tile[num6, num7].TileType == TileID.JungleGrass ||
                        Main.tile[num6, num7].TileType == TileID.MushroomGrass || Main.tile[num6, num7].TileType == TileID.CorruptGrass ||
                        Main.tile[num6, num7].TileType == TileID.CrimsonGrass || Main.tile[num6, num7].TileType == TileID.HallowedGrass ||
                        Main.tile[num6, num7].TileType == (ushort)ModContent.TileType<Ickgrass>() ||
                        Main.tile[num6, num7].TileType == (ushort)ModContent.TileType<TropicalGrass>())
                    {
                        if (WorldGen.genRand.NextBool(2))
                        {
                            flag5 = true;
                        }
                        Main.tile[num6, num7].TileType = (ushort)ModContent.TileType<DarkMatterGrass>();
                        WorldGen.SquareTileFrame(num6, num7);
                        NetMessage.SendTileSquare(-1, num6, num7, 1);
                    }
                    if (Main.tile[num6, num7].TileType == TileID.Dirt || Main.tile[num6, num7].TileType == TileID.Mud ||
                        Main.tile[num6, num7].TileType == TileID.ClayBlock ||
                        Main.tile[num6, num7].TileType == (ushort)ModContent.TileType<Loam>())
                    {
                        if (WorldGen.genRand.NextBool(2))
                        {
                            flag5 = true;
                        }
                        Main.tile[num6, num7].TileType = (ushort)ModContent.TileType<DarkMatterSoil>();
                        WorldGen.SquareTileFrame(num6, num7);
                        NetMessage.SendTileSquare(-1, num6, num7, 1);
                    }
                    else if (Main.tile[num6, num7].TileType == TileID.Stone || Main.tile[num6, num7].TileType == TileID.Crimstone ||
                        Main.tile[num6, num7].TileType == TileID.Ebonstone || Main.tile[num6, num7].TileType == TileID.Pearlstone ||
                        Main.tile[num6, num7].TileType == (ushort)ModContent.TileType<Chunkstone>() || Main.tileMoss[Main.tile[num6, num7].TileType])
                    {
                        if (WorldGen.genRand.NextBool(2))
                        {
                            flag5 = true;
                        }
                        Main.tile[num6, num7].TileType = (ushort)ModContent.TileType<DarkMatter>();
                        WorldGen.SquareTileFrame(num6, num7);
                        NetMessage.SendTileSquare(-1, num6, num7, 1);
                    }
                    else if (Main.tile[num6, num7].TileType == TileID.Sand || Main.tile[num6, num7].TileType == TileID.Pearlsand ||
                        Main.tile[num6, num7].TileType == TileID.Ebonsand || Main.tile[num6, num7].TileType == TileID.Crimsand ||
                        Main.tile[num6, num7].TileType == (ushort)ModContent.TileType<Snotsand>())
                    {
                        if (WorldGen.genRand.NextBool(2))
                        {
                            flag5 = true;
                        }
                        Main.tile[num6, num7].TileType = (ushort)ModContent.TileType<DarkMatterSand>();
                        WorldGen.SquareTileFrame(num6, num7);
                        NetMessage.SendTileSquare(-1, num6, num7, 1);
                    }
                    else if (Main.tile[num6, num7].TileType == TileID.Sandstone || Main.tile[num6, num7].TileType == TileID.CrimsonSandstone ||
                        Main.tile[num6, num7].TileType == TileID.CorruptSandstone || Main.tile[num6, num7].TileType == TileID.HallowSandstone ||
                        Main.tile[num6, num7].TileType == (ushort)ModContent.TileType<Snotsandstone>())
                    {
                        if (WorldGen.genRand.NextBool(2))
                        {
                            flag5 = true;
                        }
                        Main.tile[num6, num7].TileType = (ushort)ModContent.TileType<Darksandstone>();
                        WorldGen.SquareTileFrame(num6, num7);
                        NetMessage.SendTileSquare(-1, num6, num7, 1);
                    }
                    else if (Main.tile[num6, num7].TileType == TileID.HardenedSand || Main.tile[num6, num7].TileType == TileID.CrimsonHardenedSand ||
                        Main.tile[num6, num7].TileType == TileID.CorruptHardenedSand || Main.tile[num6, num7].TileType == TileID.HallowHardenedSand ||
                        Main.tile[num6, num7].TileType == (ushort)ModContent.TileType<HardenedSnotsand>())
                    {
                        if (WorldGen.genRand.NextBool(2))
                        {
                            flag5 = true;
                        }
                        Main.tile[num6, num7].TileType = (ushort)ModContent.TileType<HardenedDarkSand>();
                        WorldGen.SquareTileFrame(num6, num7);
                        NetMessage.SendTileSquare(-1, num6, num7, 1);
                    }
                    else if (Main.tile[num6, num7].TileType == TileID.IceBlock || Main.tile[num6, num7].TileType == TileID.FleshIce ||
                        Main.tile[num6, num7].TileType == TileID.CorruptIce || Main.tile[num6, num7].TileType == TileID.HallowedIce ||
                        Main.tile[num6, num7].TileType == (ushort)ModContent.TileType<YellowIce>() ||
                        Main.tile[num6, num7].TileType == (ushort)ModContent.TileType<GreenIce>() ||
                        Main.tile[num6, num7].TileType == (ushort)ModContent.TileType<BrownIce>())
                    {
                        if (WorldGen.genRand.NextBool(2))
                        {
                            flag5 = true;
                        }
                        Main.tile[num6, num7].TileType = (ushort)ModContent.TileType<BlackIce>();
                        WorldGen.SquareTileFrame(num6, num7);
                        NetMessage.SendTileSquare(-1, num6, num7, 1);
                    }
                }
            }
        }
    }

    public void DropComet(int tile)
    {
        bool flag = true;
        int num = 0;
        if (Main.netMode == NetmodeID.MultiplayerClient)
        {
            return;
        }
        for (int i = 0; i < 255; i++)
        {
            if (Main.player[i].active)
            {
                flag = false;
                break;
            }
        }
        int num2 = 0;
        float num3 = Main.maxTilesX / 4200;
        int num4 = (int)(400f * num3);
        for (int j = 5; j < Main.maxTilesX - 5; j++)
        {
            int num5 = 5;
            while (num5 < Main.worldSurface)
            {
                if (Main.tile[j, num5].HasTile && Main.tile[j, num5].TileType == tile)
                {
                    num2++;
                    if (num2 > num4)
                    {
                        return;
                    }
                }
                num5++;
            }
        }
        while (!flag)
        {
            float num6 = Main.maxTilesX * 0.08f;
            int num7 = Main.rand.Next(50, Main.maxTilesX - 50);
            while (num7 > Main.spawnTileX - num6 && num7 < Main.spawnTileX + num6)
            {
                num7 = Main.rand.Next(50, Main.maxTilesX - 50);
            }
            for (int k = Main.rand.Next(100); k < Main.maxTilesY; k++)
            {
                if (Main.tile[num7, k].HasTile && Main.tileSolid[Main.tile[num7, k].TileType])
                {
                    flag = Comet(num7, k, tile);
                    break;
                }
            }
            num++;
            if (num >= 100)
            {
                return;
            }
        }
    }

    public bool Comet(int i, int j, int tile)
    {
        if (i < 50 || i > Main.maxTilesX - 50)
        {
            return false;
        }
        if (j < 50 || j > Main.maxTilesY - 50)
        {
            return false;
        }
        int num = 25;
        var rectangle = new Rectangle((i - num) * 16, (j - num) * 16, num * 2 * 16, num * 2 * 16);
        for (int k = 0; k < 255; k++)
        {
            if (Main.player[k].active)
            {
                var value = new Rectangle((int)(Main.player[k].position.X + Main.player[k].width / 2 - NPC.sWidth / 2 - NPC.safeRangeX), (int)(Main.player[k].position.Y + Main.player[k].height / 2 - NPC.sHeight / 2 - NPC.safeRangeY), NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
                if (rectangle.Intersects(value))
                {
                    return false;
                }
            }
        }
        for (int l = 0; l < 200; l++)
        {
            if (Main.npc[l].active)
            {
                var value2 = new Rectangle((int)Main.npc[l].position.X, (int)Main.npc[l].position.Y, Main.npc[l].width, Main.npc[l].height);
                if (rectangle.Intersects(value2))
                {
                    return false;
                }
            }
        }
        for (int m = i - num; m < i + num; m++)
        {
            for (int n = j - num; n < j + num; n++)
            {
                if (Main.tile[m, n].HasTile && Main.tile[m, n].TileType == 21 || Main.tile[m, n].TileType == TileID.Containers2)
                {
                    return false;
                }
            }
        }
        //stopCometDrops = true;
        num = 15;
        for (int num2 = i - num; num2 < i + num; num2++)
        {
            for (int num3 = j - num; num3 < j + num; num3++)
            {
                if (num3 > j + Main.rand.Next(-2, 3) - 5 && Math.Abs(i - num2) + Math.Abs(j - num3) < num * 1.5 + Main.rand.Next(-5, 5))
                {
                    if (!Main.tileSolid[Main.tile[num2, num3].TileType])
                    {
                        Main.tile[num2, num3].Active(false);
                    }
                    Main.tile[num2, num3].TileType = (ushort)tile;
                }
            }
        }
        num = 10;
        for (int num4 = i - num; num4 < i + num; num4++)
        {
            for (int num5 = j - num; num5 < j + num; num5++)
            {
                if (num5 > j + Main.rand.Next(-2, 3) - 5 && Math.Abs(i - num4) + Math.Abs(j - num5) < num + Main.rand.Next(-3, 4))
                {
                    Main.tile[num4, num5].Active(false);
                }
            }
        }
        num = 16;
        for (int num6 = i - num; num6 < i + num; num6++)
        {
            for (int num7 = j - num; num7 < j + num; num7++)
            {
                if (Main.tile[num6, num7].TileType == 5 || Main.tile[num6, num7].TileType == 32)
                {
                    WorldGen.KillTile(num6, num7, false, false, false);
                }
                WorldGen.SquareTileFrame(num6, num7, true);
                WorldGen.SquareWallFrame(num6, num7, true);
            }
        }
        num = 23;
        for (int num8 = i - num; num8 < i + num; num8++)
        {
            for (int num9 = j - num; num9 < j + num; num9++)
            {
                if (Main.tile[num8, num9].HasTile && Main.rand.Next(10) == 0 && Math.Abs(i - num8) + Math.Abs(j - num9) < num * 1.3)
                {
                    if (Main.tile[num8, num9].TileType == 5 || Main.tile[num8, num9].TileType == 32)
                    {
                        WorldGen.KillTile(num8, num9, false, false, false);
                    }
                    Main.tile[num8, num9].TileType = (ushort)tile;
                    WorldGen.SquareTileFrame(num8, num9, true);
                }
            }
        }
        //stopCometDrops = false;
        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            Main.NewText("A comet has struck ground!", 0, 201, 190);
        }
        else if (Main.netMode == NetmodeID.Server)
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("A comet has struck ground!"), new Color(0, 201, 190));
        }
        if (Main.netMode != NetmodeID.MultiplayerClient)
        {
            NetMessage.SendTileSquare(-1, i, j, 30);
        }
        return true;
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

            #region hardmode/superhardmode stuff
            if (Main.tile[num5, num6].HasUnactuatedTile)
            {
                //ContagionHardmodeSpread(num5, num6);
                if (Main.hardMode)
                {
                    SpreadXanthophyte(num5, num6);
                }
                if (Main.hardMode)
                {
                    SpreadShroomite(num5, num6);
                }
                if (SuperHardmode && ModContent.GetInstance<BiomeTileCounts>().WorldDarkMatterTiles < BiomeTileCounts.DarkMatterTilesHardLimit)
                {
                    DarkMatterSpread(num5, num6);
                }
            }
            #endregion hardmode/superhardmode stuff

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
            if (Main.tile[num5, num6].TileType == ModContent.TileType<Tiles.Nest>() || Main.tile[num5, num6].TileType == TileID.Hive)
            {
                int num14 = Main.tile[num5, num6].TileType;
                if (!Main.tile[num5, num9].HasTile && !Main.tile[num5, num6].IsHalfBlock && Main.tile[num5, num6].Slope == SlopeType.Solid && WorldGen.genRand.NextBool(700) && (num14 == ModContent.TileType<Tiles.Nest>() || num14 == TileID.Hive))
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

            #region crystal shard in crystal mines
            if (SuperHardmode)
            {
                int type = (int)Main.tile[num5, num6].TileType;
                if ((type == ModContent.TileType<CrystalStone>()) && num6 > Main.rockLayer && Main.rand.Next(65) == 0)
                {
                    int num = WorldGen.genRand.Next(4);
                    int xdir = 0;
                    int ydir = 0;
                    if (num == 0)
                    {
                        xdir = -1;
                    }
                    else if (num == 1)
                    {
                        xdir = 1;
                    }
                    else if (num == 0)
                    {
                        ydir = -1;
                    }
                    else
                    {
                        ydir = 1;
                    }
                    if (!Main.tile[num5 + xdir, num6 + ydir].HasTile)
                    {
                        int q = 0;
                        int z = 6;
                        for (int k = num5 - z; k <= num5 + z; k++)
                        {
                            for (int l = num6 - z; l <= num6 + z; l++)
                            {
                                if (Main.tile[k, l].HasTile && Main.tile[k, l].TileType == TileID.Crystals)
                                {
                                    q++;
                                }
                            }
                        }
                        if (q < 2)
                        {
                            if (ydir != -1)
                            {
                                WorldGen.PlaceTile(num5 + xdir, num6 + ydir, TileID.Crystals, true, false, -1, 0);
                                NetMessage.SendTileSquare(-1, num5 + xdir, num6 + ydir, 1);
                            }
                        }
                    }
                }
            }
            #endregion

            #region crystal fruit and giant crystal spawning
            if (Main.tile[num5, num6].TileType == ModContent.TileType<CrystalStone>() && SuperHardmode &&
                ModContent.GetInstance<DownedBossSystem>().DownedMechasting && num6 > Main.rockLayer) // CHANGE LATER TO OBLIVION
            {
                if (WorldGen.genRand.Next(50) == 0 && Main.tile[num5, num9].LiquidAmount == 0)
                {
                    bool flag16 = true;
                    int distanceCheck = 40;
                    for (int num80 = num5 - distanceCheck; num80 < num5 + distanceCheck; num80 += 2)
                    {
                        for (int num81 = num6 - distanceCheck; num81 < num6 + distanceCheck; num81 += 2)
                        {
                            if (num80 > 1 && num80 < Main.maxTilesX - 2 && num81 > 1 && num81 < Main.maxTilesY - 2 && Main.tile[num80, num81].HasTile && Main.tile[num80, num81].TileType == ModContent.TileType<CrystalFruit>())
                            {
                                flag16 = false;
                                break;
                            }
                        }
                    }
                    if (flag16)
                    {
                        WorldGen.Place2x2(num5, num9, (ushort)ModContent.TileType<CrystalFruit>(), WorldGen.genRand.Next(3));
                        WorldGen.SquareTileFrame(num5, num9, true);
                        WorldGen.SquareTileFrame(num5 + 1, num9 + 1, true);
                        if (Main.tile[num5, num9].TileType == ModContent.TileType<CrystalFruit>() && Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendTileSquare(-1, num5, num9, 4);
                        }
                    }
                    else
                    {
                        WorldGen.Place2x2(num5, num9, (ushort)ModContent.TileType<GiantCrystalShard>(), WorldGen.genRand.Next(3));
                        WorldGen.SquareTileFrame(num5, num9, true);
                        WorldGen.SquareTileFrame(num5 + 1, num9 + 1, true);
                        if (Main.tile[num5, num9].TileType == ModContent.TileType<GiantCrystalShard>() && Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendTileSquare(-1, num5, num9, 4);
                        }
                    }
                }
            }
            #endregion

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
                if (!Main.tile[num5, num9].HasTile && Main.tile[num5, num9].LiquidAmount == 0 &&
                    !Main.tile[num5, num6].IsHalfBlock && Main.tile[num5, num6].Slope == SlopeType.Solid &&
                    WorldGen.genRand.NextBool(5) && num14 == ModContent.TileType<Ickgrass>())
                {
                    WorldGen.PlaceTile(num5, num9, ModContent.TileType<ContagionShortGrass>(), true);
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

            #region lazite grass
            if (Main.tile[num5, num6].TileType == ModContent.TileType<LaziteGrass>())
            {
                int num14 = Main.tile[num5, num6].TileType;

                // where lazite tallgrass would grow
                if (!Main.tile[num5, num9].HasTile && Main.tile[num5, num9].LiquidAmount == 0 &&
                    !Main.tile[num5, num6].IsHalfBlock && Main.tile[num5, num6].Slope == SlopeType.Solid &&
                    WorldGen.genRand.NextBool(15) && num14 == ModContent.TileType<LaziteGrass>())
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
            #endregion lazite grass

            #region dark matter grass
            if (Main.tile[num5, num6].TileType == ModContent.TileType<DarkMatterGrass>())
            {
                int num14 = Main.tile[num5, num6].TileType;
                // regular bush (3x2)
                if (!Main.tile[num5, num9].HasTile && Main.tile[num5, num9].LiquidAmount == 0 &&
                    !Main.tile[num5, num6].IsHalfBlock && Main.tile[num5, num6].Slope == SlopeType.Solid &&
                    WorldGen.genRand.NextBool(35) && num14 == ModContent.TileType<DarkMatterGrass>())
                {
                    WorldGen.Place3x2(num5, num9, (ushort)ModContent.TileType<DarkMatterBush>());
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

                // tall bush (2x3)
                if (!Main.tile[num5, num9].HasTile && Main.tile[num5, num9].LiquidAmount == 0 &&
                    !Main.tile[num5, num6].IsHalfBlock && Main.tile[num5, num6].Slope == SlopeType.Solid &&
                    WorldGen.genRand.NextBool(40) && num14 == ModContent.TileType<DarkMatterGrass>())
                {
                    WorldGen.Place2xX(num5, num9, (ushort)ModContent.TileType<DarkMatterTallBush>());
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

                // where dark tallgrass would grow
                if (!Main.tile[num5, num9].HasTile && Main.tile[num5, num9].LiquidAmount == 0 &&
                    !Main.tile[num5, num6].IsHalfBlock && Main.tile[num5, num6].Slope == SlopeType.Solid &&
                    WorldGen.genRand.NextBool(1) && num14 == ModContent.TileType<DarkMatterGrass>())
                {
                    WorldGen.PlaceTile(num5, num9, ModContent.TileType<DarkMatterShortGrass>(), true);
                    Main.tile[num5, num9].TileFrameX = (short)(WorldGen.genRand.Next(0, 9) * 18);
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

                //bool flag2 = false;
                //for (int m = num7; m < num8; m++)
                //{
                //    for (int n = num9; n < num10; n++)
                //    {
                //        if ((num5 != m || num6 != n) && Main.tile[m, n].HasTile)
                //        {
                //            if (Main.tile[m, n].TileType == ModContent.TileType<DarkMatterSoil>())
                //            {
                //                WorldGen.SpreadGrass(m, n, ModContent.TileType<DarkMatterSoil>(),
                //                    ModContent.TileType<DarkMatterGrass>(), false, Main.tile[num5, num6].TileColor);
                //            }

                //            if (Main.tile[m, n].TileType == num14)
                //            {
                //                WorldGen.SquareTileFrame(m, n);
                //                flag2 = true;
                //            }
                //        }
                //    }
                //}

                //if (Main.netMode == NetmodeID.Server && flag2)
                //{
                //    NetMessage.SendTileSquare(-1, num5, num6, 3);
                //}
            }
            #endregion dark matter grass

            #region impgrass
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
            #endregion impgrass

            #region tropical grass
            if (Main.tile[num5, num6].TileType == ModContent.TileType<TropicalGrass>())
            {
                int num14 = Main.tile[num5, num6].TileType;
                // twilight plume
                if (!Main.tile[num5, num9].HasTile && Main.tile[num5, num9].LiquidAmount == 0 &&
                    !Main.tile[num5, num6].IsHalfBlock && Main.tile[num5, num6].Slope == SlopeType.Solid &&
                    WorldGen.genRand.NextBool(num6 > Main.worldSurface ? 75 : 250) && num14 == ModContent.TileType<TropicalGrass>())
                {
                    WorldGen.PlaceTile(num5, num9, ModContent.TileType<Tiles.Herbs.TwilightPlume>(), true, false, -1, 0);
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
                // regular grass
                if (!Main.tile[num5, num9].HasTile && Main.tile[num5, num9].LiquidAmount == 0 &&
                    !Main.tile[num5, num6].IsHalfBlock && Main.tile[num5, num6].Slope == SlopeType.Solid &&
                    WorldGen.genRand.NextBool(5) && num14 == ModContent.TileType<TropicalGrass>())
                {
                    WorldGen.PlaceTile(num5, num9, ModContent.TileType<TropicalShortGrass>(), true);
                    Main.tile[num5, num9].TileFrameX = (short)(WorldGen.genRand.Next(0, 8) * 18);
                    if (num9 > Main.rockLayer)
                    {
                        if (WorldGen.genRand.NextBool(60))
                        {
                            Main.tile[num5, num9].TileFrameX = 18 * 8; // shroom cap
                        }
                        else if (WorldGen.genRand.NextBool(230))
                        {
                            Main.tile[num5, num9].TileFrameX = 18 * 9; // nature's gift
                        }
                    }
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
                //bool flag2 = false;
                //for (int m = num7; m < num8; m++)
                //{
                //    for (int n = num9; n < num10; n++)
                //    {
                //        if ((num5 != m || num6 != n) && Main.tile[m, n].HasTile)
                //        {
                //            if (Main.tile[m, n].TileType == ModContent.TileType<Loam>())
                //            {
                //                WorldGen.SpreadGrass(m, n, ModContent.TileType<Loam>(), ModContent.TileType<TropicalGrass>(), false,
                //                    Main.tile[num5, num6].TileColor);
                //            }

                //            if (Main.tile[m, n].TileType == num14)
                //            {
                //                //WorldGen.SquareTileFrame(m, n);
                //                flag2 = true;
                //            }
                //        }
                //    }
                //}

                //if (Main.netMode == NetmodeID.Server && flag2)
                //{
                //    NetMessage.SendTileSquare(-1, num5, num6, 3);
                //}
            }
            #endregion tropical grass

            #region impvines growing
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
            #endregion impvines

            #region tropics vines growing
            if ((Main.tile[num5, num6].TileType == ModContent.TileType<TropicalGrass>() ||
                 Main.tile[num5, num6].TileType == ModContent.TileType<TropicalVines>()) &&
                WorldGen.genRand.NextBool(15) && !Main.tile[num5, num6 + 1].HasTile &&
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
                        Main.tile[num5, num47].TileType == ModContent.TileType<TropicalGrass>() &&
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
                    Main.tile[num48, num49].TileType = (ushort)ModContent.TileType<TropicalVines>();

                    Tile t = Main.tile[num48, num49];
                    t.HasTile = true;
                    WorldGen.SquareTileFrame(num48, num49);
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendTileSquare(-1, num48, num49, 3);
                    }
                }
            }
            #endregion tropical vines
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
