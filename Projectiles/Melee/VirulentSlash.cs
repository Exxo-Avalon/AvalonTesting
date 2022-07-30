using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee;

public class VirulentSlash : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Virulent Slash");
        Main.projFrames[Projectile.type] = 6;
    }
    public override void SetDefaults()
    {
        Projectile.Size = new Vector2(172, 140);
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.ignoreWater = true;
        Projectile.timeLeft = 30;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 20;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }
    public override bool ShouldUpdatePosition()
    {
        return false;
    }
    public override void AI()
    {
        //Projectile.spriteDirection = Projectile.direction;

        Projectile.rotation = Projectile.velocity.ToRotation();

        Projectile.frameCounter++;
        if (Projectile.frameCounter >= 5)
        {
            Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Projectile.type];
            Projectile.frameCounter = 0;
        }

        if (Projectile.timeLeft > 10)
        {
            int green = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.ContagionSpray>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, default, default, 1f);
            Main.dust[green].noGravity = true;
        }
    }
    public float gloopie = 1;
    public int gloopie2 = 255;
    /*public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);
        Rectangle frame = texture.Frame(1, Main.projFrames[Type], 0, Projectile.frame);
        Vector2 frameOrigin = frame.Size() / 2f;
        Vector2 offset = new Vector2(Projectile.width / 2 - frameOrigin.X, Projectile.height - frame.Height);
        Vector2 drawPos = Projectile.position - Main.screenPosition + frameOrigin + offset;

        gloopie += 0.02f;
        gloopie2 -= 25; // 255 / 15

        Main.EntitySpriteDraw(texture, drawPos, frame, new Color(gloopie2, gloopie2, gloopie2, gloopie2), Projectile.rotation, frameOrigin, Projectile.scale * gloopie, SpriteEffects.None, 0);
        return true;
    }*/
}
