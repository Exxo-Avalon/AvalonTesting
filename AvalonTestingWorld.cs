using System;
using System.Threading;
using AvalonTesting.Items.Material;
using AvalonTesting.Items.Placeable.Seed;
using AvalonTesting.Tiles;
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
    private Version worldVersion;
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
    public static bool downedBacteriumPrime = false;
    public static bool downedDesertBeak = false;
    public static bool downedPhantasm = false;
    public static bool downedDragonLord = false;
    public static bool downedMechasting = false;
    public static bool oblivionDead = false;
    public static bool downedKingSting = false;
    public static bool stoppedArmageddon = false;
    public static int specialWireHitCount = 0;

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
    public bool SuperHardmode { get; private set; }
    public static int WorldDarkMatterTiles = 0;
    public static int wosT;
    public static int wosB;
    public static int wosF = 0;
    public static int wos = -1;
    public static bool jungleLocationKnown = false;
    public override void PostUpdateEverything()
    {
        Items.Armor.SpectrumHelmet.StaticUpdate();
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
                    if (!Main.tile[num5, num9].HasTile && Main.tile[num5, num9].LiquidAmount == 0 && !Main.tile[num5, num6].IsHalfBlock && Main.tile[num5, num6].Slope == SlopeType.Solid && WorldGen.genRand.Next(5) == 0 && num14 == ModContent.TileType<LaziteGrass>())
                    {
                        WorldGen.PlaceTile(num5, num9, ModContent.TileType<LaziteShortGrass>(), true, false, -1, 0);
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
                                    WorldGen.SpreadGrass(m, n, ModContent.TileType<BlastedStone>(), ModContent.TileType<LaziteGrass>(), false, Main.tile[num5, num6].TileColor);
                                }
                                if (Main.tile[m, n].TileType == num14)
                                {
                                    WorldGen.SquareTileFrame(m, n, true);
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
                if (Main.tile[num5, num6].TileType == ModContent.TileType<Tiles.Impgrass>())
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
                                    WorldGen.SpreadGrass(m, n, TileID.Ash, ModContent.TileType<Tiles.Impgrass>(), false, Main.tile[num5, num6].TileColor);
                                }
                                if (Main.tile[m, n].TileType == num14)
                                {
                                    WorldGen.SquareTileFrame(m, n, true);
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
                if ((Main.tile[num5, num6].TileType == ModContent.TileType<Tiles.Impgrass>() || Main.tile[num5, num6].TileType == ModContent.TileType<Tiles.Impvines>()) && WorldGen.genRand.Next(15) == 0 && !Main.tile[num5, num6 + 1].HasTile && Main.tile[num5, num6 + 1].LiquidType != LiquidID.Lava)
                {
                    bool flag10 = false;
                    for (int num47 = num6; num47 > num6 - 10; num47--)
                    {
                        if (Main.tile[num5, num47].BottomSlope)
                        {
                            flag10 = false;
                            break;
                        }
                        if (Main.tile[num5, num47].HasTile && Main.tile[num5, num47].TileType == ModContent.TileType<Tiles.Impgrass>() && !Main.tile[num5, num47].BottomSlope)
                        {
                            flag10 = true;
                            break;
                        }
                    }
                    if (flag10)
                    {
                        int num48 = num5;
                        int num49 = num6 + 1;
                        Main.tile[num48, num49].TileType = (ushort)ModContent.TileType<Tiles.Impvines>();
                        //Main.tile[num48, num49].active(true);
                        WorldGen.SquareTileFrame(num48, num49, true);
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
                WorldGen.KillTile(x, u, false, false, false);
            }
            if (type == (ushort)ModContent.TileType<Tiles.LargeHerbsStage1>() || type == (ushort)ModContent.TileType<Tiles.LargeHerbsStage2>() ||
                type == (ushort)ModContent.TileType<Tiles.LargeHerbsStage3>()) // 469 through 471 are the immature tiles of the large herb; 472 is the mature version
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
                }// 3710 through 3719 are the seeds
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
        ThreadPool.QueueUserWorkItem(new WaitCallback(shmCallback), 1);
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
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The ancient souls have been disturbed..."), new Color(255, 60, 0));
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The heavens above are rumbling..."), new Color(50, 255, 130));
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Your world has been blessed with the elements!"), new Color(0, 255, 0));
        }
        if (Main.netMode == NetmodeID.Server)
        {
            Netplay.ResetSections();
        }
    }

    public static void GenerateSHMOres()
    {
        if (Main.rand == null)
        {
            Main.rand = new UnifiedRandom((int)DateTime.Now.Ticks);
        }
        // oblivion ore
        for (int a = 0; a < (int)((Main.maxTilesX * Main.maxTilesY) * 0.00012); a++)
        {
            int x = Main.rand.Next(100, Main.maxTilesX - 100);
            int y = Main.rand.Next((int)Main.rockLayer, Main.maxTilesY - 200);
            WorldGen.OreRunner(x, y, Main.rand.Next(5, 9), Main.rand.Next(4, 6), (ushort)ModContent.TileType<Tiles.Ores.OblivionOre>());
        }
        // opals
        for (int a = 0; a < (int)((Main.maxTilesX * Main.maxTilesY) * 0.00012); a++)
        {
            int x = Main.rand.Next(100, Main.maxTilesX - 100);
            int y = Main.rand.Next((int)Main.rockLayer, Main.maxTilesY - 200);
            WorldGen.OreRunner(x, y, Main.rand.Next(4, 7), Main.rand.Next(1, 4), (ushort)ModContent.TileType<Tiles.Ores.Opal>());
        }
        // onyx
        for (int a = 0; a < (int)((Main.maxTilesX * Main.maxTilesY) * 0.00012); a++)
        {
            int x = Main.rand.Next(100, Main.maxTilesX - 100);
            int y = Main.rand.Next((int)Main.rockLayer, Main.maxTilesY - 200);
            WorldGen.OreRunner(x, y, Main.rand.Next(4, 7), Main.rand.Next(1, 4), (ushort)ModContent.TileType<Tiles.Ores.Onyx>());
        }
        // kunzite
        for (int a = 0; a < (int)((Main.maxTilesX * Main.maxTilesY) * 0.00012); a++)
        {
            int x = Main.rand.Next(100, Main.maxTilesX - 100);
            int y = Main.rand.Next((int)Main.rockLayer, Main.maxTilesY - 200);
            WorldGen.OreRunner(x, y, Main.rand.Next(4, 7), Main.rand.Next(1, 4), (ushort)ModContent.TileType<Tiles.Ores.Kunzite>());
        }
        // primordial ore
        for (int a = 0; a < (int)((Main.maxTilesX * Main.maxTilesY) * 0.00012); a++)
        {
            int x = Main.rand.Next(100, Main.maxTilesX - 100);
            int y = Main.rand.Next((int)Main.rockLayer, Main.maxTilesY - 200);
            WorldGen.OreRunner(x, y, Main.rand.Next(3, 6), Main.rand.Next(5, 8), (ushort)ModContent.TileType<Tiles.Ores.PrimordialOre>());
        }
    }
    public static void ConvertFromThings(int x, int y, int convert, bool tileframe = true)
    {
        Tile tile = Main.tile[x, y];
        int type = tile.TileType;
        int wall = tile.WallType;
        if (!WorldGen.InWorld(x, y, 1))
        {
            return;
        }
        if (convert == 0)
        {
            if (Main.tile[x, y] != null)
            {
                if (wall == ModContent.WallType<Walls.ContagionGrassWall>())
                {
                    Main.tile[x, y].WallType = WallID.GrassUnsafe;
                }
                else if (wall == ModContent.WallType<Walls.ChunkstoneWall>())
                {
                    Main.tile[x, y].WallType = WallID.Stone;
                }
            }
            if (Main.tile[x, y] != null)
            {
                if (type == ModContent.TileType<Ickgrass>())
                {
                    tile.TileType = TileID.Grass;
                }
                else if (type == ModContent.TileType<YellowIce>())
                {
                    tile.TileType = TileID.IceBlock;
                }
                else if (type == ModContent.TileType<Snotsand>())
                {
                    tile.TileType = TileID.Sand;
                }
                else if (type == ModContent.TileType<Chunkstone>())
                {
                    tile.TileType = TileID.Stone;
                }
                else if (type == ModContent.TileType<Snotsandstone>())
                {
                    tile.TileType = TileID.Sandstone;
                }
                else if (type == ModContent.TileType<HardenedSnotsand>())
                {
                    tile.TileType = TileID.HardenedSand;
                }
                //else if (type == ModContent.TileType<ContagionShortGrass>())
                //{
                //    tile.type = TileID.Plants;
                //}
                if (TileID.Sets.Conversion.Grass[type] || type == 0)
                {
                    WorldGen.SquareTileFrame(x, y);
                }
            }
        }
        if (convert == 1)
        {
            if (Main.tile[x, y] != null)
            {
                if (wall == ModContent.WallType<Walls.ContagionGrassWall>() || wall == WallID.CrimsonGrassUnsafe || wall == WallID.CorruptGrassUnsafe || wall == WallID.HallowedGrassUnsafe)
                {
                    Main.tile[x, y].WallType = WallID.JungleUnsafe;
                }
                else if (wall == WallID.DirtUnsafe)
                {
                    Main.tile[x, y].WallType = WallID.MudUnsafe;
                }
            }
            if (Main.tile[x, y] != null)
            {
                if (type == ModContent.TileType<Ickgrass>() || type == TileID.CrimsonGrass || type == TileID.CorruptGrass || type == TileID.Grass || type == TileID.HallowedGrass)
                {
                    tile.TileType = TileID.JungleGrass;
                }
                else if (type == TileID.Dirt)
                {
                    tile.TileType = TileID.Mud;
                }
                else if (type == ModContent.TileType<Snotsand>() || type == TileID.Sand || type == TileID.Crimsand || type == TileID.Ebonsand || type == TileID.Pearlsand)
                {
                    tile.TileType = TileID.Sand;
                }
                else if (type == ModContent.TileType<Chunkstone>() || type == TileID.Pearlstone || type == TileID.Crimstone || type == TileID.Ebonstone)
                {
                    tile.TileType = TileID.Stone;
                }
                else if (type == ModContent.TileType<Snotsandstone>() || type == TileID.HallowSandstone || type == TileID.CrimsonSandstone || type == TileID.CorruptSandstone)
                {
                    tile.TileType = TileID.Sandstone;
                }
                else if (type == ModContent.TileType<HardenedSnotsand>() || type == TileID.HallowHardenedSand || type == TileID.CrimsonHardenedSand || type == TileID.CorruptHardenedSand)
                {
                    tile.TileType = TileID.HardenedSand;
                }
                else if (type == ModContent.TileType<YellowIce>() || type == TileID.HallowedIce || type == TileID.FleshIce || type == TileID.CorruptIce || type == TileID.IceBlock)
                {
                    tile.TileType = (ushort)ModContent.TileType<GreenIce>();
                }
                if (TileID.Sets.Conversion.Grass[type] || type == 0)
                {
                    WorldGen.SquareTileFrame(x, y);
                }
            }
        }
        if (convert == 2 && WorldDarkMatterTiles < 250000)
        {
            if (Main.tile[x, y] != null)
            {
                if (wall == WallID.JungleUnsafe || wall == WallID.GrassUnsafe || wall == ModContent.WallType<Walls.ContagionGrassWall>() || wall == WallID.CrimsonGrassUnsafe || wall == WallID.CorruptGrassUnsafe || wall == WallID.HallowedGrassUnsafe)
                {
                    Main.tile[x, y].WallType = (ushort)ModContent.WallType<Walls.DarkMatterGrassWall>();
                }
                else if (wall == WallID.DirtUnsafe)
                {
                    Main.tile[x, y].WallType = (ushort)ModContent.WallType<Walls.DarkMatterSoilWall>();
                }
                else if (WallID.Sets.Conversion.Stone[wall])
                {
                    Main.tile[x, y].WallType = (ushort)ModContent.WallType<Walls.DarkMatterStoneWall>();
                }
            }
            if (Main.tile[x, y] != null)
            {
                if (type == ModContent.TileType<Ickgrass>() || type == ModContent.TileType<TropicalGrass>() || type == TileID.JungleGrass || type == TileID.MushroomGrass || type == TileID.CrimsonGrass || type == TileID.CorruptGrass || type == TileID.Grass || type == TileID.HallowedGrass)
                {
                    tile.TileType = (ushort)ModContent.TileType<DarkMatterGrass>();
                }
                else if (type == TileID.Dirt || type == TileID.ClayBlock || type == TileID.Mud || type == ModContent.TileType<TropicalMud>())
                {
                    tile.TileType = (ushort)ModContent.TileType<DarkMatterSoil>();
                }
                else if (type == ModContent.TileType<Snotsand>() || type == TileID.Sand || type == TileID.Crimsand || type == TileID.Ebonsand || type == TileID.Pearlsand)
                {
                    tile.TileType = (ushort)ModContent.TileType<DarkMatterSand>();
                }
                else if (type == ModContent.TileType<Chunkstone>() || type == TileID.Stone || type == TileID.Pearlstone || type == TileID.Crimstone || type == TileID.Ebonstone)
                {
                    tile.TileType = (ushort)ModContent.TileType<DarkMatter>();
                }
                else if (type == ModContent.TileType<Snotsandstone>() || type == TileID.Sandstone || type == TileID.HallowSandstone || type == TileID.CrimsonSandstone || type == TileID.CorruptSandstone)
                {
                    tile.TileType = (ushort)ModContent.TileType<Darksandstone>();
                }
                else if (type == ModContent.TileType<HardenedSnotsand>() || type == TileID.HardenedSand || type == TileID.HallowHardenedSand || type == TileID.CrimsonHardenedSand || type == TileID.CorruptHardenedSand)
                {
                    tile.TileType = (ushort)ModContent.TileType<HardenedDarkSand>();
                }
                else if (type == ModContent.TileType<YellowIce>() || type == TileID.HallowedIce || type == TileID.FleshIce || type == TileID.CorruptIce || type == TileID.IceBlock || type == ModContent.TileType<GreenIce>() || type == ModContent.TileType<BrownIce>())
                {
                    tile.TileType = (ushort)ModContent.TileType<BlackIce>();
                }
                if (TileID.Sets.Conversion.Grass[type] || type == 0)
                {
                    WorldGen.SquareTileFrame(x, y);
                }
            }
        }
        if (tileframe)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                WorldGen.SquareTileFrame(x, y);
            }
            else if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendTileSquare(-1, x, y, 1);
            }
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
}
