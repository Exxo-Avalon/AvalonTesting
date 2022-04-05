using System.Collections.Generic;
using System.IO;
using AvalonTesting.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Projectiles.Summon;

public abstract class BaseDagger<T> : ModProjectile where T : ModBuff
{
    private LinkedListNode<int> positionNode;
    private int hostPosition = -1;

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

        if (player.dead || !player.active)
        {
            player.ClearBuff(ModContent.BuffType<T>());
        }

        if (player.HasBuff(ModContent.BuffType<T>()))
        {
            Projectile.timeLeft = 2;
        }


        if (hostPosition == -1)
        {
            positionNode ??= summonPlayer.HandleDaggerSummon();
        }
        else
        {
            positionNode ??= summonPlayer.ObtainExistingDaggerSummon(hostPosition);
        }

        #region movement

        int closestNPC = Projectile.FindClosestNPC(16 * 40);
        if (closestNPC == -1)
        {
            int radius = 50;
            float speed = 0.1f;
            Vector2 target = player.Center +
                             (Vector2.One.RotatedBy(
                                 ((MathHelper.TwoPi / summonPlayer.DaggerSummons.Count) * positionNode.Value) +
                                 buffPlayer.DaggerStaffRotation) * radius);
            Vector2 error = target - Projectile.Center;

            Projectile.velocity = player.velocity + (error * speed);
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
        else
        {
            Vector2 vector56 = Projectile.position;
            float num768 = 400f;
            bool flag31 = false;
            for (int num769 = 0; num769 < 200; num769++)
            {
                NPC nPC4 = Main.npc[num769];
                if (nPC4.active && !nPC4.dontTakeDamage && !nPC4.friendly && nPC4.lifeMax > 5)
                {
                    float num770 = Vector2.Distance(nPC4.Center, Projectile.Center);
                    if (((Vector2.Distance(Projectile.Center, vector56) > num770 && num770 < num768) || !flag31) &&
                        Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, nPC4.position,
                            nPC4.width, nPC4.height))
                    {
                        num768 = num770;
                        vector56 = nPC4.Center;
                        flag31 = true;
                    }
                }
            }

            if (flag31 && Projectile.ai[0] == 0f)
            {
                Vector2 vector57 = vector56 - Projectile.Center; // npc center minus projectile center
                float num772 = vector57.Length(); // distance between npc and proj
                vector57.Normalize();
                if (num772 > 200f)
                {
                    float scaleFactor5 = 8f;
                    vector57 *= scaleFactor5;
                    Projectile.velocity = ((Projectile.velocity * 40f) + vector57) / 41f;
                }
                else
                {
                    float num773 = 4f;
                    vector57 *= -num773;
                    Projectile.velocity = ((Projectile.velocity * 40f) + vector57) / 41f;
                }
            }
            else
            {
                float num774 = 6f;
                if (Projectile.ai[0] == 1f)
                {
                    num774 = 15f;
                }

                Vector2 value19 = Projectile.Center;
                Vector2 vector58 = Main.player[Projectile.owner].Center - value19 + new Vector2(0f, -60f);
                float num775 = vector58.Length();
                if (num775 > 200f && num774 < 8f)
                {
                    num774 = 8f;
                }

                if (num775 < 150f && Projectile.ai[0] == 1f &&
                    !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                {
                    Projectile.ai[0] = 0f;
                    Projectile.netUpdate = true;
                }

                if (num775 > 70f)
                {
                    vector58.Normalize();
                    vector58 *= num774;
                    Projectile.velocity = ((Projectile.velocity * 40f) + vector58) / 41f;
                }
                else if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
                {
                    Projectile.velocity.X = -0.15f;
                    Projectile.velocity.Y = -0.05f;
                }
            }


            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (Projectile.ai[1] > 0)
            {
                Projectile.ai[1] += Main.rand.Next(1, 4);
            }

            if (Projectile.ai[1] > 50)
            {
                Projectile.ai[1] = 0;
                Projectile.netUpdate = true;
            }

            if (Projectile.type == ModContent.ProjectileType<BaseDagger<T>>() && Projectile.ai[1] == 0f && flag31 &&
                num768 < 500f)
            {
                Projectile.ai[1]++;
                if (Main.myPlayer == Projectile.owner)
                {
                    Projectile.ai[0] = 2f;
                    Vector2 value21 = vector56 - Projectile.Center;
                    value21.Normalize();
                    Projectile.velocity = value21 * 8f;
                    Projectile.netUpdate = true;
                }
            }
        }

        //projectile.Center = target;

        #endregion
    }
}
