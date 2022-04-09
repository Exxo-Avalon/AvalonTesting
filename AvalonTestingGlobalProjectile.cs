using AvalonTesting.Buffs;
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
