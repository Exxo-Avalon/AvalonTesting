using Avalon.Players;
using Terraria.GameContent.Bestiary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.DataStructures;

namespace Avalon.NPCs;

public class GrossyFloat : ModNPC
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Grossy Float");
        Main.npcFrameCount[NPC.type] = 5;
        NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
        {
            SpecificallyImmuneTo = new int[]
            {
                BuffID.OnFire,
                BuffID.Poisoned,
                BuffID.CursedInferno
            }
        };
        NPCID.Sets.DebuffImmunitySets[Type] = debuffData;
    }

    public override void SetDefaults()
    {
        NPC.damage = 70;
        NPC.noTileCollide = true;
        NPC.lifeMax = 200;
        NPC.defense = 10;
        NPC.noGravity = true;
        NPC.alpha = 100;
        NPC.width = 24;
        NPC.aiStyle = 22;
        NPC.value = 500f;
        NPC.height = 56;
        NPC.knockBackResist = 0.7f;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath2;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<Items.Banners.GrossyFloatBanner>();
        SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.Contagion>().Type };
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement("Unimplemented")
        });
    }
    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.55f);
        NPC.damage = (int)(NPC.damage * 0.5f);
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
        NPC.rotation = NPC.velocity.X * 0.1f;
        if (NPC.type == NPCID.Bee || NPC.type == NPCID.BeeSmall)
        {
            NPC.frameCounter += 1.0;
            NPC.rotation = NPC.velocity.X * 0.2f;
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
    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (spawnInfo.Player.GetModPlayer<ExxoBiomePlayer>().ZoneContagion && spawnInfo.Player.ZoneRockLayerHeight)
            return (spawnInfo.Player.GetModPlayer<ExxoBiomePlayer>().ZoneContagion && spawnInfo.Player.ZoneRockLayerHeight) ? 1f : 0f;
        return 0f;
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Accessories.AmmoMagazine>(), 50));
    }
    public override void HitEffect(int hitDirection, double damage)
    {
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("GrossyFloatHead").Type, 0.9f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("GrossyFloatTail").Type, 0.9f);
        }
    }
}
