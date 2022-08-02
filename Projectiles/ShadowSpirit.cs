using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Avalon.Projectiles;

public class ShadowSpirit : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Shadow Spirit");
    }
    public override void SetDefaults()
    {
        Projectile.penetrate = 1;
        Projectile.width = 24;
        Projectile.height = 24;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.timeLeft = 900;
        DrawOffsetX = -2;
    }
    public float timer;
    public float maxSpeed = 12f + Main.rand.NextFloat(4f);
    public float homeDistance = 400;
    public float homeStrength = 5f;
    public override void AI()
    {
        Vector2 startPosition = Projectile.Center;
        int closest = Projectile.FindClosestNPC(homeDistance, npc => !npc.active || npc.townNPC || npc.dontTakeDamage || npc.lifeMax <= 5 || npc.type == NPCID.TargetDummy || npc.type == NPCID.CultistBossClone || npc.friendly);
        if(closest != -1)
        {
            if (Collision.CanHit(Main.npc[closest], Projectile))
            {
                Vector2 target = Main.npc[closest].Center;
                float distance = Vector2.Distance(target, startPosition);
                Vector2 goTowards = Vector2.Normalize(target - startPosition) * ((homeDistance - distance) / (homeDistance / homeStrength));

                Projectile.velocity += goTowards;

                if (Projectile.velocity.Length() > maxSpeed)
                {
                    Projectile.velocity = Vector2.Normalize(Projectile.velocity) * maxSpeed;
                }
            }
        }

        Projectile.rotation = Projectile.velocity.ToRotation();

        timer++;
        if (timer <= 10)
        {
            Projectile.scale *= 1.03f;
        }
        if (timer >= 10)
        {
            Projectile.scale *= 0.97f;
        }
        if (timer == 20)
        {
            timer = 0;
            Projectile.scale = 1f;
        }

        if(Projectile.timeLeft == 900)
        {
            for (int i = 0; i < 20; i++)
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.GiantCursedSkullBolt, 0f, 0f, 100);
                Main.dust[dust].velocity *= 3f;
                Main.dust[dust].velocity += Projectile.velocity * 0.75f;
                Main.dust[dust].scale *= 1.2f;
                Main.dust[dust].noGravity = true;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.GiantCursedSkullBolt, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100);
            Main.dust[dust].velocity *= 0.2f;
            Main.dust[dust].scale *= 1.4f;
            Main.dust[dust].noGravity = true;
        }
    }
    public override void Kill(int timeLeft)
    {
        for (int i = 0; i < 20; i++)
        {
            int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.GiantCursedSkullBolt, 0f, 0f, 100);
            Main.dust[dust].velocity *= 3f;
            Main.dust[dust].scale *= 1.2f;
            Main.dust[dust].noGravity = true;
        }
    }
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        if(target.life <= 0)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<ShadowPortal>(), 0, default, Main.player[Projectile.owner].whoAmI);
        }
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 255, 100);
    }
}
public class ShadowPortal : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Shadow Spirit");
    }
    public override void SetDefaults()
    {
        Projectile.penetrate = -1;
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.timeLeft = 180;
    }
}
