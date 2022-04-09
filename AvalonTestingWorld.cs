using AvalonTesting.Items.Material;
using AvalonTesting.Items.Placeable.Seed;
using AvalonTesting.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
}
