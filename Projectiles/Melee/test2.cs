using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;


namespace Avalon.Projectiles.Melee;

public class test2 : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("test");
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 4;
    }
    public Player player => Main.player[Projectile.owner];
    public int SwingSpeed = 20;
    public override void SetDefaults()
    {
        Projectile.width = 8;
        Projectile.height = 8;
        Projectile.aiStyle = -1;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.alpha = 255;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.scale = 1.5f;
        Projectile.ownerHitCheck = true;
        Projectile.timeLeft = SwingSpeed;
    }
    public Vector2 swingRadius = Vector2.Zero;
    public bool firstFrame = true;
    public float swordVel;
    public float posY;
    public override void AI()
    {
        player.heldProj = Projectile.whoAmI;

        if (firstFrame)
        {
            Vector2 toMouse = Vector2.Normalize(Main.MouseWorld - player.Center) * player.direction;
            posY = player.Center.Y - Projectile.Center.Y;
            posY = MathF.Sign(posY);
            swingRadius = Projectile.Center - player.Center;
            swingRadius = swingRadius.RotatedBy(toMouse.ToRotation());
            firstFrame = false;
        }

        swingRadius = swingRadius.RotatedBy(8f * swordVel / SwingSpeed * player.direction * posY);

        if (Projectile.timeLeft < 10)
        {
            swingRadius *= 0.99f;
        }

        swordVel = MathHelper.Lerp(0f, 1f, Projectile.timeLeft / (float)SwingSpeed);

        if (Main.rand.NextBool(4) && Projectile.timeLeft > 3)
        {
            Vector2 dustVel = Vector2.Normalize(Main.MouseWorld - player.Center) * 0f;
            Dust.NewDust(Projectile.Center, 1, 1, ModContent.DustType<Dusts.GlowyDust>(), dustVel.X, dustVel.Y, default, default, 1f);
            Dust.NewDust(Vector2.Lerp(player.Center, Projectile.Center, 0.6f), 1, 1, ModContent.DustType<Dusts.GlowyDust>(), dustVel.X, dustVel.Y, default, default, 1f);
        }

        Projectile.Center = swingRadius + player.Center;

        Projectile.rotation = Vector2.Normalize(Projectile.Center - player.Center).ToRotation() + (45 * (MathHelper.Pi / 180));
        player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation + MathHelper.PiOver4 + MathHelper.Pi);
    }
    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
        float collisionPoint = 0;
        return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, player.Center, 16, ref collisionPoint);
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>("Avalon/Projectiles/Melee/test", AssetRequestMode.ImmediateLoad).Value;
        Rectangle frame = texture.Frame();
        Vector2 drawPos = Projectile.Center - Main.screenPosition;
        for (int i = 0; i < Projectile.oldPos.Length; i++)
        {
            Vector2 drawPosOld = Projectile.oldPos[i] - Main.screenPosition + Projectile.Size / 2f;
            Main.EntitySpriteDraw(texture, drawPosOld, frame, new Color(255, 255, 255, 225) * (1 - (i * 0.25f)) * 0.25f, Projectile.oldRot[i], frame.Size() / 2f + new Vector2(21f, -21f), Projectile.scale, SpriteEffects.None, 0);
        }
        Main.EntitySpriteDraw(texture, drawPos, frame, new Color(255, 255, 255, 225), Projectile.rotation, frame.Size() / 2f + new Vector2(21f, -21f), Projectile.scale, SpriteEffects.None, 0);
        return false;
    }
}
