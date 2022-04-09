using AvalonTesting.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting;
public class AvalonTestingWorld : ModSystem
{
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
}
