using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee;

public class VirulentExtraScythe : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Virulent Scythe");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = 120;
        Projectile.height = 120;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.ignoreWater = true;
        Projectile.extraUpdates = 0;
        Projectile.timeLeft = 120;
    }
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        if (Main.rand.NextBool(4))
        {
            target.AddBuff(ModContent.BuffType<Buffs.Virulent>(), 60 * 5);
        }
    }
    public override void OnHitPvp(Player target, int damage, bool crit)
    {
        if (Main.rand.NextBool(4))
        {
            target.AddBuff(ModContent.BuffType<Buffs.Virulent>(), 60 * 5);
        }
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 255, 0);
    }

    public override void AI()
    {
        Projectile.ai[0]++;
        if (Projectile.ai[0] > 1)
            Projectile.velocity *= 0f;
        Projectile.rotation -= 0.33f;
        if (Projectile.ai[0] > 10)
            Projectile.Kill();
    }
}
