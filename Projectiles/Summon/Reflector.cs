using System.Collections.Generic;
using System.IO;
using Avalon.Network;
using Avalon.Network.Handlers;
using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Summon;

public class Reflector : ModProjectile
{
    private int hostPosition = -1;
    private LinkedListNode<int> positionNode;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Reflector");
        Main.projFrames[Projectile.type] = 20;
        base.SetStaticDefaults();
    }
    public override void SetDefaults()
    {
        Projectile.width = 22;
        Projectile.height = 36;
        Projectile.netImportant = true;
        Projectile.friendly = true;
        Projectile.ignoreWater = true;
        Projectile.minionSlots = 1f;
        Projectile.timeLeft = 18000;
        Projectile.penetrate = -1;
        Projectile.timeLeft *= 5;
        Projectile.minion = true;
        Projectile.tileCollide = false;
        //Main.projPet[projectile.type] = true;
    }
    public override bool? CanCutTiles()
    {
        return false;
    }
    public override void SendExtraAI(BinaryWriter writer)
    {
        ExxoSummonPlayer summonPlayer = Main.player[Projectile.owner].GetModPlayer<ExxoSummonPlayer>();
        positionNode ??= summonPlayer.HandleReflectorSummon();
        writer.Write(positionNode.Value);
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
        base.ReceiveExtraAI(reader);
        hostPosition = reader.ReadInt32();
    }

    public override void Kill(int timeLeft)
    {
        Main.player[Projectile.owner].GetModPlayer<ExxoSummonPlayer>().RemoveReflectorSummon(positionNode);
        base.Kill(timeLeft);
    }
    public override void AI()
    {
        Projectile.frameCounter++;
        if (Projectile.frameCounter > 3)
        {
            Projectile.frame++;
            Projectile.frameCounter = 0;
        }
        if (Projectile.frame > 19)
        {
            Projectile.frame = 0;
        }
        if (Projectile.frame < 1)
        {
            Projectile.frame = 0;
        }
        Projectile.damage = 0;
        Player player = Main.player[Projectile.owner];
        ExxoBuffPlayer buffPlayer = player.GetModPlayer<ExxoBuffPlayer>();
        ExxoSummonPlayer summonPlayer = player.GetModPlayer<ExxoSummonPlayer>();

        // Check if should be alive
        if (player.dead || !player.active)
        {
            player.ClearBuff(ModContent.BuffType<Buffs.Reflector>());
            return;
        }

        Projectile.ai[0]++;
        if (Projectile.owner == Main.myPlayer && Main.netMode != NetmodeID.SinglePlayer && Projectile.ai[0] % 300 == 1)
        {
            Projectile.netUpdate = true;
            ModContent.GetInstance<SyncReflectorStaff>().Send(new BasicPlayerNetworkArgs(player));
        }

        if (player.HasBuff(ModContent.BuffType<Buffs.Reflector>()))
        {
            Projectile.timeLeft = 2;
        }

        // Get position in circle
        if (hostPosition == -1)
        {
            if (Projectile.type == ModContent.ProjectileType<Reflector>())
                positionNode ??= summonPlayer.HandleReflectorSummon();
            else
                positionNode ??= summonPlayer.HandleDaggerSummon();
        }
        else
        {
            if (Projectile.type == ModContent.ProjectileType<Reflector>())
                positionNode ??= summonPlayer.ObtainExistingReflectorSummon(hostPosition);
            else
                positionNode ??= summonPlayer.ObtainExistingDaggerSummon(hostPosition);
        }

        int closest = AvalonGlobalProjectile.FindClosestHostile(Projectile.Center, 160f);
        if (closest != -1)
        {
            Projectile targ = Main.projectile[closest];
            Projectile.velocity = Vector2.Normalize(targ.Center - Projectile.Center) * 8f;
            if (Vector2.Distance(Projectile.Center, targ.Center) < 160)
            {
                Rectangle proj = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
                Rectangle targetProj = new Rectangle((int)targ.position.X, (int)targ.position.Y, targ.width, targ.height);
                if (proj.Intersects(targetProj) && !targ.bobber && !Data.Sets.Projectile.DontReflect[targ.type])
                {
                    targ.hostile = false;
                    targ.friendly = true;
                    targ.damage = (int)MathHelper.Clamp(targ.damage, targ.damage * 3, 150);
                    targ.velocity *= -1f;
                }
            }
        }
        else
        {
            const int radius = 100;
            const float speed = 0.1f;
            int count = summonPlayer.Reflectors.Count;
            Vector2 target = player.Center +
                             (Vector2.One.RotatedBy(
                                 (MathHelper.TwoPi / count * positionNode.Value) +
                                 buffPlayer.DaggerStaffRotation) * radius);
            Vector2 error = target - Projectile.Center;

            Projectile.velocity = player.velocity + (error * speed);
        }
    }
    //public override void AI()
    //{
    //    Projectile.damage = 0;
    //}
    /*public override void AI()
    {
        Projectile.damage = 0;
        bool playerPosLessProj = false;
        bool playerPosGreaterProj = false;
        Projectile.spriteDirection = Main.player[Projectile.owner].direction;
        Projectile.rotation = Projectile.velocity.X * 0.075f;
        Projectile.frameCounter++;
        if (Projectile.frameCounter > 3)
        {
            Projectile.frame++;
            Projectile.frameCounter = 0;
        }
        if (Projectile.frame > 19)
        {
            Projectile.frame = 0;
        }
        if (Projectile.frame < 1)
        {
            Projectile.frame = 0;
        }
        //if (Vector2.Distance(projectile.position, Main.player[projectile.owner].position) > 25 * 16)
        //{
        //    if (projectile.velocity.X == 0f || projectile.velocity.Y == 0f) projectile.tileCollide = false;
        //}
        //else projectile.tileCollide = true;

        Main.player[Projectile.owner].AddBuff(ModContent.BuffType<Buffs.Reflector>(), 3600);
        if (Projectile.type == ModContent.ProjectileType<Reflector>())
        {
            if (Main.player[Projectile.owner].dead)
            {
                Main.player[Projectile.owner].Avalon().reflectorMinion = false;
            }
            if (Main.player[Projectile.owner].Avalon().reflectorMinion)
            {
                Projectile.timeLeft = 2;
            }
        }

        int num321 = 10;
        int num322 = 40 * (Projectile.minionPos + 1) * Main.player[Projectile.owner].direction;
        if (Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) < Projectile.position.X + (float)(Projectile.width / 2) - num321 + num322)
        {
            playerPosLessProj = true;
        }
        else if (Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) > Projectile.position.X + (float)(Projectile.width / 2) + num321 + num322)
        {
            playerPosGreaterProj = true;
        }
        if (playerPosLessProj)
        {
            if (Projectile.velocity.X > 0f)
            {
                Projectile.velocity.X = Projectile.velocity.X * 0.94f;
            }
            Projectile.velocity.X = Projectile.velocity.X - 0.3f;
            if (Projectile.velocity.X > 9f)
            {
                Projectile.velocity.X = 9f;
            }
        }
        else if (playerPosGreaterProj)
        {
            if (Projectile.velocity.X < 0f)
            {
                Projectile.velocity.X = Projectile.velocity.X * 0.94f;
            }
            Projectile.velocity.X = Projectile.velocity.X + 0.2f;
            if (Projectile.velocity.X < -8f)
            {
                Projectile.velocity.X = -8f;
            }
        }
        if (playerPosLessProj || playerPosGreaterProj)
        {
            int num421 = (int)(Projectile.position.X + (float)(Projectile.width / 2)) / 16;
            int j2 = (int)(Projectile.position.Y + (float)(Projectile.height / 2)) / 16;
            if (Projectile.type == 236)
            {
                num421 += Projectile.direction;
            }
            if (playerPosLessProj)
            {
                num421--;
            }
            if (playerPosGreaterProj)
            {
                num421++;
            }
            num421 += (int)Projectile.velocity.X;
        }
        //Collision.StepUp(ref projectile.position, ref projectile.velocity, projectile.width, projectile.height, ref projectile.stepSpeed, ref projectile.gfxOffY, 1, false);
        int closest = AvalonGlobalProjectile.FindClosestHostile(Projectile.Center, 160f);
        if (closest != -1)
        {
            Projectile targ = Main.projectile[closest];
            Projectile.velocity = Vector2.Normalize(targ.Center - Projectile.Center) * 8f;
            if (Vector2.Distance(Projectile.Center, targ.Center) < 160)
            {
                Rectangle proj = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
                Rectangle targetProj = new Rectangle((int)targ.position.X, (int)targ.position.Y, targ.width, targ.height);
                if (proj.Intersects(targetProj) && !targ.bobber && !Data.Sets.Projectile.DontReflect[targ.type])
                {
                    targ.hostile = false;
                    targ.friendly = true;
                    targ.damage = (int)MathHelper.Clamp(targ.damage, targ.damage * 3, 150);
                    targ.velocity *= -1f;
                }
            }
        }
        else
        {
            if (Projectile.position.Y > Main.player[Projectile.owner].Center.Y - 100)
            {
                if (Projectile.velocity.Y > 0f)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y * 0.96f;
                }
                Projectile.velocity.Y = Projectile.velocity.Y - 0.3f;
                if (Projectile.velocity.Y > 6f)
                {
                    Projectile.velocity.Y = 6f;
                }
            }
            else if (Projectile.position.Y < Main.player[Projectile.owner].Center.Y - 100)
            {
                if (Projectile.velocity.Y < 0f)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y * 0.96f;
                }
                Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
                if (Projectile.velocity.Y < -6f)
                {
                    Projectile.velocity.Y = -6f;
                }
            }
        }
    }*/
}
