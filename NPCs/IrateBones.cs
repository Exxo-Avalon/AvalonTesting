using System;
using Avalon.Items.Banners;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs;

public class IrateBones : ModNPC
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Irate Bones");
        Main.npcFrameCount[NPC.type] = 15;
        var debuffData = new NPCDebuffImmunityData { SpecificallyImmuneTo = new[] { BuffID.Confused } };
        NPCID.Sets.DebuffImmunitySets[Type] = debuffData;
    }

    public override void SetDefaults()
    {
        NPC.damage = 35;
        NPC.lifeMax = 350;
        NPC.defense = 15;
        NPC.lavaImmune = true;
        NPC.width = 18;
        NPC.aiStyle = 3;
        NPC.value = 1000f;
        NPC.height = 40;
        NPC.knockBackResist = 0.5f;
        NPC.HitSound = SoundID.NPCHit2;
        NPC.DeathSound = SoundID.NPCDeath2;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<IrateBonesBanner>();
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
            new FlavorTextBestiaryInfoElement("These skeletons are simply armored Angry Bones."),
        });

    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.7f);
        NPC.damage = (int)(NPC.damage * 0.5f);
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
            NPC.frame.Y = frameHeight;
        }
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo) => Main.hardMode && spawnInfo.Player.ZoneDungeon
        ? 0.6f * AvalonGlobalNPC.EndoSpawnRate
        : 0f;

    public override void HitEffect(int hitDirection, double damage)
    {
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity,
                Mod.Find<ModGore>("IrateBonesHelmet").Type);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, 43);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, 43);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, 44);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, 44);
        }
    }
}
