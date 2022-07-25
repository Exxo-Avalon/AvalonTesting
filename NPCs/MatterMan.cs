using Terraria.GameContent.Bestiary;
using System;
using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;

namespace Avalon.NPCs;

public class MatterMan : ModNPC
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Matter Man");
        Main.npcFrameCount[NPC.type] = 5;
        NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
        {
            SpecificallyImmuneTo = new int[]
            {
                BuffID.Confused,
                BuffID.CursedInferno,
                BuffID.OnFire
            }
        };
        NPCID.Sets.DebuffImmunitySets[Type] = debuffData;
    }

    public override void SetDefaults()
    {
        NPC.damage = 100;
        NPC.netAlways = true;
        NPC.scale = 1f;
        NPC.lifeMax = 1200;
        NPC.defense = 40;
        NPC.width = 18;
        NPC.aiStyle = NPCAIStyleID.Unicorn;
        NPC.value = Item.buyPrice(0, 1, 0, 0);
        NPC.height = 40;
        NPC.knockBackResist = 0.01f;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath2;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<Items.Banners.MatterManBanner>();
        SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.DarkMatter>().Type };
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement("A man that matters.")
        });
    }
    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.65f);
        NPC.damage = (int)(NPC.damage * 0.6f);
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (spawnInfo.Player.GetModPlayer<ExxoBiomePlayer>().ZoneDarkMatter && !spawnInfo.Player.InPillarZone() && ModContent.GetInstance<AvalonWorld>().SuperHardmode)
        {
            return 1f;
        }
        return 0f;
    }

    public override void FindFrame(int frameHeight)
    {
        if (NPC.velocity.X > 0f)
        {
            NPC.spriteDirection = 1;
        }
        if (NPC.velocity.X < 0f)
        {
            NPC.spriteDirection = -1;
        }
        NPC.frameCounter += 1.0;
        if (NPC.frameCounter >= 6.0)
        {
            NPC.frame.Y = NPC.frame.Y + frameHeight;
            NPC.frameCounter = 0.0;
        }
        if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
        {
            NPC.frame.Y = 0;
        }
    }

    public override void HitEffect(int hitDirection, double damage)
    {
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MatterManHead").Type, 1f);
        }
    }
}
