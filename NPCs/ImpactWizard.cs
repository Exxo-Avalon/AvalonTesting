using System;
using AvalonTesting.Items.Banners;
using AvalonTesting.Items.Placeable.Tile;
using AvalonTesting.Projectiles;
using AvalonTesting.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.NPCs;

public class ImpactWizard : ModNPC
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Impact Wizard");
        Main.npcFrameCount[NPC.type] = 3;
        var debuffData = new NPCDebuffImmunityData { SpecificallyImmuneTo = new[] { BuffID.OnFire } };
        NPCID.Sets.DebuffImmunitySets[Type] = debuffData;
    }

    public override void SetDefaults()
    {
        NPC.damage = 150;
        NPC.lifeMax = 1200;
        NPC.defense = 50;
        NPC.width = 18;
        NPC.aiStyle = -1;
        NPC.npcSlots = 2f;
        NPC.value = 20000f;
        NPC.height = 40;
        NPC.knockBackResist = 0.4f;
        NPC.HitSound = SoundID.NPCHit2;
        NPC.DeathSound = SoundID.NPCDeath2;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<ImpactWizardBanner>();
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
            new FlavorTextBestiaryInfoElement(
                "These monsters are capable of launching a sphere of energy that is extremely dangerous. If you get too close, this energy sphere will shoot bolts at you."),
        });

    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.55f);
        NPC.damage = (int)(NPC.damage * 0.55f);
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot) =>
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Phantoplasm>(), 10));

    public override float SpawnChance(NPCSpawnInfo spawnInfo) =>
        Main.hardMode && ModContent.GetInstance<DownedBossSystem>().DownedArmageddon &&
        ModContent.GetInstance<AvalonTestingWorld>().SuperHardmode && spawnInfo.Player.ZoneDungeon
            ? 0.2f * AvalonTestingGlobalNPC.EndoSpawnRate
            : 0f;

    public override void AI()
    {
        NPC.TargetClosest();
        NPC.velocity.X = NPC.velocity.X * 0.93f;
        if (NPC.velocity.X > -0.1 && NPC.velocity.X < 0.1)
        {
            NPC.velocity.X = 0f;
        }

        if (NPC.ai[0] == 0f)
        {
            NPC.ai[0] = 500f;
        }

        if (NPC.ai[2] != 0f && NPC.ai[3] != 0f)
        {
            SoundEngine.PlaySound(SoundID.Item8, NPC.position);
            for (int num231 = 0; num231 < 50; num231++)
            {
                int num234 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height,
                    DustID.MagnetSphere);
                Main.dust[num234].velocity *= 2f;
                Main.dust[num234].scale = 1.4f;
            }

            NPC.position.X = (NPC.ai[2] * 16f) - (NPC.width / 2) + 8f;
            NPC.position.Y = (NPC.ai[3] * 16f) - NPC.height;
            NPC.velocity.X = 0f;
            NPC.velocity.Y = 0f;
            NPC.ai[2] = 0f;
            NPC.ai[3] = 0f;
            SoundEngine.PlaySound(SoundID.Item8, NPC.position);
            for (int num239 = 0; num239 < 50; num239++)
            {
                int num243 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height,
                    DustID.MagnetSphere);
                Main.dust[num243].velocity *= 2f;
                Main.dust[num243].scale = 1.4f;
            }
        }

        NPC.ai[0] += 1f;
        if (NPC.ai[0] == 50f || NPC.ai[0] == 100f || NPC.ai[0] == 150f || NPC.ai[0] == 200f || NPC.ai[0] == 250f)
        {
            NPC.ai[1] = 30f;
            NPC.netUpdate = true;
        }

        if (NPC.ai[0] >= 400f)
        {
            NPC.ai[0] = 700f;
        }

        if (NPC.ai[0] == 100f || NPC.ai[0] == 200f || NPC.ai[0] == 300f)
        {
            NPC.ai[1] = 30f;
            NPC.netUpdate = true;
        }

        if (NPC.ai[0] >= 650f && Main.netMode != NetmodeID.MultiplayerClient)
        {
            NPC.ai[0] = 1f;
            int num247 = (int)Main.player[NPC.target].position.X / 16;
            int num248 = (int)Main.player[NPC.target].position.Y / 16;
            int num249 = (int)NPC.position.X / 16;
            int num250 = (int)NPC.position.Y / 16;
            int num251 = 20;
            int num252 = 0;
            bool flag28 = false;
            if (Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) +
                Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 2000f)
            {
                num252 = 100;
                flag28 = true;
            }

            while (!flag28 && num252 < 100)
            {
                num252++;
                int num253 = Main.rand.Next(num247 - num251, num247 + num251);
                int num254 = Main.rand.Next(num248 - num251, num248 + num251);
                for (int num255 = num254; num255 < num248 + num251; num255++)
                {
                    if ((num255 < num248 - 4 || num255 > num248 + 4 || num253 < num247 - 4 || num253 > num247 + 4) &&
                        (num255 < num250 - 1 || num255 > num250 + 1 || num253 < num249 - 1 || num253 > num249 + 1) &&
                        Main.tile[num253, num255].HasUnactuatedTile)
                    {
                        bool flag29 = true;
                        if (!Main.wallDungeon[Main.tile[num253, num255 - 1].WallType])
                        {
                            flag29 = false;
                        }
                        else if (Main.tile[num253, num255 - 1].LiquidType == LiquidID.Lava)
                        {
                            flag29 = false;
                        }

                        if (flag29 && Main.tileSolid[Main.tile[num253, num255].TileType] &&
                            !Collision.SolidTiles(num253 - 1, num253 + 1, num255 - 4, num255 - 1))
                        {
                            NPC.ai[1] = 20f;
                            NPC.ai[2] = num253;
                            NPC.ai[3] = num255;
                            flag28 = true;
                            break;
                        }
                    }
                }
            }

            NPC.netUpdate = true;
        }

        if (NPC.ai[1] > 0f)
        {
            NPC.ai[1] -= 1f;
            if (NPC.ai[1] == 25)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    float num256 = 6f;
                    var vector23 = new Vector2(NPC.position.X + (NPC.width * 0.5f), NPC.position.Y);
                    float num257 = Main.player[NPC.target].position.X + (Main.player[NPC.target].width * 0.5f) -
                                   vector23.X;
                    float num258 = Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * 0.5f) -
                                   vector23.Y;
                    float num259 = (float)Math.Sqrt((num257 * num257) + (num258 * num258));
                    num259 = num256 / num259;
                    num257 *= num259;
                    num258 *= num259;
                    num257 *= 1.4f;
                    num258 *= 1.4f;
                    if (!NPC.GetGlobalNPC<AvalonTestingGlobalNPCInstance>().Silenced)
                    {
                        int num262 = Projectile.NewProjectile(NPC.GetSource_FromAI(), vector23.X, vector23.Y, num257,
                            num258, ModContent.ProjectileType<Projectiles.Hostile.ImpactSphere>(), Main.expertMode ? 35 : 65, 0f,
                            Main.myPlayer);
                        Main.projectile[num262].timeLeft = 300;
                    }

                    NPC.localAI[0] = 0f;
                }
            }
        }

        if (Main.rand.Next(2) == 0)
        {
            int num275 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y + 2f), NPC.width, NPC.height,
                DustID.MagnetSphere);
            Dust? dust23 = Main.dust[num275];
            dust23.velocity.X = dust23.velocity.X * 0.5f;
            Dust? dust24 = Main.dust[num275];
            dust24.velocity.Y = dust24.velocity.Y * 0.5f;
        }
    }

    public override void FindFrame(int frameHeight)
    {
        if (NPC.velocity.Y == 0f)
        {
            if (NPC.direction == 1)
            {
                NPC.spriteDirection = 1;
            }

            if (NPC.direction == -1)
            {
                NPC.spriteDirection = -1;
            }
        }

        NPC.frame.Y = 0;
        if (NPC.velocity.Y != 0f)
        {
            NPC.frame.Y = NPC.frame.Y + frameHeight;
        }
        else if (NPC.ai[1] > 0f)
        {
            NPC.frame.Y = NPC.frame.Y + (frameHeight * 2);
        }
    }
}
