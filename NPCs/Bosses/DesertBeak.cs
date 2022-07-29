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

namespace Avalon.NPCs.Bosses;

[AutoloadBossHead]
public class DesertBeak : ModNPC
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Desert Beak");
        Main.npcFrameCount[NPC.type] = 3;
        NPCID.Sets.DebuffImmunitySets[Type] = new NPCDebuffImmunityData { SpecificallyImmuneTo = new[] { ModContent.BuffType<Frozen>(), BuffID.Confused } };
    }
    private bool transformed;
    private Vector2 lockon_player;
    public override void SetDefaults()
    {
        NPC.TargetClosest();
        Player player = Main.player[NPC.target];

        NPC.damage = 65;
        NPC.boss = true;
        NPC.noTileCollide = true;
        NPC.lifeMax = 3650;
        NPC.defense = 30;
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
        NPC.scale = 1.3f;
    }

    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.66f * bossLifeScale);
        NPC.damage = (int)(NPC.damage * 0.58f);
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
    private bool divebomb;
    private int divedirection;
    private int dive;
    private int teleports;
    private int flightimer;


    public override void AI()
    {
        Player player = Main.player[NPC.target];
        if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
        {
            NPC.TargetClosest();
        }
        if (Main.player[NPC.target].dead)
        {
            NPC.velocity.Y -= 0.04f;
            if (NPC.timeLeft > 10)
            {
                NPC.timeLeft = 10;
                return;
            }
        }
        NPC.dontTakeDamage = NPC.alpha > 200;


        if (NPC.life >= (int)NPC.lifeMax * 0.45f  && !Main.player[NPC.target].dead)
        {
            NPC.TargetClosest();
            switch (mode)
            {
                case 0:
                    NPC.spriteDirection = NPC.direction;
                    NPC.rotation = NPC.velocity.X * 0.1f;
                    lockon_player = player.Center;
                    NPC.alpha += 8;
                    NPC.velocity = NPC.DirectionTo(player.Center + new Vector2(0, -300));

                    if (NPC.alpha >= 254)
                    {
                        divebomb = false;
                        SoundEngine.PlaySound(SoundID.NPCHit28);
                        NPC.velocity = new Vector2(0,0);
                        NPC.Center = lockon_player + new Vector2 (-500, -500);
                        mode = 1;
                       //dive = Main.rand.Next(3);

                        if(player.velocity.X > 0)
                        {
                            dive = 2;
                        }
                        else
                        {
                            dive = 1;
                        }

                    }
                    break;
                case 1:
                    NPC.spriteDirection = NPC.direction;
                    NPC.rotation = NPC.velocity.X * 0.1f;

                    switch (dive)
                    {
                        case 1:
                            if (NPC.alpha <= 100)
                            {
                                NPC.alpha = 0;
                                if (NPC.Center.X < lockon_player.X)
                                {
                                    divetimer++;                            //the closer to 1 the less vertical speed decay
                                    NPC.velocity = new Vector2(9, (float)(9 * Math.Pow(0.98, divetimer)));
                                }
                                else
                                {
                                    if (!divebomb)
                                    {
                                        for (int i = 0; i < 3; i++)
                                        {

                                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(0, 1).RotatedByRandom(5), ProjectileID.BombSkeletronPrime, 30, 0, player.whoAmI);

                                        }
                                        divebomb = true;
                                    }
                                    //the closer to 1 the less vertical speed decay
                                    divetimer--;
                                    NPC.velocity = new Vector2(9, (float)(-9 * Math.Pow(0.98, divetimer)));
                                }
                            }
                            else
                            {
                                //curently alpha is used as a trigger for the dive but it can be changed
                                lockon_player = player.Center + (player.velocity * 5);
                                NPC.alpha -= 8;
                                NPC.velocity = new Vector2(0, 0);
                                NPC.Center = lockon_player + new Vector2(-300, -250);
                            }

                            if (NPC.Center.Y < lockon_player.Y - 260)
                            {
                                mode = 2;
                                NPC.alpha = 0;
                                NPC.velocity = new Vector2(0, 0);
                            }
                            break;
                        default:
                            if (NPC.alpha <= 100)
                            {
                                NPC.alpha = 0;
                                if (NPC.Center.X > lockon_player.X)
                                {
                                    divetimer++;                            //the closer to 1 the less vertical speed decay
                                    NPC.velocity = new Vector2(-9, (float)(9 * Math.Pow(0.99, divetimer)));
                                }
                                else
                                {
                                    if (!divebomb)
                                    {
                                        for (int i = 0; i < 3; i++)
                                        {

                                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(0, 1).RotatedByRandom(5), ProjectileID.BombSkeletronPrime, 30, 0, player.whoAmI);

                                        }
                                        divebomb = true;
                                    }
                                    //the closer to 1 the less vertical speed decay
                                    divetimer--;
                                    NPC.velocity = new Vector2(-9, (float)(-9 * Math.Pow(0.99, divetimer)));
                                }
                            }
                            else
                            {
                                //curently alpha is used a a trigger for the dive but it can be changed
                                lockon_player = player.Center + (player.velocity * 5);
                                NPC.alpha -= 8;
                                NPC.velocity = new Vector2(0, 0);
                                NPC.Center = lockon_player + new Vector2(300, -250);
                            }

                            if (NPC.Center.Y < lockon_player.Y - 260)
                            {
                                mode = 2;
                                NPC.alpha = 0;
                                NPC.velocity = new Vector2(0, 0);
                            }

                            break;
                    }
                    break;
                case 2:
                    NPC.spriteDirection = NPC.direction;
                    NPC.rotation = NPC.velocity.X * 0.1f;


                    if (Main.player[NPC.target].position.X < NPC.position.X)
                    {
                        if (NPC.velocity.X > -8)
                            NPC.velocity.X -= 0.22f;
                    }
                    if (Main.player[NPC.target].position.X > NPC.position.X)
                    {
                        if (NPC.velocity.X < 8)
                            NPC.velocity.X += 0.22f;
                    }
                    if (Main.player[NPC.target].position.Y < NPC.position.Y + 300)
                    {
                        if (NPC.velocity.Y < 0)
                        {
                            if (NPC.velocity.Y > -4)
                                NPC.velocity.Y -= 0.8f;
                        }
                        else
                            NPC.velocity.Y -= 0.6f;
                        if (NPC.velocity.Y < -4)
                            NPC.velocity.Y = -4;
                    }
                    if (Main.player[NPC.target].position.Y > NPC.position.Y + 300)
                    {
                        if (NPC.velocity.Y > 0)
                        {
                            if (NPC.velocity.Y < 4)
                                NPC.velocity.Y += 0.8f;
                        }
                        else
                            NPC.velocity.Y += 0.6f;
                        if (NPC.velocity.Y > 4)
                            NPC.velocity.Y = 4;
                    }
                    modetimer++;
                    divetimer++;

                    if(divetimer >= 60)
                    {
                        SoundEngine.PlaySound(SoundID.Item64);

                        Vector2 targetPosition = Main.player[NPC.target].position;
                        Vector2 position = NPC.Center;
                        Vector2 target = Main.player[NPC.target].Center;
                        Vector2 direction = targetPosition - position;
                        position += Vector2.Normalize(direction) * 2f;
                        Vector2 perturbedSpeed = direction * 0.3f;

                        const int NumProjectiles = 3;

                        float rotation = MathHelper.ToRadians(21);

                        for (int i = 0; i < NumProjectiles; i++)
                        {
                            Vector2 newVelocity = perturbedSpeed.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (NumProjectiles - 1f)));
                            Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), position, newVelocity * 0.1f, ModContent.ProjectileType<Projectiles.Hostile.DesertBeakFeather>(), 30, 0, player.whoAmI);
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
            NPC.damage = 0;
            NPC.TargetClosest();
            //When at this stage the boss whips up strong winds pushing the player in whatever direction the wind blows
            if (!transformed)
            {
                Main.NewText("The wind is growing stronger and is kicking up a lot of sand.", Color.SandyBrown);
                transformed = true;
                mode = 0;
                NPC.velocity = new Vector2(0, 0);
                divetimer = 0;
                modetimer = 0;
            }
            for (int i = 0; i < 15; i++)
            {
                int dustType = DustID.SandstormInABottle;
                int dustIndex = Dust.NewDust(player.position - new Vector2(600, 600), player.width * 100, player.height * 100, -100, 0, 0, dustType);


            }
            player.AddBuff(BuffID.Darkness, 60);

            switch (mode)
            {
                case 0:
                    NPC.spriteDirection = NPC.direction;
                    NPC.rotation = NPC.velocity.X * 0.1f;
                    NPC.velocity = new Vector2(0, 0);
                    NPC.alpha += 8;

                    if (NPC.alpha >= 254)
                    {
                        NPC.Center = player.Center + new Vector2(Main.rand.Next(-100, 100),Main.rand.Next(-350, -250));
                        mode = 1;
                    }
                    break;
                case 1:
                    // Quickly dashes to a random location above the player and fires a spread of 3 feathers
                    NPC.spriteDirection = NPC.direction;
                    NPC.rotation = NPC.velocity.X * 0.1f;

                    if (NPC.alpha <= 100)
                    {
                        NPC.alpha = 0;
                        if(teleports == 9)
                        {
                            SoundEngine.PlaySound(SoundID.NPCHit28);

                            Vector2 targetPosition = Main.player[NPC.target].position;
                            Vector2 position = NPC.Center;
                            Vector2 target = Main.player[NPC.target].Center;
                            Vector2 direction = targetPosition - position;
                            position += Vector2.Normalize(direction) * 2f;
                            Vector2 perturbedSpeed = direction * 0.2f;

                            int NumProjectiles = (Main.expertMode || Main.masterMode) ? 30 : 20;
                            float rotation = MathHelper.ToRadians(180);

                            for (int i = 0; i < NumProjectiles; i++)
                            {
                                Vector2 newVelocity = perturbedSpeed.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (NumProjectiles - 1f)));


                                Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), position, newVelocity * 0.1f, ModContent.ProjectileType<Projectiles.Hostile.DesertBeakFeather>(), 30, 0, player.whoAmI);
                            }
                        }
                        else
                        {
                            SoundEngine.PlaySound(SoundID.Item64);

                            Vector2 targetPosition = Main.player[NPC.target].position;
                            Vector2 position = NPC.Center;
                            Vector2 target = Main.player[NPC.target].Center;
                            Vector2 direction = targetPosition - position;
                            position += Vector2.Normalize(direction) * 2f;
                            Vector2 perturbedSpeed = direction * 0.27f;

                            const int NumProjectiles = 3;
                            float rotation = MathHelper.ToRadians(25);

                            for (int i = 0; i < NumProjectiles; i++)
                            {
                                Vector2 newVelocity = perturbedSpeed.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (NumProjectiles - 1f)));


                                Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), position, newVelocity * 0.1f, ModContent.ProjectileType<Projectiles.Hostile.DesertBeakFeather>(), 30, 0, player.whoAmI);
                            }
                        }
                        teleports++;

                        if(teleports == ((Main.expertMode || Main.masterMode) ? 5 : 10))
                        {
                            mode = 2;
                            NPC.alpha = 0;
                        }
                        else
                        {
                            mode = 0;
                        }
                    }
                    else
                    {
                        NPC.alpha -= 8;
                    }
                    break;
                case 2:
                    NPC.spriteDirection = NPC.direction;
                    if (NPC.Center.X > player.Center.X)
                    {
                        NPC.velocity = NPC.DirectionTo(player.Center + new Vector2(500, 0)) * 4;
                    }
                    else
                    {
                        NPC.velocity = NPC.DirectionTo(player.Center + new Vector2(500, 0)) * 4;
                    }

                    modetimer++;
                    divetimer++;

                    if (divetimer >= (Main.expertMode || Main.masterMode ? 110 : 150))
                    {
                        float speed = (Main.expertMode || Main.masterMode ? 14f : 11f);
                        if (NPC.Center.X > player.Center.X)
                        {
                            Vector2 position = NPC.Center;
                            const int NumProjectiles = 1;

                            for (int i = 0; i < NumProjectiles; i++)
                            {
                                Projectile p = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), position, new Vector2(-speed, 0), ModContent.ProjectileType<Projectiles.Hostile.DesertBeakSandstorm>(), 40, 10, player.whoAmI);
                            }
                        }
                        else
                        {
                            Vector2 position = NPC.Center;
                            const int NumProjectiles = 1;

                            for (int i = 0; i < NumProjectiles; i++)
                            {
                                Projectile p = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), position, new Vector2(speed, 0), ModContent.ProjectileType<Projectiles.Hostile.DesertBeakSandstorm>(), 40, 10, player.whoAmI);
                            }
                        }

                        SoundEngine.PlaySound(SoundID.Item64);

                        divetimer = 0;
                    }

                    if (modetimer >= (Main.expertMode || Main.masterMode ? 450 : 600))
                    {
                        modetimer = 0;
                        mode = 0;
                        divetimer = 0;
                        teleports = 0;
                    }

                    break;

            }
        }
    }

    public override void FindFrame(int frameHeight)
    {
        if (NPC.velocity == Vector2.Zero)
        {
            NPC.frame.Y = 0;
            NPC.frameCounter = 0.0;
        }
        else
        {
            NPC.frameCounter++;
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
