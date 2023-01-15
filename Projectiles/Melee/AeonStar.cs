using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;

namespace Avalon.Projectiles.Melee;

public class AeonStar : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Aeon Star");
    }
    public override void SetDefaults()
    {
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.aiStyle = -1;
        Projectile.alpha = 0;
        Projectile.penetrate = 2;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.friendly = true;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 60;
        DrawOffsetX = -8;
        DrawOriginOffsetY = -8;
    }
    public override Color? GetAlpha(Color lightColor)
    {
       return Color.Lerp(new Color(255, 0, 128, 128), new Color(0, 200, 256, 128), Main.masterColor);
    }
    public override void OnSpawn(IEntitySource source)
    {
        SoundEngine.PlaySound(SoundID.Item9, Projectile.Center);
    }
    public override void AI()
    {
        Color col = Color.Lerp(new Color(255, 0, 128, 128), new Color(0, 200, 256, 128), Main.masterColor);
        float rotateby = (Projectile.velocity.X < 0) ? -0.15f : 0.15f;
        Projectile.rotation += rotateby;
        int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TintableDustLighted, 0, 0, 0, default, 2);
        Main.dust[d].noGravity = true;
        Main.dust[d].noLight = true;
        Main.dust[d].color = col;
        Main.dust[d].color.A = 0;
        if (Main.rand.NextBool(16))
        {
            int g = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, Main.rand.NextVector2Circular(4, 4), Main.rand.Next(16, 18), Main.rand.NextFloat(0.5f, 1f));
            Main.gore[g].rotation = Projectile.rotation;
        }
    }
    public override void Kill(int timeLeft)
    {
        Color col = Color.Lerp(new Color(255, 0, 128, 128), new Color(0, 200, 256, 128), Main.rand.NextFloat(0,1));
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        for(int i = 0; i < 10; i++)
        {
            int d = Dust.NewDust(Projectile.Center, 0, 0, DustID.TintableDustLighted, 0, 0, 0);
            Main.dust[d].scale = 1;
            Main.dust[d].fadeIn = 1.5f;
            Main.dust[d].noGravity = true;
            Main.dust[d].noLight = true;
            Main.dust[d].color = col;
            Main.dust[d].color.A = 0;
            Main.dust[d].velocity = Main.rand.NextVector2Circular(5, 5);
        }
        //for (int num949 = 4; num949 < 31; num949++)
        //{
        //    float num950 = Projectile.oldVelocity.X * (30f / (float)num949);
        //    float num951 = Projectile.oldVelocity.Y * (30f / (float)num949);
        //    int num952 = Dust.NewDust(new Vector2(Projectile.oldPosition.X - num950, Projectile.oldPosition.Y - num951), 8, 8, DustID.PinkFairy, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 255, default(Color), 1.8f);
        //    Main.dust[num952].noGravity = true;
        //    Dust dust = Main.dust[num952];
        //    dust.velocity *= 0.5f;
        //    num952 = Dust.NewDust(new Vector2(Projectile.oldPosition.X - num950, Projectile.oldPosition.Y - num951), 8, 8, DustID.PinkFairy, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 255, default(Color), 1.4f);
        //    dust = Main.dust[num952];
        //    dust.velocity *= 0.05f;
        //    Main.dust[num952].noGravity = true;
        //}
    }
}
