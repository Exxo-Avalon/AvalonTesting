using System;
using Avalon.Items.Banners;
using Avalon.Items.Placeable.Tile;
using Avalon.Projectiles;
using Avalon.Gores;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs;

public class Ectosphere : ModNPC
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Ectosphere");
        Main.npcFrameCount[NPC.type] = 12;
    }
    public override void SetDefaults()
    {
        NPC.damage = 70;
        NPC.lifeMax = 13000;
        NPC.defense = 110;
        NPC.noGravity = true;
        NPC.noTileCollide = true;
        NPC.aiStyle = -1;
        NPC.value = 20000f;
        NPC.Size = new Vector2(72, 80);
        NPC.knockBackResist = 0.05f;
        NPC.HitSound = SoundID.NPCHit36;
        NPC.DeathSound = SoundID.NPCDeath39;
        NPC.knockBackResist = 0f;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<EctosphereBanner>();
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
            new FlavorTextBestiaryInfoElement(
                "These spectres are heavily armored - they contain a large amount of ectoplasm as a result, however."),
        });
    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.7f);
        NPC.damage = (int)(NPC.damage * 0.67f);
    }
    public override bool? CanFallThroughPlatforms()
    {
        return true;
    }
    public override Color? GetAlpha(Color drawColor)
    {
        return Color.White;
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ItemID.Ectoplasm, 1, 2, 5));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Material.Phantoplasm>(), 4, 3, 6));
    }
    public float DegreesToRadians(int degrees)
    {
        return degrees / 57.2957795f;
    }
    public float moveSpeed;
    public float teleportRadius = 250f;
    public float tiltAmount = 20; //higher is less tilt and vice versa
    public float teleportDelay = 120;
    public string currentMoveState = "Charge";
    public bool runOnce;
    public override void AI()
    {
        Player player = Main.player[NPC.FindClosestPlayer()];
        float distanceFromPlayer = Vector2.Distance(player.Center, NPC.Center);
        float difference = (NPC.position.Length() - NPC.oldPosition.Length()) * -NPC.direction;
        float playerSide = NPC.Center.X - player.Center.X;
        Vector2 randomPos = new Vector2(player.Center.X, player.Center.Y + teleportRadius);
        Vector2 radius = player.Center - randomPos;
        Vector2 goToPlayer = Vector2.Normalize(player.Center - NPC.Center) * (800 - distanceFromPlayer) / 1600;
        Vector2 teleportPosition = player.Center + radius.RotatedByRandom(180 * (Math.PI / 180));
        Point point = teleportPosition.ToTileCoordinates();

        playerSide = Math.Sign(playerSide);

        NPC.ai[1]++;

        if (NPC.ai[1] > 300)
        {
            currentMoveState = "Summoning";
            if (!runOnce)
            {
                NPC.frame.Y = 80 * 6;
                runOnce = true;
            }
        }
        if (NPC.ai[1] > 370)
        {
            currentMoveState = "Charge";
            NPC.ai[1] = 0;
            runOnce = false;
        }

        //Check for left and right

        if (NPC.velocity.X > 0) //going right
        {
            NPC.direction = -1;
        }
        else //going left
        {
            NPC.direction = 1;
        }

        NPC.rotation = NPC.velocity.X / tiltAmount;
        NPC.spriteDirection = NPC.direction;

        //Move the NPC -- moveSpeed = 4f + (NPC.lifeMax - NPC.life) / (NPC.lifeMax / 4);

        if(currentMoveState == "Charge")
        {
            NPC.velocity += goToPlayer;
            NPC.ai[2] = 0;
        }
        if (currentMoveState == "Summoning")
        {
            NPC.spriteDirection = (int)playerSide;
            NPC.velocity *= 0.97f;
            NPC.ai[2]++;
            if(NPC.ai[2] == 20)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(10f * -playerSide, 0), ModContent.ProjectileType<Projectiles.Hostile.EctoBolt>(), 30, default, 255);
                NPC.ai[2] = 0;
            }
        }

        //speed limit
        if (NPC.velocity.Length() > 8f)
        {
            NPC.velocity = Vector2.Normalize(NPC.velocity) * 8f;
        }

        //Teleport when needed

        int tries = 0;
        while (Main.tile[point.X, point.Y].HasTile || !Main.wallDungeon[Main.tile[point.X, point.Y].WallType])
        {
            if (tries++ > 100)
            {
                break;
            }
            teleportPosition = player.Center + radius.RotatedByRandom(180 * (Math.PI / 180));
            point = teleportPosition.ToTileCoordinates();
        }

        if (currentMoveState == "Charge")
        {
            if (distanceFromPlayer > 800)
            {
                Teleport(teleportPosition);
            }
            if (difference < 0.05f)
            {
                NPC.ai[0]++;
                if (NPC.ai[0] > 30)
                {
                    Teleport(teleportPosition);
                    NPC.ai[0] = 0;
                }
            }
            else
            {
                NPC.ai[0] = 0;
            }
        }
    }
    public void Teleport(Vector2 pos)
    {
        Player player = Main.player[NPC.FindClosestPlayer()];
        if (Vector2.Distance(NPC.Center, player.Center) > 2000)
        {
            return;
        }
        NPC.Center = pos;
        for (int i = 0; i < 30; i++)
        {
            int dust = Dust.NewDust(new Vector2(NPC.oldPosition.X, NPC.oldPosition.Y), NPC.width, NPC.height, DustID.GiantCursedSkullBolt, 0f, 0f, 100);
            Main.dust[dust].velocity *= 3f;
            Main.dust[dust].scale *= 1.2f;
            Main.dust[dust].noGravity = true;
        }
        NPC.ai[0] = 0;
        for (int i = 0; i < 30; i++)
        {
            int dust = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.GiantCursedSkullBolt, 0f, 0f, 100);
            Main.dust[dust].velocity *= 3f;
            Main.dust[dust].scale *= 1.2f;
            Main.dust[dust].noGravity = true;
        }
    }
    /*public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
        Rectangle frame = texture.Frame();
        Vector2 frameOrigin = frame.Size() / 2f;

        Main.EntitySpriteDraw(texture, screenPos, frame, drawColor, NPC.rotation, frameOrigin, NPC.scale, SpriteEffects.None, 0);

        return false;
    }*/
    public override void HitEffect(int hitDirection, double damage)
    {
        if(NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            for (int i = 0; i < 70; i++)
            {
                int dust = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.GiantCursedSkullBolt, 0f, 0f, 100);
                Main.dust[dust].velocity *= 3f;
                Main.dust[dust].scale *= 1.6f;
                Main.dust[dust].noGravity = true;
            }
            for (int i = 0; i < 20; i++)
            {
                int dust = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.GiantCursedSkullBolt, 0f, 0f, 100);
                Main.dust[dust].velocity *= 6f;
                Main.dust[dust].scale *= 1.4f;
                Main.dust[dust].noGravity = true;
            }
            if(Main.netMode == NetmodeID.Server)
            {
                return;
            }
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity * 0.5f, Mod.Find<ModGore>("EctosphereGore2").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity * 0.5f, Mod.Find<ModGore>("EctosphereGore1").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity * 0.5f, Mod.Find<ModGore>("EctosphereGore1").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity * 0.5f, Mod.Find<ModGore>("EctosphereGore3").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity * 0.5f, Mod.Find<ModGore>("EctosphereGore3").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity * 0.5f, Mod.Find<ModGore>("EctosphereGore4").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity * 0.5f, Mod.Find<ModGore>("EctosphereGore4").Type, 1f);
        }
    }
    public override void FindFrame(int frameHeight)
    {
        NPC.frameCounter++;
        if (NPC.frameCounter >= 6)
        {
            NPC.frame.Y += frameHeight;
            NPC.frameCounter = 0;
        }
        if(currentMoveState == "Charge")
        {
            if (NPC.frame.Y > frameHeight * 5)
            {
                NPC.frame.Y = 0;
            }
        }
        if (currentMoveState == "Summoning")
        {
            if (NPC.frame.Y > frameHeight * 11)
            {
                NPC.frame.Y = frameHeight * 6;
            }
        }
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo) =>
        spawnInfo.Player.ZoneDungeon && Main.hardMode && ModContent.GetInstance<DownedBossSystem>().DownedArmageddon &&
        ModContent.GetInstance<AvalonWorld>().SuperHardmode
            ? 0.083f * AvalonGlobalNPC.EndoSpawnRate
            : 0f;
}
