using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;


namespace Avalon.Projectiles.Melee;

public class test3 : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("test");
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 4;
    }
    public Player player => Main.player[Projectile.owner];
    public int swingTime = 10;
    public override void SetDefaults()
    {
        Projectile.width = 1;
        Projectile.height = 1;
        Projectile.aiStyle = -1;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.alpha = 255;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.scale = 1.5f;
        Projectile.ownerHitCheck = true;
        Projectile.timeLeft = 20;
    }
    public Vector2 swingRadius = Vector2.Zero;
    public bool firstFrame = true;
    public float swordVel;
    public float posY;
    public float rotAmount;
    public double daMath = 1f;
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
        Projectile.ai[0]++;
        /*
        if (Projectile.ai[0] <= swingTime)
        {
            if(Projectile.ai[0] == 1)
            {
                daMath = Math.Log(Projectile.ai[0]) / Math.Log(swingTime);
            }
            else
            {
                daMath = Math.Log(Projectile.ai[0]) / Math.Log(swingTime) - Math.Log((Projectile.ai[0] - 1)) / Math.Log(swingTime);
            }
            swingRadius = swingRadius.RotatedBy(rotAmount * daMath * player.direction * posY);
        }
        */

        if (Projectile.ai[0] >= 7)
        {
            swingRadius /= 1.02f;
            daMath *= 0.75f;
        }

        rotAmount = 0.4f * (float)daMath;

        if(rotAmount < 0.1f)
        {
            rotAmount = 0.1f;
        }

        swingRadius = swingRadius.RotatedBy(rotAmount * player.direction * posY);

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
        Texture2D texture = ModContent.Request<Texture2D>("Avalon/Projectiles/Melee/test4", AssetRequestMode.ImmediateLoad).Value;
        Rectangle frame = texture.Frame();
        Vector2 drawPos = Projectile.Center - Main.screenPosition;
        Vector2 offset = new Vector2(31f, -31f);
        for (int i = 0; i < Projectile.oldPos.Length; i++)
        {
            Vector2 drawPosOld = Projectile.oldPos[i] - Main.screenPosition + Projectile.Size / 2f;
            Main.EntitySpriteDraw(texture, drawPosOld, frame, lightColor * (1 - (i * 0.25f)) * 0.25f, Projectile.oldRot[i], frame.Size() / 2f + offset, Projectile.scale, SpriteEffects.None, 0);
        }
        Main.EntitySpriteDraw(texture, drawPos, frame, lightColor, Projectile.rotation, frame.Size() / 2f + offset, Projectile.scale, SpriteEffects.None, 0);
        return false;
    }
}
