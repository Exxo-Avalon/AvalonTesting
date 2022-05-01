using AvalonTesting.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Logic;

public static class ShadowTeleport
{
    public static void Teleport(int teleportType = 0, bool forceHandle = false, int whoAmI = 0)
    {
        bool syncData = forceHandle || Main.netMode == NetmodeID.SinglePlayer;
        if (syncData)
        {
            TeleportPlayer(teleportType, forceHandle, whoAmI);
        }
        else
        {
            Network.ShadowTeleport.SendPacket(teleportType);
        }
    }

    private static void TeleportPlayer(int teleportType = 0, bool syncData = false, int whoAmI = 0)
    {
        Player player;
        if (!syncData)
        {
            player = Main.LocalPlayer;
        }
        else
        {
            player = Main.player[whoAmI];
        }

        switch (teleportType)
        {
            case 0:
                DungeonTeleport(player, syncData);
                break;

            case 1:
                JungleTeleport(player, syncData);
                break;

            case 2:
                LeftOceanTeleport(player, syncData);
                break;

            case 3:
                RightOceanTeleport(player, syncData);
                break;

            case 4:
                UnderworldTeleport(player, syncData);
                break;
        }
    }

    private static void DungeonTeleport(Player player, bool syncData = false)
    {
        RunTeleport(player, new Vector2(Main.dungeonX, Main.dungeonY), syncData, true);
    }

    private static void LeftOceanTeleport(Player player, bool syncData = false)
    {
        Vector2 previousPos = player.position;
        Vector2 pos = previousPos;
        for (int x = 0; x < 200; ++x)
        {
            for (int y = 0; y < Main.tile.Height; ++y)
            {
                if (Main.tile[x, y] == null)
                {
                    continue;
                }

                if (Main.tile[x, y].TileType != 81)
                {
                    continue;
                }

                pos = new Vector2((x + 1) * 16, (y - 16) * 16);
                break;
            }
        }

        if (pos != previousPos)
        {
            RunTeleport(player, new Vector2(pos.X, pos.Y), syncData);
        }
    }

    private static void RightOceanTeleport(Player player, bool syncData = false)
    {
        Vector2 previousPos = player.position;
        Vector2 pos = previousPos;
        for (int x = Main.maxTilesX - 200; x < Main.maxTilesX - 150; ++x)
        {
            for (int y = 0; y < Main.tile.Height; ++y)
            {
                if (Main.tile[x, y] == null)
                {
                    continue;
                }

                if (Main.tile[x, y].TileType != 81)
                {
                    continue;
                }

                pos = new Vector2((x + 1) * 16, (y - 16) * 16);
                break;
            }
        }

        if (pos != previousPos)
        {
            RunTeleport(player, new Vector2(pos.X, pos.Y), syncData);
        }
    }

    private static void UnderworldTeleport(Player player, bool syncData = false)
    {
        Vector2 previousPos = player.position;
        Vector2 pos = previousPos;
        for (int x = (Main.maxTilesX / 2) - 100; x < (Main.maxTilesX / 2) + 100; ++x)
        {
            for (int y = 0; y < Main.tile.Height; ++y)
            {
                if (Main.tile[x, y] == null)
                {
                    continue;
                }

                if (Main.tile[x, y].TileType != 75)
                {
                    continue;
                }

                pos = new Vector2((x - 3) * 16, (y + 2) * 16);
                break;
            }
        }

        //for (int y = 0; y < Main.tile.GetLength(1); ++y)
        //{
        //    if (Main.tile[Main.maxTilesX / 2, y] == null) continue;
        //    //if (Main.tile[Main.maxTilesX / 2, y].type != 75) continue;
        //    pos = new Vector2(Main.maxTilesX / 2 * 16, (y + 2) * 16);
        //    break;
        //}
        if (pos != previousPos)
        {
            RunTeleport(player, new Vector2(pos.X, pos.Y), syncData);
        }
    }

    private static void JungleTeleport(Player player, bool syncData = false)
    {
        Vector2 previousPos = player.position;
        Vector2 pos = previousPos;
        int startX = ModContent.GetInstance<ExxoWorldGen>().JungleX;
        if (startX < 0)
        {
            startX = 0;
        }

        for (int y = (int)Main.worldSurface - 150; y < Main.worldSurface; y++)
        {
            for (int x = startX; x < startX + 50; x++)
            {
                if (Main.tile[x, y].HasTile && Main.tileSolid[Main.tile[x, y].TileType])
                {
                    pos = new Vector2(x * 16, (y - 2) * 16);
                    break;
                }
            }

            if (pos != previousPos)
            {
                break;
            }
        }

        if (pos != previousPos)
        {
            RunTeleport(player, new Vector2(pos.X, pos.Y), syncData);
        }
    }

    private static void RunTeleport(Player player, Vector2 pos, bool syncData = false, bool convertFromTiles = false)
    {
        bool postImmune = player.immune;
        int postImmuneTime = player.immuneTime;

        if (convertFromTiles)
        {
            pos = new Vector2((pos.X * 16) + 8 - (player.width / 2), (pos.Y * 16) - player.height);
        }

        //Kill hooks
        player.grappling[0] = -1;
        player.grapCount = 0;
        for (int index = 0; index < 1000; ++index)
        {
            if (Main.projectile[index].active && Main.projectile[index].owner == player.whoAmI &&
                Main.projectile[index].aiStyle == 7)
            {
                Main.projectile[index].Kill();
            }
        }

        player.Teleport(pos, 2);
        player.velocity = Vector2.Zero;
        player.immune = postImmune;
        player.immuneTime = postImmuneTime;

        if (Main.netMode != NetmodeID.Server)
        {
            return;
        }

        if (syncData)
        {
            RemoteClient.CheckSection(player.whoAmI, player.position);
            NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, player.whoAmI, pos.X, pos.Y, 3);
        }
    }
}
