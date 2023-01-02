using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
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
        Projectile.penetrate = 1;
        Projectile.friendly = true;
        Projectile.extraUpdates = 1;
        Projectile.aiStyle = -1;
        Projectile.width = 24;
        Projectile.height = 28;
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
        SoundEngine.PlaySound(soul, Projectile.position);
        return true;
    }
    public override void Kill(int timeLeft)
    {
        
        for (int i = 0; i < 15; i++)
        {
            int d = Dust.NewDust(Projectile.position, 8, 8, DustID.DungeonSpirit);
            Main.dust[d].noGravity = true;
            Main.dust[d].velocity *= 1.5f;
            Main.dust[d].scale *= 0.7f;
        }
    }
    public float DegreesToRadians(int degrees)
    {
        return degrees / 57.2957795f;
    }

    public override void AI()
    {
        // turn the projectile around if it gets too far from the player
        if (Vector2.Distance(Projectile.position, Main.player[Projectile.owner].position) > 16 * 40 && Projectile.ai[0] < 3)
        {
            Projectile.velocity *= -1;
            Projectile.ai[0]++;
        }
        float num4 = 400f;
        Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) - 1.57f;
        Projectile.velocity = Projectile.velocity.RotatedByRandom(MathHelper.Pi / 30);

        for (int num26 = 0; num26 < 8; num26++)
        {
            int rn = Main.rand.Next(2);
            if (rn == 0) rn = -1;
            float x2 = Projectile.position.X - Projectile.velocity.X / 10f * num26;
            float y2 = Projectile.position.Y - Projectile.velocity.Y / 10f * num26;
            int num27 = Dust.NewDust(new Vector2(x2 + 5, y2 + 5), 16, 16, DustID.DungeonSpirit, 0f, 0f, 0, default, 1f);
            Main.dust[num27].alpha = Projectile.alpha;
            Main.dust[num27].color.A = 0;
            Main.dust[num27].scale *= 1f;
            //Main.dust[num27].position.X = x2;
            //Main.dust[num27].position.Y = y2;
            Main.dust[num27].velocity *= 0f;
            Main.dust[num27].noGravity = true;
        }

        Lighting.AddLight(Projectile.Center, 0.5f, 0.2f, 0.9f);

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
}
