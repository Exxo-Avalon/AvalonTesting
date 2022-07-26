using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged;

public class PyroscoricBullet : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Pyroscoric Bullet");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 4 / 20;
        Projectile.height = dims.Height * 4 / 20 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = 1;
        AIType = ProjectileID.Bullet;
        Projectile.friendly = true;
        Projectile.penetrate = 1;
        Projectile.light = 0.8f;
        Projectile.alpha = 0;
        Projectile.scale = 1.2f;
        Projectile.timeLeft = 1200;
        Projectile.DamageType = DamageClass.Ranged;
    }
    public override void AI()
    {
        Projectile.ai[1]++;
        if (Projectile.ai[1] % 15 == 0)
        {
            int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Vector2.Zero, ProjectileID.Grenade, Projectile.damage, Projectile.knockBack, Projectile.owner);
            Main.projectile[p].timeLeft = 1;
        }
    }
}
