using AvalonTesting.Items.Banners;
using AvalonTesting.Items.Material;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.NPCs;

public class ArmoredHellTortoise : ModNPC
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Armored Hell Tortoise");
        Main.npcFrameCount[NPC.type] = 8;
        var debuffData = new NPCDebuffImmunityData
        {
            SpecificallyImmuneTo = new[] { BuffID.Confused, BuffID.OnFire },
        };
        NPCID.Sets.DebuffImmunitySets[Type] = debuffData;
    }

    public override void SetDefaults()
    {
        NPC.damage = 130;
        NPC.lifeMax = 1600;
        NPC.defense = 30;
        NPC.lavaImmune = true;
        NPC.noGravity = false;
        NPC.width = 46;
        NPC.aiStyle = 39;
        NPC.npcSlots = 2f;
        NPC.value = 10000f;
        NPC.height = 32;
        NPC.knockBackResist = 0.2f;
        NPC.HitSound = SoundID.NPCHit24;
        NPC.DeathSound = SoundID.NPCDeath27;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<ArmoredHellTortoiseBanner>();
    }

    public override void ModifyNPCLoot(NPCLoot loot) =>
        loot.Add(ItemDropRule.Common(ModContent.ItemType<SpikedBlastShell>(), 9));

    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.35f);
        NPC.damage = (int)(NPC.damage * 0.5f);
    }

    public override void HitEffect(int hitDirection, double damage)
    {
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity,
                Mod.Find<ModGore>("ArmoredHellTortoiseGore1").Type, 0.9f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity,
                Mod.Find<ModGore>("ArmoredHellTortoiseGore2").Type, 0.9f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity,
                Mod.Find<ModGore>("ArmoredHellTortoiseGore3").Type, 0.9f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity,
                Mod.Find<ModGore>("ArmoredHellTortoiseGore3").Type, 0.9f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity,
                Mod.Find<ModGore>("ArmoredHellTortoiseGore3").Type, 0.9f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity,
                Mod.Find<ModGore>("ArmoredHellTortoiseGore3").Type, 0.9f);
        }
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo) =>
        Main.hardMode && ModContent.GetInstance<AvalonTestingWorld>().SuperHardmode &&
        spawnInfo.Player.ZoneUnderworldHeight
            ? 0.125f * AvalonTestingGlobalNPC.EndoSpawnRate
            : 0f;
}
