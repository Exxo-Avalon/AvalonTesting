using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile;

public class CaesiumFireball : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Caesium Fireball");
    }

    public override void SetDefaults()
    {
        Projectile.width = 28;
        Projectile.height = 28;
        Projectile.aiStyle = -1;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.alpha = 0;
        Projectile.friendly = false;
        Projectile.hostile = true;
        //Projectile.GetGlobalProjectile<AvalonGlobalProjectileInstance>().notReflect = true;
    }

    public override void AI()
    {
        Projectile.rotation++;
        for (int num917 = 0; num917 < 5; num917++)
        {
            float num918 = Projectile.velocity.X / 3f * num917;
            float num919 = Projectile.velocity.Y / 3f * num917;
            int num920 = 4;
            int num921 = Dust.NewDust(new Vector2(Projectile.position.X + num920, Projectile.position.Y + num920),
                Projectile.width - (num920 * 2), Projectile.height - (num920 * 2), ModContent.DustType<CaesiumDust>(),
                0f, 0f, 100, default, 1.2f);
            Main.dust[num921].noGravity = true;
            Main.dust[num921].velocity *= 0.1f;
            Main.dust[num921].velocity += Projectile.velocity * 0.1f;
            Dust dust105 = Main.dust[num921];
            dust105.position.X = dust105.position.X - num918;
            Dust dust106 = Main.dust[num921];
            dust106.position.Y = dust106.position.Y - num919;
        }
    }

    public override void Kill(int timeLeft)
    {
        SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/Fireball"), Projectile.position);
        Projectile.damage <<= 1;
        Projectile.penetrate = 10;
        Projectile.width <<= 3;
        Projectile.height <<= 3;
        Projectile.Damage();
        for (int i = 0; i < 30; i++)
        {
            float velX = 2f - (Main.rand.Next(20) / 5f);
            float velY = 2f - (Main.rand.Next(20) / 5f);
            velX *= 4f;
            velY *= 4f;
            int p = Dust.NewDust(
                new Vector2(Projectile.position.X - (Projectile.width >> 1),
                    Projectile.position.Y - (Projectile.height >> 1)), Projectile.width, Projectile.height,
                ModContent.DustType<CaesiumDust>(), velX, velY, 160, default, 1.5f);
            Dust.NewDust(
                new Vector2(Projectile.position.X - (Projectile.width >> 1),
                    Projectile.position.Y - (Projectile.height >> 1)), Projectile.width, Projectile.height,
                DustID.CursedTorch, velX, velY, 160, default, 1.5f);
        }
    }
}
