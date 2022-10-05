using Avalon.Logic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System.IO;
using System;

namespace Avalon.Projectiles.Melee;

public class ElementWaterBeam : ModProjectile
{
    Vector3 DiscoRGB;
    Color RGB;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Elemental Water Beam");
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
    public override bool PreAI()
    {
        Lighting.AddLight(Projectile.position, 106f, 210f, 255f);
        return true;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        return true;
    }
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        target.AddBuff(BuffID.Oiled, 60 * 5);
        target.AddBuff(BuffID.Wet, 60 * 5);
    }
    public override void OnHitPvp(Player target, int damage, bool crit)
    {
        target.AddBuff(BuffID.Oiled, 60 * 5);
        target.AddBuff(BuffID.Wet, 60 * 5);
    }
    public override void AI()
    {
        int num12 = ModContent.DustType<Dusts.ElementWaterDust>();

        DiscoRGB = new Vector3(106f, 210f, 255f);
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
        int num = 0;
        Projectile.localAI[num]++;
        Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f + 0.785f;
        float x = Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedBy(Projectile.localAI[num] * ((float)Math.PI / Main.rand.Next(10, 15))).X;
        Vector2 value = Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedBy(1.5707963705062866);
        //Projectile.position += value * x * Main.rand.NextFloat(1f, 1.5f)
        Projectile.position += value * x * Main.rand.NextFloat(5f, 10.5f); //exaggerated sine wave to test trail
    }
    public override void Kill(int timeLeft)
    {
        DiscoRGB = new Vector3((float)Main.DiscoR / 255f, (float)Main.DiscoG / 255f, (float)Main.DiscoB / 255f);
        RGB = new Color(DiscoRGB.X, DiscoRGB.Y, DiscoRGB.Z);

        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        for (int num394 = 4; num394 < 24; num394++)
        {
            float num395 = Projectile.oldVelocity.X * (30f / (float)num394);
            float num396 = Projectile.oldVelocity.Y * (30f / (float)num394);
            int num12 = DustID.RainbowRod;
            int num398 = Dust.NewDust(new Vector2(Projectile.position.X - num395, Projectile.position.Y - num396), 8, 8, num12, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f, 100, RGB, 1.8f);
            Main.dust[num398].velocity *= 1f;
            Main.dust[num398].noGravity = true;
        }
    }
}
