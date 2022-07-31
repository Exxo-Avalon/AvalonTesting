using Terraria.GameContent.Bestiary;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using System;
using Terraria.Audio;
using System.IO;

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
        minLength = 4;
        maxLength = 5;
        tailType = ModContent.NPCType<TestGroundWormHead>();
        bodyType = ModContent.NPCType<TestGroundWormBody>();
        headType = ModContent.NPCType<TestGroundWormHead>();
        speed = 5.5f;
        turnSpeed = 0.045f;
        directional = true;
        snake = true;
    }
}

public abstract class Snake : ModNPC
{
    public int bodyType;
    public bool directional;

    public bool flies;

    /* ai[0] = follower
     * ai[1] = following
     * ai[2] = distanceFromTail
     * ai[3] = head
     */
    public bool head;
    public int headType;
    public int maxLength;
    public int minLength;
    public float speed;
    public bool tail;
    public int tailType;
    public float turnSpeed;
    public bool snake;

    // stored values
    public int mode;
    public float following;
    public float distanceFromTail;
    public float isHead;

    public override void SendExtraAI(BinaryWriter writer)
    {

    }
    public override void ReceiveExtraAI(BinaryReader reader)
    {

    }
    public override void AI()
    {
        if (NPC.localAI[1] == 0f)
        {
            NPC.localAI[1] = 1f;
            Init();
        }

        if (NPC.ai[3] > 0f)
        {
            NPC.realLife = (int)NPC.ai[3];
        }

        if (!head && NPC.timeLeft < 300)
        {
            NPC.timeLeft = 300;
        }

        if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
        {
            NPC.TargetClosest();
        }

        if (Main.player[NPC.target].dead && NPC.timeLeft > 300)
        {
            NPC.timeLeft = 300;
        }

        if (Main.netMode != NetmodeID.MultiplayerClient)
        {
            if (!tail && NPC.ai[0] == 0f)
            {
                if (head)
                {
                    NPC.ai[3] = NPC.whoAmI;
                    NPC.realLife = NPC.whoAmI;
                    NPC.ai[2] = Main.rand.Next(minLength, maxLength + 1);
                    NPC.ai[0] = NPC.NewNPC(NPC.GetSource_FromAI(), (int)(NPC.position.X + (NPC.width / 2)),
                        (int)(NPC.position.Y + NPC.height), bodyType, NPC.whoAmI);
                }
                else if (NPC.ai[2] > 0f)
                {
                    NPC.ai[0] = NPC.NewNPC(NPC.GetSource_FromAI(), (int)(NPC.position.X + (NPC.width / 2)),
                        (int)(NPC.position.Y + NPC.height), NPC.type, NPC.whoAmI);
                }
                else
                {
                    NPC.ai[0] = NPC.NewNPC(NPC.GetSource_FromAI(), (int)(NPC.position.X + (NPC.width / 2)),
                        (int)(NPC.position.Y + NPC.height), tailType, NPC.whoAmI);
                }

                Main.npc[(int)NPC.ai[0]].ai[3] = NPC.ai[3];
                Main.npc[(int)NPC.ai[0]].realLife = NPC.realLife;
                Main.npc[(int)NPC.ai[0]].ai[1] = NPC.whoAmI;
                Main.npc[(int)NPC.ai[0]].ai[2] = NPC.ai[2] - 1f;
                NPC.netUpdate = true;
            }

            if (!head && (!Main.npc[(int)NPC.ai[1]].active ||
                          (Main.npc[(int)NPC.ai[1]].type != headType && Main.npc[(int)NPC.ai[1]].type != bodyType)))
            {
                NPC.life = 0;
                NPC.HitEffect();
                NPC.active = false;
            }

            if (!tail && (!Main.npc[(int)NPC.ai[0]].active ||
                          (Main.npc[(int)NPC.ai[0]].type != bodyType && Main.npc[(int)NPC.ai[0]].type != tailType)))
            {
                NPC.life = 0;
                NPC.HitEffect();
                NPC.active = false;
            }

            if (!NPC.active && Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(MessageID.DamageNPC, -1, -1, null, NPC.whoAmI, -1f);
            }
        }

        int num180 = (int)(NPC.position.X / 16f) - 1;
        int num181 = (int)((NPC.position.X + NPC.width) / 16f) + 2;
        int num182 = (int)(NPC.position.Y / 16f) - 1;
        int num183 = (int)((NPC.position.Y + NPC.height) / 16f) + 2;
        if (num180 < 0)
        {
            num180 = 0;
        }

        if (num181 > Main.maxTilesX)
        {
            num181 = Main.maxTilesX;
        }

        if (num182 < 0)
        {
            num182 = 0;
        }

        if (num183 > Main.maxTilesY)
        {
            num183 = Main.maxTilesY;
        }

        bool flag18 = flies;
        if (!flag18)
        {
            for (int num184 = num180; num184 < num181; num184++)
            {
                for (int num185 = num182; num185 < num183; num185++)
                {
                    if (((Main.tile[num184, num185].HasUnactuatedTile &&
                        (Main.tileSolid[Main.tile[num184, num185].TileType] ||
                        (Main.tileSolidTop[Main.tile[num184, num185].TileType] &&
                            Main.tile[num184, num185].TileFrameY == 0))) ||
                        Main.tile[num184, num185].LiquidAmount > 64))
                    {
                        Vector2 vector17;
                        vector17.X = num184 * 16;
                        vector17.Y = num185 * 16;
                        if (NPC.position.X + NPC.width > vector17.X && NPC.position.X < vector17.X + 16f &&
                            NPC.position.Y + NPC.height > vector17.Y && NPC.position.Y < vector17.Y + 16f)
                        {
                            flag18 = true;
                            if (Main.rand.NextBool(100) && NPC.behindTiles &&
                                Main.tile[num184, num185].HasUnactuatedTile)
                            {
                                WorldGen.KillTile(num184, num185, true, true);
                            }

                            if (Main.netMode != NetmodeID.MultiplayerClient && Main.tile[num184, num185].TileType == 2)
                            {
                                ushort arg_BFCA_0 = Main.tile[num184, num185 - 1].TileType;
                            }
                        }
                    }
                }
            }
        }

        if (!flag18 && head)
        {
            var rectangle = new Rectangle((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height);
            int num186 = 1000;
            bool flag19 = true;
            for (int num187 = 0; num187 < 255; num187++)
            {
                if (Main.player[num187].active)
                {
                    var rectangle2 = new Rectangle((int)Main.player[num187].position.X - num186,
                        (int)Main.player[num187].position.Y - num186, num186 * 2, num186 * 2);
                    if (rectangle.Intersects(rectangle2))
                    {
                        flag19 = false;
                        break;
                    }
                }
            }

            if (flag19)
            {
                flag18 = true;
            }
        }

        if (directional)
        {
            if (NPC.velocity.X < 0f)
            {
                NPC.spriteDirection = 1;
            }
            else if (NPC.velocity.X > 0f)
            {
                NPC.spriteDirection = -1;
            }
        }

        CustomBehavior();
    }

    public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) => head ? null : false;

    public virtual void Init()
    {
    }

    public virtual bool ShouldRun() => false;

    public virtual void CustomBehavior()
    {
    }
}
