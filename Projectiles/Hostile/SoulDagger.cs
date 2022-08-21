using Avalon.Dusts;
using Avalon.Items.Weapons.Magic;
using IL.Terraria.Graphics.CameraModifiers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile;

public class SoulDagger : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Soul Dagger");
        Main.projFrames[Projectile.type] = 3;
    }
    public int alpha = 255;
    public override void SetDefaults()
    {
        Projectile.width = 9;
        Projectile.height = 9;
        Projectile.aiStyle = -1;
        Projectile.tileCollide = false;
        Projectile.alpha = 0;
        Projectile.friendly = false;
        Projectile.hostile = true;
        Projectile.scale = 1.3f;
        Projectile.timeLeft = 99999999;
        //Projectile.GetGlobalProjectile<AvalonGlobalProjectileInstance>().notReflect = true;
    }
    public Vector2 towardsBoss;
    public bool isIdle = true;
    public int count;
    public override void AI()
    {
        if (AvalonGlobalNPC.PhantasmBoss != -1)
        {
            float timing = 60 + (Projectile.ai[1] * 20);

            NPC boss = Main.npc[AvalonGlobalNPC.PhantasmBoss];
            Player player = GetClosestTo(Projectile.Center);

            Projectile.frame = Main.rand.Next(3);

            if (boss.ai[0] == 1)
            {
                if (Projectile.ai[0] == 1)
                {
                    Projectile.rotation = Vector2.Normalize(player.Center - Projectile.Center).ToRotation() + MathHelper.PiOver2;
                }
                count++;
                if(count == timing)
                {
                    isIdle = false;
                }
            }
            else
            {
                Projectile.rotation = Vector2.Normalize(boss.Center - Projectile.Center).ToRotation() - MathHelper.PiOver2;
            }

            if (Projectile.ai[0] == 0)
            {
                towardsBoss = Projectile.Center - boss.Center;
                for (int i = 0; i < 20; i++)
                {
                    int num893 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num893].velocity *= 2f;
                    Main.dust[num893].scale = 1.5f;
                    Main.dust[num893].noGravity = true;
                }
                Projectile.ai[0]++;
            }
            if (isIdle)
            {
                towardsBoss = towardsBoss.RotatedBy(0.1);
                Projectile.Center = Vector2.Lerp(Projectile.Center, boss.Center - towardsBoss, 0.5f);
            }
            else
            {
                if (Projectile.ai[0] == 1)
                {
                    Projectile.timeLeft = 50;
                    Projectile.velocity = Vector2.Normalize(player.Center - Projectile.Center) * 35f;
                    Projectile.ai[0]++;
                    for (int i = 0; i < 20; i++)
                    {
                        int num893 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1f);
                        Main.dust[num893].velocity *= 2f;
                        Main.dust[num893].scale = 1.5f;
                        Main.dust[num893].noGravity = true;
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        int num893 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1f);
                        Main.dust[num893].velocity *= 4f;
                        Main.dust[num893].scale = 1.5f;
                        Main.dust[num893].noGravity = true;
                    }
                }
                if (Projectile.timeLeft < 15)
                {
                    alpha -= 20;
                }
            }
        }
        else
        {
            Projectile.Kill();
        }
    }
    static Player GetClosestTo(Vector2 position)
    {
        Player closest = null;
        float closestDistSQ = -1;
        for (int i = 0; i < Main.player.Length - 1; i++)
        {
            Player player = Main.player[i];
            if (player.active && (player.DistanceSQ(position) < closestDistSQ || closestDistSQ == -1))
            {
                closest = player;
                closestDistSQ = player.DistanceSQ(position);
            }
        }
        return closest;
    }
    public int randTex = Main.rand.Next(3);
    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>("Avalon/Projectiles/Hostile/SoulDagger").Value;
        if (randTex == 1)
        {
            texture = ModContent.Request<Texture2D>("Avalon/Projectiles/Hostile/SoulDagger1").Value;
        }
        if (randTex == 2)
        {
            texture = ModContent.Request<Texture2D>("Avalon/Projectiles/Hostile/SoulDagger2").Value;
        }
        Rectangle frame = texture.Frame();
        Vector2 drawPos = Projectile.Center - Main.screenPosition;
        Color color = new Color(alpha, alpha, alpha, (alpha / 4) * 3);
        for (int i = 1; i < 4; i++)
        {
            Main.EntitySpriteDraw(texture, drawPos + new Vector2(Projectile.velocity.X * (-i * 1), Projectile.velocity.Y * (-i * 1)), frame, (color * (1 - (i * 0.25f))) * 0.75f, Projectile.rotation, texture.Size() / 2f - new Vector2(0, 20f), Projectile.scale, SpriteEffects.None, 0);
        }
        Main.EntitySpriteDraw(texture, drawPos, frame, color, Projectile.rotation, texture.Size() / 2f - new Vector2(0, 20f), Projectile.scale, SpriteEffects.None, 0);
        Main.EntitySpriteDraw(texture, drawPos, frame, color * 0.3f, Projectile.rotation, texture.Size() / 2f - new Vector2(0, 14f), Projectile.scale * 1.3f, SpriteEffects.None, 0);
        Main.EntitySpriteDraw(texture, drawPos, frame, color * 0.15f, Projectile.rotation, texture.Size() / 2f - new Vector2(0, 10f), Projectile.scale * 1.6f, SpriteEffects.None, 0);
        return false;
    }
}
