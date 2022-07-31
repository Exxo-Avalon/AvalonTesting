using Terraria.GameContent.Bestiary;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs;

public class TestGroundWormHead : TestGroundWorm
{
    public override string Texture => "Avalon/NPCs/MechanicalLeechHead";

    public override void SetDefaults()
    {
        NPC.width = 14;
        NPC.height = 14;
        NPC.aiStyle = 6;
        NPC.netAlways = true;
        NPC.damage = 40;
        NPC.defense = 6;
        NPC.lifeMax = 300;
        NPC.HitSound = SoundID.NPCHit4;
        NPC.DeathSound = SoundID.NPCDeath14;
        NPC.knockBackResist = 0f;
    }
    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.55f);
        NPC.damage = (int)(NPC.damage * 0.8f);
    }
    public override void Init()
    {
        base.Init();
        head = true;
        minLength = 6;
        maxLength = 10;
    }
}

public class TestGroundWormBody : TestGroundWorm
{
    public override string Texture => "Avalon/NPCs/MechanicalLeechBody";
    public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
    {
        return false;
    }
    public override void SetDefaults()
    {
        NPC.width = 14;
        NPC.height = 14;
        NPC.aiStyle = 6;
        NPC.netAlways = true;
        NPC.damage = 35;
        NPC.defense = 6;
        NPC.lifeMax = 300;
        NPC.HitSound = SoundID.NPCHit4;
        NPC.DeathSound = SoundID.NPCDeath14;
        NPC.noGravity = false;
        NPC.noTileCollide = false;
        NPC.knockBackResist = 0f;
    }
    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.55f);
        NPC.damage = (int)(NPC.damage * 0.8f);
    }
}

public class TestGroundWormTail : TestGroundWorm
{
    public override string Texture => "Avalon/NPCs/MechanicalLeechTail";
    public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
    {
        return false;
    }
    public override void SetDefaults()
    {
        NPC.width = 14;
        NPC.height = 14;
        NPC.aiStyle = 6;
        NPC.netAlways = true;
        NPC.damage = 30;
        NPC.defense = 15;
        NPC.lifeMax = 300;
        NPC.HitSound = SoundID.NPCHit4;
        NPC.DeathSound = SoundID.NPCDeath14;
        NPC.knockBackResist = 0f;
    }
    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.55f);
        NPC.damage = (int)(NPC.damage * 0.8f);
    }
    public override void Init()
    {
        base.Init();
        tail = true;
    }
}
public abstract class TestGroundWorm : Worm
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Test Ground Worm");
    }

    public override void Init()
    {
        minLength = 6;
        maxLength = 10;
        tailType = ModContent.NPCType<TestGroundWormHead>();
        bodyType = ModContent.NPCType<TestGroundWormBody>();
        headType = ModContent.NPCType<TestGroundWormHead>();
        speed = 5.5f;
        turnSpeed = 0.045f;
    }
}
