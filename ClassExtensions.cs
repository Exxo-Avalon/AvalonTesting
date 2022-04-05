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
    public static int FindClosestNPC(this Projectile p, float dist)
    {
        int closest = -1;
        float last = dist;
        for (int i = 0; i < Main.npc.Length; i++)
        {
            NPC n = Main.npc[i];
            if (!n.active || n.townNPC || n.dontTakeDamage)
            {
                continue;
            }

            if (Vector2.Distance(p.Center, n.Center) < last)
            {
                last = Vector2.Distance(p.Center, n.Center);
                closest = i;
            }
        }

        return closest;
    }

    public static ExxoPlayer Avalon(this Player p)
    {
        return p.GetModPlayer<ExxoPlayer>();
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
