using AvalonTesting.Common;
using AvalonTesting.Tiles.Ores;
using Microsoft.Xna.Framework;
using On.Terraria;
using Terraria.Chat;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AvalonTesting.Hooks;

[Autoload(Side = ModSide.Both)]
public class EvilAltar : ModHook
{
    protected override void Apply() => WorldGen.SmashAltar += OnSmashAltar;

    private static void OnSmashAltar(WorldGen.orig_SmashAltar orig, int i, int j)
    {
        if (Terraria.Main.netMode == NetmodeID.MultiplayerClient || !Terraria.Main.hardMode ||
            Terraria.WorldGen.noTileActions || Terraria.WorldGen.gen)
        {
            return;
        }

        int currentOreTier = Terraria.WorldGen.altarCount % 3;
        int num6 = (Terraria.WorldGen.altarCount / 3) + 1;
        float amountOfOreToSpawn = Terraria.Main.maxTilesX / 4200;
        int num8 = 1 - currentOreTier;
        amountOfOreToSpawn = (amountOfOreToSpawn * 310f) - (85 * currentOreTier);
        amountOfOreToSpawn *= 0.85f;
        amountOfOreToSpawn /= num6;
        bool flag = false;

        switch (currentOreTier)
        {
            case 0:
            {
                if (Terraria.WorldGen.SavedOreTiers.Cobalt == -1)
                {
                    flag = true;
                    switch (Terraria.WorldGen.genRand.Next(3))
                    {
                        case 0:
                            Terraria.WorldGen.SavedOreTiers.Cobalt = TileID.Cobalt;
                            break;
                        case 1:
                            Terraria.WorldGen.SavedOreTiers.Cobalt = TileID.Palladium;
                            break;
                        case 2:
                            Terraria.WorldGen.SavedOreTiers.Cobalt = ModContent.TileType<DurataniumOre>();
                            break;
                    }
                }

                if (Terraria.WorldGen.SavedOreTiers.Cobalt == TileID.Palladium ||
                    Terraria.WorldGen.SavedOreTiers.Cobalt == ModContent.TileType<DurataniumOre>())
                {
                    amountOfOreToSpawn *= 0.9f;
                }

                if (Terraria.Main.netMode == NetmodeID.SinglePlayer)
                {
                    if (Terraria.WorldGen.SavedOreTiers.Cobalt == TileID.Cobalt)
                    {
                        Terraria.Main.NewText("Your world has been blessed with Cobalt!", 26, 105, 161);
                    }
                    else if (Terraria.WorldGen.SavedOreTiers.Cobalt == TileID.Palladium)
                    {
                        Terraria.Main.NewText("Your world has been blessed with Palladium!", 235, 87, 47);
                    }
                    else if (Terraria.WorldGen.SavedOreTiers.Cobalt == ModContent.TileType<DurataniumOre>())
                    {
                        Terraria.Main.NewText("Your world has been blessed with Duratanium!", 137, 81, 89);
                    }
                }
                else if (Terraria.Main.netMode == NetmodeID.Server)
                {
                    if (Terraria.WorldGen.SavedOreTiers.Cobalt == TileID.Cobalt)
                    {
                        ChatHelper.BroadcastChatMessage(
                            NetworkText.FromLiteral("Your world has been blessed with Cobalt!"),
                            new Color(26, 105, 161));
                    }
                    else if (Terraria.WorldGen.SavedOreTiers.Cobalt == TileID.Palladium)
                    {
                        ChatHelper.BroadcastChatMessage(
                            NetworkText.FromLiteral("Your world has been blessed with Palladium!"),
                            new Color(235, 87, 47));
                    }
                    else if (Terraria.WorldGen.SavedOreTiers.Cobalt == ModContent.TileType<DurataniumOre>())
                    {
                        ChatHelper.BroadcastChatMessage(
                            NetworkText.FromLiteral("Your world has been blessed with Duratanium!"),
                            new Color(137, 81, 89));
                    }
                }

                currentOreTier = Terraria.WorldGen.SavedOreTiers.Cobalt;
                amountOfOreToSpawn *= 1.05f;
                break;
            }
            case 1:
            {
                if (Terraria.Main.drunkWorld)
                {
                    if (Terraria.WorldGen.SavedOreTiers.Mythril == TileID.Mythril)
                    {
                        Terraria.WorldGen.SavedOreTiers.Mythril = TileID.Orichalcum;
                    }
                    else if (Terraria.WorldGen.SavedOreTiers.Mythril == TileID.Orichalcum)
                    {
                        Terraria.WorldGen.SavedOreTiers.Mythril = ModContent.TileType<NaquadahOre>();
                    }
                    else if (Terraria.WorldGen.SavedOreTiers.Mythril == ModContent.TileType<NaquadahOre>())
                    {
                        Terraria.WorldGen.SavedOreTiers.Mythril = TileID.Mythril;
                    }
                }

                if (Terraria.WorldGen.SavedOreTiers.Mythril == -1)
                {
                    flag = true;
                    switch (Terraria.WorldGen.genRand.Next(3))
                    {
                        case 0:
                            Terraria.WorldGen.SavedOreTiers.Mythril = TileID.Mythril;
                            break;
                        case 1:
                            Terraria.WorldGen.SavedOreTiers.Mythril = TileID.Orichalcum;
                            break;
                        case 2:
                            Terraria.WorldGen.SavedOreTiers.Mythril = ModContent.TileType<NaquadahOre>();
                            break;
                    }
                }

                int num11 = 13;
                if (Terraria.WorldGen.SavedOreTiers.Mythril == TileID.Orichalcum ||
                    Terraria.WorldGen.SavedOreTiers.Mythril == ModContent.TileType<NaquadahOre>())
                {
                    num11 += 9;
                    amountOfOreToSpawn *= 0.9f;
                }

                if (Terraria.Main.netMode == NetmodeID.SinglePlayer)
                {
                    if (Terraria.WorldGen.SavedOreTiers.Mythril == TileID.Mythril)
                    {
                        Terraria.Main.NewText("Your world has been blessed with Mythril!", 93, 147, 88);
                    }
                    else if (Terraria.WorldGen.SavedOreTiers.Mythril == TileID.Orichalcum)
                    {
                        Terraria.Main.NewText("Your world has been blessed with Orichalcum!", 163, 22, 158);
                    }
                    else if (Terraria.WorldGen.SavedOreTiers.Mythril == ModContent.TileType<NaquadahOre>())
                    {
                        Terraria.Main.NewText("Your world has been blessed with Naquadah!", 0, 38);
                    }
                }
                else if (Terraria.Main.netMode == NetmodeID.Server)
                {
                    if (Terraria.WorldGen.SavedOreTiers.Mythril == TileID.Mythril)
                    {
                        ChatHelper.BroadcastChatMessage(
                            NetworkText.FromLiteral("Your world has been blessed with Mythril!"),
                            new Color(93, 147, 88));
                    }
                    else if (Terraria.WorldGen.SavedOreTiers.Mythril == TileID.Orichalcum)
                    {
                        ChatHelper.BroadcastChatMessage(
                            NetworkText.FromLiteral("Your world has been blessed with Orichalcum!"),
                            new Color(163, 22, 158));
                    }
                    else if (Terraria.WorldGen.SavedOreTiers.Mythril == ModContent.TileType<NaquadahOre>())
                    {
                        ChatHelper.BroadcastChatMessage(
                            NetworkText.FromLiteral("Your world has been blessed with Naquadah!"),
                            new Color(0, 38, 255));
                    }
                }

                currentOreTier = Terraria.WorldGen.SavedOreTiers.Mythril;
                break;
            }
            default:
            {
                if (Terraria.Main.drunkWorld)
                {
                    if (Terraria.WorldGen.SavedOreTiers.Cobalt == TileID.Cobalt)
                    {
                        Terraria.WorldGen.SavedOreTiers.Cobalt = TileID.Palladium;
                    }
                    else if (Terraria.WorldGen.SavedOreTiers.Cobalt == TileID.Palladium)
                    {
                        Terraria.WorldGen.SavedOreTiers.Cobalt = ModContent.TileType<DurataniumOre>();
                    }
                    else if (Terraria.WorldGen.SavedOreTiers.Cobalt == ModContent.TileType<DurataniumOre>())
                    {
                        Terraria.WorldGen.SavedOreTiers.Cobalt = TileID.Cobalt;
                    }
                }

                if (Terraria.Main.drunkWorld)
                {
                    if (Terraria.WorldGen.SavedOreTiers.Adamantite == TileID.Adamantite)
                    {
                        Terraria.WorldGen.SavedOreTiers.Adamantite = TileID.Titanium;
                    }
                    else if (Terraria.WorldGen.SavedOreTiers.Adamantite == TileID.Titanium)
                    {
                        Terraria.WorldGen.SavedOreTiers.Adamantite = ModContent.TileType<TroxiniumOre>();
                    }
                    else if (Terraria.WorldGen.SavedOreTiers.Adamantite == ModContent.TileType<TroxiniumOre>())
                    {
                        Terraria.WorldGen.SavedOreTiers.Adamantite = TileID.Adamantite;
                    }
                }

                if (Terraria.WorldGen.SavedOreTiers.Adamantite == -1)
                {
                    flag = true;
                    switch (Terraria.WorldGen.genRand.Next(3))
                    {
                        case 0:
                            Terraria.WorldGen.SavedOreTiers.Adamantite = TileID.Adamantite;
                            break;
                        case 1:
                            Terraria.WorldGen.SavedOreTiers.Adamantite = TileID.Titanium;
                            break;
                        case 2:
                            Terraria.WorldGen.SavedOreTiers.Adamantite = ModContent.TileType<TroxiniumOre>();
                            break;
                    }
                }

                int num9 = 14;
                if (Terraria.WorldGen.SavedOreTiers.Adamantite == 223)
                {
                    num9 += 9;
                    amountOfOreToSpawn *= 0.9f;
                }

                if (Terraria.Main.netMode == NetmodeID.SinglePlayer)
                {
                    if (Terraria.WorldGen.SavedOreTiers.Adamantite == TileID.Adamantite)
                    {
                        Terraria.Main.NewText("Your world has been blessed with Adamantite!", 221, 85, 152);
                    }
                    else if (Terraria.WorldGen.SavedOreTiers.Adamantite == TileID.Titanium)
                    {
                        Terraria.Main.NewText("Your world has been blessed with Titanium!", 185, 194, 215);
                    }
                    else if (Terraria.WorldGen.SavedOreTiers.Adamantite == ModContent.TileType<TroxiniumOre>())
                    {
                        Terraria.Main.NewText("Your world has been blessed with Troxinium!", 193, 218, 72);
                    }
                }
                else if (Terraria.Main.netMode == NetmodeID.Server)
                {
                    if (Terraria.WorldGen.SavedOreTiers.Adamantite == TileID.Adamantite)
                    {
                        ChatHelper.BroadcastChatMessage(
                            NetworkText.FromLiteral("Your world has been blessed with Adamantite!"),
                            new Color(221, 85, 152));
                    }
                    else if (Terraria.WorldGen.SavedOreTiers.Adamantite == TileID.Titanium)
                    {
                        ChatHelper.BroadcastChatMessage(
                            NetworkText.FromLiteral("Your world has been blessed with Titanium!"),
                            new Color(185, 194, 215));
                    }
                    else if (Terraria.WorldGen.SavedOreTiers.Adamantite == ModContent.TileType<TroxiniumOre>())
                    {
                        ChatHelper.BroadcastChatMessage(
                            NetworkText.FromLiteral("Your world has been blessed with Troxinium!"),
                            new Color(193, 218, 72));
                    }
                }

                currentOreTier = Terraria.WorldGen.SavedOreTiers.Adamantite;
                break;
            }
        }

        if (flag)
        {
            Terraria.NetMessage.SendData(MessageID.WorldData);
        }

        for (int k = 0; k < amountOfOreToSpawn; k++)
        {
            int i2 = Terraria.WorldGen.genRand.Next(100, Terraria.Main.maxTilesX - 100);
            double num12 = Terraria.Main.worldSurface;
            if (currentOreTier == TileID.Mythril || currentOreTier == TileID.Orichalcum ||
                currentOreTier == ModContent.TileType<NaquadahOre>())
            {
                num12 = Terraria.Main.rockLayer;
            }

            if (currentOreTier == TileID.Adamantite || currentOreTier == TileID.Titanium ||
                currentOreTier == ModContent.TileType<TroxiniumOre>())
            {
                num12 = (Terraria.Main.rockLayer + Terraria.Main.rockLayer + Terraria.Main.maxTilesY) / 3.0;
            }

            int j2 = Terraria.WorldGen.genRand.Next((int)num12, Terraria.Main.maxTilesY - 150);
            Terraria.WorldGen.OreRunner(i2, j2, Terraria.WorldGen.genRand.Next(5, 9 + num8),
                Terraria.WorldGen.genRand.Next(5, 9 + num8), (ushort)currentOreTier);
        }

        int num13 = Terraria.WorldGen.genRand.Next(3);
        int num2 = 0;
        while (num13 != 2 && num2++ < 1000)
        {
            int num3 = Terraria.WorldGen.genRand.Next(100, Terraria.Main.maxTilesX - 100);
            int num4 = Terraria.WorldGen.genRand.Next((int)Terraria.Main.rockLayer + 50, Terraria.Main.maxTilesY - 300);
            if (!Terraria.Main.tile[num3, num4].HasTile || Terraria.Main.tile[num3, num4].TileType != 1)
            {
                continue;
            }

            if (num13 == 0)
            {
                if (Terraria.WorldGen.crimson)
                {
                    Terraria.Main.tile[num3, num4].TileType = TileID.Crimstone;
                }
                else
                {
                    Terraria.Main.tile[num3, num4].TileType = TileID.Ebonstone;
                }
            }
            else
            {
                Terraria.Main.tile[num3, num4].TileType = TileID.Pearlstone;
            }

            if (Terraria.Main.netMode == 2)
            {
                Terraria.NetMessage.SendTileSquare(-1, num3, num4);
            }

            break;
        }

        if (Terraria.Main.netMode != NetmodeID.MultiplayerClient)
        {
            int num5 = Terraria.Main.rand.Next(2) + 1;
            for (int l = 0; l < num5; l++)
            {
                Terraria.NPC.SpawnOnPlayer(Terraria.Player.FindClosest(new Vector2(i * 16, j * 16), 16, 16), 82);
            }
        }

        Terraria.WorldGen.altarCount++;
        AchievementsHelper.NotifyProgressionEvent(6);
    }
}
