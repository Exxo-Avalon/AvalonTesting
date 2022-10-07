using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Templates;
public class SwordSwingGeneric : ModProjectile
{
    Color yellow = new Color(180, 160, 60);
    Color lightyellow = new Color(255, 240, 150);
    Color otheryellow = new Color(255, 255, 80);

    public bool CanCutTile { get; set; }
    public override void SetDefaults()
    {
        CanCutTile = true;
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = -1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.ownerHitCheck = true;
        Projectile.ownerHitCheckDistance = 300f;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 30;
        Projectile.alpha = 255;
    }
    public override void AI()
    {
        Projectile.localAI[0]++;
        Player player = Main.player[Projectile.owner];
        float num = Projectile.localAI[0] / Projectile.ai[1];
        float num2 = Projectile.ai[0];
        float num3 = Projectile.velocity.ToRotation();
        Projectile.rotation = (float)Math.PI * num2 * num + num3 + num2 * (float)Math.PI + player.fullRotation;
        float num5 = 0.2f;
        float num6 = 1f;

        Projectile.Center = player.RotatedRelativePoint(player.MountedCenter) - Projectile.velocity;
        Projectile.scale = num6 + num * num5;

        float num8 = Projectile.rotation + Main.rand.NextFloatDirection() * ((float)Math.PI / 2f) * 0.7f;
        Vector2 vector2 = Projectile.Center + num8.ToRotationVector2() * 84f * Projectile.scale;
        Vector2 vector3 = (num8 + Projectile.ai[0] * ((float)Math.PI / 2f)).ToRotationVector2();
        if (Main.rand.NextFloat() * 2f < Projectile.Opacity)
        {
            Dust dust2 = Dust.NewDustPerfect(Projectile.Center + num8.ToRotationVector2() * (Main.rand.NextFloat() * 80f * Projectile.scale + 20f * Projectile.scale), 278, vector3 * 1f, 100, Color.Lerp(Color.Gold, Color.White, Main.rand.NextFloat() * 0.3f), 0.4f);
            dust2.fadeIn = 0.4f + Main.rand.NextFloat() * 0.15f;
            dust2.noGravity = true;
        }
        if (Main.rand.NextFloat() * 1.5f < Projectile.Opacity)
        {
            Dust.NewDustPerfect(vector2, 43, vector3 * 1f, 100, Color.White * Projectile.Opacity, 1.2f * Projectile.Opacity);
        }
        //Projectile.scale *= Projectile.ai[2];
        if (Projectile.localAI[0] >= Projectile.ai[1])
        {
            Projectile.Kill();
        }
    }
    public override bool PreDraw(ref Color lightColor)
    {
        DrawProj_Excalibur(Projectile);
        return true;
    }
    public override bool? CanCutTiles() => CanCutTile;

    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
        float coneLength2 = 90f * Projectile.scale;
        float num3 = 0.5105088f * Projectile.ai[0];
        float maximumAngle2 = (float)Math.PI / 8f;
        float coneRotation2 = Projectile.rotation + num3;
        float num4 = (float)Math.PI / 4f * Projectile.ai[0];
        _ = Projectile.rotation;
        return targetHitbox.IntersectsCone(Projectile.Center, coneLength2, coneRotation2, maximumAngle2);
    }
    private static void DrawPrettyStarSparkle(float opacity, SpriteEffects dir, Vector2 drawpos, Color drawColor, Color shineColor, float flareCounter, float fadeInStart, float fadeInEnd, float fadeOutStart, float fadeOutEnd, float rotation, Vector2 scale, Vector2 fatness)
    {
        Texture2D value = TextureAssets.Extra[98].Value;
        Color color = shineColor * opacity * 0.5f;
        color.A = 0;
        Vector2 origin = value.Size() / 2f;
        Color color2 = drawColor * 0.5f;
        float num = Utils.GetLerpValue(fadeInStart, fadeInEnd, flareCounter, clamped: true) * Utils.GetLerpValue(fadeOutEnd, fadeOutStart, flareCounter, clamped: true);
        Vector2 vector = new Vector2(fatness.X * 0.5f, scale.X) * num;
        Vector2 vector2 = new Vector2(fatness.Y * 0.5f, scale.Y) * num;
        color *= num;
        color2 *= num;
        Main.EntitySpriteDraw(value, drawpos, null, color, (float)Math.PI / 2f + rotation, origin, vector, dir, 0);
        Main.EntitySpriteDraw(value, drawpos, null, color, 0f + rotation, origin, vector2, dir, 0);
        Main.EntitySpriteDraw(value, drawpos, null, color2, (float)Math.PI / 2f + rotation, origin, vector * 0.6f, dir, 0);
        Main.EntitySpriteDraw(value, drawpos, null, color2, 0f + rotation, origin, vector2 * 0.6f, dir, 0);
    }
    public void DrawProj_Excalibur(Projectile proj)
    {
        Vector2 vector = proj.Center - Main.screenPosition;
        Main.NewText(vector);
        Asset<Texture2D> val = ModContent.Request<Texture2D>("Avalon/Projectiles/Melee/VertexSlash");
        Rectangle rectangle = val.Frame(1, 4);
        Vector2 origin = rectangle.Size() / 2f;
        float num = proj.scale * 1.1f;
        SpriteEffects effects = ((!(proj.ai[0] >= 0f)) ? SpriteEffects.FlipVertically : SpriteEffects.None);
        float num2 = proj.localAI[0] / proj.ai[1];
        float num3 = Utils.Remap(num2, 0f, 0.6f, 0f, 1f) * Utils.Remap(num2, 0.6f, 1f, 1f, 0f);
        float num4 = 0.975f;
        float fromValue = Lighting.GetColor(proj.Center.ToTileCoordinates()).ToVector3().Length() / (float)Math.Sqrt(3.0);
        fromValue = Utils.Remap(fromValue, 0.2f, 1f, 0f, 1f);
        Main.spriteBatch.Draw(val.Value, vector, rectangle, yellow * fromValue * num3, proj.rotation + proj.ai[0] * ((float)Math.PI / 4f) * -1f * (1f - num2), origin, num, effects, 0f);
        Color questionmarkcolor = Color.White * num3 * 0.5f;
        questionmarkcolor.A = (byte)(questionmarkcolor.A * (1f - fromValue));
        Color color5 = questionmarkcolor * fromValue * 0.5f;
        color5.G = (byte)(color5.G * fromValue);
        color5.B = (byte)(color5.R * (0.25f + fromValue * 0.75f));
        Main.spriteBatch.Draw(val.Value, vector, rectangle, color5 * 0.15f, proj.rotation + proj.ai[0] * 0.01f, origin, num, effects, 0f);
        Main.spriteBatch.Draw(val.Value, vector, rectangle, otheryellow * fromValue * num3 * 0.3f, proj.rotation, origin, num, effects, 0f);
        Main.spriteBatch.Draw(val.Value, vector, rectangle, lightyellow * fromValue * num3 * 0.5f, proj.rotation, origin, num * num4, effects, 0f);
        Main.spriteBatch.Draw(val.Value, vector, val.Frame(1, 4, 0, 3), Color.White * 0.6f * num3, proj.rotation + proj.ai[0] * 0.01f, origin, num, effects, 0f);
        Main.spriteBatch.Draw(val.Value, vector, val.Frame(1, 4, 0, 3), Color.White * 0.5f * num3, proj.rotation + proj.ai[0] * -0.05f, origin, num * 0.8f, effects, 0f);
        Main.spriteBatch.Draw(val.Value, vector, val.Frame(1, 4, 0, 3), Color.White * 0.4f * num3, proj.rotation + proj.ai[0] * -0.1f, origin, num * 0.6f, effects, 0f);
        for (float num5 = 0f; num5 < 8f; num5++)
        {
            float num6 = proj.rotation + proj.ai[0] * num5 * ((float)Math.PI * -2f) * 0.025f + Utils.Remap(num2, 0f, 1f, 0f, (float)Math.PI / 4f) * proj.ai[0];
            Vector2 drawpos = vector + num6.ToRotationVector2() * (val.Value.Width * 0.5f - 6f) * num;
            float num7 = num5 / 9f;
            DrawPrettyStarSparkle(proj.Opacity, SpriteEffects.None, drawpos, new Color(255, 255, 255, 0) * num3 * num7, otheryellow, num2, 0f, 0.5f, 0.5f, 1f, num6, new Vector2(0f, Utils.Remap(num2, 0f, 1f, 3f, 0f)) * num, Vector2.One * num);
        }
        Vector2 drawpos2 = vector + (proj.rotation + Utils.Remap(num2, 0f, 1f, 0f, (float)Math.PI / 4f) * proj.ai[0]).ToRotationVector2() * (val.Value.Width * 0.5f - 4f) * num;
        DrawPrettyStarSparkle(proj.Opacity, SpriteEffects.None, drawpos2, new Color(255, 255, 255, 0) * num3 * 0.5f, otheryellow, num2, 0f, 0.5f, 0.5f, 1f, 0f, new Vector2(2f, Utils.Remap(num2, 0f, 1f, 4f, 1f)) * num, Vector2.One * num);
    }
}
