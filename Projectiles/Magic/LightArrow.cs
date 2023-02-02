using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles.Magic;

public class LightArrow : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("LightArrow");
    }
    public override void SetDefaults()
    {
        Projectile.width = 12;
        Projectile.height = 12;
        Projectile.aiStyle = -1;
        Projectile.penetrate = 2;
        Projectile.scale = 1f;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.extraUpdates = 1;
    }
    public override void AI()
    {
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        Projectile.ai[0]++;
        int dust1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.GlowyDust>(), 0f, 0f, default, Color.Purple, 1f);
        Dust dust2 = Main.dust[dust1];
        dust2.velocity *= 0.5f;
        dust2.noGravity = true;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }
    /*public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
        Rectangle frame = texture.Frame();
        Vector2 frameOrigin = frame.Size() / 2f;
        Vector2 drawPos = Projectile.position - Main.screenPosition + frameOrigin;

        for (int i = 1; i < 4; i++)
        {
            Main.EntitySpriteDraw(texture, drawPos + new Vector2(Projectile.velocity.X * (-i * 2), Projectile.velocity.Y * (-i * 2)), frame, (lightColor * (1 - (i * 0.25f))) * 0.5f, Projectile.rotation * (1 - (i * 0.1f)), frameOrigin, Projectile.scale * (1 - (i * 0.1f)), SpriteEffects.None, 0);
        }
        Main.EntitySpriteDraw(texture, drawPos, frame, lightColor, Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None, 0);
        return false;
    }*/
}
