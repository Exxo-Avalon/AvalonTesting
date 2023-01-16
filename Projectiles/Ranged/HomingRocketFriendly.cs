using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles.Ranged;

public class HomingRocketFriendly : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Homing Rocket");
    }

    public override void SetDefaults()
    {
        Projectile.width = 14;
        Projectile.height = 14;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.penetrate = 1;
    }
    public override void Kill(int timeLeft)
    {
        foreach (NPC P in Main.npc)
        {
            if (P.getRect().Intersects(Projectile.getRect()) && !P.dontTakeDamage && !P.townNPC && P.type != NPCID.TargetDummy)
            {
                P.StrikeNPC(Projectile.damage, Projectile.knockBack, 0);
            }
        }
        SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        Projectile.position.X += Projectile.width / 2;
        Projectile.position.Y += Projectile.height / 2;
        Projectile.width = 22;
        Projectile.height = 22;
        Projectile.position.X -= Projectile.width / 2;
        Projectile.position.Y -= Projectile.height / 2;
        for (int num341 = 0; num341 < 30; num341++)
        {
            int num342 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default(Color), 1.5f);
            Main.dust[num342].velocity *= 1.4f;
        }
        for (int num343 = 0; num343 < 20; num343++)
        {
            int num344 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default(Color), 3.5f);
            Main.dust[num344].noGravity = true;
            Main.dust[num344].velocity *= 7f;
            num344 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default(Color), 1.5f);
            Main.dust[num344].velocity *= 3f;
        }
        for (int num345 = 0; num345 < 2; num345++)
        {
            float scaleFactor8 = 0.4f;
            if (num345 == 1)
            {
                scaleFactor8 = 0.8f;
            }
            int num346 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num346].velocity *= scaleFactor8;
            Main.gore[num346].velocity.X++;
            Main.gore[num346].velocity.Y++;
            num346 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num346].velocity *= scaleFactor8;
            Main.gore[num346].velocity.X--;
            Main.gore[num346].velocity.Y++;
            num346 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num346].velocity *= scaleFactor8;
            Main.gore[num346].velocity.X++;
            Main.gore[num346].velocity.Y--;
            num346 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num346].velocity *= scaleFactor8;
            Main.gore[num346].velocity.X--;
            Main.gore[num346].velocity.Y--;
        }
    }
    public static int HowManyFireDebuffs(int npcIndex)
    {
        int fireDebuffCount = 0;
        for (int i = 0; i < Main.npc[npcIndex].buffType.Length; i++)
        {
            if (Main.npc[npcIndex].buffType[i] == BuffID.OnFire || Main.npc[npcIndex].buffType[i] == BuffID.CursedInferno ||
                Main.npc[npcIndex].buffType[i] == BuffID.OnFire3 || Main.npc[npcIndex].buffType[i] == BuffID.ShadowFlame ||
                Main.npc[npcIndex].buffType[i] == BuffID.Frostburn || Main.npc[npcIndex].buffType[i] == BuffID.Frostburn2 ||
                Main.npc[npcIndex].buffType[i] == ModContent.BuffType<Buffs.Inferno>() ||
                Main.npc[npcIndex].buffType[i] == ModContent.BuffType<Buffs.DarkInferno>())
            {
                fireDebuffCount++;
            }
        }
        return fireDebuffCount;
    }
    public override void AI()
    {
        if (Math.Abs(Projectile.velocity.X) >= 5f || Math.Abs(Projectile.velocity.Y) >= 5f)
        {
            for (int num264 = 0; num264 < 2; num264++)
            {
                float num265 = 0f;
                float num266 = 0f;
                if (num264 == 1)
                {
                    num265 = Projectile.velocity.X * 0.5f;
                    num266 = Projectile.velocity.Y * 0.5f;
                }
                int num267 = Dust.NewDust(new Vector2(Projectile.position.X + 3f + num265, Projectile.position.Y + 3f + num266) - Projectile.velocity * 0.5f, Projectile.width - 8, Projectile.height - 8, DustID.Torch, 0f, 0f, 100, default(Color), 1f);
                Main.dust[num267].scale *= 2f + Main.rand.Next(10) * 0.1f;
                Main.dust[num267].velocity *= 0.2f;
                Main.dust[num267].noGravity = true;
                num267 = Dust.NewDust(new Vector2(Projectile.position.X + 3f + num265, Projectile.position.Y + 3f + num266) - Projectile.velocity * 0.5f, Projectile.width - 8, Projectile.height - 8, DustID.Smoke, 0f, 0f, 100, default(Color), 0.5f);
                Main.dust[num267].fadeIn = 1f + Main.rand.Next(5) * 0.1f;
                Main.dust[num267].velocity *= 0.05f;
            }
        }
        if (Math.Abs(Projectile.velocity.X) < 15f && Math.Abs(Projectile.velocity.Y) < 15f)
        {
            Projectile.velocity *= 1.1f;
        }
        Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
        for (int p = 0; p < Main.npc.Length; p++)
        {
            if (Main.npc[p].active)
            {
                if (ClassExtensions.NewRectVector2(Main.npc[p].position, new Vector2(Main.npc[p].width, Main.npc[p].height)).Intersects(ClassExtensions.NewRectVector2(Projectile.position, new Vector2(Projectile.width, Projectile.height))))
                {
                    Projectile.timeLeft = 3;
                    break;
                }
            }
        }
        if (Projectile.timeLeft <= 3)
        {
            Projectile.position.X += Projectile.width / 2;
            Projectile.position.Y += Projectile.height / 2;
            Projectile.width = 128;
            Projectile.height = 128;
            Projectile.position.X -= Projectile.width / 2;
            Projectile.position.Y -= Projectile.height / 2;
            Projectile.knockBack = 8f;
            Projectile.Kill();
        }
        float num26 = (float)Math.Sqrt(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y);
        float num27 = Projectile.localAI[0];
        if (num27 == 0f)
        {
            Projectile.localAI[0] = num26;
            num27 = num26;
        }
        if (Projectile.alpha > 0)
        {
            Projectile.alpha -= 25;
        }
        if (Projectile.alpha < 0)
        {
            Projectile.alpha = 0;
        }
        var projPosStoredX = Projectile.position.X;
        var projPosStoredY = Projectile.position.Y;
        var distance = 300f;
        var flag = false;
        var npcArrayIndexStored = 0;
        if (Projectile.ai[1] == 0f)
        {
            for (var npcArrayIndex = 0; npcArrayIndex < 200; npcArrayIndex++)
            {
                if (Main.npc[npcArrayIndex].active && !Main.npc[npcArrayIndex].dontTakeDamage && !Main.npc[npcArrayIndex].friendly && Main.npc[npcArrayIndex].lifeMax > 5 && (Projectile.ai[1] == 0f || Projectile.ai[1] == npcArrayIndex + 1))
                {
                    Vector2 npcCenter = Main.npc[npcArrayIndex].Center;
                    if (HowManyFireDebuffs(npcArrayIndex) > 0)
                    {
                        npcCenter = Vector2.Lerp(Main.npc[npcArrayIndex].Center, Projectile.Center, Math.Min(0.5f + HowManyFireDebuffs(npcArrayIndex) * 0.2f, 1f));
                    }
                    var num37 = Math.Abs(Projectile.position.X + Projectile.width / 2 - npcCenter.X) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - npcCenter.Y);
                    if (num37 < distance && Collision.CanHit(new Vector2(Projectile.position.X + Projectile.width / 2, Projectile.position.Y + Projectile.height / 2), 1, 1, Main.npc[npcArrayIndex].position, Main.npc[npcArrayIndex].width, Main.npc[npcArrayIndex].height))
                    {
                        distance = num37;
                        projPosStoredX = npcCenter.X;
                        projPosStoredY = npcCenter.Y;
                        flag = true;
                        npcArrayIndexStored = npcArrayIndex;
                    }
                }
            }
            if (flag)
            {
                Projectile.ai[1] = npcArrayIndexStored + 1;
            }
            flag = false;
        }
        if (Projectile.ai[1] != 0f)
        {
            var npcArrayIndexAgain = (int)(Projectile.ai[1] - 1f);
            if (Main.npc[npcArrayIndexAgain].active)
            {
                var npcCenterX = Main.npc[npcArrayIndexAgain].position.X + Main.npc[npcArrayIndexAgain].width / 2;
                var npcCenterY = Main.npc[npcArrayIndexAgain].position.Y + Main.npc[npcArrayIndexAgain].height / 2;
                var num41 = Math.Abs(Projectile.position.X + Projectile.width / 2 - npcCenterX) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - npcCenterY);
                if (num41 < 1000f)
                {
                    flag = true;
                    projPosStoredX = Main.npc[npcArrayIndexAgain].position.X + Main.npc[npcArrayIndexAgain].width / 2;
                    projPosStoredY = Main.npc[npcArrayIndexAgain].position.Y + Main.npc[npcArrayIndexAgain].height / 2;
                }
            }
        }
        if (flag)
        {
            var num42 = num27;
            var projCenter = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
            var num43 = projPosStoredX - projCenter.X;
            var num44 = projPosStoredY - projCenter.Y;
            var num45 = (float)Math.Sqrt(num43 * num43 + num44 * num44);
            num45 = num42 / num45;
            num43 *= num45;
            num44 *= num45;
            var num46 = 8;
            Projectile.velocity.X = (Projectile.velocity.X * (num46 - 1) + num43) / num46;
            Projectile.velocity.Y = (Projectile.velocity.Y * (num46 - 1) + num44) / num46;
        }
    }
}
