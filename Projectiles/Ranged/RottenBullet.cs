using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Projectiles.Ranged;

public class RottenBullet : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Rotten Bullet");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 10 / 12;
        Projectile.timeLeft = 30;
        Projectile.height = dims.Height * 10 / 12 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = 1;
        AIType = ProjectileID.Bullet;
        Projectile.scale = 1f;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Ranged;
    }
}
