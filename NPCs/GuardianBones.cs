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
        NPC.aiStyle = -1;
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
    public override void CustomBehavior()
    {
        NPC.ai[0]++;
        Player targ = Main.player[NPC.target];

        if (NPC.ai[0] > 240 && Collision.CanHit(NPC, targ))
        {
            if (NPC.ai[0] % 45 == 0)
            {
                int p = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position, new Vector2(1), ModContent.ProjectileType<Projectiles.Hostile.Skeleton>(), 100, 4f);
                Main.projectile[p].velocity = Vector2.Normalize(targ.position - NPC.position) * 10f;
            }
            if (NPC.ai[0] > 400)
            {
                NPC.ai[0] = 0;
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
        if (Vector2.Distance(NPC.position, targ.position) < 20)
        {
            NPC.noTileCollide = NPC.noGravity = false;
        }
        if (AvalonGlobalNPC.CheckIfLineCanBeDrawn(NPC.position, new Vector2(NPC.width, NPC.height), targ.position, new Vector2(targ.width, targ.height)) &&
            !NPC.justHit)
        {
            Point tile = NPC.position.ToTileCoordinates();
            Tile t = Main.tile[tile.X, tile.Y];
            if (!t.HasTile || !Main.tileSolid[t.TileType])
            {
                if (Vector2.Distance(NPC.position, targ.position) > 700)
                {
                    NPC.noGravity = true;
                    NPC.velocity += Vector2.Normalize(targ.position - NPC.position);
                    NPC.velocity.Y += 0.3f;
                    NPC.noGravity = false;
                }
            }
            else if (t.HasTile || Vector2.Distance(NPC.position, targ.position) < 300)
            {
                NPC.velocity.Y += 0.3f;
                NPC.noTileCollide = NPC.noGravity = false;
            }
        }
    }
    /*public override void AI()
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
        if (Vector2.Distance(NPC.position, targ.position) < 20)
        {
            NPC.noTileCollide = NPC.noGravity = false;
        }
        if (CheckIfLineCanBeDrawn(NPC.position, new Vector2(NPC.width, NPC.height), targ.position, new Vector2(targ.width, targ.height)) &&
            !NPC.justHit)
        {
            Point tile = NPC.position.ToTileCoordinates();
            Tile t = Main.tile[tile.X, tile.Y];
            if (!t.HasTile || !Main.tileSolid[t.TileType])
            {
                if (Vector2.Distance(NPC.position, targ.position) > 700)
                {
                    NPC.noGravity = true;
                    NPC.velocity += Vector2.Normalize(targ.position - NPC.position);
                    NPC.velocity.Y += 0.3f;
                    NPC.noGravity = false;
                }
            }
            else if (t.HasTile || Vector2.Distance(NPC.position, targ.position) < 300)
            {
                NPC.velocity.Y += 0.3f;
                NPC.noTileCollide = NPC.noGravity = false;
            }
        }
    }*/
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
