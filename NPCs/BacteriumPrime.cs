using System;
using Avalon.Items.BossBags;
using Avalon.Items.Placeable.Tile;
using Avalon.Items.Placeable.Trophy;
using Avalon.Items.Vanity;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs;

[AutoloadBossHead]
public class BacteriumPrime : ModNPC
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bacterium Prime");
        Main.npcFrameCount[NPC.type] = 8;
    }

    public override void SetDefaults()
    {
        NPC.damage = 31;
        NPC.boss = true;
        NPC.noTileCollide = true;
        NPC.lifeMax = 1500;
        NPC.defense = 10;
        NPC.noGravity = true;
        NPC.width = 120;
        NPC.aiStyle = -1;
        NPC.npcSlots = 6f;
        NPC.value = 50000f;
        NPC.timeLeft = NPC.activeTime * 30;
        NPC.height = 120;
        NPC.HitSound = SoundID.NPCHit8;
        NPC.DeathSound = SoundID.NPCDeath10;
        NPC.knockBackResist = 0f;
        Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/BacteriumPrime");
        //bossBag = ModContent.ItemType<Items.BossBags.BacteriumPrimeBossBag>();
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BacteriumPrimeTrophy>(), 10));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BacteriumPrimeMask>(), 10));
        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<BacteriumPrimeBossBag>()));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Ore.PandemiteOre>(), 1, 15, 41));
        if (!ModContent.GetInstance<DownedBossSystem>().DownedBacteriumPrime)
        {
            ModContent.GetInstance<DownedBossSystem>().DownedBacteriumPrime = true;
        }
    }

    public override void AI()
    {
        if (NPC.type == ModContent.NPCType<BacteriumPrime>())
        {
            AvalonGlobalNPC.BoogerBoss = NPC.whoAmI;
        }

        if (Main.player[NPC.target].dead)
        {
            NPC.velocity.Y = NPC.velocity.Y - 0.04f;
            if (NPC.timeLeft > 10)
            {
                NPC.timeLeft = 10;
                return;
            }
        }

        if (Main.netMode != NetmodeID.MultiplayerClient && NPC.localAI[0] == 0f)
        {
            NPC.localAI[0] = 1f;
            for (int num898 = 0; num898 < 20; num898++)
            {
                float num899 = NPC.Center.X;
                float num900 = NPC.Center.Y;
                num899 += Main.rand.Next(-NPC.width, NPC.width);
                num900 += Main.rand.Next(-NPC.height, NPC.height);
                int num901 = NPC.NewNPC(NPC.GetSource_FromAI(), (int)num899, (int)num900,
                    ModContent.NPCType<BactusMinion>());
                Main.npc[num901].velocity = new Vector2(Main.rand.Next(-30, 31) * 0.1f, Main.rand.Next(-30, 31) * 0.1f);
                Main.npc[num901].netUpdate = true;
            }
        }

        if (Main.netMode != NetmodeID.MultiplayerClient)
        {
            NPC.TargetClosest();
            int num902 = 6000;
            if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) +
                Math.Abs(NPC.Center.Y - Main.player[NPC.target].Center.Y) > num902)
            {
                NPC.active = false;
                NPC.life = 0;
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, NetworkText.FromLiteral(""), NPC.whoAmI);
                }
            }
        }

        if (NPC.ai[0] < 0f)
        {
            if (NPC.localAI[2] == 0f)
            {
                SoundEngine.PlaySound(SoundID.NPCHit1, NPC.position);
                NPC.localAI[2] = 1f;
                for (int num903 = 0; num903 < 20; num903++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, Main.rand.Next(-30, 31) * 0.2f,
                        Main.rand.Next(-30, 31) * 0.2f);
                }

                if (NPC.type == ModContent.NPCType<BacteriumPrime>())
                {
                    NPC.lifeMax = 1500;
                    NPC.life = 1500;
                }

                SoundEngine.PlaySound(SoundID.Roar, NPC.position);
            }

            NPC.dontTakeDamage = false;
            NPC.knockBackResist = 0.5f;
            NPC.TargetClosest();
            var vector109 = new Vector2(NPC.Center.X, NPC.Center.Y);
            float num904 = Main.player[NPC.target].Center.X - vector109.X;
            float num905 = Main.player[NPC.target].Center.Y - vector109.Y;
            float num906 = (float)Math.Sqrt((num904 * num904) + (num905 * num905));
            float num907 = 8f;
            num906 = num907 / num906;
            num904 *= num906;
            num905 *= num906;
            NPC.velocity.X = ((NPC.velocity.X * 50f) + num904) / 51f;
            NPC.velocity.Y = ((NPC.velocity.Y * 50f) + num905) / 51f;
            if (NPC.ai[0] == -1f)
            {
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    return;
                }

                NPC.localAI[1] += 1f;
                if (NPC.localAI[1] >= 60 + Main.rand.Next(120))
                {
                    NPC.localAI[1] = 0f;
                    NPC.TargetClosest();
                    int num908 = 0;
                    int num909;
                    int num910;
                    do
                    {
                        num908++;
                        num909 = (int)Main.player[NPC.target].Center.X / 16;
                        num910 = (int)Main.player[NPC.target].Center.Y / 16;
                        if (Main.rand.Next(2) == 0)
                        {
                            num909 += Main.rand.Next(7, 13);
                        }
                        else
                        {
                            num909 -= Main.rand.Next(7, 13);
                        }

                        if (Main.rand.Next(2) == 0)
                        {
                            num910 += Main.rand.Next(7, 13);
                        }
                        else
                        {
                            num910 -= Main.rand.Next(7, 13);
                        }

                        if (!WorldGen.SolidTile(num909, num910))
                        {
                            goto IL_3556C;
                        }
                    } while (num908 <= 100);

                    return;
                    IL_3556C:
                    NPC.ai[0] = -2f;
                    NPC.ai[1] = num909;
                    NPC.ai[2] = num910;
                    NPC.netUpdate = true;
                    return;
                }

                return;
            }

            if (NPC.ai[0] == -2f)
            {
                NPC.velocity *= 0.9f;
                NPC.alpha += 25;
                if (NPC.alpha >= 255)
                {
                    NPC.alpha = 255;
                    NPC.position.X = (NPC.ai[1] * 16f) - (NPC.width / 2);
                    NPC.position.Y = (NPC.ai[2] * 16f) - (NPC.height / 2);
                    SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
                    NPC.ai[0] = -3f;
                    return;
                }

                return;
            }

            if (NPC.ai[0] != -3f)
            {
                return;
            }

            NPC.alpha -= 25;
            if (NPC.alpha <= 0)
            {
                NPC.alpha = 0;
                NPC.ai[0] = -1f;
                return;
            }

            return;
        }

        NPC.TargetClosest();
        if (NPC.justHit && Main.netMode != NetmodeID.MultiplayerClient &&
            NPC.type == ModContent.NPCType<BacteriumPrime>())
        {
            NPC.life = NPC.lifeMax;
        }

        var vector110 = new Vector2(NPC.Center.X, NPC.Center.Y);
        float num911 = Main.player[NPC.target].Center.X - vector110.X;
        float num912 = Main.player[NPC.target].Center.Y - vector110.Y;
        float num913 = (float)Math.Sqrt((num911 * num911) + (num912 * num912));
        float num914 = 1f;
        if (num913 < num914)
        {
            NPC.velocity.X = num911;
            NPC.velocity.Y = num912;
        }
        else
        {
            num913 = num914 / num913;
            NPC.velocity.X = num911 * num913;
            NPC.velocity.Y = num912 * num913;
        }

        if (NPC.ai[0] == 0f)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            int num915 = 0;
            for (int num916 = 0; num916 < 200; num916++)
            {
                if (Main.npc[num916].active &&
                    ((Main.npc[num916].type == NPCID.Creeper && NPC.type == NPCID.BrainofCthulhu) ||
                     (Main.npc[num916].type == ModContent.NPCType<BactusMinion>() &&
                      NPC.type == ModContent.NPCType<BacteriumPrime>())))
                {
                    num915++;
                }
            }

            if (num915 == 0)
            {
                NPC.ai[0] = -1f;
                NPC.localAI[1] = 0f;
                NPC.alpha = 0;
                NPC.netUpdate = true;
            }

            NPC.localAI[1] += 1f;
            if (NPC.localAI[1] >= 120 + Main.rand.Next(300))
            {
                NPC.localAI[1] = 0f;
                NPC.TargetClosest();
                int num917 = 0;
                int num918;
                int num919;
                do
                {
                    num917++;
                    num918 = (int)Main.player[NPC.target].Center.X / 16;
                    num919 = (int)Main.player[NPC.target].Center.Y / 16;
                    num918 += Main.rand.Next(-50, 51);
                    num919 += Main.rand.Next(-50, 51);
                    if (!WorldGen.SolidTile(num918, num919) && Collision.CanHit(new Vector2(num918 * 16, num919 * 16),
                            1, 1, Main.player[NPC.target].position, Main.player[NPC.target].width,
                            Main.player[NPC.target].height))
                    {
                        goto IL_35A0D;
                    }
                } while (num917 <= 100);

                return;
                IL_35A0D:
                NPC.ai[0] = 1f;
                NPC.ai[1] = num918;
                NPC.ai[2] = num919;
                NPC.netUpdate = true;
                return;
            }

            return;
        }

        if (NPC.ai[0] == 1f)
        {
            NPC.alpha += 5;
            if (NPC.alpha >= 255)
            {
                SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
                NPC.alpha = 255;
                NPC.position.X = (NPC.ai[1] * 16f) - (NPC.width / 2);
                NPC.position.Y = (NPC.ai[2] * 16f) - (NPC.height / 2);
                NPC.ai[0] = 2f;
                return;
            }

            return;
        }

        if (NPC.ai[0] != 2f)
        {
            return;
        }

        NPC.alpha -= 5;
        if (NPC.alpha <= 0)
        {
            NPC.alpha = 0;
            NPC.ai[0] = 0f;
            return;
        }
    }

    public override void FindFrame(int frameHeight)
    {
        NPC.frameCounter += 1.0;
        if (NPC.frameCounter > 6.0)
        {
            NPC.frameCounter = 0.0;
            NPC.frame.Y = NPC.frame.Y + frameHeight;
        }

        if (NPC.ai[0] >= 0f)
        {
            if (NPC.frame.Y > frameHeight * 3)
            {
                NPC.frame.Y = 0;
            }
        }
        else
        {
            if (NPC.frame.Y < frameHeight * 4)
            {
                NPC.frame.Y = frameHeight * 4;
            }

            if (NPC.frame.Y > frameHeight * 7)
            {
                NPC.frame.Y = frameHeight * 4;
            }
        }
    }

    public override void HitEffect(int hitDirection, double damage)
    {
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f,
                Mod.Find<ModGore>("BacteriumPrime1").Type);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f,
                Mod.Find<ModGore>("BacteriumPrime2").Type);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f,
                Mod.Find<ModGore>("BacteriumPrime3").Type);
        }
    }
}
