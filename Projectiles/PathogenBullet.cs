using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Projectiles;

public class PathogenBullet : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Pathogen Bullet");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 4 / 20;
        Projectile.height = dims.Height * 4 / 20 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = 1;
        Projectile.friendly = true;
        AIType = ProjectileID.Bullet;
        Projectile.penetrate = 1;
        Projectile.light = 0.9f;
        Projectile.alpha = 0;
        Projectile.MaxUpdates = 1;
        Projectile.scale = 1f;
        Projectile.timeLeft = 1200;
        Projectile.DamageType = DamageClass.Ranged;
    }

    public override void AI()
    {
        Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
        if (Projectile.velocity.Y > 16f)
        {
            Projectile.velocity.Y = 16f;
        }
    }
}
