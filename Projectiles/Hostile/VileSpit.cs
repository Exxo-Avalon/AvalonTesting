using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles.Hostile;

public class VileSpit : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Vile Spit");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width;
        Projectile.height = dims.Height / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.hostile = true;
        Projectile.light = 0f;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.penetrate = 1;
        Projectile.scale = 1f;
        Projectile.tileCollide = true;
    }

    public override void AI()
    {
        if (Projectile.type == ModContent.ProjectileType<Magic.Boomlash>() || Projectile.type == ModContent.ProjectileType<VileSpit>())
        {
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 15;
            }
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
        }
        if (Projectile.type == ModContent.ProjectileType<VileSpit>())
        {
            for (var j = 0; j < 2; j++)
            {
                var num19 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 2f), Projectile.width, Projectile.height, DustID.CorruptGibs, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 80, default(Color), 1.3f);
                Main.dust[num19].velocity *= 0.3f;
                Main.dust[num19].noGravity = true;
            }
        }
        Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
        if (Projectile.velocity.Y > 16f)
        {
            Projectile.velocity.Y = 16f;
        }
    }
    public override void Kill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.NPCDeath9, Projectile.position);
        Projectile.active = false;
    }
}
