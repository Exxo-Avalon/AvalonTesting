using System;
using System.Threading;
using AvalonTesting.Items.Armor;
using AvalonTesting.Items.Material;
using AvalonTesting.Items.Placeable.Seed;
using AvalonTesting.Systems;
using AvalonTesting.Tiles;
using AvalonTesting.Tiles.Ores;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AvalonTesting;

public class AvalonTestingWorld : ModSystem
{
    public static int shmOreTier1 = -1;
    public static int shmOreTier2 = -1;
    public static int hallowAltarCount;
    public static bool contagion = false;
    public static int hallowedAltarCount = 0;
    public static bool stopCometDrops = false;
    public static Vector2 hiddenTemplePos;
    public static bool retroGenned = false;
    public static bool generatingBaccilite = false;
    public static int dungeonSide = 0;
    public static int jungleX = 0;
    public static int grassSpread = 0;
    public static bool contaigonSet = false;
    public static Vector2 LoK = Vector2.Zero;
    public static int specialWireHitCount = 0;
    public static int wosT;
    public static int wosB;
    public static int wosF = 0;
    public static int wos = -1;
    public static bool jungleLocationKnown = false;
    private Version worldVersion;
    public bool SuperHardmode { get; private set; }

    public override void PostUpdateEverything()
    {
        SpectrumHelmet.StaticUpdate();
    }

