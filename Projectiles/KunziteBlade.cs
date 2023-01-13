using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles;
public class KunziteBlade : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.JestersArrow);
        Projectile.aiStyle = -1;
        Projectile.alpha = 255;
        Projectile.tileCollide = false;
        Projectile.scale = 1f;
        Projectile.Size = new Vector2(70);
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 255;
    }
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Vorazylcum Katana");
        Main.projFrames[Type] = 1;
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);
        Rectangle frame = texture.Frame();
        Vector2 frameOrigin = frame.Size() / 2f;
        Color col = Color.Lerp(Color.Pink, Color.Magenta, Main.masterColor);
        Vector2 stretchscale = new Vector2(Projectile.scale);


        for (int i = 1; i < Projectile.oldPos.Length; i++)
        {
            col.A = 0;
            Vector2 drawPos = Projectile.oldPos[i] - Main.screenPosition + frameOrigin;
            //int col = (int)(128 - (i * 16) * Projectile.Opacity);
            //Main.EntitySpriteDraw(texture, drawPos, frame, new Color(col / i, col / i, col, 0), Projectile.oldRot[i], frameOrigin, Projectile.scale, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(texture, drawPos, frame, new Color(col.R, col.G - (i * 8), col.B, 0) * (1 - (i * 0.04f)), Projectile.oldRot[i], frameOrigin, new Vector2(stretchscale.X - (i * 0.01f), stretchscale.Y - (i * 0.01f)), SpriteEffects.None, 0);
        }
        col.A = 150;
        //Main.EntitySpriteDraw(texture, Projectile.position - Main.screenPosition + frameOrigin, frame, Color.Lerp(col, Color.White, 0.5f) * Projectile.Opacity, Projectile.rotation, frameOrigin, stretchscale * 1.1f, SpriteEffects.None, 0);

        return false;
    }
    public override void AI()
    {
        Projectile.scale -= 0.03f;
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        if (Projectile.scale <= 0.1f)
            Projectile.Kill();
    }
}
