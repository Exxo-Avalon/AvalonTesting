using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Projectiles.Ranged;

public class XanthophyteBullet : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Xanthophyte Bullet");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 4 / 20;
        Projectile.height = dims.Height * 4 / 20 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = 1;
        AIType = ProjectileID.Bullet;
        Projectile.friendly = true;
        Projectile.penetrate = 1;
        Projectile.light = 0.8f;
        Projectile.alpha = 0;
        Projectile.scale = 1.2f;
        Projectile.timeLeft = 1200;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.MaxUpdates = 2;
    }
    public override void Kill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        for (int i = 0; i < 15; i++)
        {
            int d = Dust.NewDust(Projectile.position, 8, 8, ModContent.DustType<Dusts.ContagionSpray>());
            Main.dust[d].noGravity = true;
            Main.dust[d].velocity *= 1.5f;
            Main.dust[d].scale *= 0.7f;
        }
        if (Projectile.owner == Main.myPlayer)
        {
            for (int num133 = 0; num133 < 3; num133++)
            {
                float num134 = -Projectile.velocity.X * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 2f;
                float num135 = -Projectile.velocity.Y * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 2f;
                num134 = MathHelper.Clamp(num134, 4.5f, 6f);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + num134, Projectile.position.Y + num135, num134, num135, ModContent.ProjectileType<XanthophyteBulletSplit>(), Projectile.damage, 0f, Projectile.owner, 0f, 0f);
            }
        }
    }
}
