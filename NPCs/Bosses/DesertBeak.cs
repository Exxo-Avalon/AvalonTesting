using System;
using Avalon.Buffs;
using Avalon.Items.BossBags;
using Avalon.Items.Material;
using Avalon.Items.Vanity;
using Avalon.Items.Weapons.Magic;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.NPCs.Bosses;

[AutoloadBossHead]
public class DesertBeak : ModNPC
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Desert Beak");
        Main.npcFrameCount[NPC.type] = 3;
        var debuffData = new NPCDebuffImmunityData { SpecificallyImmuneTo = new[] { ModContent.BuffType<Frozen>() } };
        NPCID.Sets.DebuffImmunitySets[Type] = debuffData;
    }
    private bool transformed;
    private Vector2 lockon_player;
    public override void SetDefaults()
    {
        NPC.TargetClosest();
        Player player = Main.player[NPC.target];

        NPC.damage = 40;
        NPC.boss = true;
        NPC.noTileCollide = true;
        NPC.lifeMax = 3650;
        NPC.defense = 18;
        NPC.noGravity = true;
        NPC.width = 130;
        NPC.aiStyle = -1;
        NPC.npcSlots = 100f;
        NPC.value = 50000f;
        NPC.timeLeft = 22500;
        NPC.height = 78;
        NPC.knockBackResist = 0f;
        NPC.HitSound = SoundID.NPCHit28;
        NPC.DeathSound = SoundID.NPCDeath31;
        Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/DesertBeak");
        NPC.Center = player.Center + new Vector2(300, -600);
        transformed = false;
    }

    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.57f * bossLifeScale);
        NPC.damage = (int)(NPC.damage * 0.55f);
    }

    public override void OnKill()
    {
        if (!ModContent.GetInstance<DownedBossSystem>().DownedDesertBeak)
        {
            ModContent.GetInstance<DownedBossSystem>().DownedDesertBeak = true;
        }
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.SandBlock, 1, 22, 55));
        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<DesertBeakMask>(), 7));
        npcLoot.Add(
            ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<DesertFeather>(), 1, 6, 10));
        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),
            ModContent.GetInstance<ExxoWorldGen>().RhodiumOre.GetRhodiumVariantItemOre(), 1, 15, 26));
        npcLoot.Add(
            ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<TomeoftheDistantPast>(), 3));
        npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<DesertBeakBossBag>()));
    }

    private int winddir;
    private int mode;
    
    private int divetimer;
    private int modetimer;

    public override void AI()
    {
        Player player = Main.player[NPC.target];

        if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
        {
            NPC.TargetClosest();
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

        if(NPC.alpha > 110)
        {
            NPC.dontTakeDamage = true;
        }
        else
        {
            NPC.dontTakeDamage = false;
        }


        if (NPC.life > (int)NPC.lifeMax * 0.45f  && !Main.player[NPC.target].dead)
        {
            NPC.TargetClosest();
            switch (mode)
            {
                case 0:

                    lockon_player = player.Center;
                    NPC.alpha += 7;
                    NPC.velocity = NPC.DirectionTo(player.Center + new Vector2(0, -300));

                    if (NPC.alpha >= 254)
                    {
                        SoundEngine.PlaySound(SoundID.NPCHit28);
                        NPC.velocity = new Vector2(0,0);
                        NPC.Center = lockon_player + new Vector2 (-500, -500);
                        mode = 1;
                    }

                    break;


                case 1:


                    if (NPC.alpha <= 100)
                    {
                        NPC.alpha = 0;
                        if (NPC.Center.X < lockon_player.X)
                        {
                            divetimer++;                            //the closer to 1 the less vertical speed decay
                            NPC.velocity = new Vector2(6, (float)(7 * Math.Pow(0.98, divetimer))); 
                        }
                        else
                        {
                                                                   //the closer to 1 the less vertical speed decay
                            divetimer--;
                            NPC.velocity = new Vector2(6, (float)(-7 * Math.Pow(0.98, divetimer)));
                        }
                    }
                    else
                    {
                        //curently alpha is used a a trigger for the dive but it can be changed
                        lockon_player = player.Center;
                        NPC.alpha -= 7;
                        NPC.velocity = new Vector2(0, 0);
                        NPC.Center = lockon_player + new Vector2(-300, -250);
                    }

                    if(NPC.Center.Y < lockon_player.Y -260)
                    {
                        mode = 2;
                        NPC.alpha = 0;
                        NPC.velocity = new Vector2(0, 0);
                    }

                    break;

                case 2:

                    NPC.velocity = NPC.DirectionTo(player.Center + new Vector2 (0,-300)) * 7;

                    modetimer++;
                    divetimer++;

                    if(divetimer >= 60)
                    {
                        for (int i = 0; i < 3; i++)
                        {

                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(0,6).RotatedByRandom(5), ProjectileID.DD2BetsyFireball, 20, 0, player.whoAmI);

                        }
                        divetimer = 0;
                    }
                    if (modetimer >= 600)
                    {
                        modetimer = 0;
                        mode = 0;
                        divetimer = 0;
                    }


                    break;

            }
        }
        else if (NPC.life < (int)NPC.lifeMax * 0.45f && !Main.player[NPC.target].dead)
        {
            NPC.TargetClosest();
            //When at this stage the boss whips up strong winds pushing the player in whatever direction the wind blows
            if (!transformed)
            {
                Main.NewText("The wind is growing stronger and is kicking up a lot of sand.", Color.SandyBrown);
                transformed = true;
                mode = 0;
                NPC.velocity = new Vector2(0, 0);
            }

            for (int i = 0; i < 15; i++)
            {
                int dustType = 32;
                int dustIndex = Dust.NewDust(player.position - new Vector2(600, 600), player.width * 100, player.height * 100, -100, 0, 0, dustType);


            }


            player.AddBuff(BuffID.WindPushed, 60);

            switch (mode)
            {
                case 0:






                    break;


                case 1:
                    // Quickly dashes to a random location above the player and fires a spread of 3 feathers

                    break;

                case 2:
                    // Flies in front of the player and shoots a sand tornado

                    break;

            }
        }
    }

    public override void FindFrame(int frameHeight)
    {
        {
            NPC.spriteDirection = NPC.direction;
            NPC.rotation = NPC.velocity.X * 0.1f;
            if (NPC.velocity.X == 0f && NPC.velocity.Y == 0f)
            {
                NPC.frame.Y = 0;
                NPC.frameCounter = 0.0;
            }
            else
            {
                NPC.frameCounter += 1.0;
                if (NPC.frameCounter < 4.0)
                {
                    NPC.frame.Y = frameHeight;
                }
                else
                {
                    NPC.frame.Y = frameHeight * 2;
                    if (NPC.frameCounter >= 7.0)
                    {
                        NPC.frameCounter = 0.0;
                    }
                }
            }
        }
    }

    public override void HitEffect(int hitDirection, double damage)
    {
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DesertBeakHead").Type,
                0.9f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DesertBeakWing").Type,
                0.9f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DesertBeakWing").Type,
                0.9f);
        }
    }
}
