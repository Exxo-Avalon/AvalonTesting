using Terraria.GameContent.Bestiary;
using System;
using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Avalon.NPCs.Template;

public class test : ModNPC
{
    public float maxMoveSpeed = 2.5f;
    public float maxAirSpeed = 3.5f;
    public float acceleration = 0.1f;
    public float airAcceleration = 0.1f;
    public float maxJumpHeight = 8f;
    public float jumpDistance = 150;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Hellbound Lizard");
        Main.npcFrameCount[NPC.type] = 16;
        NPCID.Sets.DebuffImmunitySets[Type] = new NPCDebuffImmunityData
        {
            SpecificallyImmuneTo = new int[]
            {
                BuffID.Confused,
                BuffID.OnFire
            }
        };
    }
    public override void SetDefaults()
    {
        NPC.damage = 90;
        NPC.lifeMax = 1080;
        NPC.defense = 20;
        NPC.lavaImmune = true;
        NPC.noGravity = false;
        NPC.width = 18;
        NPC.aiStyle = -1;
        NPC.value = 1000f;
        NPC.height = 40;
        NPC.knockBackResist = 0.1f;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new ModBiomeBestiaryInfoElement(Mod, "Hellcastle", "Assets/Bestiary/HellcastleIcon", "Assets/Bestiary/HellcastleBG", null),
            new FlavorTextBestiaryInfoElement("Similar in appearance to Lihzahrds, they run about in the Hellcastle, seemingly without purpose.")
        });
    }
    public override bool? CanFallThroughPlatforms()
    {
        Player player = Main.player[NPC.FindClosestPlayer()];
        float upOrDown = NPC.Center.Y - player.Center.Y;

        return NPC.collideY && upOrDown < -15;
    }
    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.55f);
        NPC.damage = (int)(NPC.damage * 0.85f);
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 255);
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
                NPC.frame.Y = 0;
                NPC.frameCounter = 0.0;
            }
            else
            {
                NPC.frameCounter += Math.Abs(NPC.velocity.X) * 2f;
                NPC.frameCounter++;
                if (NPC.frameCounter > 6.0)
                {
                    NPC.frame.Y += frameHeight;
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
            NPC.frame.Y = frameHeight;
        }
    }
    public bool isHit;
    public bool isInJump;
    public float jumpdelay = 3;
    public override void AI()
    {
        Player player = Main.player[NPC.FindClosestPlayer()];
        float distanceBetweenPlayer = Vector2.Distance(player.Center, NPC.Center);
        float dir = NPC.Center.X - player.Center.X;
        float upOrDown = NPC.Center.Y - player.Center.Y;

        dir = Math.Sign(dir);

        float moveSpeedMulti = NPC.velocity.X + (acceleration * -dir);
        float airSpeedMulti = NPC.velocity.X + (airAcceleration * -dir);
        moveSpeedMulti = Math.Clamp(moveSpeedMulti, -maxMoveSpeed, maxMoveSpeed);
        if (!isHit)
        {
            airSpeedMulti = Math.Clamp(airSpeedMulti, -maxAirSpeed, maxAirSpeed);
        }
        NPC.spriteDirection = -(int)dir;

        if (NPC.velocity.Y == 0)
        {
            NPC.velocity.X = moveSpeedMulti;
        }
        else
        {
            NPC.velocity.X = airSpeedMulti;
        }
        if(distanceBetweenPlayer < jumpDistance && NPC.collideY && upOrDown > 1 && Collision.CanHitLine(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
        {
            Jump(maxJumpHeight);
        }

        Point a = NPC.Bottom.ToTileCoordinates();
        float height = 0;
        if ((NPC.collideY || Main.tileSolid[Main.tile[a.X, a.Y].TileType] && Main.tile[a.X, a.Y].HasTile) && NPC.collideX)
        {
            for (int i = 0; i < 10; i++)
            {
                if(Main.tile[a.X + 1 * -(int)dir, a.Y - i].HasTile && Main.tileSolid[Main.tile[a.X + 1 * -(int)dir, a.Y - i].TileType])
                {
                    height = i + 1;
                }
            }
            Jump(height);
        }
        if (NPC.collideY || Main.tileSolid[Main.tile[a.X, a.Y].TileType] && Main.tile[a.X, a.Y].HasTile)
        {
            Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY);
            isInJump = false;
            isHit = false;
            if(jumpdelay != 0)
            {
                jumpdelay--;
            }
            if((!Main.tileSolid[Main.tile[a.X + 1 * -(int)dir, a.Y].TileType] || !Main.tile[a.X + 1 * -(int)dir, a.Y].HasTile) && (!Main.tileSolid[Main.tile[a.X + 2 * -(int)dir, a.Y].TileType] || !Main.tile[a.X + 2 * -(int)dir, a.Y].HasTile) && upOrDown > -20)
            {
                Jump(maxJumpHeight);
            }
        }
        else
        {
            if (NPC.velocity.Y < 0)
            {
                isInJump = true;
            }
            if (NPC.velocity.Y > 0)
            {
                Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY);
            }
        }
    }
    public void Jump(float height)
    {
        if(jumpdelay == 0)
        {
            height = Math.Clamp(height + 2.5f, 0f, maxJumpHeight);
            NPC.velocity.Y = -(height);
            jumpdelay = 3;
        }
    }
    public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
    {
        isHit = true;
    }
    public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
    {
        isHit = true;
    }
    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (spawnInfo.Player.GetModPlayer<ExxoBiomePlayer>().ZoneHellcastle && Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY].WallType == (ushort)ModContent.WallType<Walls.ImperviousBrickWallUnsafe>())
        {
            return 3f;
        }
        return 0f;
    }
    public override void HitEffect(int hitDirection, double damage)
    {
        for (int i = 0; i < 30; i++)
        {
            int num890 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch, 0f, 0f, 0, default(Color), 1f);
            Main.dust[num890].velocity *= 5f;
            Main.dust[num890].scale = 1.2f;
            Main.dust[num890].noGravity = true;
        }
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HellboundLizardGore1").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HellboundLizardGore2").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HellboundLizardGore2").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HellboundLizardGore3").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HellboundLizardGore3").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HellboundLizardGore4").Type, 1f);
        }
    }
}
