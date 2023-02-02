using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Avalon.Projectiles.Melee;

public class test : ModProjectile
{
    private Player player => Main.player[Projectile.owner];
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("test");
    }
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
    }
    public Vector2 swingRadius = Vector2.Zero;
    public float rotamount = 0.5f;
    public override void AI()
    {
        player.heldProj = Projectile.whoAmI;
        if (Projectile.ai[0] == 0)
        {
            swingRadius = Vector2.Normalize(player.Center - Main.MouseWorld) * 100f;
            swingRadius = swingRadius.RotatedBy(45 * (MathHelper.Pi / 180) * player.direction);
        }
        //swingRadius = swingRadius.RotatedBy(rotamount * player.direction);
        //rotamount -= 0.035f;

        //rotamount = MathHelper.Clamp(rotamount, 0f, 0.5f);

        if (Projectile.ai[0] < 12 && Projectile.ai[0] != 0 && Main.rand.NextBool(2))
        {
            Dust.NewDust(Vector2.Lerp(player.Center, Projectile.Center, 0.6f), Projectile.width, Projectile.height, ModContent.DustType<Dusts.GlowyDust>(), 0f, 0f, default, default, 1f);
            Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.GlowyDust>(), 0f, 0f, default, default, 1f);
        }

        Projectile.ai[0]++;

        if (Projectile.ai[0] == 15)
        {
            Projectile.Kill();
        }

        //Projectile.Center = swingRadius + player.Center;

        Projectile.Center = player.RotatedRelativePoint(player.MountedCenter) + swingRadius;

        Projectile.rotation = Vector2.Normalize(Projectile.Center - player.MountedCenter - new Vector2(0, 5)).ToRotation() + (45 * (MathHelper.Pi / 180));

        player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation + MathHelper.PiOver4 + MathHelper.Pi);
    }
    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
        float collisionPoint = 0;
        return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, player.Center, 8, ref collisionPoint);
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>("Avalon/Projectiles/Melee/test").Value;
        Rectangle frame = texture.Frame();
        Vector2 drawPos = Projectile.Center - Main.screenPosition;

        Main.EntitySpriteDraw(texture, drawPos, frame, new Color(255, 255, 255, 225), Projectile.rotation, texture.Size() / 2f + new Vector2(23f, -23f), Projectile.scale, SpriteEffects.None, 0);
        return false;
    }
}
