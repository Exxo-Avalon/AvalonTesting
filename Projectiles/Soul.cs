using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Projectiles;
public class Soul : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Soul Edge");
        Main.projFrames[Type] = 1;
    }

    public override void SetDefaults()
    {
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = 3;
        Projectile.friendly = true;
        Projectile.extraUpdates = 1;
        Projectile.aiStyle = -1;
        Projectile.width = Projectile.height = 16;
    }
    public float DegreesToRadians(int degrees)
    {
        return degrees / 57.2957795f;
    }
    public override void AI()
    {
        //Projectile.ai[1]++;
        //if (Projectile.ai[1] > 30) Projectile.ai[1] = 1;
        //float num2 = 30f * Projectile.ai[1];
        //float num3 = 6f * Projectile.ai[1];
        float num4 = 400f;
        Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) - 1.57f;
        Lighting.AddLight(Projectile.Center, 0.5f, 0.2f, 0.9f);
        if (Main.player[Projectile.owner].active && !Main.player[Projectile.owner].dead)
        {
            //if (Projectile.Distance(Main.player[Projectile.owner].Center) < num4)
            //{
                if (Projectile.rotation >= DegreesToRadians(-360) && Projectile.rotation < DegreesToRadians(-270))
                {
                    Projectile.ai[0]++;
                    float min = Main.rand.NextFloat(-14f, -12f);
                    float max = Main.rand.NextFloat(12f, 14f);
                    float val = Main.rand.NextFloat(min, max);
                    if (val < 0 && val > -12) val = -12;
                    if (val > 0 && val < 12) val = 12;
                    if (Projectile.ai[1]++ < Math.Abs(val))
                    {
                        Projectile.position.X += 3 * Math.Sign(val);
                    }
                    else Projectile.ai[1] = 0;
                }
                else if (Projectile.rotation >= DegreesToRadians(-270) && Projectile.rotation < DegreesToRadians(-180))
                {
                    Projectile.ai[0]++;
                    float min = Main.rand.NextFloat(-14f, -12f);
                    float max = Main.rand.NextFloat(12f, 14f);
                    float val = Main.rand.NextFloat(min, max);
                    if (val < 0 && val > -12) val = -12;
                    if (val > 0 && val < 12) val = 12;
                    
                    if (Projectile.ai[1]++ < Math.Abs(val))
                    {
                        Projectile.position.Y += 3 * Math.Sign(val);
                    }
                    else Projectile.ai[1] = 0;
                }
                else if (Projectile.rotation >= DegreesToRadians(-180) && Projectile.rotation < DegreesToRadians(-90))
                {
                    Projectile.ai[0]++;
                    float min = Main.rand.NextFloat(-14f, -12f);
                    float max = Main.rand.NextFloat(12f, 14f);
                    float val = Main.rand.NextFloat(min, max);
                    if (val < 0 && val > -12) val = -12;
                    if (val > 0 && val < 12) val = 12;
                    if (Projectile.ai[1]++ < Math.Abs(val))
                    {
                        Projectile.position.X += 3 * Math.Sign(val);
                    }
                    else Projectile.ai[1] = 0;
                }
                else if (Projectile.rotation >= DegreesToRadians(-90) && Projectile.rotation < DegreesToRadians(0))
                {
                    Projectile.ai[0]++;
                    float min = Main.rand.NextFloat(-14f, -12f);
                    float max = Main.rand.NextFloat(12f, 14f);
                    float val = Main.rand.NextFloat(min, max);
                    if (val < 0 && val > -12) val = -12;
                    if (val > 0 && val < 12) val = 12;
                    if (Projectile.ai[1]++ < Math.Abs(val))
                    {
                        Projectile.position.Y += 3 * Math.Sign(val);
                    }
                    else Projectile.ai[1] = 0;
                }
                else if (Projectile.rotation >= DegreesToRadians(0) && Projectile.rotation < DegreesToRadians(90))
                {
                    Projectile.ai[0]++;
                    float min = Main.rand.NextFloat(-14f, -12f);
                    float max = Main.rand.NextFloat(12f, 14f);
                    float val = Main.rand.NextFloat(min, max);
                    if (val < 0 && val > -12) val = -12;
                    if (val > 0 && val < 12) val = 12;
                    if (Projectile.ai[1]++ < Math.Abs(val))
                    {
                        Projectile.position.X += 3 * Math.Sign(val);
                    }
                    else Projectile.ai[1] = 0;
                }
            //}
            //else
            //{
                float num383 = Projectile.Center.X;
                float num384 = Projectile.Center.Y;
                float num385 = 500f;
                bool flag = false;
                int num386 = 0;
                for (int num387 = 0; num387 < 200; num387++)
                {
                    if (Main.npc[num387].CanBeChasedBy(this) && Projectile.Distance(Main.npc[num387].Center) < num385 && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[num387].Center, 1, 1))
                    {
                        float num388 = Main.npc[num387].position.X + (float)(Main.npc[num387].width / 2);
                        float num389 = Main.npc[num387].position.Y + (float)(Main.npc[num387].height / 2);
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
           // }
        }
        else if (Projectile.timeLeft > 30)
        {
            Projectile.timeLeft = 30;
        }
    }
}
