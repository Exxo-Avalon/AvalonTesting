using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System;
using System.IO;

namespace Avalon.Projectiles.Melee;

public class BlahBeam : ModProjectile
{
    private int allowHomingCounter;
    private int tileCollideCounter;
    private int npcIndex;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Blah Beam");
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.aiStyle = 27;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = 2;
        Projectile.alpha = 255;
        Projectile.friendly = true;
        allowHomingCounter = 0;
        npcIndex = 0;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        if (Projectile.localAI[1] >= 15f)
        {
            return new Color(255, 255, 255, Projectile.alpha);
        }
        if (Projectile.localAI[1] < 5f)
        {
            return Color.Transparent;
        }
        int num7 = (int)((Projectile.localAI[1] - 5f) / 10f * 255f);
        return new Color(num7, num7, num7, num7);
    }
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        int randomNum = Main.rand.Next(7);
        if (randomNum == 0) target.AddBuff(20, 300);
        else if (randomNum == 1) target.AddBuff(24, 200);
        else if (randomNum == 2) target.AddBuff(31, 120);
        else if (randomNum == 3) target.AddBuff(39, 300);
        else if (randomNum == 4) target.AddBuff(44, 300);
        else if (randomNum == 5) target.AddBuff(70, 240);
        else if (randomNum == 6) target.AddBuff(69, 300);
        allowHomingCounter = 70;
        npcIndex = 0;
    }
    public static int FindClosest(Vector2 pos, float dist)
    {
        int closest = -1;
        float last = dist;
        for (int i = 0; i < Main.npc.Length; i++)
        {
            NPC N = Main.npc[i];
            if (!N.active || N.townNPC || N.dontTakeDamage) continue;
            if (Vector2.Distance(pos, N.Center) < last)
            {
                last = Vector2.Distance(pos, N.Center);
                closest = i;
            }
            else continue;
        }
        return closest;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (Projectile.type == ModContent.ProjectileType<BlahBeam>())
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            tileCollideCounter++;
            if (tileCollideCounter >= 4f)
            {
                Projectile.position += Projectile.velocity;
                Projectile.Kill();
            }
            else
            {
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
            }
        }
        return false;
    }
    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(allowHomingCounter);
        writer.Write(tileCollideCounter);
        writer.Write(npcIndex);
    }
    public override void ReceiveExtraAI(BinaryReader reader)
    {
        allowHomingCounter = reader.ReadInt32();
        tileCollideCounter = reader.ReadInt32();
        npcIndex = reader.ReadInt32();
    }
    public override void AI()
    {
        Lighting.AddLight(Projectile.position, 255 / 255f, 175 / 255f, 0);
        //int closest = Projectile.FindClosestNPC(16 * 20, npc => !npc.active || npc.townNPC || npc.dontTakeDamage || npc.lifeMax <= 5 || npc.type == NPCID.TargetDummy || npc.type == NPCID.CultistBossClone);
        //if (closest != -1)
        //{
        //    if (Main.npc[closest].lifeMax > 5 && !Main.npc[closest].friendly && !Main.npc[closest].townNPC)
        //    {
        //        Vector2 v = Main.npc[closest].position;
        //        if (Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, v, Main.npc[closest].width, Main.npc[closest].height))
        //        {
        //            Projectile.velocity = Vector2.Normalize(v - Projectile.position) * 13f;
        //        }
        //    }
        //}
        Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 0.785f;
        allowHomingCounter--;
        var num28 = (float)Math.Sqrt(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y);
        var num29 = Projectile.localAI[0];
        if (num29 == 0f)
        {
            Projectile.localAI[0] = num28;
            num29 = num28;
        }
        var projPosStoredX = Projectile.position.X;
        var projPosStoredY = Projectile.position.Y;
        var distance = 300f;
        var flag = false;
        var npcArrayIndexStored = 0;
        if (npcIndex == 0f)
        {
            for (var npcArrayIndex = 0; npcArrayIndex < 200; npcArrayIndex++)
            {
                if (Main.npc[npcArrayIndex].active && !Main.npc[npcArrayIndex].dontTakeDamage && !Main.npc[npcArrayIndex].friendly && Main.npc[npcArrayIndex].lifeMax > 5 && (npcIndex == 0f || npcIndex == npcArrayIndex + 1))
                {
                    var npcCenterX = Main.npc[npcArrayIndex].position.X + Main.npc[npcArrayIndex].width / 2;
                    var npcCenterY = Main.npc[npcArrayIndex].position.Y + Main.npc[npcArrayIndex].height / 2;
                    var num37 = Math.Abs(Projectile.position.X + Projectile.width / 2 - npcCenterX) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - npcCenterY);
                    if (num37 < distance && Collision.CanHit(new Vector2(Projectile.position.X + Projectile.width / 2, Projectile.position.Y + Projectile.height / 2), 1, 1, Main.npc[npcArrayIndex].position, Main.npc[npcArrayIndex].width, Main.npc[npcArrayIndex].height))
                    {
                        distance = num37;
                        projPosStoredX = npcCenterX;
                        projPosStoredY = npcCenterY;
                        flag = true;
                        npcArrayIndexStored = npcArrayIndex;
                    }
                }
            }
            if (flag)
            {
                npcIndex = npcArrayIndexStored + 1;
            }
            flag = false;
        }
        if (npcIndex != 0f)
        {
            var npcArrayIndexAgain = (int)(npcIndex - 1f);
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
        if (flag && allowHomingCounter <= 0)
        {
            var num42 = num29;
            var projCenter = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
            var num43 = projPosStoredX - projCenter.X;
            var num44 = projPosStoredY - projCenter.Y;
            var num45 = (float)Math.Sqrt(num43 * num43 + num44 * num44);
            num45 = num42 / num45;
            num43 *= num45;
            num44 *= num45;
            var num46 = 2;
            Projectile.velocity.X = (Projectile.velocity.X * (num46 - 1) + num43) / num46;
            Projectile.velocity.Y = (Projectile.velocity.Y * (num46 - 1) + num44) / num46;
        }
        if (Projectile.localAI[1] < 15)
        {
            Projectile.localAI[1]++;
        }
        if (Projectile.localAI[1] > 7f)
        {
            var num483 = Dust.NewDust(new Vector2(Projectile.position.X - Projectile.velocity.X * 4f + 5f, Projectile.position.Y + 2f - Projectile.velocity.Y * 4f), 8, 8, DustID.Torch, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100, default(Color), 1.5f);
            Main.dust[num483].velocity *= -0.25f;
            Main.dust[num483].noGravity = true;
            num483 = Dust.NewDust(new Vector2(Projectile.position.X - Projectile.velocity.X * 4f + 5f, Projectile.position.Y + 2f - Projectile.velocity.Y * 4f), 8, 8, DustID.Torch, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100, default(Color), 1.5f);
            Main.dust[num483].velocity *= -0.25f;
            Main.dust[num483].position -= Projectile.velocity * 0.5f;
            Main.dust[num483].noGravity = true;
        }
    }
}
