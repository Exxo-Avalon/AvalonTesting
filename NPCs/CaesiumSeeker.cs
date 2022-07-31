using Avalon.Items.Placeable.Tile;
using Avalon.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;

namespace Avalon.NPCs;

public class CaesiumSeekerHead : CaesiumSeekerWorm
{
    public override string Texture => "Avalon/NPCs/CaesiumSeekerHead";
    public override void SetDefaults()
    {
        NPC.width = 50;
        NPC.height = 50;
        NPC.aiStyle = 6;
        NPC.scale = 0.8f;
        NPC.netAlways = true;
        NPC.damage = 65;
        NPC.defense = 15;
        NPC.lifeMax = 1800;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.noGravity = true;
        NPC.noTileCollide = true;
        NPC.value = 1000;
        NPC.knockBackResist = 0f;
        NPC.behindTiles = true;
        DrawOffsetY = 25;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<Items.Banners.CaesiumSeekerBanner>();
        SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.CaesiumBlastplains>().Type };
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement("These worms are some of the longest discovered.")
        });
    }
    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (spawnInfo.Player.AvalonBiome().ZoneCaesium && spawnInfo.Player.ZoneUnderworldHeight && !NPC.AnyNPCs(NPCID.WallofFlesh)) // && !NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.WallofSteel>()))
            return 0.6f;
        return 0;
    }
    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.55f);
        NPC.damage = (int)(NPC.damage * 0.5f);
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.DownedAllMechBosses(), ModContent.ItemType<Items.Ore.CaesiumOre>(), 10, 2, 5));
    }
    public override void HitEffect(int hitDirection, double damage)
    {
        if (Main.netMode == NetmodeID.Server) return;
        if (NPC.life <= 0)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("CaesiumSeekerHead").Type, 0.8f);
        }
    }
    public override void Init()
    {
        base.Init();
        head = true;
        minLength = 10;
        maxLength = 18;
    }
}

public class CaesiumSeekerBody : CaesiumSeekerWorm
{
    public override string Texture => "Avalon/NPCs/CaesiumSeekerBody";

    public override void SetDefaults()
    {
        NPC.width = 50;
        NPC.height = 50;
        NPC.aiStyle = 6;
        NPC.netAlways = true;
        NPC.damage = 60;
        NPC.scale = 0.8f;
        NPC.defense = 45;
        NPC.lifeMax = 1800;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.noGravity = true;
        NPC.noTileCollide = true;
        NPC.knockBackResist = 0f;
        NPC.behindTiles = true;
        DrawOffsetY = 25;
    }
    public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
    {
        return false;
    }
    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.55f * bossLifeScale);
        NPC.damage = (int)(NPC.damage * 0.5f);
    }
    public override void HitEffect(int hitDirection, double damage)
    {
        if (Main.netMode == NetmodeID.Server) return;
        if (NPC.life <= 0)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("CaesiumSeekerBody").Type, 0.8f);
        }
    }
}

public class CaesiumSeekerTail : CaesiumSeekerWorm
{
    public override string Texture => "Avalon/NPCs/CaesiumSeekerTail";

    public override void SetDefaults()
    {
        NPC.width = 50;
        NPC.height = 50;
        NPC.aiStyle = 6;
        NPC.scale = 0.8f;
        NPC.netAlways = true;
        NPC.damage = 49;
        NPC.defense = 15;
        NPC.lifeMax = 1800;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.noGravity = true;
        NPC.noTileCollide = true;
        NPC.knockBackResist = 0f;
        NPC.behindTiles = true;
        DrawOffsetY = 25;
    }
    public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
    {
        return false;
    }
    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.55f * bossLifeScale);
        NPC.damage = (int)(NPC.damage * 0.5f);
    }
    public override void HitEffect(int hitDirection, double damage)
    {
        if (Main.netMode == NetmodeID.Server) return;
        if (NPC.life <= 0)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("CaesiumSeekerTail").Type, 0.8f);
        }
    }
    public override void Init()
    {
        base.Init();
        tail = true;
    }
}
public abstract class CaesiumSeekerWorm : Worm
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Caesium Seeker");
        NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
        {
            SpecificallyImmuneTo = new int[]
            {
                BuffID.OnFire,
                BuffID.CursedInferno,
                BuffID.Daybreak
            }
        };
        NPCID.Sets.DebuffImmunitySets[Type] = debuffData;
        var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
        { // Influences how the NPC looks in the Bestiary
            CustomTexturePath = "Avalon/Assets/Bestiary/CaesiumSeeker", // If the NPC is multiple parts like a worm, a custom texture for the Bestiary is encouraged.
            Position = new Vector2(40f, 24f),
            PortraitPositionXOverride = 0f,
            PortraitPositionYOverride = 12f,
            Scale = 0.5f
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
    }

    public override void Init()
    {
        minLength = 10;
        maxLength = 18;
        tailType = ModContent.NPCType<CaesiumSeekerTail>();
        bodyType = ModContent.NPCType<CaesiumSeekerBody>();
        headType = ModContent.NPCType<CaesiumSeekerHead>();
        speed = 15f;
        turnSpeed = 0.15f;
    }
}
