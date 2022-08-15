using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged;

public class PhantasmalBullet : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Phantasmal Bullet");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = 6;
        Projectile.height = 6;
        Projectile.aiStyle = 1;
        Projectile.friendly = true;
        AIType = ProjectileID.CursedBullet;
        Projectile.penetrate = 2;
        //Projectile.light = 0.8f;
        Projectile.alpha = 255;
        Projectile.scale = 1.2f;
        Projectile.tileCollide = false;
        Projectile.timeLeft = 1200;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 60;
        Projectile.MaxUpdates = 2;
    }
    public override bool PreAI()
    {
        Lighting.AddLight(Projectile.position, 75 / 255f, 15 / 255f, 35 / 255f);
        return true;
    }
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        for (int i = 0; i < 15; i++)
        {
            int d = Dust.NewDust(Projectile.position, 8, 8, DustID.VampireHeal);
            Main.dust[d].noGravity = true;
            Main.dust[d].velocity *= 1.5f;
            Main.dust[d].scale *= 0.7f;
        }
    }
    public override void OnHitPvp(Player target, int damage, bool crit)
    {
        for (int i = 0; i < 15; i++)
        {
            int d = Dust.NewDust(Projectile.position, 8, 8, DustID.VampireHeal);
            Main.dust[d].noGravity = true;
            Main.dust[d].velocity *= 1.5f;
            Main.dust[d].scale *= 0.7f;
        }
    }
    public override void Kill(int timeLeft)
    {
        if (Projectile.penetrate == 1)
        {
            Projectile.maxPenetrate = -1;
            Projectile.penetrate = -1;

            int explosionArea = 60;
            Vector2 oldSize = Projectile.Size;
            Projectile.position = Projectile.Center;
            Projectile.Size += new Vector2(explosionArea);
            Projectile.Center = Projectile.position;

            Projectile.tileCollide = false;
            Projectile.velocity *= 0.01f;
            //Projectile.Damage();
            Projectile.scale = 0.01f;

            Projectile.position = Projectile.Center;
            Projectile.Size = new Vector2(10);
            Projectile.Center = Projectile.position;
        }

        SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/NPC_Killed_6") with { Volume = 0.5f, Pitch = -0.5f }, Projectile.position);
        for (int i = 0; i < 10; i++)
        {
            Dust dust = Dust.NewDustDirect(Projectile.position - Projectile.velocity, Projectile.width, Projectile.height, DustID.VampireHeal, 0, 0, 100, Color.Black, 0.8f);
            dust.noGravity = true;
            dust.velocity *= 1.5f;
            dust.scale *= 0.7f;
            Dust.NewDustDirect(Projectile.position - Projectile.velocity, Projectile.width, Projectile.height, DustID.VampireHeal, 0f, 0f, 100, Color.Black, 0.5f);
        }
        for (int i = 0; i < 10; i++)
        {
            int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.VampireHeal, 0f, 0f, 100, default(Color), 3f);
            Main.dust[dustIndex].noGravity = true;
            Main.dust[dustIndex].velocity *= 1.5f;
            Main.dust[dustIndex].scale *= 0.7f;
            dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.VampireHeal, 0f, 0f, 100, default(Color), 2f);
            Main.dust[dustIndex].velocity *= 1.5f;
            Main.dust[dustIndex].scale *= 0.7f;
        }

        Projectile.position.X += Projectile.width / 2;
        Projectile.position.Y += Projectile.height / 2;
        Projectile.width = 80;
        Projectile.height = 80;
        Projectile.position.X -= Projectile.width / 2;
        Projectile.position.Y -= Projectile.height / 2;
        Projectile.active = false;
    }
    public bool CurveDirectionStart = true;
    public bool CurveDirection;
    public int maxSpeed = 15;
    public override void AI()
    {
        if (Projectile.alpha > 0)
        {
            Projectile.alpha -= 2;
        }
        if (Projectile.alpha < 0)
        {
            Projectile.alpha = 0;
        }
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        if (CurveDirectionStart)
        {
            Projectile.velocity = Projectile.velocity.RotatedBy(0.096f);
            CurveDirectionStart = false;
        }
        if (!CurveDirection && !CurveDirectionStart)
        {
            Projectile.ai[0]++;
            Projectile.velocity = Projectile.velocity.Length() * Projectile.velocity.RotatedBy(-0.024f);
            if (Projectile.ai[0] >= 8)
            {
                CurveDirection = true;
                Projectile.ai[0] = 0;
            }
        }
        if (CurveDirection && !CurveDirectionStart)
        {
            Projectile.ai[0]++;
            Projectile.velocity = Projectile.velocity.Length() * Projectile.velocity.RotatedBy(0.024f);
            if (Projectile.ai[0] >= 8)
            {
                CurveDirection = false;
                Projectile.ai[0] = 0;
            }
        }
        if (Projectile.velocity.Length() > maxSpeed)
        {
            Projectile.velocity = Vector2.Normalize(Projectile.velocity) * maxSpeed;
        }
        //var num308 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.FireworksRGB, 0f, 0f, 100, new Color(140, 20, 40), 1f);
        //Main.dust[num308].noGravity = true;
        //Main.dust[num308].velocity *= 0;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>(Texture + "_Trail").Value;
        Texture2D bulletTex = ModContent.Request<Texture2D>(Texture).Value;
        Rectangle frame = texture.Frame();
        Vector2 frameOrigin = frame.Size() / 2f;
        Vector2 offset = new Vector2(Projectile.width / 2 - frameOrigin.X, Projectile.height - frameOrigin.Y - 4);
        Vector2 drawPos = Projectile.position - Main.screenPosition + frameOrigin + offset;

        for (int i = 0; i < 3; i++)
        {
            Main.EntitySpriteDraw(texture, drawPos + new Vector2(Projectile.velocity.X * -i, Projectile.velocity.Y * -i), frame, new Color(255 - 255 / 7 * i, 0, 0, 100), Projectile.rotation, frameOrigin, Projectile.scale - 0.01f * i, SpriteEffects.None, 0);
        }
        Main.EntitySpriteDraw(bulletTex, drawPos, frame, Color.White, Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None, 0);
        return false;
    }
}
