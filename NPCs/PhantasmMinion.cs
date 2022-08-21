using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs;

public class PhantasmMinion : ModNPC
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Phantasm Minion");
        Main.npcFrameCount[NPC.type] = 3;
    }

    public override void SetDefaults()
    {
        NPC.damage = 41;
        NPC.lifeMax = 600;
        NPC.defense = 40;
        NPC.width = 24;
        NPC.aiStyle = -1;
        NPC.value = 1000f;
        NPC.knockBackResist = 0.2f;
        NPC.height = 24;
        NPC.noGravity = true;
        NPC.noTileCollide = true;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath6;
    }
    public override Color? GetAlpha(Color drawColor)
    {
        return Color.White;
    }
    public Vector2 towardsBoss;
    public override void AI()
    {
        if (AvalonGlobalNPC.PhantasmBoss != -1)
        {
            NPC boss = Main.npc[AvalonGlobalNPC.PhantasmBoss];
            if (NPC.ai[0] == 0)
            {
                towardsBoss = NPC.Center - boss.Center;
                for (int i = 0; i < 20; i++)
                {
                    int num893 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num893].velocity *= 2f;
                    Main.dust[num893].scale = 1.5f;
                    Main.dust[num893].noGravity = true;
                }
                NPC.ai[0]++;
            }
            towardsBoss = towardsBoss.RotatedBy(0.05);
            NPC.Center = Vector2.Lerp(NPC.Center ,boss.Center - towardsBoss, 0.4f);
        }
    }
    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.55f * bossLifeScale);
        NPC.damage = (int)(NPC.damage * 0.8f);
    }
    public override void FindFrame(int frameHeight)
    {
        NPC.frameCounter++;
        if (NPC.frameCounter >= 6.0)
        {
            NPC.frame.Y += frameHeight;
            NPC.frameCounter = 0.0;
        }
        if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
        {
            NPC.frame.Y = 0;
        }
    }
}
