using System;
using AvalonTesting.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting;

public static class ClassExtensions
{
    /// <summary>
    ///     Rotate a Vector2.
    /// </summary>
    /// <param name="spinningpoint">The origin.</param>
    /// <param name="radians">The angle in radians to rotate the Vector2 by.</param>
    /// <param name="center"></param>
    /// <returns>Returns the rotated Vector2.</returns>
    public static Vector2 Rotate(this Vector2 spinningpoint, double radians, Vector2 center = default)
    {
        float num = (float)Math.Cos(radians);
        float num2 = (float)Math.Sin(radians);
        Vector2 vector = spinningpoint - center;
        Vector2 result = center;
        result.X += (vector.X * num) - (vector.Y * num2);
        result.Y += (vector.X * num2) + (vector.Y * num);
        return result;
    }
    public static int FindClosestNPC(this Entity entity, float maxDistance, Func<NPC, bool> invalidNPCPredicate)
    {
        int closest = -1;
        float lastDistance = maxDistance;
        for (int i = 0; i < Main.npc.Length; i++)
        {
            NPC npc = Main.npc[i];
            if (invalidNPCPredicate.Invoke(npc))
            {
                continue;
            }

            if (Vector2.Distance(entity.Center, npc.Center) < lastDistance)
            {
                lastDistance = Vector2.Distance(entity.Center, npc.Center);
                closest = i;
            }
        }

        return closest;
    }
    /// <summary>
    /// Helper method for checking if the current item is an armor piece - used for armor prefixes.
    /// </summary>
    /// <param name="item"></param>
    /// <returns>Whether or not the item is an armor piece.</returns>
    public static bool IsArmor(this Item item)
    {
        if (item.headSlot != -1 || item.bodySlot != -1 || item.legSlot != -1)
        {
            return !item.vanity;
        }

        return false;
    }

    public static ExxoPlayer Avalon(this Player p)
    {
        return p.GetModPlayer<ExxoPlayer>();
    }

    public static ExxoBiomePlayer AvalonBiome(this Player p)
    {
        return p.GetModPlayer<ExxoBiomePlayer>();
    }

    public static Asset<Texture2D> GetTexture(this ModTexturedType texturedType)
    {
        return ModContent.Request<Texture2D>(texturedType.Texture);
    }

    public static Rectangle GetDims(this ModTexturedType texturedType)
    {
        return Main.netMode == NetmodeID.Server ? Rectangle.Empty : texturedType.GetTexture().Frame();
    }

    /// <summary>
    ///     A helper method to check if the given Player is touching the ground.
    /// </summary>
    /// <param name="player"></param>
    /// <returns>Returns true if the player is touching the ground.</returns>
    public static bool IsOnGround(this Player player)
    {
        return (Main.tile[(int)(player.position.X / 16f), (int)(player.position.Y / 16f) + 3].HasTile &&
                Main.tileSolid
                    [Main.tile[(int)(player.position.X / 16f), (int)(player.position.Y / 16f) + 3].TileType]) ||
               (Main.tile[(int)(player.position.X / 16f) + 1, (int)(player.position.Y / 16f) + 3].HasTile &&
                Main.tileSolid[
                    Main.tile[(int)(player.position.X / 16f) + 1, (int)(player.position.Y / 16f) + 3].TileType] &&
                player.velocity.Y == 0f);
    }

    public static bool Exists(this Item item)
    {
        return item.type > ItemID.None && item.stack > 0;
    }
}
