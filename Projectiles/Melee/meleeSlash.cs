using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace Avalon.Projectiles.Melee;

public class meleeSlash : ModProjectile
{
    private Player player => Main.player[Projectile.owner];
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("test");
    }
    public override void SetDefaults()
    {
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.aiStyle = -1;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.scale = 1f;
        Projectile.ownerHitCheck = true;
        Projectile.timeLeft = 120;
    }
    public Vector2 swingRadius = Vector2.Zero;
    public override void AI()
    {
        player.heldProj = Projectile.whoAmI;
        if (Projectile.ai[0] == 0)
        {
            swingRadius = Vector2.Normalize(player.Center - new Vector2(-1, 0)) * 100f;
            //swingRadius = swingRadius.RotatedBy(15 * (MathHelper.Pi / 180));
        }

        swingRadius = swingRadius.RotatedBy(-0.06);

        //rotamount -= 0.035f;

        //rotamount = MathHelper.Clamp(rotamount, 0f, 0.5f);

        /*if (Projectile.ai[0] < 12 && Projectile.ai[0] != 0 && Main.rand.NextBool(2))
        {
            Dust.NewDust(Vector2.Lerp(player.Center, Projectile.Center, 0.6f), Projectile.width, Projectile.height, ModContent.DustType<Dusts.GlowyDust>(), 0f, 0f, default, default, 1f);
            Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.GlowyDust>(), 0f, 0f, default, default, 1f);
        }*/

        Projectile.ai[0]++;

        if (Projectile.ai[0] == 59)
        {
            Projectile.Kill();
        }

        //Projectile.Center = swingRadius + player.Center;

        Projectile.Center = player.RotatedRelativePoint(player.MountedCenter) + swingRadius;

        //Projectile.rotation = Vector2.Normalize(Projectile.Center - player.MountedCenter - new Vector2(0, 5)).ToRotation() + (45 * (MathHelper.Pi / 180));

        //player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation + MathHelper.PiOver4 + MathHelper.Pi);
    }
    /*public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>("Avalon/Projectiles/Melee/test").Value;
        Rectangle frame = texture.Frame();
        Vector2 drawPos = Projectile.Center - Main.screenPosition;

        Main.EntitySpriteDraw(texture, drawPos, frame, new Color(255, 255, 255, 225), Projectile.rotation, texture.Size() / 2f + new Vector2(23f, -23f), Projectile.scale, SpriteEffects.None, 0);
        return false;
    }*/
}
