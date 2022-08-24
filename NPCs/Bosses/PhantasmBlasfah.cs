using System;
using Avalon.Items.Material;
using Avalon.Items.Placeable.Trophy;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using System.Linq;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework.Graphics;
using Steamworks;
using System.Security.Cryptography;
using System.Transactions;

namespace Avalon.NPCs.Bosses;

//[AutoloadBossHead]
public class PhantasmBlasfah : ModNPC
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Phantasm");
        Main.npcFrameCount[NPC.type] = 4;
    }
    public override void SetDefaults()
    {
        NPC.Size = new Vector2(66);
        NPC.boss = NPC.noTileCollide = NPC.noGravity = true;
        NPC.npcSlots = 100f;
        NPC.damage = 95;
        NPC.lifeMax = 92700;
        NPC.defense = 80;
        NPC.aiStyle = -1;
        NPC.value = 100000f;
        NPC.knockBackResist = 0f;
        NPC.scale = 1.5f;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath39;
        Music = Avalon.MusicMod != null ? MusicLoader.GetMusicSlot(Avalon.MusicMod, "Sounds/Music/Phantasm") : MusicID.EmpressOfLight;
    }
    public override void BossLoot(ref string name, ref int potionType)
    {
        potionType = ItemID.SuperHealingPotion;
    }

    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.65f * bossLifeScale);
        NPC.damage = (int)(NPC.damage * 0.75f);
    }

    public Vector2 radius = new Vector2(0, -400f);
    public int oldPhase = 100;
    public float playerDir;
    public float playerUpOrDown;
    public int timeTillNextAttack = 180;
    public int dashTimer;
    public Vector2 dashDir = Vector2.Zero;
    public bool hasDashed;
    public float rotDir;
    public override void AI()
    {
        AvalonGlobalNPC.PhantasmBoss = NPC.whoAmI;
        Player player = Main.player[NPC.FindClosestPlayer()];
        float distanceFromPlayer = Vector2.Distance(player.Center, NPC.Center);
        Vector2 towardsPlayer = player.Center - NPC.Center;

        NPC.ai[1]++;

        playerDir = NPC.Center.X - player.Center.X;
        playerDir = Math.Sign(playerDir);


        if (NPC.ai[1] > timeTillNextAttack)
        {
            NPC.ai[2] = 0;
            NPC.ai[3] = 0;
            NPC.ai[0] = Main.rand.Next(4);

            if (NPC.ai[0] == oldPhase)
            {
                NPC.ai[0]++;
            }
            if (NPC.ai[0] > 3)
            {
                NPC.ai[0] = 0;
            }
            if (NPC.ai[0] == 1)
            {
                if (!Main.projectile.SkipLast(1).Any(p => p.type == ModContent.ProjectileType<Projectiles.Hostile.SoulDagger>() && p.active))
                {
                    NPC.ai[0] = 0;
                }
            }
            if (NPC.ai[0] == 2)
            {
                radius = Vector2.Normalize(player.Center - NPC.Center) * radius.Length();
                rotDir = NPC.Center.X - player.Center.X;
                rotDir = Math.Sign(rotDir);
            }
            if (NPC.ai[0] == 3)
            {
                NPC.ai[3] = 25;
            }
            //NPC.ai[0] = 3;
            NPC.ai[1] = 0;
        }

        //glowing eyes wip idk

        /*Dust dust = Dust.NewDustPerfect(NPC.Center - new Vector2(20f, -30f).RotatedBy(NPC.rotation), DustID.DungeonSpirit, Vector2.Zero, 0, default, 2f);
        dust.noGravity = true;
        Dust dust1 = Dust.NewDustPerfect(NPC.Center - new Vector2(-20f, -30f).RotatedBy(NPC.rotation), DustID.DungeonSpirit, Vector2.Zero, 0, default, 2f);
        dust1.noGravity = true;*/

        //attack types
        if (NPC.ai[0] == 0)
        {
            oldPhase = 0;
            timeTillNextAttack = 180;
            if (NPC.ai[1] == 90)
            {
                SpawnMinions(6);
            }
            NPC.rotation = NPC.velocity.X * 0.02f;
            NPC.velocity += Vector2.Normalize(towardsPlayer + new Vector2(0, -300f)) * 0.75f;
            if (NPC.velocity.Length() > 12f)
            {
                NPC.velocity = Vector2.Normalize(NPC.velocity) * 12f;
            }
        }
        if (NPC.ai[0] == 1)
        {
            oldPhase = 1;
            timeTillNextAttack = 240;
            if (NPC.ai[1] >= 200)
            {
                if (NPC.ai[1] == 200)
                {
                    NPC.velocity = Vector2.Normalize(towardsPlayer) * 30f;
                    for (int i = 0; i < 40; i++)
                    {
                        int num893 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1f);
                        Main.dust[num893].velocity *= 5f;
                        Main.dust[num893].scale = 2f;
                        Main.dust[num893].noGravity = true;
                    }
                }
                NPC.velocity *= 0.97f;
                int num896 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num896].velocity *= 3f;
                Main.dust[num896].scale = 1.5f;
                Main.dust[num896].noGravity = true;
            }
            else
            {
                NPC.velocity += Vector2.Normalize(towardsPlayer + new Vector2(450f * playerDir, 0)) * 1f;
                if (NPC.velocity.Length() > 14f)
                {
                    NPC.velocity = Vector2.Normalize(NPC.velocity) * 14f;
                }
            }
            NPC.rotation = towardsPlayer.ToRotation() - MathHelper.PiOver2;
        }
        if (NPC.ai[0] == 2)
        {
            if (NPC.ai[3] == 0)
            {
                for (int i = 0; i < 40; i++)
                {
                    int num893 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num893].velocity *= 6f;
                    Main.dust[num893].scale = 1.5f;
                    Main.dust[num893].noGravity = true;
                }
                NPC.ai[3]++;
            }
            NPC.ai[2]++;
            if (NPC.ai[2] == 25)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(player.Center - NPC.Center) * 20f, ModContent.ProjectileType<Projectiles.Hostile.SoulGrabber>(), 35, default, 255);
                NPC.ai[2] = 0;
            }
            oldPhase = 2;
            timeTillNextAttack = 180;
            radius = radius.RotatedBy(-0.085f * rotDir);
            NPC.rotation = NPC.velocity.ToRotation() - MathHelper.PiOver2;
            NPC.velocity = radius.RotatedBy((90 * rotDir) * (Math.PI / 180));
            NPC.Center = Vector2.Lerp(NPC.Center, player.Center - radius, 0.05f);
            if (NPC.velocity.Length() > 20f)
            {
                NPC.velocity = Vector2.Normalize(NPC.velocity) * 20f;
            }
            for (int i = 0; i < 2; i++)
            {
                int num893 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num893].velocity *= 2f;
                Main.dust[num893].scale = 1.5f;
                Main.dust[num893].noGravity = true;
            }
        }
        /*if (NPC.ai[0] == 3) old wip attack
        {
            oldPhase = 3;
            timeTillNextAttack = 300;
            radiusSweeping = radiusSweeping.RotatedBy(-0.125f);
            radiusSweepingOffset = radiusSweepingOffset.RotatedBy(-0.125f / 4);
            NPC.velocity = radiusSweeping.RotatedBy(90 * (Math.PI / 180));
            NPC.rotation = NPC.velocity.ToRotation() - MathHelper.PiOver2;
            NPC.Center = Vector2.Lerp(NPC.Center, player.Center - radiusSweepingOffset - radiusSweeping, 0.025f);
            if (NPC.velocity.Length() > 20f)
            {
                NPC.velocity = Vector2.Normalize(NPC.velocity) * 20f;
            }
            for (int i = 0; i < 2; i++)
            {
                int num893 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num893].velocity *= 2f;
                Main.dust[num893].scale = 1.5f;
                Main.dust[num893].noGravity = true;
            }
        }*/
        /*if (NPC.ai[0] == 3)
        {
            oldPhase = 3;
            timeTillNextAttack = 9999;
            dashTimer++;
            if (dashTimer == 1 && NPC.ai[3] <= 4)
            {
                NPC.velocity += Vector2.Normalize(towardsPlayer) * 35f;
                NPC.ai[3]++;
                Main.NewText(NPC.ai[3]);
                playerUpOrDown = NPC.Center.Y - player.Center.Y;
                playerUpOrDown = Math.Sign(playerUpOrDown);
                for (int i = 0; i < 30; i++)
                {
                    int num893 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num893].velocity *= 6f;
                    Main.dust[num893].scale = 1.5f;
                    Main.dust[num893].noGravity = true;
                }
            }
            if (NPC.ai[3] == 5)
            {
                timeTillNextAttack = 30;
            }
            if (dashTimer > 30 && NPC.ai[3] <= 4)
            {
                NPC.velocity = NPC.velocity.RotatedBy(0.1f * playerUpOrDown);
                if (NPC.velocity.ToRotation() > NPC.AngleTo(player.Center) - 0.1f && NPC.velocity.ToRotation() < NPC.AngleTo(player.Center) + 0.1f)
                {
                    dashTimer = 0;
                }
            }

            NPC.rotation = NPC.velocity.ToRotation() - MathHelper.PiOver2;
            NPC.velocity *= 0.97f;

            if (NPC.velocity.Length() > 35f)
            {
                NPC.velocity = Vector2.Normalize(NPC.velocity) * 35f;
            }
            if (NPC.velocity.Length() < 18f)
            {
                NPC.velocity = Vector2.Normalize(NPC.velocity) * 18f;
            }

            for (int i = 0; i < 2; i++)
            {
                int num893 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num893].velocity *= 2f;
                Main.dust[num893].scale = 1.5f;
                Main.dust[num893].noGravity = true;
            }
        }*/
        /*if (NPC.ai[0] == 3)
        {
            oldPhase = 3;
            timeTillNextAttack = 180;
            NPC.ai[3]++;
            if (dashCounter < 5)
            {
                if (NPC.ai[3] == 25)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        int num893 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1f);
                        Main.dust[num893].velocity *= 6f;
                        Main.dust[num893].scale = 1.5f;
                        Main.dust[num893].noGravity = true;
                    }
                    dashCounter++;
                }
                if (NPC.ai[3] > 25)
                {
                    NPC.velocity += Vector2.Normalize(towardsPlayer) * 6f;
                    NPC.rotation = NPC.velocity.ToRotation() - MathHelper.PiOver2;
                }
                if (NPC.ai[3] == 35)
                {
                    NPC.ai[3] = 0;
                }
            }
            NPC.velocity *= 0.97f;
            for (int i = 0; i < 2; i++)
            {
                int num893 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num893].velocity *= 2f;
                Main.dust[num893].scale = 1.5f;
                Main.dust[num893].noGravity = true;
            }
            if (NPC.velocity.Length() > 30f)
            {
                NPC.velocity = Vector2.Normalize(NPC.velocity) * 30f;
            }
            if (NPC.velocity.Length() < 10f)
            {
                NPC.velocity = Vector2.Normalize(NPC.velocity) * 10f;
            }
        }*/
        if(NPC.ai[0] == 3)
        {
            timeTillNextAttack = 310;
            oldPhase = 3;
            Vector2 goToPos = Vector2.Normalize(player.Center - NPC.Center) * 300;
            NPC.rotation = towardsPlayer.ToRotation() - MathHelper.PiOver2;
            if (!hasDashed)
            {
                NPC.ai[3]++;
                NPC.velocity += Vector2.Normalize(towardsPlayer - goToPos) * 1f;
            }
            if (NPC.ai[3] == 40)
            {
                NPC.velocity = Vector2.Normalize(towardsPlayer) * 30f;
                NPC.ai[3] = 0;
                hasDashed = true;
                for (int i = 0; i < 40; i++)
                {
                    int num893 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num893].velocity *= 6f;
                    Main.dust[num893].scale = 1.5f;
                    Main.dust[num893].noGravity = true;
                }
            }
            if(hasDashed)
            {
                if (NPC.velocity.Length() > 35f)
                {
                    NPC.velocity = Vector2.Normalize(NPC.velocity) * 35f;
                }
                NPC.velocity *= 0.98f;
                dashTimer++;
                if(dashTimer == 30)
                {
                    hasDashed = false;
                    dashTimer = 0;
                }
                for (int i = 0; i < 2; i++)
                {
                    int num893 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num893].velocity *= 2f;
                    Main.dust[num893].scale = 1.5f;
                    Main.dust[num893].noGravity = true;
                }
            }
            else
            {
                if (NPC.velocity.Length() > 15f)
                {
                    NPC.velocity = Vector2.Normalize(NPC.velocity) * 15f;
                }

            }
        }
    }
    public void SpawnMinions(int amount)
    {
        if(Main.projectile.SkipLast(1).Any(p => p.type == ModContent.ProjectileType<Projectiles.Hostile.SoulDagger>() && p.active))
        {
            return;
        }
        int degrees = 360 / amount;
        for (int i = 0; i < amount; i++)
        {
            double rotateAmount = degrees * i * (Math.PI / 180);
            Vector2 spawnPos = new Vector2(0, -125f);
            spawnPos = NPC.Center + spawnPos.RotatedBy(rotateAmount);
            //NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnPos.X, (int)spawnPos.Y + 12, ModContent.NPCType<PhantasmMinion>());
            Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(spawnPos.X, spawnPos.Y + 4.5f), Vector2.Zero, ModContent.ProjectileType<Projectiles.Hostile.SoulDagger>(), 35, 0, Main.myPlayer, 0f, i + 1);
        }
    }
    public override void OnKill()
    {
        AvalonGlobalNPC.PhantasmBoss = -1;
        if (!ModContent.GetInstance<DownedBossSystem>().DownedPhantasm)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText("The spirits are stirring in the depths!", new Color(50, 255, 130));
            }
            else if (Main.netMode == NetmodeID.Server)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The spirits are stirring in the depths!"), new Color(50, 255, 130));
            }
            NPC.SetEventFlagCleared(ref ModContent.GetInstance<DownedBossSystem>().DownedPhantasm, -1);
        }
    }
    public override void HitEffect(int hitDirection, double damage)
    {
        if (NPC.life <= 0)
        {
            for (int i = 0; i < 40; i++)
            {
                int num890 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num890].velocity *= 5f;
                Main.dust[num890].scale = 1.5f;
                Main.dust[num890].noGravity = true;
                Main.dust[num890].fadeIn = 2f;
            }
            for (int i = 0; i < 20; i++)
            {
                int num893 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num893].velocity *= 2f;
                Main.dust[num893].scale = 1.5f;
                Main.dust[num893].noGravity = true;
                Main.dust[num893].fadeIn = 3f;
            }
            for (int i = 0; i < 40; i++)
            {
                int num892 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.SpectreStaff, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num892].velocity *= 5f;
                Main.dust[num892].scale = 1.5f;
                Main.dust[num892].noGravity = true;
                Main.dust[num892].fadeIn = 2f;
            }
            for (int i = 0; i < 40; i++)
            {
                int num891 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num891].velocity *= 10f;
                Main.dust[num891].scale = 1.5f;
                Main.dust[num891].noGravity = true;
                Main.dust[num891].fadeIn = 1.5f;
            }
        }
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
        notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, new int[] { ModContent.ItemType<Items.Weapons.Magic.PhantomKnives>(), ModContent.ItemType<Items.Accessories.EtherealHeart>(), ModContent.ItemType<Items.Accessories.VampireTeeth>() }));
        notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<GhostintheMachine>(), 1, 3, 6));
        npcLoot.Add(notExpertRule);
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PhantasmTrophy>(), 10));
        npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<Items.BossBags.PhantasmBossBag>()));
    }
    public override void FindFrame(int frameHeight)
    {
        NPC.frameCounter++;
        if (NPC.frameCounter >= 4.0)
        {
            NPC.frame.Y += frameHeight;
            NPC.frameCounter = 0.0;
        }
        if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
        {
            NPC.frame.Y = 0;
        }
    }
    public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
    {
        position -= new Vector2(0, -25f);
        return true;
    }
    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>("Avalon/NPCs/Bosses/PhantasmBlasfah").Value;
        int frameHeight = texture.Height / Main.projFrames[NPC.type];
        Rectangle sourceRectangle = new Rectangle(0, NPC.frame.Y, texture.Width, frameHeight);
        Vector2 frameOrigin = sourceRectangle.Size() / 2f;
        Vector2 offset = new Vector2(NPC.width / 2 - frameOrigin.X, NPC.height - sourceRectangle.Height);
        Vector2 drawPos = NPC.position - Main.screenPosition + frameOrigin + offset;

        for (int i = 1; i < 4; i++)
        {
            Main.EntitySpriteDraw(texture, drawPos + new Vector2(NPC.velocity.X * (-i * 2), NPC.velocity.Y * (-i * 2)), sourceRectangle, new Color(255, 255, 255, 225) * (1 - (i * 0.25f)) * 0.25f, NPC.rotation, frameOrigin, NPC.scale, SpriteEffects.None, 0);
        }
        Main.EntitySpriteDraw(texture, drawPos, sourceRectangle, new Color(255, 255, 255, 225) * 0.3f, NPC.rotation, frameOrigin, NPC.scale * 1.1f, SpriteEffects.None, 0);
        Main.EntitySpriteDraw(texture, drawPos, sourceRectangle, new Color(255, 255, 255, 225) * 0.15f, NPC.rotation, frameOrigin, NPC.scale * 1.2f, SpriteEffects.None, 0);

        Main.EntitySpriteDraw(texture, drawPos, sourceRectangle, new Color(255, 255, 255, 225), NPC.rotation, frameOrigin, NPC.scale, SpriteEffects.None, 0);
        return false;
    }
}