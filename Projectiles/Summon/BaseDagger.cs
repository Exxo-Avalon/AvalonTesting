using System.Collections.Generic;
using System.IO;
using AvalonTesting.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Projectiles.Summon;

// TODO: Implement AI which throws dagger from circle fast and then slashes fast back and forth a few times and then pulls back, repeat (bezier maybe?)
public abstract class BaseDagger<T> : ModProjectile where T : ModBuff
{
    private int hostPosition = -1;
    private LinkedListNode<int> positionNode;

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
        ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        Main.projPet[Projectile.type] = true;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width;
        Projectile.height = dims.Height;

        Projectile.minion = true;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.light = 0.3f;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = false;
        Projectile.minionSlots = 1f;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 60;
    }

    public override bool MinionContactDamage()
    {
        return true;
    }

    public override bool? CanCutTiles()
    {
        return false;
    }

    public override void SendExtraAI(BinaryWriter writer)
    {
        ExxoSummonPlayer summonPlayer = Main.player[Projectile.owner].GetModPlayer<ExxoSummonPlayer>();
        positionNode ??= summonPlayer.HandleDaggerSummon();
        writer.Write(positionNode.Value);
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
        base.ReceiveExtraAI(reader);
        hostPosition = reader.ReadInt32();
    }

    public override void Kill(int timeLeft)
    {
        Main.player[Projectile.owner].GetModPlayer<ExxoSummonPlayer>().RemoveDaggerSummon(positionNode);
        base.Kill(timeLeft);
    }

    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        ExxoBuffPlayer buffPlayer = player.GetModPlayer<ExxoBuffPlayer>();
        ExxoSummonPlayer summonPlayer = player.GetModPlayer<ExxoSummonPlayer>();

        // Check if should be alive
        if (player.dead || !player.active)
        {
            player.ClearBuff(ModContent.BuffType<T>());
            return;
        }

        Projectile.ai[0]++;
        if (Projectile.owner == Main.myPlayer && Main.netMode != NetmodeID.SinglePlayer && Projectile.ai[0] % 300 == 1)
        {
            Projectile.netUpdate = true;
            buffPlayer.SyncDaggerStaff();
        }

        if (player.HasBuff(ModContent.BuffType<T>()))
        {
            Projectile.timeLeft = 2;
        }

        // Get position in circle
        if (hostPosition == -1)
        {
            positionNode ??= summonPlayer.HandleDaggerSummon();
        }
        else
        {
            positionNode ??= summonPlayer.ObtainExistingDaggerSummon(hostPosition);
        }

        int targetedNPCIndex = Projectile.FindMinionTargetNPC();
        if (targetedNPCIndex == -1)
        {
            targetedNPCIndex = Projectile.FindClosestNPC(16 * 40,
                npc => !npc.active || npc.townNPC || npc.dontTakeDamage || npc.lifeMax <= 5);
        }

        if (targetedNPCIndex == -1)
        {
            // Spin in circle around player
            const int radius = 50;
            const float speed = 0.1f;
            Vector2 target = player.Center +
                             (Vector2.One.RotatedBy(
                                 (MathHelper.TwoPi / summonPlayer.DaggerSummons.Count * positionNode.Value) +
                                 buffPlayer.DaggerStaffRotation) * radius);
            Vector2 error = target - Projectile.Center;

            Projectile.velocity = player.velocity + (error * speed);
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
        else
        {
            NPC targetedNPC = Main.npc[targetedNPCIndex];
            float distanceToNPC = Vector2.Distance(targetedNPC.Center, Projectile.Center);

            Vector2 directionToNPC = (targetedNPC.Center - Projectile.Center).SafeNormalize(Vector2.UnitX);
            Vector2 moveVector = directionToNPC * 8;
            Projectile.velocity = ((Projectile.velocity * 40f) + moveVector) / 41f;

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
    }
}
