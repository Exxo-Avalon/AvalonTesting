using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles;

public class FocusBeam : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Focus Beam");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 10 / 70;
        Projectile.height = dims.Height * 10 / 70 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.penetrate = 4;
        Projectile.light = 0.6f;
        Projectile.alpha = 0;
        Projectile.scale = 1.2f;
        Projectile.timeLeft = 1200;
        Projectile.DamageType = DamageClass.Magic;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 2f)
        {
            Projectile.position += Projectile.velocity;
            Projectile.Kill();
        }
        else
        {
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
        }
        return false;
    }
    public override void AI()
    {
        Lighting.AddLight(Projectile.position, 0, 255 / 255f, 255 / 255f);
        if (Projectile.ai[0] >= 15f)
        {
            Projectile.ai[0] = 15f;
            Projectile.velocity.Y = Projectile.velocity.Y + 0.1f;
        }
        Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
        if (Projectile.velocity.Y > 16f)
        {
            Projectile.velocity.Y = 16f;
        }
    }
}
