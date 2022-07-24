using Microsoft.Xna.Framework;
using Terraria;

namespace Avalon.Projectiles;

public static class Utilities
{
    public static int FindMinionTargetNPC(this Projectile projectile)
    {
        Player player = Main.player[projectile.owner];

        if (player.HasMinionAttackTargetNPC)
        {
            NPC npc = Main.npc[player.MinionAttackTargetNPC];
            float distance = Vector2.Distance(npc.Center, projectile.Center);

            // Reasonable distance away so it doesn't target across multiple screens
            if (distance < 2000f)
            {
                return player.MinionAttackTargetNPC;
            }
        }

        return -1;
    }
}
