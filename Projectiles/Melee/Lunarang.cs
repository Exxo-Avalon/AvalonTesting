using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles.Melee;

public class Lunarang : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Lunarang");
    }
    public override void SetDefaults()
    {
        Projectile.Size = new Vector2(22, 30);
        Projectile.aiStyle = 3;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.tileCollide = true;
        Projectile.penetrate = -1;
        Projectile.timeLeft = 9000;
        Projectile.extraUpdates = 1;
    }
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        if (!Main.dayTime)
        {
            for (int i = 0; i < 3; i++)
            {
                int dust = Dust.NewDust(target.position, target.width, target.height, DustID.Shadowflame, 0f, 0f, 100, default, 1.25f);
                Main.dust[dust].velocity *= 3f;
            }
        }
    }
    public override void AI()
    {
        if (Main.rand.NextBool(5))
        {
            int dust = Dust.NewDust(Projectile.position, 22, 30, DustID.Shadowflame, 0f, 0f, 100, default, 1.25f);
            Main.dust[dust].velocity *= 0.4f;
        }
    }
}
