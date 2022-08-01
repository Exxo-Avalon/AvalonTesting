using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System.IO;

namespace Avalon.Projectiles.Magic;

public class Drone : ModProjectile
{
    private int tileCollideCounter;
    public bool readyToHome = true;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Drone");
    }
    public override void SetDefaults()
    {
        Projectile.width = 12;
        Projectile.height = 12;
        Projectile.aiStyle = -1;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.penetrate = 5;
        Projectile.alpha = 255;
        Projectile.friendly = true;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 60;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
        if (Projectile.localAI[1] >= 15f)
        {
            return new Color(255, 255, 255, Projectile.alpha);
        }
        int num7 = (int)((Projectile.localAI[1] - 5f) / 10f * 255f);
        return new Color(num7, num7, num7, num7);
    }
    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(tileCollideCounter);
    }
    public override void ReceiveExtraAI(BinaryReader reader)
    {
        tileCollideCounter = reader.ReadInt32();
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (Projectile.type == ModContent.ProjectileType<Drone>())
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
    public float maxSpeed = 10f + Main.rand.NextFloat(10f);
    public float homeDistance = 450;
    public float homeStrength = 5f;
    public float homeDelay;
    public override void AI()
    {
        Lighting.AddLight(Projectile.position, 219 / 255f, 205 / 255f, 79 / 255f);
        //for (int num26 = 0; num26 < 4; num26++)
        //{
        //    float x2 = Projectile.position.X - Projectile.velocity.X / 10f * num26;
        //    float y2 = Projectile.position.Y - Projectile.velocity.Y / 10f * num26;
        //    int num27 = Dust.NewDust(new Vector2(x2 + 5, y2 + 5), 5, 5, ModContent.DustType<Dusts.ContagionSpray>(), 0f, 0f, 0, default, 1f);
        //    Main.dust[num27].alpha = Projectile.alpha;
        //    Main.dust[num27].color.A = 0;
        //    Main.dust[num27].scale *= 1f;
        //    Main.dust[num27].position = new Vector2(x2, y2);
        //    Main.dust[num27].velocity *= 0f;
        //    Main.dust[num27].noGravity = true;
        //}
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        if (!readyToHome)
        {
            homeDelay++;
            if(homeDelay >= 30)
            {
                readyToHome = true;
                homeDelay = 0;
            }
        }

        Vector2 startPosition = Projectile.Center;
        int closest = Projectile.FindClosestNPC(homeDistance, npc => !npc.active || npc.townNPC || npc.dontTakeDamage || npc.lifeMax <= 5 || npc.type == NPCID.TargetDummy || npc.type == NPCID.CultistBossClone || npc.friendly);
        if (closest != -1 && readyToHome)
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
}
