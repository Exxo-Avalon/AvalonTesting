using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles.Hostile;

public class EctoBolt : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Ecto Bolt");
    }
    public override void SetDefaults()
    {
        Projectile.width = 10;
        Projectile.height = 10;
        Projectile.aiStyle = -1;
        Projectile.alpha = 255;
        Projectile.hostile = true;
        Projectile.friendly = false;
        Projectile.ignoreWater = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.tileCollide = false;
        Projectile.extraUpdates = 1;
        Projectile.timeLeft = 360;
    }
    public float timer;
    public float maxSpeed = 10f;
    public float homeDistance = 800;
    public float homeStrength = 10f;
    public override void OnHitPlayer(Player target, int damage, bool crit)
    {
        Projectile.Kill();
    }
    public override void AI()
    {
        Projectile.localAI[0]++;
        if (Projectile.localAI[0] == 6f)
        {
            SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
            for (int num166 = 0; num166 < 40; num166++)
            {
                int num167 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.GiantCursedSkullBolt, 0f, 0f, 100);
                Main.dust[num167].velocity *= 3f;
                Main.dust[num167].velocity += Projectile.velocity * 0.75f;
                Main.dust[num167].scale *= 1.2f;
                Main.dust[num167].noGravity = true;
            }
        }
        if (Projectile.localAI[0] > 6f)
        {
            for (int num168 = 0; num168 < 3; num168++)
            {
                int num169 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.GiantCursedSkullBolt, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100);
                Main.dust[num169].velocity *= 0.6f;
                Main.dust[num169].scale *= 1.4f;
                Main.dust[num169].noGravity = true;
            }
        }

        Vector2 startPosition = Projectile.Center;

        //if (Projectile.localAI[0] < 40f)
        {
            foreach (Player plr in Main.player)
            {
                if (plr.active && !plr.dead)
                {
                    if (Vector2.Distance(Projectile.Center, plr.Center) < homeDistance)
                    {
                        Vector2 target = plr.Center;
                        float distance = Vector2.Distance(target, startPosition);
                        Vector2 goTowards = Vector2.Normalize(target - startPosition) * 0.3f;

                        Projectile.velocity += goTowards;

                        if (Projectile.velocity.Length() > maxSpeed)
                        {
                            Projectile.velocity = Vector2.Normalize(Projectile.velocity) * maxSpeed;
                        }
                    }
                }
            }
        }
    }
}
