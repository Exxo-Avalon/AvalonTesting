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
}
