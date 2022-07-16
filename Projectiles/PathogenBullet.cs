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
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
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
        //if ((Projectile.type == ModContent.ProjectileType<MissileBolt>() && Projectile.ai[1] < 45f) || (Projectile.type != ModContent.ProjectileType<VileSpit>() && Projectile.type != ModContent.ProjectileType<RottenBullet>() && Projectile.type != ModContent.ProjectileType<PatellaBullet>() && Projectile.type != ModContent.ProjectileType<Soundwave>() && Projectile.type != ModContent.ProjectileType<FeroziumBullet>() && Projectile.type != ModContent.ProjectileType<Electrobullet>() && Projectile.type != ModContent.ProjectileType<SpikeCannon>() && Projectile.type != ModContent.ProjectileType<PathogenBullet>() && Projectile.type != ModContent.ProjectileType<MagmaticBullet>() && Projectile.type != ModContent.ProjectileType<TritonBullet>() && Projectile.type != ModContent.ProjectileType<FocusBeam>() && Projectile.type != ModContent.ProjectileType<VileSpit>() && Projectile.type != ModContent.ProjectileType<InfectiousSpore>()))
        //{
        //    Projectile.ai[0] += 1f;
        //}
        if (Projectile.type == ModContent.ProjectileType<VileSpit>())
        {
            for (var j = 0; j < 2; j++)
            {
                var num19 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 2f), Projectile.width, Projectile.height, DustID.CorruptGibs, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 80, default(Color), 1.3f);
                Main.dust[num19].velocity *= 0.3f;
                Main.dust[num19].noGravity = true;
            }
        }
        if (Projectile.type == ModContent.ProjectileType<KunzitePulseBolt>())
        {
            if (Projectile.alpha < 170)
            {
                for (var n = 0; n < 10; n++)
                {
                    var x = Projectile.position.X - Projectile.velocity.X / 10f * n;
                    var y = Projectile.position.Y - Projectile.velocity.Y / 10f * n;
                    var num25 = Dust.NewDust(new Vector2(x, y), 1, 1, DustID.UnusedWhiteBluePurple, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num25].alpha = Projectile.alpha;
                    Main.dust[num25].position.X = x;
                    Main.dust[num25].position.Y = y;
                    Main.dust[num25].velocity *= 0f;
                    Main.dust[num25].noGravity = true;
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
        }
        else
        {
            if (Projectile.ai[0] >= 15f)
            {
                Projectile.ai[0] = 15f;
                Projectile.velocity.Y = Projectile.velocity.Y + 0.1f;
            }
        }
        Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
        if (Projectile.velocity.Y > 16f)
        {
            Projectile.velocity.Y = 16f;
        }
    }
}
