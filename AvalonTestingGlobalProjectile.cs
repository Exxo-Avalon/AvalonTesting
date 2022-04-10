using AvalonTesting.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting;

public class AvalonTestingGlobalProjectile : GlobalProjectile
{
    public override bool CanHitPlayer(Projectile projectile, Player target)
    {
        if (target.HasBuff<BeeSweet>() && projectile.type == ProjectileID.Stinger)
        {
            return false;
        }

        return base.CanHitPlayer(projectile, target);
    }
    public static int FindClosestHostile(Vector2 pos, float dist)
    {
        int closest = -1;
        float last = dist;
        for (int i = 0; i < Main.projectile.Length; i++)
        {
            Projectile p = Main.projectile[i];
            if (!p.active || !p.hostile) continue;
            if (Vector2.Distance(pos, p.Center) < last)
            {
                last = Vector2.Distance(pos, p.Center);
                closest = i;
            }
        }
        return closest;
    }
    public static int howManyProjectiles(int min, int max)
    {
        var output = min;
        for (var i = min; i < max; i++)
        {
            if (Main.rand.Next(2 ^ (max - i)) == 0)
            {
                output++;
            }
        }
        return output;
    }
}