    public static void GenerateSulphur()
    {
        if (Main.rand == null)
        {
            Main.rand = new UnifiedRandom((int)DateTime.Now.Ticks);
        }
        for (int a = 0; a < (int)((Main.maxTilesX * Main.maxTilesY) * 0.00012); a++)
        {
            int x = Main.rand.Next(100, Main.maxTilesX - 100);
            int y = Main.rand.Next((int)Main.rockLayer, Main.maxTilesY - 150);
            WorldGen.OreRunner(x, y, Main.rand.Next(7, 9), Main.rand.Next(5, 7), (ushort)ModContent.TileType<Tiles.Ores.SulphurOre>());
        }
        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            Main.NewText("The underground smells like rotten eggs...", 210, 183, 4);
        }
        else if (Main.netMode == NetmodeID.Server)
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The underground smells like rotten eggs..."), new Color(210, 183, 4));
        }
    }

    public override void PostUpdateWorld()
    {
        float num2 = 3E-05f * (float)WorldGen.GetWorldUpdateRate();
        //float num3 = 1.5E-05f * (float)Main.worldRate;
        int num4 = 0;
        while (num4 < Main.maxTilesX * Main.maxTilesY * num2)
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

            if (Main.tile[num5, num6] != null)
            {
                #region lazite grass

                if (Main.tile[num5, num6].TileType == ModContent.TileType<LaziteGrass>())
                {
                    int num14 = Main.tile[num5, num6].TileType;
                    // where lazite tallgrass would grow
                    if (!Main.tile[num5, num9].HasTile && Main.tile[num5, num9].LiquidAmount == 0 &&
                        !Main.tile[num5, num6].IsHalfBlock && Main.tile[num5, num6].Slope == SlopeType.Solid &&
                        WorldGen.genRand.Next(5) == 0 && num14 == ModContent.TileType<LaziteGrass>())
                    {
                        WorldGen.PlaceTile(num5, num9, ModContent.TileType<LaziteShortGrass>(), true);
                        Main.tile[num5, num9].TileFrameX = (short)(WorldGen.genRand.Next(0, 10) * 18);
                        if (Main.tile[num5, num9].HasTile)
                        {
                            //Main.tile[num5, num9].TileColor = (Main.tile[num5, num6].TileColor);
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

                #endregion

                #region impgrass growing

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

                #endregion

                #region impvines growing

                if ((Main.tile[num5, num6].TileType == ModContent.TileType<Impgrass>() ||
                     Main.tile[num5, num6].TileType == ModContent.TileType<Impvines>()) &&
                    WorldGen.genRand.Next(15) == 0 && !Main.tile[num5, num6 + 1].HasTile &&
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
                        //Main.tile[num48, num49].active(true);
                        WorldGen.SquareTileFrame(num48, num49);
                        if (Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendTileSquare(-1, num48, num49, 3);
                        }
                    }
                }

                #endregion impvines growing
            }

            num4++;
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
        int yframe = Main.tile[x, y].TileFrameY;
        fixedY -= yframe / 18;
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

            if (type == (ushort)ModContent.TileType<LargeHerbsStage1>() ||
                type == (ushort)ModContent.TileType<LargeHerbsStage2>() ||
                type == (ushort)ModContent
                    .TileType<
                        LargeHerbsStage3>()) // 469 through 471 are the immature tiles of the large herb; 472 is the mature version
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

    public void InitiateSuperHardmode()
    {
        ThreadPool.QueueUserWorkItem(shmCallback, 1);
    }
    public void GenerateSkyFortress()
    {
        ThreadPool.QueueUserWorkItem(new WaitCallback(SkyFortressCallback), 1);
    }

    public void shmCallback(object threadContext)
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
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The ancient souls have been disturbed..."),
                new Color(255, 60, 0));
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The heavens above are rumbling..."),
                new Color(50, 255, 130));
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Your world has been blessed with the elements!"),
                new Color(0, 255, 0));
        }

        if (Main.netMode == NetmodeID.Server)
        {
            Netplay.ResetSections();
        }
    }

    public void SkyFortressCallback(object threadContext)
    {
        if (Main.rand == null)
        {
            Main.rand = new UnifiedRandom((int)DateTime.Now.Ticks);
        }
        if (!ModContent.GetInstance<DownedBossSystem>().DownedArmageddon) return;
        int x = Main.maxTilesX / 3;
        int y = 50;
        if (Main.maxTilesY == 1800) y = 60;
        if (Main.maxTilesY == 2400) y = 70;
        if (Main.rand.Next(2) == 0)
        {
            x = Main.maxTilesX - Main.maxTilesX / 3;

        }
        World.Utils.GetSkyFortressXCoord(x, y, 209, 158, ref x);
        World.Structures.SkyFortress.Generate(x, y);
        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            Main.NewText("Something is hiding on the top of your world...", 244, 140, 140);
        }
        else if (Main.netMode == NetmodeID.Server)
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Something is hiding on the top of your world..."), new Color(244, 140, 140));
        }
    }

    public static void GenerateSHMOres()
    {
        if (Main.rand == null)
        {
            Main.rand = new UnifiedRandom((int)DateTime.Now.Ticks);
        }

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

    public static void StopRain()
    {
        Main.rainTime = 0;
        Main.raining = false;
        Main.maxRaining = 0f;
    }

    public static void StartRain()
    {
        const int num = 86400;
        const int num2 = num / 24;
        Main.rainTime = Main.rand.Next(num2 * 8, num);
        if (Main.rand.Next(3) == 0)
        {
            Main.rainTime += Main.rand.Next(0, num2);
        }

        if (Main.rand.Next(4) == 0)
        {
            Main.rainTime += Main.rand.Next(0, num2 * 2);
        }

        if (Main.rand.Next(5) == 0)
        {
            Main.rainTime += Main.rand.Next(0, num2 * 2);
        }

        if (Main.rand.Next(6) == 0)
        {
            Main.rainTime += Main.rand.Next(0, num2 * 3);
        }

        if (Main.rand.Next(7) == 0)
        {
            Main.rainTime += Main.rand.Next(0, num2 * 4);
        }

        if (Main.rand.Next(8) == 0)
        {
            Main.rainTime += Main.rand.Next(0, num2 * 5);
        }

        float num3 = 1f;
        if (Main.rand.Next(2) == 0)
        {
            num3 += 0.05f;
        }

        if (Main.rand.Next(3) == 0)
        {
            num3 += 0.1f;
        }

        if (Main.rand.Next(4) == 0)
        {
            num3 += 0.15f;
        }

        if (Main.rand.Next(5) == 0)
        {
            num3 += 0.2f;
        }

        Main.rainTime = (int)(Main.rainTime * num3);
        ChangeRain();
        Main.raining = true;
    }

    public static void ChangeRain()
    {
        if (Main.cloudBGActive >= 1f || Main.numClouds > 150.0)
        {
            if (Main.rand.Next(3) == 0)
            {
                Main.maxRaining = Main.rand.Next(20, 90) * 0.01f;
                return;
            }

            Main.maxRaining = Main.rand.Next(40, 90) * 0.01f;
        }
        else if (Main.numClouds > 100.0)
        {
            if (Main.rand.Next(3) == 0)
            {
                Main.maxRaining = Main.rand.Next(10, 70) * 0.01f;
                return;
            }

            Main.maxRaining = Main.rand.Next(20, 60) * 0.01f;
        }
        else
        {
            if (Main.rand.Next(3) == 0)
            {
                Main.maxRaining = Main.rand.Next(5, 40) * 0.01f;
                return;
            }

            Main.maxRaining = Main.rand.Next(5, 30) * 0.01f;
        }
    }

    #region WorldGen Variants

    public enum JungleVariant
    {
        jungle,
        tropics,
        random
    }

    public enum CopperVariant
    {
        copper,
        tin,
        bronze,
        random
    }

    public enum IronVariant
    {
        iron,
        lead,
        nickel,
        random
    }

    public enum SilverVariant
    {
        silver,
        tungsten,
        zinc,
        random
    }

    public enum GoldVariant
    {
        gold,
        platinum,
        bismuth,
        random
    }

    public enum RhodiumVariant
    {
        rhodium,
        osmium,
        iridium,
        random
    }

    public enum CobaltVariant
    {
        cobalt,
        palladium,
        duratanium,
        random
    }

    public enum MythrilVariant
    {
        mythril,
        orichalcum,
        naquadah,
        random
    }

    public enum AdamantiteVariant
    {
        adamantite,
        titanium,
        troxinium,
        random
    }

    public enum SHMTier1Variant
    {
        tritanorium,
        pyroscoric,
        random
    }

    public enum SHMTier2Variant
    {
        unvolandite,
        vorazylcum,
        random
    }

    public static JungleVariant jungleMenuSelection = JungleVariant.random;
    public static CopperVariant copperOre = CopperVariant.random;
    public static IronVariant ironOre = IronVariant.random;
    public static SilverVariant silverOre = SilverVariant.random;
    public static GoldVariant goldOre = GoldVariant.random;
    public static RhodiumVariant rhodiumOre = RhodiumVariant.random;
    public static CobaltVariant cobaltOre = CobaltVariant.cobalt;
    public static MythrilVariant mythrilOre = MythrilVariant.random;
    public static AdamantiteVariant adamantiteOre = AdamantiteVariant.random;
    public static SHMTier1Variant shmTier1Ore = SHMTier1Variant.random;
    public static SHMTier2Variant shmTier2Ore = SHMTier2Variant.random;

    #endregion WorldGen Variants
}
