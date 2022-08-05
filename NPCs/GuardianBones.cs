using System;
using Avalon.Items.Accessories;
using Avalon.Items.Placeable.Tile;
using Avalon.NPCs.Template;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs;

public class GuardianBones : CustomFighterAI
{
    private int timer;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Guardian Bones");
        Main.npcFrameCount[NPC.type] = 15;
        var debuffData = new NPCDebuffImmunityData
        {
            SpecificallyImmuneTo = new[] { BuffID.Confused, BuffID.OnFire },
        };
        NPCID.Sets.DebuffImmunitySets[Type] = debuffData;
    }

    public override void SetDefaults()
    {
        NPC.damage = 140;
        NPC.scale = 1f;
        NPC.lifeMax = 9000;
        NPC.defense = 120;
        NPC.width = 31;
        NPC.aiStyle = 3;
        NPC.npcSlots = 4f;
        NPC.value = 10000f;
        NPC.timeLeft = 10050;
        NPC.height = 68;
        NPC.Hitbox = new(0, 0, 28, 40);
        NPC.knockBackResist = 0f;
        NPC.HitSound = SoundID.NPCHit2;
        NPC.DeathSound = SoundID.NPCDeath2;
    }
    public override float MaxJumpHeight { get; set; } = 12f;
    public override float Acceleration { get; set; } = 0.2f;
    public override float MaxMoveSpeed { get; set; } = 4f;
    public override float JumpRadius { get; set; } = 250f;
    public override bool JumpOverDrop { get; set; } = false;
    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.55f);
        NPC.damage = (int)(NPC.damage * 0.44f);
    }
    private byte jumpTimer;
    public override void ModifyNPCLoot(NPCLoot loot)
    {
        loot.Add(ItemDropRule.Common(ModContent.ItemType<AegisofAges>(), 20));
        loot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Material.Phantoplasm>(), 10));
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo) =>
        spawnInfo.Player.ZoneDungeon && Main.hardMode && ModContent.GetInstance<DownedBossSystem>().DownedArmageddon &&
        ModContent.GetInstance<AvalonWorld>().SuperHardmode
            ? 0.083f * AvalonGlobalNPC.EndoSpawnRate
            : 0f;

    public override bool? CanFallThroughPlatforms()
    {
        Player player = Main.player[NPC.FindClosestPlayer()];
        float upOrDown = NPC.Center.Y - player.Center.Y;

        //if the player is under the npc then fall through the platform, should maybe check for canhitline but vanilla doesn't do that so idk
        return NPC.collideY && upOrDown < -15;
    }
    public override void CustomBehavior()
    {
        timer++;
        NPC.TargetClosest();
        Player targ = Main.player[NPC.target];

        if (timer > 240 && Collision.CanHit(NPC, targ))
        {
            if (timer % 45 == 0)
            {
                int p = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position, new Vector2(1), ModContent.ProjectileType<Projectiles.Hostile.Skeleton>(), 100, 4f);
                Main.projectile[p].velocity = Vector2.Normalize(targ.position - NPC.position) * 10f;
            }
            if (timer > 400)
            {
                timer = 0;
            }
        }
        if ((NPC.velocity.X < 0f && NPC.spriteDirection == -1) || (NPC.velocity.X > 0f && NPC.spriteDirection == 1))
        {
            if (NPC.collideX)
            {
                NPC.velocity.Y *= 1.03f;
                NPC.netUpdate = true;
            }
        }
        if (NPC.collideY)
        {
            if (NPC.spriteDirection == -1)
            {
                NPC.velocity.X -= 0.6f;
            }
            else
            {
                NPC.velocity.X += 0.6f;
            }
        }
        // code to force the player to stay inside a box around the mob (doesn't work)
        //if (Vector2.Distance(targ.position, NPC.position) > 16 * 16)
        //{
        //    if (NPC.position.X - targ.position.X < 0)
        //    {
        //        targ.velocity.X = MathHelper.Clamp(targ.position.X, NPC.velocity.X - targ.velocity.X, NPC.velocity.X);
        //    }

        //    if (NPC.position.X - targ.position.X > 0)
        //    {
        //        targ.velocity.X = MathHelper.Clamp(targ.position.X, NPC.velocity.X, NPC.velocity.X + targ.velocity.X);
        //    }
        //}
        // end
        if (Vector2.Distance(NPC.position, targ.position) < 20)
        {
            NPC.noTileCollide = NPC.noGravity = false;
        }
        Point tile2 = (NPC.oldPosition + new Vector2(NPC.width, NPC.height + 8)).ToTileCoordinates();
        bool oldCollide = Main.tileSolid[Main.tile[tile2.X, tile2.Y + 1].TileType] && Main.tile[tile2.X, tile2.Y + 1].HasTile ||
            (Main.tileSolid[Main.tile[tile2.X, tile2.Y + 2].TileType] && Main.tile[tile2.X, tile2.Y + 2].HasTile) && NPC.velocity.Y == 0;
        if (AvalonGlobalNPC.CheckIfLineCanBeDrawn(NPC.position, new Vector2(NPC.width, NPC.height), targ.position, new Vector2(targ.width, targ.height)) &&
            !NPC.justHit && !NPC.collideY && oldCollide)
        {
            Point tile = NPC.position.ToTileCoordinates();
            Tile t = Main.tile[tile.X, tile.Y];
            if (!t.HasTile || !Main.tileSolid[t.TileType])
            {
                jumpTimer++;
                if (Vector2.Distance(NPC.position, targ.position) > 500)// && jumpTimer <= 2)
                {
                    Main.NewText(jumpTimer);
                    NPC.noGravity = true;
                    NPC.TargetClosest();
                    NPC.velocity += Vector2.Normalize(targ.position - NPC.position) * 5;
                    NPC.velocity.Y += 0.3f;
                    NPC.noGravity = false;
                }
                else
                    jumpTimer = 0;
            }
            else if (t.HasTile || Vector2.Distance(NPC.position, targ.position) < 300)
            {
                NPC.TargetClosest();
                NPC.velocity.Y += 0.3f;
                NPC.noTileCollide = NPC.noGravity = false;
            }
        }
        //Main.NewText("hi");
    }
    public override void AI()
    {
        CustomBehavior();

    }
    public override void FindFrame(int frameHeight)
    {
        if (NPC.velocity.Y == 0f)
        {
            if (NPC.direction == 1)
            {
                NPC.spriteDirection = 1;
            }

            if (NPC.direction == -1)
            {
                NPC.spriteDirection = -1;
            }

            if (NPC.velocity.X == 0f)
            {
                if (NPC.type == NPCID.PossessedArmor)
                {
                    NPC.frame.Y = frameHeight;
                    NPC.frameCounter = 0.0;
                }
                else
                {
                    NPC.frame.Y = 0;
                    NPC.frameCounter = 0.0;
                }
            }
            else
            {
                NPC.frameCounter += Math.Abs(NPC.velocity.X) * 2f;
                NPC.frameCounter += 1.0;
                if (NPC.frameCounter > 6.0)
                {
                    NPC.frame.Y = NPC.frame.Y + frameHeight;
                    NPC.frameCounter = 0.0;
                }

                if (NPC.frame.Y / frameHeight >= Main.npcFrameCount[NPC.type])
                {
                    NPC.frame.Y = frameHeight * 2;
                }
            }
        }
        else
        {
            NPC.frameCounter = 0.0;
            NPC.frame.Y = 0;
        }
    }
}
