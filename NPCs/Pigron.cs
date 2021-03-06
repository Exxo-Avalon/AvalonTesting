using Terraria.GameContent.Bestiary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Avalon.NPCs;

public class Pigron : ModNPC
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Pigron");
        Main.npcFrameCount[NPC.type] = 14;
        NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
        {
            SpecificallyImmuneTo = new int[]
            {
                BuffID.Confused
            }
        };
        NPCID.Sets.DebuffImmunitySets[Type] = debuffData;
    }

    public override void SetDefaults()
    {
        NPC.damage = 70;
        NPC.lifeMax = 230;
        NPC.defense = 16;
        NPC.width = 44;
        NPC.aiStyle = 2;
        AIType = NPCID.PigronCorruption;
        NPC.value = 2000f;
        NPC.height = 36;
        NPC.knockBackResist = 0.5f;
        NPC.HitSound = SoundID.NPCHit27;
        NPC.DeathSound = SoundID.NPCDeath30;
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new ModBiomeBestiaryInfoElement(Mod, "Contagion", "Assets/Bestiary/ContagionIcon", "Assets/Bestiary/ContagionBG", null),
            new FlavorTextBestiaryInfoElement("This elusive dragon-pig hybrid has excellent stealth capabilities despite its rotund figure. It is uncertain how they came to exist.")
        });
    }
    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        return (spawnInfo.Player.GetModPlayer<Players.ExxoBiomePlayer>().ZoneUndergroundContagion &&
            Main.SceneMetrics.SnowTileCount > 200 && !spawnInfo.Player.InPillarZone() && Main.hardMode) ? 0.7f : 0f;
    }
    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.55f * bossLifeScale);
        NPC.damage = (int)(NPC.damage * 0.8f);
    }
    public override void FindFrame(int frameHeight)
    {
        NPC.spriteDirection = NPC.direction;
        NPC.frameCounter++;
        if (NPC.frameCounter >= 4.0)
        {
            NPC.frame.Y += frameHeight;
            NPC.frameCounter = 0.0;
        }
        if (NPC.frame.Y >= frameHeight * 14)
        {
            NPC.frame.Y = 0;
        }
    }
}
