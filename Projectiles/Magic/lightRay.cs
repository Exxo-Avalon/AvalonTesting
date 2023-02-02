using Avalon.Dusts;
using Avalon.Items.Weapons.Magic;
using IL.Terraria.Graphics.CameraModifiers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Avalon.NPCs.Bosses;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Avalon.Projectiles.Magic;

public class lightRay : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.penetrate = 1;
        Projectile.width = 10;
        Projectile.height = 10;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.timeLeft = 300;
        Projectile.scale = 1f;
    }
    public override void ModifyDamageHitbox(ref Rectangle hitbox)
    {
        int size = Projectile.width * 2;
        hitbox.X -= size / 2;
        hitbox.Y -= size / 2;
        hitbox.Width += size;
        hitbox.Height += size;
    }
    Color randCol;
    public override void AI()
    {
        Projectile.ai[0] += 0.1f;
        Projectile.ai[0] = MathHelper.Clamp(Projectile.ai[0], 0f, 1f);
        Projectile.spriteDirection = Projectile.direction;
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        Projectile.velocity *= Main.rand.NextFloat(1f, 1.2f);
        randCol = new Color(Main.rand.Next(0, 256), Main.rand.Next(0, 256), Main.rand.Next(0, 256), 0);
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>("Avalon/Projectiles/Magic/lightRay").Value;
        int frameHeight = texture.Height / Main.projFrames[Projectile.type];
        Rectangle frame = new Rectangle(0, frameHeight * Projectile.frame, texture.Width, frameHeight);
        Vector2 drawPos = Projectile.Center - Main.screenPosition;

        Main.EntitySpriteDraw(texture, drawPos, frame, randCol * Projectile.ai[0], Projectile.rotation, new Vector2(texture.Width, frameHeight) / 2, new Vector2(Projectile.scale * 0.5f, Projectile.scale * Projectile.velocity.Length() / 15f ) * 0.75f, Projectile.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
        Main.EntitySpriteDraw(texture, drawPos, frame, randCol * Projectile.ai[0] * 0.5f, Projectile.rotation, new Vector2(texture.Width, frameHeight) / 2, new Vector2(Projectile.scale * 0.5f, Projectile.scale * Projectile.velocity.Length() / 15f) * 1.5f, Projectile.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);

        return false;
    }
    public override void Kill(int timeLeft)
    {
        for (int i = 0; i < 10; i++)
        {
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<GlowyDust>(), 0f, 0f, 0, default, 1.5f * Main.rand.NextFloat(0, 3));
        }
    }
}
