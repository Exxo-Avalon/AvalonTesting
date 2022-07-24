using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged;

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
        Projectile.alpha = 255;
        Projectile.scale = 1.2f;
        Projectile.timeLeft = 1200;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.MaxUpdates = 2;
    }
    public override void AI()
    {
        if (Projectile.alpha < 255)
        {
            for (int num26 = 0; num26 < 10; num26++)
            {
                float x2 = Projectile.position.X - Projectile.velocity.X / 10f * num26;
                float y2 = Projectile.position.Y - Projectile.velocity.Y / 10f * num26;
                int num27 = Dust.NewDust(new Vector2(x2, y2), 1, 1, ModContent.DustType<Dusts.ContagionSpray>(), 0f, 0f, 0, default, 1f);
                Main.dust[num27].alpha = Projectile.alpha;
                Main.dust[num27].scale *= 0.7f;
                Main.dust[num27].position.X = x2;
                Main.dust[num27].position.Y = y2;
                Main.dust[num27].velocity *= 0f;
                Main.dust[num27].noGravity = true;
            }
        }
        if (Projectile.alpha > 0)
        {
            Projectile.alpha -= 25;
        }
        if (Projectile.alpha < 0)
        {
            Projectile.alpha = 0;
        }
        Projectile.ai[0] += 2;
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
            for (int num133 = 0; num133 < 4; num133++)
            {
                float num134 = -Projectile.velocity.X * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 3f;
                float num135 = -Projectile.velocity.Y * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 3f;
                num134 = MathHelper.Clamp(num134, -5f, 5f);
                num135 = MathHelper.Clamp(num135, -10f, 10f);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + num134, Projectile.position.Y + num135, num134, num135, ModContent.ProjectileType<XanthophyteBulletSplit>(), Projectile.damage, 0f, Projectile.owner, 0f, 0f);
            }
        }
    }
}
