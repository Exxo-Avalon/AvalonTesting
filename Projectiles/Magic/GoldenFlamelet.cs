using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic;

public class GoldenFlamelet : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Golden Flamelet");
    }
    public override void SetDefaults()
    {
        Projectile.width = 8;
        Projectile.height = 8;
        Projectile.scale = 1;
        Projectile.alpha = 100;
        Projectile.aiStyle = 8;
        Projectile.timeLeft = 600;
        Projectile.friendly = true;
        Projectile.penetrate = 5;
        Projectile.light = 0.2f;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.tileCollide = true;
        Projectile.hostile = false;
        Projectile.knockBack = 4;
    }
}
