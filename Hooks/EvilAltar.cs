using AvalonTesting.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AvalonTesting.Hooks;

public class EvilAltar
{
    public static void OnSmashAltar(On.Terraria.WorldGen.orig_SmashAltar orig, int i, int j)
    {
        if (Main.netMode == NetmodeID.MultiplayerClient || !Main.hardMode || WorldGen.noTileActions || WorldGen.gen)
        {
            return;
        }
        int currentOreTier = WorldGen.altarCount % 3;
        int num6 = WorldGen.altarCount / 3 + 1;
        float amountOfOreToSpawn = Main.maxTilesX / 4200;
        int num8 = 1 - currentOreTier;
        amountOfOreToSpawn = amountOfOreToSpawn * 310f - (float)(85 * currentOreTier);
        amountOfOreToSpawn *= 0.85f;
        amountOfOreToSpawn /= num6;
        bool flag = false;
        
        switch (currentOreTier)
        {
            case 0:
                {
                    if (WorldGen.SavedOreTiers.Cobalt == -1)
                    {
                        flag = true;
                        switch (WorldGen.genRand.Next(3))
                        {
                            case 0:
                                WorldGen.SavedOreTiers.Cobalt = TileID.Cobalt;
                                break;
                            case 1:
                                WorldGen.SavedOreTiers.Cobalt = TileID.Palladium;
                                break;
                            case 2:
                                WorldGen.SavedOreTiers.Cobalt = ModContent.TileType<Tiles.Ores.DurataniumOre>();
                                break;
                        }
                    }
                    if (WorldGen.SavedOreTiers.Cobalt == TileID.Palladium || WorldGen.SavedOreTiers.Cobalt == ModContent.TileType<Tiles.Ores.DurataniumOre>())
                    {
                        amountOfOreToSpawn *= 0.9f;
                    }
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        if (WorldGen.SavedOreTiers.Cobalt == TileID.Cobalt)
                            Main.NewText("Your world has been blessed with Cobalt!", 26, 105, 161);
                        else if (WorldGen.SavedOreTiers.Cobalt == TileID.Palladium)
                            Main.NewText("Your world has been blessed with Palladium!", 235, 87, 47);
                        else if (WorldGen.SavedOreTiers.Cobalt == ModContent.TileType<Tiles.Ores.DurataniumOre>())
                            Main.NewText("Your world has been blessed with Duratanium!", 137, 81, 89);
                    }
                    else if (Main.netMode == NetmodeID.Server)
                    {
                        if (WorldGen.SavedOreTiers.Cobalt == TileID.Cobalt)
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Your world has been blessed with Cobalt!"), new Color(26, 105, 161));
                        else if (WorldGen.SavedOreTiers.Cobalt == TileID.Palladium)
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Your world has been blessed with Palladium!"), new Color(235, 87, 47));
                        else if (WorldGen.SavedOreTiers.Cobalt == ModContent.TileType<Tiles.Ores.DurataniumOre>())
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Your world has been blessed with Duratanium!"), new Color(137, 81, 89));
                    }
                    currentOreTier = WorldGen.SavedOreTiers.Cobalt;
                    amountOfOreToSpawn *= 1.05f;
                    break;
                }
            case 1:
                {
                    if (Main.drunkWorld)
                    {
                        if (WorldGen.SavedOreTiers.Mythril == TileID.Mythril)
                        {
                            WorldGen.SavedOreTiers.Mythril = TileID.Orichalcum;
                        }
                        else if (WorldGen.SavedOreTiers.Mythril == TileID.Orichalcum)
                        {
                            WorldGen.SavedOreTiers.Mythril = ModContent.TileType<Tiles.Ores.NaquadahOre>();
                        }
                        else if (WorldGen.SavedOreTiers.Mythril == ModContent.TileType<Tiles.Ores.NaquadahOre>())
                        {
                            WorldGen.SavedOreTiers.Mythril = TileID.Mythril;
                        }
                    }
                    if (WorldGen.SavedOreTiers.Mythril == -1)
                    {
                        flag = true;
                        switch (WorldGen.genRand.Next(3))
                        {
                            case 0:
                                WorldGen.SavedOreTiers.Mythril = TileID.Mythril;
                                break;
                            case 1:
                                WorldGen.SavedOreTiers.Mythril = TileID.Orichalcum;
                                break;
                            case 2:
                                WorldGen.SavedOreTiers.Mythril = ModContent.TileType<Tiles.Ores.NaquadahOre>();
                                break;
                        }
                    }
                    int num11 = 13;
                    if (WorldGen.SavedOreTiers.Mythril == TileID.Orichalcum || WorldGen.SavedOreTiers.Mythril == ModContent.TileType<Tiles.Ores.NaquadahOre>())
                    {
                        num11 += 9;
                        amountOfOreToSpawn *= 0.9f;
                    }
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        if (WorldGen.SavedOreTiers.Mythril == TileID.Mythril)
                            Main.NewText("Your world has been blessed with Mythril!", 93, 147, 88);
                        else if (WorldGen.SavedOreTiers.Mythril == TileID.Orichalcum)
                            Main.NewText("Your world has been blessed with Orichalcum!", 163, 22, 158);
                        else if (WorldGen.SavedOreTiers.Mythril == ModContent.TileType<Tiles.Ores.NaquadahOre>())
                            Main.NewText("Your world has been blessed with Naquadah!", 0, 38, 255);
                    }
                    else if (Main.netMode == NetmodeID.Server)
                    {
                        if (WorldGen.SavedOreTiers.Mythril == TileID.Mythril)
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Your world has been blessed with Mythril!"), new Color(93, 147, 88));
                        else if (WorldGen.SavedOreTiers.Mythril == TileID.Orichalcum)
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Your world has been blessed with Orichalcum!"), new Color(163, 22, 158));
                        else if (WorldGen.SavedOreTiers.Mythril == ModContent.TileType<Tiles.Ores.NaquadahOre>())
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Your world has been blessed with Naquadah!"), new Color(0, 38, 255));
                    }
                    currentOreTier = WorldGen.SavedOreTiers.Mythril;
                    break;
                }
            default:
                {
                    if (Main.drunkWorld)
                    {
                        if (WorldGen.SavedOreTiers.Cobalt == TileID.Cobalt)
                        {
                            WorldGen.SavedOreTiers.Cobalt = TileID.Palladium;
                        }
                        else if (WorldGen.SavedOreTiers.Cobalt == TileID.Palladium)
                        {
                            WorldGen.SavedOreTiers.Cobalt = ModContent.TileType<Tiles.Ores.DurataniumOre>();
                        }
                        else if (WorldGen.SavedOreTiers.Cobalt == ModContent.TileType<Tiles.Ores.DurataniumOre>())
                        {
                            WorldGen.SavedOreTiers.Cobalt = TileID.Cobalt;
                        }
                    }
                    if (Main.drunkWorld)
                    {
                        if (WorldGen.SavedOreTiers.Adamantite == TileID.Adamantite)
                        {
                            WorldGen.SavedOreTiers.Adamantite = TileID.Titanium;
                        }
                        else if (WorldGen.SavedOreTiers.Adamantite == TileID.Titanium)
                        {
                            WorldGen.SavedOreTiers.Adamantite = ModContent.TileType<Tiles.Ores.TroxiniumOre>();
                        }
                        else if (WorldGen.SavedOreTiers.Adamantite == ModContent.TileType<Tiles.Ores.TroxiniumOre>())
                        {
                            WorldGen.SavedOreTiers.Adamantite = TileID.Adamantite;
                        }
                    }
                    if (WorldGen.SavedOreTiers.Adamantite == -1)
                    {
                        flag = true;
                        switch (WorldGen.genRand.Next(3))
                        {
                            case 0:
                                WorldGen.SavedOreTiers.Adamantite = TileID.Adamantite;
                                break;
                            case 1:
                                WorldGen.SavedOreTiers.Adamantite = TileID.Titanium;
                                break;
                            case 2:
                                WorldGen.SavedOreTiers.Adamantite = ModContent.TileType<Tiles.Ores.TroxiniumOre>();
                                break;
                        }
                    }
                    int num9 = 14;
                    if (WorldGen.SavedOreTiers.Adamantite == 223)
                    {
                        num9 += 9;
                        amountOfOreToSpawn *= 0.9f;
                    }
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        if (WorldGen.SavedOreTiers.Adamantite == TileID.Adamantite)
                            Main.NewText("Your world has been blessed with Adamantite!", 221, 85, 152);
                        else if (WorldGen.SavedOreTiers.Adamantite == TileID.Titanium)
                            Main.NewText("Your world has been blessed with Titanium!", 185, 194, 215);
                        else if (WorldGen.SavedOreTiers.Adamantite == ModContent.TileType<Tiles.Ores.TroxiniumOre>())
                            Main.NewText("Your world has been blessed with Troxinium!", 193, 218, 72);
                    }
                    else if (Main.netMode == NetmodeID.Server)
                    {
                        if (WorldGen.SavedOreTiers.Adamantite == TileID.Adamantite)
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Your world has been blessed with Adamantite!"), new Color(221, 85, 152));
                        else if (WorldGen.SavedOreTiers.Adamantite == TileID.Titanium)
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Your world has been blessed with Titanium!"), new Color(185, 194, 215));
                        else if (WorldGen.SavedOreTiers.Adamantite == ModContent.TileType<Tiles.Ores.TroxiniumOre>())
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Your world has been blessed with Troxinium!"), new Color(193, 218, 72));
                    }
                    currentOreTier = WorldGen.SavedOreTiers.Adamantite;
                    break;
                }
        }
        if (flag)
        {
            NetMessage.SendData(MessageID.WorldData);
        }
        for (int k = 0; k < amountOfOreToSpawn; k++)
        {
            int i2 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
            double num12 = Main.worldSurface;
            if (currentOreTier == TileID.Mythril || currentOreTier == TileID.Orichalcum || currentOreTier == ModContent.TileType<Tiles.Ores.NaquadahOre>())
            {
                num12 = Main.rockLayer;
            }
            if (currentOreTier == TileID.Adamantite || currentOreTier == TileID.Titanium || currentOreTier == ModContent.TileType<Tiles.Ores.TroxiniumOre>())
            {
                num12 = (Main.rockLayer + Main.rockLayer + Main.maxTilesY) / 3.0;
            }
            int j2 = WorldGen.genRand.Next((int)num12, Main.maxTilesY - 150);
            WorldGen.OreRunner(i2, j2, WorldGen.genRand.Next(5, 9 + num8), WorldGen.genRand.Next(5, 9 + num8), (ushort)currentOreTier);
        }
        int num13 = WorldGen.genRand.Next(3);
        int num2 = 0;
        while (num13 != 2 && num2++ < 1000)
        {
            int num3 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
            int num4 = WorldGen.genRand.Next((int)Main.rockLayer + 50, Main.maxTilesY - 300);
            if (!Main.tile[num3, num4].HasTile || Main.tile[num3, num4].TileType != 1)
            {
                continue;
            }
            if (num13 == 0)
            {
                if (WorldGen.crimson)
                {
                    Main.tile[num3, num4].TileType = TileID.Crimstone;
                }
                else
                {
                    Main.tile[num3, num4].TileType = TileID.Ebonstone;
                }
            }
            else
            {
                Main.tile[num3, num4].TileType = TileID.Pearlstone;
            }
            if (Main.netMode == 2)
            {
                NetMessage.SendTileSquare(-1, num3, num4);
            }
            break;
        }
        if (Main.netMode != NetmodeID.MultiplayerClient)
        {
            int num5 = Main.rand.Next(2) + 1;
            for (int l = 0; l < num5; l++)
            {
                NPC.SpawnOnPlayer(Player.FindClosest(new Vector2(i * 16, j * 16), 16, 16), 82);
            }
        }
        WorldGen.altarCount++;
        AchievementsHelper.NotifyProgressionEvent(6);
    }
}
