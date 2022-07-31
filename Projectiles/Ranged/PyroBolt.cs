using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged;

public class PyroBolt : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Pyro Bolt");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.Size = new Vector2(8);
        Projectile.aiStyle = 1;
        AIType = ProjectileID.WoodenArrowFriendly;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Ranged;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public SoundStyle Firework = new SoundStyle("Terraria/Sounds/Item_110")
    {
        Volume = 3f,
        PitchVariance = 0.2f,
        MaxInstances = 10,
    };
    public override void Kill(int timeLeft)
    {
        SoundEngine.PlaySound(Firework, Projectile.position);
    }
    public override void AI()
    {
        var dust = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 1, 1, DustID.Torch, Projectile.velocity.X, Projectile.velocity.Y, 50, default, 2f);
        Main.dust[dust].noGravity = true;
        Main.dust[dust].velocity *= 0.3f;
        if (Main.rand.NextBool(10))
        {
            int num161 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f)),
                Main.rand.Next(61, 64));
            Gore gore30 = Main.gore[num161];
            Gore gore40 = gore30;
            gore40.velocity *= 0.3f;
            gore40.scale = Main.rand.NextFloat(0.5f, 1f);
            gore40.alpha = 100;
            Main.gore[num161].velocity.X += Main.rand.Next(-1, 2);
            Main.gore[num161].velocity.Y += Main.rand.Next(-1, 2);
        }
    }
}
/*
public class PyroBoltExplosion : ModProjectile
{

}
*/
