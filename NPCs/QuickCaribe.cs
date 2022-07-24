using System;
using Avalon.Items.Banners;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs;

public class QuickCaribe : ModNPC
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Quick Caribe");
        Main.npcFrameCount[NPC.type] = 6;
        var debuffData = new NPCDebuffImmunityData { SpecificallyImmuneTo = new[] { BuffID.Confused } };
        NPCID.Sets.DebuffImmunitySets[Type] = debuffData;
    }

    public override void SetDefaults()
    {
        NPC.damage = 80;
        NPC.lifeMax = 150;
        NPC.defense = 22;
        NPC.noGravity = true;
        NPC.width = 32;
        NPC.aiStyle = -1;
        NPC.value = Item.buyPrice(0, 0, 50);
        NPC.timeLeft = 750;
        NPC.height = 24;
        NPC.knockBackResist = 0.8f;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<QuickCaribeBanner>();
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
            new FlavorTextBestiaryInfoElement(
                "This aggressive fish is capable of leaping out of the water to attack you. Be warned!"),
        });

    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.55f);
        NPC.damage = (int)(NPC.damage * 0.8f);
    }

    public override void AI()
    {
        if (NPC.direction == 0)
        {
            NPC.TargetClosest();
        }

        if (!NPC.wet)
        {
            bool flag78 = false;
            NPC.TargetClosest(false);
            if (Main.player[NPC.target].wet && !Main.player[NPC.target].dead)
            {
                flag78 = true;
            }

            var vector162 = new Vector2(NPC.position.X + (NPC.width * 0.5f), NPC.position.Y + (NPC.height * 0.5f));
            float num1210 = Main.player[NPC.target].position.X + (Main.player[NPC.target].width * 0.5f) - vector162.X;
            float num1211 = Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * 0.5f) - vector162.Y;
            num1210 += Main.rand.Next(-20, 21);
            num1211 += Main.rand.Next(-20, 21);
            float num1212 = (float)Math.Sqrt((num1210 * num1210) + (num1211 * num1211));
            if (num1212 <= 200f && NPC.ai[1] <= 0f && NPC.velocity.Y < 0.6f && !flag78)
            {
                NPC.TargetClosest();
                int num1213 = (int)vector162.X / 16;
                int num1214 = (int)vector162.Y / 16;
                int num1215 = 10;
                //if (Main.tile[num1213, num1214 + 1] == null)
                //{
                //    Main.tile[num1213, num1214 + 1] = new Tile();
                //}
                if (Main.tile[num1213, num1214 + 1].LiquidAmount < 128)
                {
                    num1215 = 5;
                }

                if (num1210 > 16f)
                {
                    NPC.velocity.X = 3f;
                }
                else if (num1210 < -16f)
                {
                    NPC.velocity.X = -3f;
                }

                NPC.velocity.Y = -num1215;
                NPC.ai[1] = 70f;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.netUpdate = true;
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, NetworkText.FromLiteral(""), NPC.whoAmI);
                }
            }
            else if (NPC.ai[1] > 0f)
            {
                NPC.ai[1] = NPC.ai[1] - 1f;
            }

            if (NPC.velocity.Y == 0f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.velocity.Y = Main.rand.Next(-50, -20) * 0.1f;
                NPC.velocity.X = Main.rand.Next(-20, 20) * 0.1f;
                NPC.netUpdate = true;
                NetMessage.SendData(MessageID.SyncNPC, -1, -1, NetworkText.FromLiteral(""), NPC.whoAmI);
            }

            NPC.velocity.Y = NPC.velocity.Y + 0.3f;
            if (NPC.velocity.Y > 10f)
            {
                NPC.velocity.Y = 10f;
            }

            NPC.ai[0] = 1f;
            return;
        }

        bool flag79 = false;
        NPC.TargetClosest(false);
        if (Main.player[NPC.target].wet && !Main.player[NPC.target].dead)
        {
            flag79 = true;
        }

        var vector163 = new Vector2(NPC.position.X + (NPC.width * 0.5f), NPC.position.Y + (NPC.height * 0.5f));
        float num1216 = Main.player[NPC.target].position.X + (Main.player[NPC.target].width * 0.5f) - vector163.X;
        float num1217 = Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * 0.5f) - vector163.Y;
        num1216 += Main.rand.Next(-20, 21);
        num1217 += Main.rand.Next(-20, 21);
        float num1218 = (float)Math.Sqrt((num1216 * num1216) + (num1217 * num1217));
        if (!flag79)
        {
            if (NPC.collideX)
            {
                NPC.velocity.X = NPC.velocity.X * -1f;
                NPC.direction *= -1;
                NPC.netUpdate = true;
            }

            if (NPC.collideY)
            {
                NPC.netUpdate = true;
                if (NPC.velocity.Y > 0f)
                {
                    NPC.velocity.Y = Math.Abs(NPC.velocity.Y) * -1f;
                    NPC.directionY = -1;
                    NPC.ai[0] = -1f;
                }
                else if (NPC.velocity.Y < 0f)
                {
                    NPC.velocity.Y = Math.Abs(NPC.velocity.Y);
                    NPC.directionY = 1;
                    NPC.ai[0] = 1f;
                }
            }
        }

        if (flag79)
        {
            NPC.TargetClosest();
            NPC.velocity.X = NPC.velocity.X + (NPC.direction * 0.15f);
            NPC.velocity.Y = NPC.velocity.Y + (NPC.directionY * 0.15f);
            if (NPC.velocity.X > 6f)
            {
                NPC.velocity.X = 6f;
            }

            if (NPC.velocity.X < -6f)
            {
                NPC.velocity.X = -6f;
            }

            if (NPC.velocity.Y > 4f)
            {
                NPC.velocity.Y = 4f;
            }

            if (NPC.velocity.Y < -4f)
            {
                NPC.velocity.Y = -4f;
                return;
            }

            return;
        }

        NPC.velocity.X = NPC.velocity.X + (NPC.direction * 0.1f);
        if (NPC.velocity.X < -2.5f || NPC.velocity.X > 2.5f)
        {
            NPC.velocity.X = NPC.velocity.X * 0.95f;
        }

        if (num1218 <= 300.0)
        {
            NPC.TargetClosest();
            NPC.velocity.Y = NPC.velocity.Y - 0.15f;
        }
        else if (NPC.ai[0] == -1f)
        {
            NPC.velocity.Y = NPC.velocity.Y - 0.015f;
            if (NPC.velocity.Y < -0.50000002235174179)
            {
                NPC.ai[0] = 1f;
            }
        }
        else
        {
            NPC.velocity.Y = NPC.velocity.Y + 0.015f;
            if (NPC.velocity.Y > 0.50000002235174179)
            {
                NPC.ai[0] = -1f;
            }
        }

        int num1219 = (int)(NPC.position.X + (NPC.width / 2)) / 16;
        int num1220 = (int)(NPC.position.Y + (NPC.height / 2)) / 16;
        //if (Main.tile[num1219, num1220 - 1] == null)
        //{
        //    Main.tile[num1219, num1220 - 1] = new Tile();
        //}
        //if (Main.tile[num1219, num1220 + 1] == null)
        //{
        //    Main.tile[num1219, num1220 + 1] = new Tile();
        //}
        //if (Main.tile[num1219, num1220 + 2] == null)
        //{
        //    Main.tile[num1219, num1220 + 2] = new Tile();
        //}
        if (Main.tile[num1219, num1220 - 1].LiquidType > 128)
        {
            if (Main.tile[num1219, num1220 + 1].HasTile)
            {
                NPC.ai[0] = -1f;
            }
            else if (Main.tile[num1219, num1220 + 2].HasTile)
            {
                NPC.ai[0] = -1f;
            }
        }

        if (NPC.velocity.Y > 0.60000002384185791 || NPC.velocity.Y < -0.60000002384185791)
        {
            NPC.velocity.Y = NPC.velocity.Y * 0.95f;
            return;
        }
    }

    public override void FindFrame(int frameHeight)
    {
        NPC.spriteDirection = NPC.direction;
        NPC.frameCounter += 1.0;
        if (NPC.wet)
        {
            if (NPC.frameCounter < 6.0)
            {
                NPC.frame.Y = 0;
            }
            else if (NPC.frameCounter < 12.0)
            {
                NPC.frame.Y = frameHeight;
            }
            else if (NPC.frameCounter < 18.0)
            {
                NPC.frame.Y = frameHeight * 2;
            }
            else if (NPC.frameCounter < 24.0)
            {
                NPC.frame.Y = frameHeight * 3;
            }
            else
            {
                NPC.frameCounter = 0.0;
            }
        }
        else if (NPC.frameCounter < 6.0)
        {
            NPC.frame.Y = frameHeight * 4;
        }
        else if (NPC.frameCounter < 12.0)
        {
            NPC.frame.Y = frameHeight * 5;
        }
        else
        {
            NPC.frameCounter = 0.0;
        }
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo) =>
        spawnInfo.Player.ZoneJungle && !spawnInfo.Player.InPillarZone() && Main.hardMode &&
        ModContent.GetInstance<AvalonWorld>().SuperHardmode
            ? 0.041f * AvalonGlobalNPC.EndoSpawnRate
            : 0f;
}
