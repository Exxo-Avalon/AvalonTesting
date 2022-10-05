using Avalon.Logic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles.Melee;

public class ElementMetalBeam : ModProjectile
{
    Vector3 DiscoRGB;
    Color RGB;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Elemental Metal Beam");
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
        Projectile.ArmorPenetration = 15;
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
        Lighting.AddLight(Projectile.position, 196f, 142f, 238f);
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
        int num12 = ModContent.DustType<Dusts.ElementMetalDust>();

        DiscoRGB = new Vector3(196f, 142f, 238f);
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
    }
    public override void Kill(int timeLeft)
    {
        DiscoRGB = new Vector3(196f, 142f, 238f);
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
