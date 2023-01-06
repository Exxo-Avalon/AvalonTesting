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

public class DarkMatterSlime : ModNPC
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Dark Matter Slime");
        Main.npcFrameCount[NPC.type] = 2;
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
        NPC.damage = 100;
        NPC.lifeMax = 760;
        NPC.defense = 30;
        NPC.width = 32;
        NPC.aiStyle = 1;
        NPC.scale = 1.4f;
        NPC.value = 1000f;
        NPC.height = 24;
        NPC.knockBackResist = 0.1f;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<Items.Banners.DarkMatterSlimeBanner>();
        SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.DarkMatter>().Type };
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
            return 1f;
        }
        return 0f;
    }

    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.5f);
        NPC.damage = (int)(NPC.damage * 0.5f);
    }

    public override void PostAI()
    {
        NPC.TargetClosest(true);
        NPC.ai[0] += 1;
        if (NPC.ai[0] == -2000)
        {
            NPC.velocity.Y = -10;
        }
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.ByCondition(new DropConditions.DropIfNoArmaAlive(), ModContent.ItemType<DarkMatterGel>(), 1, 2, 4));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SixHundredWattLightbulb>(), 70));
    }
    public override void FindFrame(int frameHeight)
    {
        var num2 = 0;
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

    public override void OnHitPlayer(Player target, int damage, bool crit)
    {
        target.AddBuff(ModContent.BuffType<Buffs.DarkInferno>(), 300);
        if (Main.rand.NextBool(3))
        {
            target.AddBuff(BuffID.Blackout, 90);
        }
    }
}
