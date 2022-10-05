using Avalon.Logic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System;
using System.IO;

namespace Avalon.Projectiles.Melee;

public class ElementEarthBeam : ModProjectile
{
    Vector3 DiscoRGB;
    Color RGB;
    public float gravityTimer;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Elemental Earth Beam");
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.aiStyle = 27;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = 5;
        Projectile.light = 0.3f;
        Projectile.friendly = true;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        if (Projectile.localAI[1] >= 15f)
        {
            return new Color(255, 255, 255, Projectile.alpha);
        }
        if (Projectile.localAI[1] < 5f)
        {
            return Color.Transparent;
        }
        int num7 = (int)((Projectile.localAI[1] - 5f) / 10f * 255f);
        return new Color(num7, num7, num7, num7);
    }
    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(gravityTimer);
    }
    public override void ReceiveExtraAI(BinaryReader reader)
    {
        gravityTimer = reader.ReadSingle();
    }
    public override bool PreAI()
    {
        Lighting.AddLight(Projectile.position, 170f, 78f, 17f);
        return true;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        return true;
    }
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        
    }
    public override void OnHitPvp(Player target, int damage, bool crit)
    {
        
    }
    public override void AI()
    {
        int num12 = ModContent.DustType<Dusts.ElementEarthDust>();

        DiscoRGB = new Vector3(170f, 78f, 17f);
        RGB = new Color(DiscoRGB.X, DiscoRGB.Y, DiscoRGB.Z);

        if (Projectile.localAI[1] > 7f)
        {
            var num484 = Dust.NewDust(new Vector2(Projectile.position.X - Projectile.velocity.X * 2f + 2f, Projectile.position.Y + 2f - Projectile.velocity.Y * 2f), 8, 8, num12, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100, RGB, 1.25f);
            Main.dust[num484].velocity *= -0.25f;
            Main.dust[num484].noGravity = true;
            num484 = Dust.NewDust(new Vector2(Projectile.position.X - Projectile.velocity.X * 2f + 2f, Projectile.position.Y + 2f - Projectile.velocity.Y * 2f), 8, 8, num12, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100, RGB, 1.25f);
            Main.dust[num484].velocity *= -0.25f;
            Main.dust[num484].noGravity = true;
            Main.dust[num484].position -= Projectile.velocity * 0.5f;
        }

        int num924 = 25;

        if (Projectile.alpha > 0)
        {
            Projectile.alpha -= num924;
        }
        if (Projectile.alpha < 0)
        {
            Projectile.alpha = 0;
        }
        if (Projectile.ai[0] == 0f)
        {
            {
                Projectile.ai[1]++;
                if (Projectile.ai[1] >= 45f)
                {
                    float num928 = 0.995f;
                    float num929 = 0.15f;
                    Projectile.ai[1] = 45f;
                    Projectile.velocity.X *= num928;
                    Projectile.velocity.Y += num929;
                }
                Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
            }
        }
        if (Projectile.ai[0] == 1f)
        {
            Vector2 center3 = Projectile.Center;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            int num930 = 5 * Projectile.MaxUpdates;
            bool flag39 = false;
            bool flag40 = false;
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] % 30f == 0f)
            {
                flag40 = true;
            }
            int num931 = (int)Projectile.ai[1];
            if (Projectile.localAI[0] >= 60 * num930)
            {
                flag39 = true;
            }
            else if (num931 < 0 || num931 >= 200)
            {
                flag39 = true;
            }
            else if (Main.npc[num931].active && !Main.npc[num931].dontTakeDamage)
            {
                Projectile.Center = Main.npc[num931].Center - Projectile.velocity * 2f;
                Projectile.gfxOffY = Main.npc[num931].gfxOffY;
                if (flag40)
                {
                    Main.npc[num931].HitEffect(0, 1.0);
                }
            }
            else
            {
                flag39 = true;
            }
            if (flag39)
            {
                Projectile.Kill();
            }
        }
        gravityTimer++;
        if (gravityTimer > 5)
        {
            Projectile.velocity.Y += 0.1f;
            if (Projectile.velocity.Y > 16) Projectile.velocity.Y = 16;
            gravityTimer = 0;
        }
        //Lighting.AddLight(Projectile.Center, 0.8f, 0.7f, 0.4f);
    }
    public override void Kill(int timeLeft)
    {
        DiscoRGB = new Vector3((float)Main.DiscoR / 255f, (float)Main.DiscoG / 255f, (float)Main.DiscoB / 255f);
        RGB = new Color(DiscoRGB.X, DiscoRGB.Y, DiscoRGB.Z);

        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        for (int num394 = 4; num394 < 24; num394++)
        {
            float num395 = Projectile.oldVelocity.X * (30f / num394);
            float num396 = Projectile.oldVelocity.Y * (30f / num394);
            int num12 = DustID.RainbowRod;
            int num398 = Dust.NewDust(new Vector2(Projectile.position.X - num395, Projectile.position.Y - num396), 8, 8, num12, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f, 100, RGB, 1.8f);
            Main.dust[num398].velocity *= 1f;
            Main.dust[num398].noGravity = true;
        }
    }
}
