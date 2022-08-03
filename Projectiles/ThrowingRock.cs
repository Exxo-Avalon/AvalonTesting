using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles;

public class ThrowingRock : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Rock");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width;
        Projectile.height = dims.Height;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.light = 0.9f;
        Projectile.alpha = 0;
        Projectile.scale = 1f;
        Projectile.timeLeft = 600;
        Projectile.tileCollide = true;
    }

    public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
    {
        fallThrough = false;
        return true;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (Projectile.velocity.X != oldVelocity.X)
        {
            Projectile.velocity.X = oldVelocity.X * -0.75f;
        }
        if (Projectile.velocity.Y != oldVelocity.Y && (double)oldVelocity.Y > 1.5)
        {
            Projectile.velocity.Y = oldVelocity.Y * -0.7f;
        }
        return false;
    }
    public override void AI()
    {
        if (Projectile.velocity.Y == 0f)
        {
            Projectile.velocity.X *= 0.94f;
        }
        Projectile.rotation += Projectile.velocity.X * 0.1f;
        Projectile.velocity.Y += 0.2f;
    }
}
