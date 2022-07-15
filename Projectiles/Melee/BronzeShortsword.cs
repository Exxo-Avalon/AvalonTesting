using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Projectiles.Melee;

public class BronzeShortsword : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bronze Shortsword");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.penetrate = -1;
        Projectile.width = 18;
        Projectile.height = 18;
        Projectile.aiStyle = ProjAIStyleID.ShortSword;
        AIType = ProjectileID.TinShortswordStab;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.tileCollide = false;
        Projectile.ownerHitCheck = true;
        Projectile.extraUpdates = 1;
        Projectile.hide = true;
        Projectile.timeLeft = 360;
    }
}
