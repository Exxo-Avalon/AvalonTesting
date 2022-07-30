//using Avalon.Items.Accessories;
//using Avalon.Items.Material;

using Avalon.Items.Accessories;
using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;

namespace Avalon.NPCs;

public class DarkMatterSlimer : ModNPC
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Dark Matter Slimer");
        Main.npcFrameCount[NPC.type] = 4;
        NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
        {
            SpecificallyImmuneTo = new int[]
            {
                BuffID.Confused,
                BuffID.Poisoned
            }
        };
        NPCID.Sets.DebuffImmunitySets[Type] = debuffData;
    }

    public override void SetDefaults()
    {
        NPC.damage = 120;
        NPC.lifeMax = 760;
        NPC.defense = 30;
        NPC.width = 40;
        NPC.aiStyle = NPCAIStyleID.Bat;
        NPC.scale = 1.2f;
        NPC.value = 1000f;
        NPC.height = 30;
        AnimationType = NPCID.Slimer;
        NPC.knockBackResist = 0.05f;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        //Banner = NPC.type;
        //BannerItem = ModContent.ItemType<Items.Banners.DarkMatterSlimeBanner>();
        SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.DarkMatter>().Type };
    }
    public override void HitEffect(int hitDirection, double damage)
    {
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f, Mod.Find<ModGore>("DarkMatterSlimerWing").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f, Mod.Find<ModGore>("DarkMatterSlimerWing").Type, 1f);
        }
    }
    public override void OnKill()
    {
        NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<DarkMatterSlime>());
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement("Gelatinous, but also unstable. Made of dark matter.")
        });
    }
    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (spawnInfo.Player.GetModPlayer<Players.ExxoBiomePlayer>().ZoneDarkMatter && !spawnInfo.Player.InPillarZone() && ModContent.GetInstance<AvalonWorld>().SuperHardmode)
        {
            return 0.8f;
        }
        return 0f;
    }

    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.5f);
        NPC.damage = (int)(NPC.damage * 0.5f);
    }


    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.ByCondition(new DropConditions.DropIfNoArmaAlive(), ModContent.ItemType<DarkMatterGel>(), 1, 2, 4));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SixHundredWattLightbulb>(), 70));
    }

    public override void OnHitPlayer(Player target, int damage, bool crit)
    {
        target.AddBuff(ModContent.BuffType<Buffs.DarkInferno>(), 300);
    }
}
