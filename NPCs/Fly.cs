using System;
using Avalon.Items.Material;
using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;

namespace Avalon.NPCs;

public class Fly : ModNPC
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Fly");
        Main.npcFrameCount[NPC.type] = 4;
    }

    public override void SetDefaults()
    {
        NPC.damage = 20;
        NPC.lifeMax = 20;
        NPC.defense = 5;
        NPC.noGravity = true;
        NPC.width = 16;
        NPC.aiStyle = 5;
        NPC.npcSlots = 1f;
        NPC.value = 110f;
        NPC.height = 18;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        AnimationType = NPCID.Bee;
        NPC.knockBackResist = 0.5f;
        //Banner = NPC.type;
        //BannerItem = ModContent.ItemType<Items.Banners.BactusBanner>();
        //SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.Tropics>().Type };
        //DrawOffsetY = 10;
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement("")
        });
    }
    //public override float SpawnChance(NPCSpawnInfo spawnInfo)
    //{
    //    if (spawnInfo.Player.GetModPlayer<ExxoBiomePlayer>().ZoneContagion && spawnInfo.Player.ZoneOverworldHeight && !spawnInfo.Player.InPillarZone())
    //        return 1;
    //    return 0;
    //}
    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.55f);
        NPC.damage = (int)(NPC.damage * 0.65f);
    }

    public override void FindFrame(int frameHeight)
    {
        NPC.frameCounter++;
        if (NPC.frameCounter >= 8.0)
        {
            NPC.frame.Y += frameHeight;
            NPC.frameCounter = 0.0;
        }
        if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
        {
            NPC.frame.Y = 0;
        }
    }

    //public override void HitEffect(int hitDirection, double damage)
    //{
    //    if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
    //    {
    //        Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f, Mod.Find<ModGore>("Bactus").Type, 1f);
    //    }
    //}
}
