using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles.Ranged;

public class XanthophyteArrow : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Xanthophyte Arrow");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.penetrate = 4;
        Projectile.width = dims.Width * 10 / 32;
        Projectile.height = dims.Height * 10 / 32 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Ranged;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        for (int i = 0; i < Main.rand.Next(2, 4); i++)
        {
            float num134 = -Projectile.velocity.X * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 3f;
            float num135 = -Projectile.velocity.Y * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 3f;
            num134 = MathHelper.Clamp(num134, -5f, 5f);
            num135 = MathHelper.Clamp(num135, -10f, 10f);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + num134, Projectile.position.Y + num135, num134, num135, ModContent.ProjectileType<XanthophyteArrowSplit>(), Projectile.damage, 0f, Projectile.owner, 0f, 0f);
        }
        Projectile.Kill();
        return false;
    }
    public override void Kill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        for (int num121 = 0; num121 < 10; num121++)
        {
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.ContagionSpray>(), Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 150, default, 1.2f);
        }
    }
}
