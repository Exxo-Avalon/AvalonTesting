using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles.Ranged
{
    public class CherrybombRocket : ModProjectile
    {
        public bool runOnce = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Firecracker Rocket");
        }
        public override void SetDefaults()
        {
            Projectile.penetrate = -1;
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 60;
            Projectile.scale = 1f;
            DrawOriginOffsetY -= 5;
            Projectile.usesLocalNPCImmunity = true;
        }
        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction;
            Projectile.velocity.Y += 0.075f;
            Projectile.velocity *= 0.995f;
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            if (Projectile.timeLeft == 60)
            {
                for (int num237 = 0; num237 < 10; num237++)
                {
                    int num239 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), 18, 18, DustID.Smoke, Projectile.velocity.X, Projectile.velocity.Y, default, default, 1.5f);
                    Dust dust30 = Main.dust[num239];
                    dust30.noGravity = true;
                    dust30.velocity *= 0.25f;
                }
            }
            if (Main.rand.Next(2) == 0)
            {
                Dust dust0 = Dust.NewDustPerfect(Projectile.Center, DustID.Firework_Red, new Vector2(0, 0), default, default, 1.5f);
                dust0.velocity *= 0f;
                dust0.noGravity = true;
            }
            if (Main.rand.Next(2) == 0)
            {
                Dust dust0 = Dust.NewDustPerfect(Projectile.Center, DustID.Torch, new Vector2(0, 0), default, default, 2f);
                dust0.velocity *= 0f;
                dust0.noGravity = true;
            }
            if (Main.rand.Next(2) == 0)
            {
                Dust dust0 = Dust.NewDustPerfect(Projectile.Center, DustID.Smoke, new Vector2(0, 0), default, default, 1.25f);
                dust0.velocity *= 0f;
                dust0.noGravity = true;
            }
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            if(Projectile.timeLeft <= 2)
            {
                int size = 40;
                hitbox.X -= size;
                hitbox.Y -= size;
                hitbox.Width += size * 2;
                hitbox.Height += size * 2;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if(!runOnce)
            {
                Projectile.timeLeft = 2;
                runOnce = true;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (!runOnce)
            {
                Projectile.timeLeft = 2;
                runOnce = true;
            }
            return false;
        }
        public SoundStyle boom = new SoundStyle("Terraria/Sounds/Item_110")
        {
            Volume = 1f,
            Pitch = -0.5f,
            PitchVariance = 0f,
            MaxInstances = 10,
        };
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(boom, Projectile.Center);
            for (int num237 = 0; num237 < 15; num237++)
            {
                int num239 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), 18, 18, DustID.Firework_Red, 0f, 0f, default, default, 1.5f);
                Dust dust30 = Main.dust[num239];
                dust30.noGravity = false;
                dust30.velocity *= 2.5f;
            }
            for (int num237 = 0; num237 < 5; num237++)
            {
                int num239 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), 18, 18, DustID.Firework_Red, 0f, 0f, default, default, 1f);
                Dust dust30 = Main.dust[num239];
                dust30.noGravity = true;
                dust30.velocity *= 1.5f;
            }
        }
    }
}
