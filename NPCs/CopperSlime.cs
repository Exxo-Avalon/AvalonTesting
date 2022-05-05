﻿using AvalonTesting.Items.Banners;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.NPCs;

public class CopperSlime : ModNPC
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Copper Slime");
        Main.npcFrameCount[NPC.type] = 2;
    }

    public override void SetDefaults()
    {
        NPC.damage = 20;
        NPC.lifeMax = 200;
        NPC.defense = 3;
        NPC.width = 36;
        NPC.aiStyle = 1;
        NPC.value = 1000f;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.knockBackResist = 0.4f;
        NPC.height = 24;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<CopperSlimeBanner>();
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
            new FlavorTextBestiaryInfoElement("Gelatinous, but filled with minerals."),
        });

    public override void ModifyNPCLoot(NPCLoot loot) => loot.Add(ItemDropRule.Common(ItemID.CopperOre, 1, 15, 25));

    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.65f);
        NPC.damage = (int)(NPC.damage * 0.45f);
    }

    public override void FindFrame(int frameHeight)
    {
        int num2 = 0;
        if (NPC.aiAction == 0)
        {
            if (NPC.velocity.Y < 0f)
            {
                num2 = 2;
            }
            else if (NPC.velocity.Y > 0f)
            {
                num2 = 3;
            }
            else if (NPC.velocity.X != 0f)
            {
                num2 = 1;
            }
            else
            {
                num2 = 0;
            }
        }
        else if (NPC.aiAction == 1)
        {
            num2 = 4;
        }

        NPC.frameCounter += 1.0;
        if (num2 > 0)
        {
            NPC.frameCounter += 1.0;
        }

        if (num2 == 4)
        {
            NPC.frameCounter += 1.0;
        }

        if (NPC.frameCounter >= 8.0)
        {
            NPC.frame.Y = NPC.frame.Y + frameHeight;
            NPC.frameCounter = 0.0;
        }

        if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
        {
            NPC.frame.Y = 0;
        }
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo) =>
        spawnInfo.Player.ZoneRockLayerHeight && !spawnInfo.Player.ZoneDungeon &&
        (Main.hardMode || WorldGen.SavedOreTiers.Copper == TileID.Copper)
            ? 0.00526f * AvalonTestingGlobalNPC.EndoSpawnRate
            : 0f;
}
