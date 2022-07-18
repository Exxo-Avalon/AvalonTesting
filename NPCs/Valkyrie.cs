using AvalonTesting.Items.Banners;
using AvalonTesting.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.NPCs;

public class Valkyrie : ModNPC
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Valkyrie");
        Main.npcFrameCount[NPC.type] = 6;
    }

    public override void SetDefaults()
    {
        NPC.damage = 80;
        NPC.lifeMax = 2421;
        NPC.defense = 35;
        NPC.width = 24;
        NPC.aiStyle = 14;
        NPC.value = 1000f;
        NPC.knockBackResist = 0.05f;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.height = 34;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<ValkyrieBanner>();
        SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.SkyFortress>().Type };
        AnimationType = 48;
    }

    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.65f);
        NPC.damage = (int)(NPC.damage * 0.45f);
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo) =>
        spawnInfo.Player.GetModPlayer<ExxoBiomePlayer>().ZoneSkyFortress
            ? 0.26f * AvalonTestingGlobalNPC.EndoSpawnRate
            : 0f;
}
