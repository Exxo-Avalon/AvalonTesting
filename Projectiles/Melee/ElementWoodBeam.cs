using Avalon.Logic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System.IO;

namespace Avalon.Projectiles.Melee;

public class ElementWoodBeam : ModProjectile
{
    Vector3 DiscoRGB;
    Color RGB;
    float timer;
    float splitTimes;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Elemental Wood Beam");
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
        Lighting.AddLight(Projectile.position, 104f, 170f, 17f);
        return true;
    }
    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(splitTimes);
        writer.Write(timer);
    }
    public override void ReceiveExtraAI(BinaryReader reader)
    {
        splitTimes = reader.ReadSingle();
        timer = reader.ReadSingle();
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        return true;
    }
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        target.AddBuff(BuffID.Poisoned, 60 * 5);
        target.AddBuff(BuffID.Venom, 60 * 5);
    }
    public override void OnHitPvp(Player target, int damage, bool crit)
    {
        target.AddBuff(BuffID.Poisoned, 60 * 5);
        target.AddBuff(BuffID.Venom, 60 * 5);
    }
    public override void AI()
    {
        int num12 = ModContent.DustType<Dusts.ElementWoodDust>();

        DiscoRGB = new Vector3(104f, 170f, 17f);
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
        //timer++;
        Projectile.ai[0]++;
        if (Projectile.ai[0] > 100)
        {
            Projectile.ai[1]++;
            if (Projectile.ai[1] < 3)
            {
                for (int i = 0; i < 2; i++)
                {
                    float vX = Projectile.velocity.X + Main.rand.Next(-80, 81) * 0.05f;
                    float vY = Projectile.velocity.Y + Main.rand.Next(-80, 81) * 0.05f;

                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position, new Vector2(vX, vY), Type, Projectile.damage, Projectile.knockBack, Projectile.owner, ai1: Projectile.ai[1]);
                    Projectile.active = false;
                }
            }
        }
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
