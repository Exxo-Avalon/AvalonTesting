using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles.Magic
{
    public class Moonlight : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Moonlight");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {    
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.netUpdate = true;
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 100;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;
            Projectile.ignoreWater = true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, this.Projectile.alpha);
        }
        public override void AI()
        {    
            Projectile.velocity *= 0.98f;

            if(Main.rand.Next(5) == 0)
            {
                int dust1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.GlowyDust>(), 0f, 0f, default, default, 1f);
                Dust dust2 = Main.dust[dust1];
                dust2.velocity *= 0f;
                dust2.noGravity = true;
            }

            if (Projectile.timeLeft == 100)
            {
                for (int i = 0; i < 10; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    Dust d = Dust.NewDustPerfect(Projectile.Center, 68, speed * 1.5f, Scale: 1f);
                    d.noGravity = true;
                }
            }
        }
        public override void PostDraw(Color lightColor)
        {
            int k = 0;

            Color color = new Color(0, 25, 50, 50) * 0.75f * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);

            float scale = Projectile.scale * (float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length * 0.2f;
            Texture2D tex = Mod.Assets.Request<Texture2D>("Sprites/Light").Value;

            Main.EntitySpriteDraw(tex, Projectile.oldPos[k] + Projectile.Size / 2 - Main.screenPosition, null, color, 0, tex.Size() / 2, scale * 4, default, default);
            Main.EntitySpriteDraw(tex, Projectile.oldPos[k] + Projectile.Size / 2 - Main.screenPosition, null, color * 0.3f, 0, tex.Size() / 2, scale * 6, default, default);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[0] += 0.1f;
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 15; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<Dusts.GlowyDust>(), speed * 2, Scale: 1.25f);
                d.noGravity = true;
            }
        }
    }
    /*public class MoonlightWeak : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("MoonlightWeak");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.netUpdate = true;
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 50;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;
            Projectile.ignoreWater = true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, this.Projectile.alpha);
        }
        public override void AI()
        {
            Projectile.velocity *= 0.97f;

            if (Main.rand.Next(5) == 0)
            {
                int dust1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 68, 0f, 0f, default, default, 1f);
                Dust dust2 = Main.dust[dust1];
                dust2.velocity *= 0f;
                dust2.noGravity = true;
            }

            if (Projectile.timeLeft == 50)
            {
                for (int i = 0; i < 10; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    Dust d = Dust.NewDustPerfect(Projectile.Center, 68, speed * 1.5f, Scale: 1f);
                    d.noGravity = true;
                }
            }
        }
        public override void PostDraw(Color lightColor)
        {
            int k = 0;

            Color color = new Color(0, 25, 50, 50) * 0.75f * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);

            float scale = Projectile.scale * (float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length * 0.2f;
            Texture2D tex = Mod.Assets.Request<Texture2D>("Projectiles/Light").Value;

            Main.EntitySpriteDraw(tex, Projectile.oldPos[k] + Projectile.Size / 2 - Main.screenPosition, null, color, 0, tex.Size() / 2, scale * 4, default, default);
            Main.EntitySpriteDraw(tex, Projectile.oldPos[k] + Projectile.Size / 2 - Main.screenPosition, null, color * 0.3f, 0, tex.Size() / 2, scale * 6, default, default);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[0] += 0.1f;
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 15; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, 68, speed * 2, Scale: 1.25f);
                d.noGravity = true;
            }
        }
    }*/
}
