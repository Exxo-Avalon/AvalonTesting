using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged;

public class Electrobullet : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Electrobullet");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 4 / 20;
        Projectile.height = dims.Height * 4 / 20 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = 1;
        Projectile.friendly = true;
        AIType = ProjectileID.Bullet;
        Projectile.penetrate = 1;
        Projectile.light = 0.9f;
        Projectile.alpha = 0;
        Projectile.MaxUpdates = 1;
        Projectile.scale = 1f;
        Projectile.timeLeft = 1200;
        Projectile.DamageType = DamageClass.Ranged;
        AIType = ProjectileID.CursedBullet;
    }
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        target.AddBuff(ModContent.BuffType<Buffs.Electrified>(), 300);
    }
    public override void OnHitPvp(Player target, int damage, bool crit)
    {
        target.AddBuff(ModContent.BuffType<Buffs.Electrified>(), 300);
    }
}
