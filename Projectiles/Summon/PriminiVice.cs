using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Summon;

public class PriminiVice : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Primini");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.netImportant = true;
        Projectile.width = dims.Width * 30 / 22;
        Projectile.height = dims.Height * 30 / 22 / Main.projFrames[Projectile.type];
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
    public override bool MinionContactDamage()
    {
        return true;
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
            50, 3, 1f, 0.15f);
        //Projectile.damage = (int)owner.GetDamage(DamageClass.Summon).ApplyTo(50);
        //Projectile.damage += owner.ownedProjectileCounts[ModContent.ProjectileType<PrimeArmsCounter>()] * 3;
        //Projectile.scale = 1f;
        //Projectile.scale += 0.2f * owner.ownedProjectileCounts[ModContent.ProjectileType<PrimeArmsCounter>()];
        if (Projectile.position.Y > Main.player[Projectile.owner].Center.Y + Main.rand.Next(-10, 0))
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
        else if (Projectile.position.Y < Main.player[Projectile.owner].Center.Y + Main.rand.Next(-10, 0))
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
        if (Projectile.Center.X > Main.player[Projectile.owner].Center.X + Main.rand.Next(45, 65))
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
        if (Projectile.Center.X < Main.player[Projectile.owner].Center.X + Main.rand.Next(45, 65))
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
        var num959 = Projectile.FindClosestNPC(480, npc => !npc.active || npc.townNPC || npc.dontTakeDamage || npc.lifeMax <= 5 || npc.type == NPCID.TargetDummy || npc.type == NPCID.CultistBossClone || npc.friendly);
        if (num959 == -1)
        {
            Projectile.rotation = -2.3561945f;
            return;
        }
        if (Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[num959].position, Main.npc[num959].width, Main.npc[num959].height))
        {
            if (!Main.npc[num959].active)
            {
                Projectile.ai[1] = 0f;
                return;
            }
            Projectile.ai[1] += 1f;
            if (Projectile.ai[1] >= 50f)
            {
                Projectile.velocity = Vector2.Normalize(Main.npc[num959].Center - Projectile.Center) * 9f;
                return;
            }
            if (Projectile.ai[1] >= 100f)
            {
                Projectile.velocity = Vector2.Normalize(new Vector2(Main.npc[num959].Center.X - 50f, Main.npc[num959].Center.Y)) * 2.5f;
                return;
            }
            if (Projectile.ai[1] >= 150f)
            {
                Projectile.velocity = Vector2.Normalize(new Vector2(Main.npc[num959].Center.X + 50f, Main.npc[num959].Center.Y)) * 2.5f;
                return;
            }
        }
    }
}
