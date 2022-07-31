using Avalon.Items.Banners;
using Avalon.Items.Placeable.Tile;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs;

public class TropicalSlime : ModNPC
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Tropical Slime");
        Main.npcFrameCount[NPC.type] = 2;
    }

    public override void SetDefaults()
    {
        NPC.damage = 19;
        NPC.lifeMax = 60;
        NPC.defense = 1;
        NPC.width = 36;
        NPC.aiStyle = 1;
        NPC.value = 1000f;
        NPC.knockBackResist = 0.4f;
        AnimationType = NPCID.BlueSlime;
        NPC.height = 24;
        NPC.scale = 1.2f;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<TropicalSlimeBanner>();
    }
    public override void HitEffect(int hitDirection, double damage)
    {
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            for (int i = 0; i < 4; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.TropicalMudDust>());
                Main.dust[d].noGravity = true;
            }
        }
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
            new FlavorTextBestiaryInfoElement("Slimy tropics."),
        });

    public override void ModifyNPCLoot(NPCLoot loot) =>
        loot.Add(ItemDropRule.Common(ItemID.Gel, 1, 1, 3));

    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.65f);
        NPC.damage = (int)(NPC.damage * 0.45f);
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo) =>
        spawnInfo.Player.GetModPlayer<Players.ExxoBiomePlayer>().ZoneTropics && !spawnInfo.Player.ZoneDungeon
            ? 0.5f * AvalonGlobalNPC.EndoSpawnRate
            : 0f;
}
