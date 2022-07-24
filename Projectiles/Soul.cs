using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles;
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
    /*public override void AI()
    {
        if (Projectile.ai[1] == 0f)
        {
            Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt(); // If it is moving right, then set Projectile.direction to 1. If it is moving left, then set Projectile.direction to -1.
            Projectile.rotation = Projectile.velocity.ToRotation(); // Set the rotation based on the velocity.
            Projectile.ai[1] = 1f; // Set Projectile.ai[1] to 1. This is only used to make this section of code run only once.
            Projectile.ai[0] = -Main.rand.Next(30, 80); // Set Projectile.ai[0] to a random number from -30 to -79.
            Projectile.netUpdate = true; // Sync the projectile in a multiplayer game.
        }
        Projectile.ai[0]++;
        Vector2 rotationVector = Projectile.rotation.ToRotationVector2() * 8f; // Get the rotation of the projectile.
        float ySinModifier = (float)Math.Sin((float)Math.PI * 2f * (float)(Main.timeForVisualEffects % 90.0 / 90.0)) * Projectile.direction;
        //float xModifier = (float)Math.Sin((float)Math.PI * 2f * (float)(Main.timeForVisualEffects % 90.0 / 90.0)) * Projectile.direction; // This will make the projectile fly in a sine wave fashion.
        Vector2 newVelocity = rotationVector + new Vector2(Main.WindForVisuals, ySinModifier); // Create a new velocity using the rotation and wind.
        Projectile.velocity = newVelocity.SafeNormalize(Vector2.UnitY) * Projectile.velocity.Length(); // Set the velocity to the value we calculated above.

        // If it is flying normally. i.e. not flying a loop.
        if (true)
        {
            float yModifier = MathHelper.Lerp(0.15f, 0.05f, Math.Abs(Main.WindForVisuals));

            // Half of time, decrease the y velocity a little.
            //if (Projectile.timeLeft % 40 < 20)
            //{
            //    Projectile.velocity.Y -= yModifier;
            //}
            //// The other half of time, increase the y velocity a little.
            //else
            //{
            //    Projectile.velocity.Y += yModifier;
            //}

            // Cap the y velocity so the projectile falls slowly and doesn't rise too quickly.
            // MathHelper.Clamp() allows you to set a minimum and maximum value. In this case, the result will always be between -2f and 2f (inclusive).
            Projectile.velocity.Y = MathHelper.Clamp(Projectile.velocity.Y, -2f, 2f);

            // Set the x velocity.
            // MathHelper.Clamp() allows you to set a minimum and maximum value. In this case, the result will always be between -6f and 6f (inclusive).
            Projectile.velocity.X = MathHelper.Clamp(Projectile.velocity.X + Main.WindForVisuals * 0.006f, -6f, 6f);

            // Switch direction when the current velocity and the oldVelocity have different signs.
            if (Projectile.velocity.X * Projectile.oldVelocity.X < 0f)
            {
                Projectile.direction *= -1; // Reverse the direction
                Projectile.ai[0] = -Main.rand.Next(120, 300); // Set Projectile.ai[0] to a random number from -120 to -599.
                Projectile.netUpdate = true; // Sync the projectile in a multiplayer game.
            }
        }

        // Set the rotation and spriteDirection
        Projectile.rotation = Projectile.velocity.ToRotation();
        Projectile.spriteDirection = Projectile.direction;
    }*/
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
