using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Summon;

public class PriminiLaser : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Primini");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.netImportant = true;
        Projectile.width = dims.Width;
        Projectile.height = dims.Height * 30 / 18 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.penetrate = -1;
        Projectile.timeLeft *= 5;
        Projectile.minion = true;
        Projectile.minionSlots = 0.25f;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.friendly = true;
        Main.projPet[Projectile.type] = true;
    }
    public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    {
        if (target.type == NPCID.TargetDummy)
            damage = 0;
    }
    public override void AI()
    {
        Player owner = Main.player[Projectile.owner];
        if (owner.dead)
        {
            owner.GetModPlayer<ExxoSummonPlayer>().PrimeMinion = false;
        }
        if (owner.GetModPlayer<ExxoSummonPlayer>().PrimeMinion)
        {
            Projectile.timeLeft = 2;
        }
        AvalonGlobalProjectile.ModifyProjectileStats(Projectile, ModContent.ProjectileType<PrimeArmsCounter>(),
            50, 3, 1f, 0.1f);
        //Projectile.damage = (int)owner.GetDamage(DamageClass.Summon).ApplyTo(50);
        //Projectile.damage += owner.ownedProjectileCounts[ModContent.ProjectileType<PrimeArmsCounter>()] * 3;
        //Projectile.scale = 1f;
        //Projectile.scale += 0.2f * owner.ownedProjectileCounts[ModContent.ProjectileType<PrimeArmsCounter>()];
        if (Projectile.position.Y > Main.player[Projectile.owner].Center.Y + Main.rand.Next(-5, 5))
        {
            if (Projectile.velocity.Y > 0f)
            {
                Projectile.velocity.Y *= 0.96f;
            }
            Projectile.velocity.Y -= 0.3f;
            if (Projectile.velocity.Y > 6f)
            {
                Projectile.velocity.Y = 6f;
            }
        }
        else if (Projectile.position.Y < Main.player[Projectile.owner].Center.Y + Main.rand.Next(-5, 5))
        {
            if (Projectile.velocity.Y < 0f)
            {
                Projectile.velocity.Y *= 0.96f;
            }
            Projectile.velocity.Y += 0.2f;
            if (Projectile.velocity.Y < -6f)
            {
                Projectile.velocity.Y = -6f;
            }
        }
        if (Projectile.Center.X > Main.player[Projectile.owner].Center.X - Main.rand.Next(35, 55))
        {
            if (Projectile.velocity.X > 0f)
            {
                Projectile.velocity.X *= 0.94f;
            }
            Projectile.velocity.X -= 0.3f;
            if (Projectile.velocity.X > 9f)
            {
                Projectile.velocity.X = 9f;
            }
        }
        if (Projectile.Center.X < Main.player[Projectile.owner].Center.X - Main.rand.Next(35, 55))
        {
            if (Projectile.velocity.X < 0f)
            {
                Projectile.velocity.X *= 0.94f;
            }
            Projectile.velocity.X += 0.2f;
            if (Projectile.velocity.X < -8f)
            {
                Projectile.velocity.X = -8f;
            }
        }
        var num957 = Projectile.FindClosestNPC(640, npc => !npc.active || npc.townNPC || npc.dontTakeDamage || npc.lifeMax <= 5 || npc.type == NPCID.TargetDummy || npc.type == NPCID.CultistBossClone || npc.friendly);
        if (num957 == -1)
        {
            Projectile.rotation = -0.785398164f; // -2.3561945f;
            return;
        }
        Projectile.rotation = Vector2.Normalize(Main.npc[num957].Center - Projectile.Center).ToRotation() + 1.57079637f;
        if (Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[num957].position, Main.npc[num957].width, Main.npc[num957].height))
        {
            if (!Main.npc[num957].active)
            {
                Projectile.ai[1] = 0f;
                return;
            }
            Projectile.ai[1]++;
            if (Projectile.ai[1] <= 50f)
            {
                Projectile.velocity = Vector2.Normalize(Main.npc[num957].Center - Projectile.Center) * 9f;
                if (Projectile.ai[1] == 50f)
                {
                    Projectile.ai[1] = 95f;
                    return;
                }
            }
            else if (Projectile.ai[1] >= 95f)
            {
                var num958 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 1.5f, 1.5f, ProjectileID.MiniRetinaLaser, 70, 4.5f, Projectile.owner, 0f, 0f);
                Main.projectile[num958].velocity = Vector2.Normalize(Main.npc[num957].Center - Projectile.Center) * 8f;
                Projectile.ai[1] = 51f;
                return;
            }
        }
    }
}
