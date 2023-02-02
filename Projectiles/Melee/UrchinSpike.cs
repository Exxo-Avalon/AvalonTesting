using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Avalon.Projectiles.Melee;

public class UrchinSpike : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Urchin Spike");
    }
    public override void SetDefaults()
    {
        Projectile.penetrate = 1;
        Projectile.width = 6;
        Projectile.height = 6;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.timeLeft = 60;
        DrawOffsetX = -4;
    }
    public override void AI()
    {
        Projectile.ai[0]++;
        if(Projectile.ai[0] > 3)
        {
            Projectile.damage = 18;
        }
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        Projectile.velocity.Y += 0.3f;
    }
}
