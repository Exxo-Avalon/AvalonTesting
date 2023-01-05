using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee;
public class Soul : ModProjectile
{
    public bool readyToHome = true;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Soul Edge");
        Main.projFrames[Type] = 1;
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
    }

    public override void SetDefaults()
    {
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = 1;
        Projectile.friendly = true;
        Projectile.extraUpdates = 1;
        Projectile.aiStyle = -1;
        Projectile.width = 24;
        Projectile.height = 28;
        Projectile.alpha = 0;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);
        Rectangle frame = texture.Frame();
        Vector2 frameOrigin = frame.Size() / 2f;
        Color col = Color.Lerp(Color.White, Color.Blue, Main.masterColor);
        col.A = 200;

        Main.EntitySpriteDraw(texture, Projectile.position - Main.screenPosition + frameOrigin, frame, Color.Lerp(col,Color.White,0.5f) * Projectile.Opacity, Projectile.rotation, frameOrigin, new Vector2(Projectile.scale * 1.1f - (Vector2.Distance(Projectile.position, Projectile.oldPosition) * 0.005f), Projectile.scale * 1.1f + (Vector2.Distance(Projectile.position, Projectile.oldPosition) * 0.05f)), SpriteEffects.None, 0);

        for (int i = 1; i < Projectile.oldPos.Length; i++)
        {
            col.A = 0;
            Vector2 drawPos = Projectile.oldPos[i] - Main.screenPosition + frameOrigin;
            //int col = (int)(128 - (i * 16) * Projectile.Opacity);
            //Main.EntitySpriteDraw(texture, drawPos, frame, new Color(col / i, col / i, col, 0), Projectile.oldRot[i], frameOrigin, Projectile.scale, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(texture, drawPos, frame, new Color(col.R / i, col.G / i, col.B / i,0), Projectile.oldRot[i], frameOrigin, Projectile.scale + (i * 0.1f), SpriteEffects.None, 0);
        }
        return false;
    }

    public SoundStyle soul = new SoundStyle($"{nameof(Avalon)}/Sounds/Item/SoulEdgeHitTile")
    {
        Volume = 0.2f,
        Pitch = -0.2f,
        PitchVariance = 0.2f,
        MaxInstances = 10,
    };
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        //SoundEngine.PlaySound(soul, Projectile.position);
        return true;
    }
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        readyToHome = false;
    }
    public override void Kill(int timeLeft)
    {
        SoundEngine.PlaySound(soul, Projectile.position);
        for (int i = 0; i < 15; i++)
        {
            int d = Dust.NewDust(Projectile.position, 8, 8, DustID.DungeonSpirit);
            Main.dust[d].noGravity = true;
            Main.dust[d].velocity *= 1.5f;
            Main.dust[d].scale *= 2.7f;
        }
    }
    public float DegreesToRadians(int degrees)
    {
        return degrees / 57.2957795f;
    }
    public float maxSpeed = 5f + Main.rand.NextFloat(7f);
    public float homeDistance = 1200;
    public float homeStrength = 2f;
    public float homeDelay;
    public override void AI()
    {
        if (Projectile.alpha >= 0)
        {
            Projectile.alpha -= 5;
        }
        if(Projectile.timeLeft <= 60)
        {
            Projectile.scale -= 0.01f;
        }

        // turn the projectile around if it gets too far from the player
        //if (Vector2.Distance(Projectile.position, Main.player[Projectile.owner].position) > 16 * 40 && Projectile.ai[0] < 3)
        //{
        //    Projectile.velocity *= -1;
        //    Projectile.ai[0]++;
        //}
        Projectile.ai[0]++;

        int rn = Main.rand.Next(2);
        if (rn == 0)
            rn = -1;
        float x2 = Projectile.position.X - Projectile.velocity.X / 10f;
        float y2 = Projectile.position.Y - Projectile.velocity.Y / 10f;
        int num27 = Dust.NewDust(new Vector2(x2 + 5, y2 + 5), 16, 16, DustID.DungeonSpirit, 0f, 0f, 0, default, 2f);
        //Main.dust[num27].alpha = Projectile.alpha;
        Main.dust[num27].color.A = 0;
        Main.dust[num27].scale = (255 - Projectile.alpha) / 200;
        //Main.dust[num27].position.X = x2;
        //Main.dust[num27].position.Y = y2;
        Main.dust[num27].velocity = Projectile.velocity * 0.1f;
        Main.dust[num27].noGravity = true;

        Lighting.AddLight(Projectile.Center, 0.5f, 0.2f, 0.9f);
        Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) - 1.57f;

        if (Projectile.ai[0] < 120)
        {
            float num4 = 400f;
            Projectile.velocity = Projectile.velocity.RotatedByRandom(MathHelper.Pi / 30);

            float num383 = Projectile.Center.X;
            float num384 = Projectile.Center.Y;
            float num385 = 500f;
            bool flag = false;
            int num386 = 0;
            for (int num387 = 0; num387 < 200; num387++)
            {
                if (Main.npc[num387].CanBeChasedBy(this) && Projectile.Distance(Main.npc[num387].Center) < num385 && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[num387].Center, 1, 1))
                {
                    float num388 = Main.npc[num387].position.X + Main.npc[num387].width / 2;
                    float num389 = Main.npc[num387].position.Y + Main.npc[num387].height / 2;
                    float num392 = Math.Abs(Projectile.Center.X - num388) + Math.Abs(Projectile.Center.Y - num389);
                    if (num392 < num385)
                    {
                        num385 = num392;
                        num383 = num388;
                        num384 = num389;
                        flag = true;
                        num386 = num387;
                    }
                }
            }
            if (flag)
            {
                float num397 = 6f;
                Vector2 vector22 = Projectile.Center;
                float num398 = num383 - vector22.X;
                float num399 = num384 - vector22.Y;
                float num400 = (float)Math.Sqrt(num398 * num398 + num399 * num399);
                float num401 = num400;
                num400 = num397 / num400;
                num398 *= num400;
                num399 *= num400;
                Projectile.velocity.X = (Projectile.velocity.X * 20f + num398) / 21f;
                Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num399) / 21f;
            }
        }
        else
        {
            if (Vector2.Distance(Projectile.position - Main.screenPosition, Main.MouseScreen) < 5)
            {
                readyToHome = false;
            }
            if (!readyToHome)
            {
                homeDelay++;
                if (homeDelay >= 30)
                {
                    readyToHome = true;
                    homeDelay = 0;
                }
            }
            if (readyToHome)
            {
                Vector2 startPosition = Projectile.position - Main.screenPosition;
                if (Collision.CanHitLine(Projectile.position - Main.screenPosition, Projectile.width, Projectile.height, Main.MouseScreen, 1, 1))
                {
                    //Main.NewText("Mouse Screen: " + Main.MouseScreen);
                    //Main.NewText(Projectile.position / 32);
                    Vector2 target = Main.MouseScreen;
                    float distance = Vector2.Distance(target, startPosition);
                    Vector2 goTowards = Vector2.Normalize(target - startPosition) * ((homeDistance - distance) / (homeDistance / homeStrength));

                    Projectile.velocity += goTowards / 3;
                    maxSpeed -= 0.02f;
                    if (Projectile.velocity.Length() > maxSpeed)
                    {
                        Projectile.velocity = Vector2.Normalize(Projectile.velocity) * maxSpeed;
                    }
                }
            }
        }
    }
}
