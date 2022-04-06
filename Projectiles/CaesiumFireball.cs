using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;

namespace AvalonTesting.Projectiles;

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
        //Projectile.GetGlobalProjectile<ExxoAvalonOriginsGlobalProjectileInstance>().notReflect = true;
    }
    public override void AI()
    {
        Projectile.rotation++;
        for (var num917 = 0; num917 < 5; num917++)
        {
            var num918 = Projectile.velocity.X / 3f * num917;
            var num919 = Projectile.velocity.Y / 3f * num917;
            var num920 = 4;
            var num921 = Dust.NewDust(new Vector2(Projectile.position.X + num920, Projectile.position.Y + num920), Projectile.width - num920 * 2, Projectile.height - num920 * 2, ModContent.DustType<Dusts.CaesiumDust>(), 0f, 0f, 100, default, 1.2f);
            Main.dust[num921].noGravity = true;
            Main.dust[num921].velocity *= 0.1f;
            Main.dust[num921].velocity += Projectile.velocity * 0.1f;
            var dust105 = Main.dust[num921];
            dust105.position.X = dust105.position.X - num918;
            var dust106 = Main.dust[num921];
            dust106.position.Y = dust106.position.Y - num919;
        }
    }
    public override void Kill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/Fireball"), (int)Projectile.position.X, (int)Projectile.position.Y);
        Projectile.damage <<= 1;
        Projectile.penetrate = 10;
        Projectile.width <<= 3;
        Projectile.height <<= 3;
        Projectile.Damage();
        for (int i = 0; i < 30; i++)
        {
            float velX = 2f - ((float)Main.rand.Next(20)) / 5f;
            float velY = 2f - ((float)Main.rand.Next(20)) / 5f;
            velX *= 4f;
            velY *= 4f;
            int p = Dust.NewDust(new Vector2(Projectile.position.X - (float)(Projectile.width >> 1), Projectile.position.Y - (float)(Projectile.height >> 1)), Projectile.width, Projectile.height, ModContent.DustType<Dusts.CaesiumDust>(), velX, velY, 160, default, 1.5f);
            Dust.NewDust(new Vector2(Projectile.position.X - (float)(Projectile.width >> 1), Projectile.position.Y - (float)(Projectile.height >> 1)), Projectile.width, Projectile.height, DustID.CursedTorch, velX, velY, 160, default, 1.5f);
        }
    }
}
