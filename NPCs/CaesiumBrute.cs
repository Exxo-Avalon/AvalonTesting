using Avalon.Items.Placeable.Tile;
using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;

namespace Avalon.NPCs;

public class CaesiumBrute : ModNPC
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Caesium Brute");
        Main.npcFrameCount[NPC.type] = 5;
        NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
        {
            SpecificallyImmuneTo = new int[]
            {
                BuffID.OnFire,
                BuffID.CursedInferno
            }
        };
        NPCID.Sets.DebuffImmunitySets[Type] = debuffData;
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement("Unlike normal Demons, these creatures are capable of knocking you far away from them!")
        });
    }
    public override void SetDefaults()
    {
        NPC.damage = 62;
        NPC.lifeMax = 780;
        NPC.defense = 45;
        NPC.noGravity = true;
        NPC.width = 28;
        NPC.aiStyle = 14;
        NPC.npcSlots = 2f;
        NPC.value = 15000f;
        NPC.height = 48;
        NPC.HitSound = SoundID.NPCHit21;
        NPC.DeathSound = SoundID.NPCDeath24;
        NPC.knockBackResist = 0.1f;
        NPC.lavaImmune = true;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<Items.Banners.CaesiumBruteBanner>();
        SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.CaesiumBlastplains>().Type };
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.DownedAllMechBosses(), ModContent.ItemType<CaesiumOre>(), 6, 3, 7));
    }
    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.45f);
        NPC.damage = (int)(NPC.damage * 0.4f);
    }
    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (spawnInfo.Player.GetModPlayer<ExxoBiomePlayer>().ZoneCaesium && spawnInfo.Player.ZoneUnderworldHeight && !NPC.AnyNPCs(NPCID.WallofFlesh))// && !NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.WallofSteel>()))
            return 0.8f;
        return 0;
    }
    public override void OnHitPlayer(Player target, int damage, bool crit)
    {
        if (NPC.Center.X <= target.Center.X) target.velocity.X += 15;
        else target.velocity.X -= 15;
    }
    public override void AI()
    {
        NPC.ai[0]++;
        if (NPC.ai[0] > 240)
        {
            if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
            {
                int increments = 3;
                float degrees = 15f;
                float offset = (float)((float)(degrees / increments) / 2f); // IF YOU WANT THE ATTACK TO BE AIMED WITH EVEN INCREMENTS, REMOVE OFFSET FROM THE VELOCITY CALCULATION
                Vector2 rotation = (Main.player[NPC.target].Center - NPC.Center).SafeNormalize(-Vector2.UnitY);
                float speed = 7f;
                for (int i = 0; i < increments; i++)
                {
                    Vector2 velocity = rotation.RotatedBy(MathHelper.ToRadians(((float)(degrees / 2f) * -1f) + ((float)(degrees / increments) * i) + offset)) * speed;
                    int spray = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<Projectiles.Hostile.CaesiumFireball>(), 55, 0f, NPC.target, 1f, 0f);
                    if (Main.netMode != NetmodeID.SinglePlayer)
                    {
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.Empty, spray);
                    }
                }
                SoundEngine.PlaySound(SoundID.Item8, NPC.position);
            }
            NPC.ai[0] = 0;
        }
    }
    public override void FindFrame(int frameHeight)
    {
        NPC.spriteDirection = NPC.direction;
        NPC.rotation = NPC.velocity.X * 0.1f;
        int num226 = 5;
        int num227 = 5;
        NPC.frameCounter += 1.0;
        if (NPC.frameCounter >= (double)(num226 * num227))
        {
            NPC.frameCounter = 0.0;
        }
        int num228 = (int)(NPC.frameCounter / (double)num226);
        NPC.frame.Y = num228 * frameHeight;
    }
    public override void HitEffect(int hitDirection, double damage)
    {
        if (NPC.life > 0)
        {
            SoundEngine.PlaySound(SoundID.NPCHit21 with { Volume = 1.2f, Pitch = -0.5f }, NPC.Center);
            if (Main.rand.Next(20) == 0)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, ModContent.ProjectileType<Projectiles.Hostile.CaesiumGas>(), 0, 0);
            }
        }
        if (Main.netMode == NetmodeID.Server) return;
        if (NPC.life <= 0)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f, Mod.Find<ModGore>("CaesiumBruteHead").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f, Mod.Find<ModGore>("CaesiumBruteWing").Type, 1f);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f, Mod.Find<ModGore>("CaesiumBruteWing").Type, 1f);
        }
    }
}
