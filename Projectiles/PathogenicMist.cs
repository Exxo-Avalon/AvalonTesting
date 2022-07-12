using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Projectiles;

public class PathogenicMist : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Pathogenic Mist");
    }
    public override void SetDefaults()
    {
        Projectile.width = 70;
        Projectile.height = 70;
        Projectile.aiStyle = -1;
        Projectile.penetrate = 1;
        Projectile.alpha = 100;
        Projectile.friendly = false;
        Projectile.timeLeft = 720;
        Projectile.ignoreWater = true;
        Projectile.hostile = true;
        Projectile.tileCollide = false;
        Projectile.scale = 0.6f;
        //Projectile.GetGlobalProjectile<AvalonTestingGlobalProjectileInstance>().notReflect = true;
    }

    public override void AI()
    {
        Projectile.ai[0]++;
        if (Projectile.ai[0] > 250)
        {
            Projectile.alpha++;
            if (Projectile.alpha == 255) Projectile.Kill();
        }
        Projectile.velocity *= 0.98f;
        Projectile.rotation += Projectile.velocity.ToRotation() / 500;
        if (Projectile.scale < 0.9f)
        {
            Projectile.scale *= 1.005f;
        }
        if (Main.rand.Next(125) == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                int d = Dust.NewDust(Projectile.position, 8, 8, ModContent.DustType<Dusts.CaesiumDust>());
                Main.dust[d].velocity.X = Main.rand.NextFloat(-2f, 2f);
                Main.dust[d].velocity.Y = Main.rand.NextFloat(-2f, 2f);
            }
        }
        foreach (NPC n in Main.npc)
        {
            if (n.active && n.life > 0 && n.lifeMax > 5 && !n.dontTakeDamage)
            {
                if (n.getRect().Intersects(Projectile.getRect()))
                {
                    n.AddBuff(ModContent.BuffType<Buffs.Virulent>(), 60 * 10);
                }
            }
        }
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        return false;
    }
}
