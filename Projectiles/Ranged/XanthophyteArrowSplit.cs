using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles.Ranged;

public class XanthophyteArrowSplit : ModProjectile
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
        Projectile.aiStyle = ProjAIStyleID.Arrow;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Ranged;
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
