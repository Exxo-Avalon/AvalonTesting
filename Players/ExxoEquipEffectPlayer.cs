using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Avalon.Buffs;
using Avalon.NPCs.Bosses;
using Avalon.Projectiles;
using Avalon.Tiles.Ores;
using Terraria.Localization;
using Avalon.Systems;

namespace Avalon.Players;
public class ExxoEquipEffectPlayer : ModPlayer
{
    #region accessories
    public bool ShadowCharm;
    public bool PulseCharm;
    public bool BagOfFire;
    public bool BagOfBlood;
    public bool BagOfFrost;
    public bool BagOfHallows;
    public bool BagOfIck;
    public bool BagOfShadows;
    public bool InertiaBoots;
    public bool BlahWings;
    public bool SpikeImmune;
    public bool LuckTome;
    public bool ThornHeartAmulet;
    public bool EarthInsig;
    public bool BloodyWhetstone;
    public bool ChaosCharm;
    public bool CobShield;
    public bool PallShield;
    public bool DuraShield;
    public bool BOfBacteria;
    public bool SlimeBand;
    public bool CobOmegaShield;
    public bool PallOmegaShield;
    public bool DuraOmegaShield;
    public bool TrapImmune;
    public bool ConfusionTal;
    public bool VampireTeeth;
    public bool BubbleBoost;
    public bool HeartGolem;
    public bool EthHeart;
    public bool ShadowRing;
    public bool HideVarefolk;
    public bool FrostGauntlet;
    public bool EarthInsignia;
    public bool TerraClaws;
    public bool BadgeOfBacteria;
    public bool AstralProject;
    public bool GoblinToolbelt;
    public bool GoblinAK;
    public bool BuilderBelt;
    public bool Dimlight;
    public bool PocketBench;
    #endregion accessories

    #region extras
    public int bubbleCD;
    public int AstralCooldown = 3600;
    public const int MaxAstralCooldown = 3600; //constraint cooldown, make it no more than max.
    private int[] doubleTapTimer = new int[2];
    #endregion extras

    #region armor
    public bool HyperMelee;
    public bool HyperMagic;
    public bool HyperRanged;
    public int HyperBar;
    public bool AuraThorns;
    public bool DuraArmor;
    public int DuraArmorBonusDef;

    public bool AncientLessCost;
    public bool AncientGunslinger;
    public bool AncientMinionGuide;
    public bool AncientSandVortex;

    public bool BlahArmor;

    public bool GoBerserk;
    public bool FrenzyStance;
    public bool FrenzyStanceActive;
    public bool CaesiumBoost;
    public bool CaesiumBoostActive;
    #endregion armor
    public override void ResetEffects()
    {
        // accessories
        ShadowCharm = false;
        PulseCharm = false;
        BagOfBlood = false;
        BagOfFire = false;
        BagOfFrost = false;
        BagOfHallows = false;
        BagOfIck = false;
        BagOfShadows = false;
        InertiaBoots = false;
        SpikeImmune = false;
        LuckTome = false;
        ThornHeartAmulet = false;
        EarthInsig = false;
        ChaosCharm = false;
        CobShield = false;
        PallShield = false;
        DuraShield = false;
        BOfBacteria = false;
        SlimeBand = false;
        CobOmegaShield = false;
        PallOmegaShield = false;
        TrapImmune = false;
        ConfusionTal = false;
        VampireTeeth = false;
        BubbleBoost = false;
        ShadowRing = false;
        HideVarefolk = false;
        FrostGauntlet = false;
        BloodyWhetstone = false;
        BadgeOfBacteria = false;
        AstralProject = false;
        DuraOmegaShield = false;
        BlahWings = false;
        LuckTome = false;
        TerraClaws = false;
        GoblinToolbelt = false;
        GoblinAK = false;
        BuilderBelt = false;
        Dimlight = false;
        PocketBench = false;

        // armor
        HyperMagic = false;
        HyperMelee = false;
        HyperRanged = false;
        DuraArmor = false;
        DuraArmorBonusDef = 0;
        AncientLessCost = false;
        AncientGunslinger = false;
        AncientMinionGuide = false;
        AncientSandVortex = false;
        FrenzyStance = false;
        CaesiumBoost = false;
    }
    public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
    {
        if (HyperMelee)
        {
            HyperBar++;
            if (HyperBar > 15 && HyperBar <= 25)
            {
                crit = true;
                if (HyperBar == 25)
                {
                    HyperBar = 0;
                }
            }
        }
        if (ConfusionTal && Main.rand.Next(100) <= 12)
        {
            target.AddBuff(BuffID.Confused, 540);
        }
    }
    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    {
        if (HyperMelee && proj.DamageType == DamageClass.Melee)
        {
            HyperBar++;
            if (HyperBar > 15 && HyperBar <= 25)
            {
                crit = true;
                if (HyperBar == 25)
                {
                    HyperBar = 0;
                }
            }
        }
        if (HyperRanged && proj.DamageType == DamageClass.Ranged)
        {
            HyperBar++;
            if (HyperBar > 15 && HyperBar <= 25)
            {
                crit = true;
                if (HyperBar == 25)
                {
                    HyperBar = 0;
                }
            }
        }
        if (HyperMagic && proj.DamageType == DamageClass.Magic)
        {
            HyperBar++;
            if (HyperBar > 15 && HyperBar <= 25)
            {
                crit = true;
                if (HyperBar == 25)
                {
                    HyperBar = 0;
                }
            }
        }
    }
    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        if (AstralProject && KeybindSystem.AstralHotkey.JustPressed)
        {
            if (Player.HasBuff<AstralProjecting>())
            {
                Player.ClearBuff(ModContent.BuffType<AstralProjecting>());
                AstralCooldown = MaxAstralCooldown;
            }
            else if (AstralCooldown == 0)
            {
                Player.AddBuff(ModContent.BuffType<AstralProjecting>(), 15 * 60);
            }
        }
    }
    public override void OnHitPvp(Item item, Player target, int damage, bool crit)
    {
        if (FrostGauntlet && item.DamageType == DamageClass.Melee)
        {
            target.AddBuff(BuffID.Frostburn, 60 * 4);
        }
        if (TerraClaws && item.DamageType == DamageClass.Melee)
        {
            switch (Main.rand.Next(5))
            {
                case 0:
                    target.AddBuff(BuffID.OnFire, 7 * 60);
                    break;
                case 1:
                    target.AddBuff(BuffID.Poisoned, 7 * 60);
                    break;
                case 2:
                    target.AddBuff(BuffID.Venom, 7 * 60);
                    break;
                case 3:
                    target.AddBuff(BuffID.Frostburn2, 7 * 60);
                    break;
                case 4:
                    target.AddBuff(BuffID.Ichor, 7 * 60);
                    break;
            }
        }
    }
    public override void OnHitPvpWithProj(Projectile proj, Player target, int damage, bool crit)
    {
        if (FrostGauntlet && proj.DamageType == DamageClass.Melee)
        {
            target.AddBuff(BuffID.Frostburn, 60 * 4);
        }
        if (TerraClaws && proj.DamageType == DamageClass.Melee)
        {
            switch (Main.rand.Next(5))
            {
                case 0:
                    target.AddBuff(BuffID.OnFire, 7 * 60);
                    break;
                case 1:
                    target.AddBuff(BuffID.Poisoned, 7 * 60);
                    break;
                case 2:
                    target.AddBuff(BuffID.Venom, 7 * 60);
                    break;
                case 3:
                    target.AddBuff(BuffID.Frostburn2, 7 * 60);
                    break;
                case 4:
                    target.AddBuff(BuffID.Ichor, 7 * 60);
                    break;
            }
        }
    }
    public override void OnHitByNPC(NPC npc, int damage, bool crit)
    {
        if (Dimlight && Main.rand.NextBool(10) && !Player.HasBuff(ModContent.BuffType<Untargetable>()))
        {
            Player.AddBuff(ModContent.BuffType<Untargetable>(), 60 * 5);
        }
        if (Player.whoAmI == Main.myPlayer && BadgeOfBacteria)
        {
            Player.AddBuff(ModContent.BuffType<BacteriaEndurance>(), 6 * 60);
            npc.AddBuff(ModContent.BuffType<BacteriaInfection>(), 6 * 60);
        }
        if (AuraThorns && !Player.immune && !npc.dontTakeDamage)
        {
            int x = (int)Player.position.X;
            int y = (int)Player.position.Y;
            foreach (NPC N2 in Main.npc)
            {
                if (N2.position.X >= x - 620 && N2.position.X <= x + 620 && N2.position.Y >= y - 620 &&
                    N2.position.Y <= y + 620)
                {
                    if (!N2.active || N2.dontTakeDamage || N2.townNPC || N2.life < 1 || N2.boss ||
                        N2.realLife >= 0) //|| N2.type == ModContent.NPCType<NPCs.Juggernaut>())
                    {
                        continue;
                    }

                    N2.StrikeNPC(damage, 5f, 1);
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        NetMessage.SendData(MessageID.DamageNPC, -1, -1, NetworkText.FromLiteral(""), N2.whoAmI,
                            damage);
                    }
                }
            }
        }
    }
    public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
    {
        if (VampireTeeth && item.DamageType == DamageClass.Melee)
        {
            if (target.boss)
            {
                Player.VampireHeal(damage / 2, target.Center);
            }
            else
            {
                Player.VampireHeal(damage, target.Center);
            }
        }
        if (AncientSandVortex && Main.rand.NextBool(10))
        {
            Projectile.NewProjectile(Player.GetSource_OnHit(target), target.position, Vector2.Zero,
                ModContent.ProjectileType<AncientSandnado>(), 0, 0);
        }
        if (FrostGauntlet && item.DamageType == DamageClass.Melee)
        {
            target.AddBuff(BuffID.Frostburn, 60 * 4);
        }
        if (item.DamageType == DamageClass.Melee && BloodyWhetstone)
        {
            if (!target.HasBuff<Bleeding>())
            {
                target.GetGlobalNPC<AvalonGlobalNPCInstance>().BleedStacks = 1;
            }

            target.AddBuff(ModContent.BuffType<Bleeding>(), 120);
        }
        if (TerraClaws && item.DamageType == DamageClass.Melee)
        {
            switch (Main.rand.Next(5))
            {
                case 0:
                    target.AddBuff(BuffID.OnFire, 7 * 60);
                    break;
                case 1:
                    target.AddBuff(BuffID.Poisoned, 7 * 60);
                    break;
                case 2:
                    target.AddBuff(BuffID.Venom, 7 * 60);
                    break;
                case 3:
                    target.AddBuff(BuffID.Frostburn2, 7 * 60);
                    break;
                case 4:
                    target.AddBuff(BuffID.Ichor, 7 * 60);
                    break;
            }
        }
    }
    public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
    {
        if (target.life <= 0 && AncientGunslinger && proj.owner == Main.myPlayer && proj.DamageType == DamageClass.Ranged)
        {
            Projectile.NewProjectile(Player.GetSource_OnHit(target), target.position, Vector2.Zero,
                ModContent.ProjectileType<SandyExplosion>(), damage * 2, knockback);
        }
        if (FrostGauntlet && proj.DamageType == DamageClass.Melee)
        {
            target.AddBuff(BuffID.Frostburn, 60 * 4);
        }
        if (TerraClaws && proj.DamageType == DamageClass.Melee)
        {
            switch (Main.rand.Next(5))
            {
                case 0:
                    target.AddBuff(BuffID.OnFire, 7 * 60);
                    break;
                case 1:
                    target.AddBuff(BuffID.Poisoned, 7 * 60);
                    break;
                case 2:
                    target.AddBuff(BuffID.Venom, 7 * 60);
                    break;
                case 3:
                    target.AddBuff(BuffID.Frostburn2, 7 * 60);
                    break;
                case 4:
                    target.AddBuff(BuffID.Ichor, 7 * 60);
                    break;
            }
        }
    }
    public override void MeleeEffects(Item item, Rectangle hitbox)
    {
        if (FrostGauntlet && item.DamageType == DamageClass.Melee)
        {
            int d = Dust.NewDust(new(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.IceTorch, Player.velocity.X * 0.2f + Player.direction * 3, Player.velocity.Y * 0.2f, 100, default, 2.2f);
            Main.dust[d].noGravity = true;
            Main.dust[d].velocity *= 2f;
        }
        if (TerraClaws && item.DamageType == DamageClass.Melee)
        {
            float s = 1.5f;
            int rn = Main.rand.Next(5);
            switch (rn)
            {
                case 0:
                    rn = DustID.Poisoned;
                    s = 1.4f;
                    break;
                case 1:
                    rn = DustID.IceTorch;
                    s = 1.4f;
                    break;
                case 2:
                    rn = DustID.Torch;
                    s = 1.4f;
                    break;
                case 3:
                    rn = DustID.VenomStaff;
                    s = 1.4f;
                    break;
                case 4:
                    rn = DustID.IchorTorch;
                    s = 1.4f;
                    break;
            }
            int d = Dust.NewDust(new(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, rn, Player.velocity.X * 0.2f + Player.direction * 3, Player.velocity.Y * 0.2f, 100, default, s);
            Main.dust[d].noGravity = true;
            Main.dust[d].velocity *= 2f;
        }
    }

    public void KeyDoubleTap(int keyDir)
    {
        int num = 0;
        if (Main.ReversedUpDownArmorSetBonuses)
        {
            num = 1;
        }
        if (keyDir != num)
        {
            return;
        }
        if (FrenzyStance && !Player.mount.Active)
        {
            FrenzyStanceActive = !FrenzyStanceActive;
        }
        if (CaesiumBoost && !Player.mount.Active)
        {
            CaesiumBoostActive = !CaesiumBoostActive;
        }
    }

    public override void PostUpdateEquips()
    {
        if (!AstralProject && Player.HasBuff<AstralProjecting>())
        {
            Player.ClearBuff(ModContent.BuffType<AstralProjecting>());
        }
        if (Player.HasItem(ModContent.ItemType<Items.Weapons.Blah.BlahsEnergyBlade>()))
        {
            PocketBench = true;
        }
        for (int m = 0; m < 2; m++)
        {
            doubleTapTimer[m]--;
            if (doubleTapTimer[m] < 0)
            {
                doubleTapTimer[m] = 0;
            }
        }
        for (int m = 0; m < 2; m++)
        {
            bool keyPressedAndReleased = false;
            switch (m)
            {
                case 0:
                    keyPressedAndReleased = Player.controlDown && Player.releaseDown;
                    break;
                case 1:
                    keyPressedAndReleased = Player.controlUp && Player.releaseUp;
                    break;
            }
            if (keyPressedAndReleased)
            {
                if (doubleTapTimer[m] > 0)
                {
                    KeyDoubleTap(m);
                }
                else
                {
                    doubleTapTimer[m] = 15;
                }
            }
        }

        #region royal gel avalon fix
        // doesn't work for modded accessory slots :pensive:
        for (int i = 3; i < Player.armor.Length; i++)
        {
            if (Player.armor[i].type == ItemID.RoyalGel)
            {
                Player.npcTypeNoAggro[ModContent.NPCType<NPCs.CopperSlime>()] = true;
                Player.npcTypeNoAggro[ModContent.NPCType<NPCs.TinSlime>()] = true;
                Player.npcTypeNoAggro[ModContent.NPCType<NPCs.BronzeSlime>()] = true;
                Player.npcTypeNoAggro[ModContent.NPCType<NPCs.IronSlime>()] = true;
                Player.npcTypeNoAggro[ModContent.NPCType<NPCs.LeadSlime>()] = true;
                Player.npcTypeNoAggro[ModContent.NPCType<NPCs.NickelSlime>()] = true;
                Player.npcTypeNoAggro[ModContent.NPCType<NPCs.SilverSlime>()] = true;
                Player.npcTypeNoAggro[ModContent.NPCType<NPCs.TungstenSlime>()] = true;
                Player.npcTypeNoAggro[ModContent.NPCType<NPCs.ZincSlime>()] = true;
                Player.npcTypeNoAggro[ModContent.NPCType<NPCs.GoldSlime>()] = true;
                Player.npcTypeNoAggro[ModContent.NPCType<NPCs.PlatinumSlime>()] = true;
                Player.npcTypeNoAggro[ModContent.NPCType<NPCs.BismuthSlime>()] = true;
                Player.npcTypeNoAggro[ModContent.NPCType<NPCs.RhodiumSlime>()] = true;
                Player.npcTypeNoAggro[ModContent.NPCType<NPCs.OsmiumSlime>()] = true;
                Player.npcTypeNoAggro[ModContent.NPCType<NPCs.IridiumSlime>()] = true;
                Player.npcTypeNoAggro[ModContent.NPCType<NPCs.CobaltSlime>()] = true;
                Player.npcTypeNoAggro[ModContent.NPCType<NPCs.PalladiumSlime>()] = true;
                Player.npcTypeNoAggro[ModContent.NPCType<NPCs.DurantiumSlime>()] = true;
                Player.npcTypeNoAggro[ModContent.NPCType<NPCs.MythrilSlime>()] = true;
                Player.npcTypeNoAggro[ModContent.NPCType<NPCs.OrichalcumSlime>()] = true;
                Player.npcTypeNoAggro[ModContent.NPCType<NPCs.NaquadahSlime>()] = true;
                Player.npcTypeNoAggro[ModContent.NPCType<NPCs.AdamantiteSlime>()] = true;
                Player.npcTypeNoAggro[ModContent.NPCType<NPCs.TitaniumSlime>()] = true;
                Player.npcTypeNoAggro[ModContent.NPCType<NPCs.TroxiniumSlime>()] = true;
                Player.npcTypeNoAggro[ModContent.NPCType<NPCs.DarkMatterSlime>()] = true;
            }
        }
        #endregion royal gel avalon fix

        #region inertia boots/blah's wings
        if (InertiaBoots || BlahWings)
        {
            if (Player.controlUp && Player.controlJump)
            {
                Player.wingsLogic = 0;
                Player.velocity.Y = Player.velocity.Y - (0.7f * Player.gravDir);
                if (Player.gravDir == 1f)
                {
                    if (Player.velocity.Y > 0f)
                    {
                        Player.velocity.Y = Player.velocity.Y - 1f;
                    }
                    else if (Player.velocity.Y > -Player.jumpSpeed)
                    {
                        Player.velocity.Y = Player.velocity.Y - 0.5f;
                    }

                    if (Player.velocity.Y < -Player.jumpSpeed * 3f)
                    {
                        Player.velocity.Y = -Player.jumpSpeed * 3f;
                    }
                }
                else
                {
                    if (Player.velocity.Y < 0f)
                    {
                        Player.velocity.Y = Player.velocity.Y + 1f;
                    }
                    else if (Player.velocity.Y < Player.jumpSpeed)
                    {
                        Player.velocity.Y = Player.velocity.Y + 0.5f;
                    }

                    if (Player.velocity.Y > Player.jumpSpeed * 3f)
                    {
                        Player.velocity.Y = Player.jumpSpeed * 3f;
                    }
                }
            }
        }
        #endregion inertia boots/blah's wings

        #region chaos charm
        if (ChaosCharm)
        {
            int modCrit = 2 * (int)Math.Floor((Player.statLifeMax2 - (double)Player.statLife) /
                Player.statLifeMax2 * 10.0);
            Player.GetCritChance(DamageClass.Generic) += modCrit;
        }
        #endregion chaos charm

        #region bubble boost
        if (BubbleBoost && !Player.IsOnGround() && !Player.releaseJump &&
            !NPC.AnyNPCs(ModContent.NPCType<ArmageddonSlime>()))
        {
            #region bubble timer and spawn bubble gores/sound
            bubbleCD++;
            if (bubbleCD == 20)
            {
                for (int i = 0; i < 3; i++)
                {
                    int g1 = Gore.NewGore(Player.GetSource_FromThis(),
                        Player.Center + new Vector2(Main.rand.Next(-32, 33), Main.rand.Next(-32, 33)), Player.velocity,
                        Mod.Find<ModGore>("Bubble").Type);
                    SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/Bubbles"),
                        Player.position);
                }
            }

            if (bubbleCD == 30)
            {
                for (int i = 0; i < 2; i++)
                {
                    int g1 = Gore.NewGore(Player.GetSource_FromThis(),
                        Player.Center + new Vector2(Main.rand.Next(-32, 33), Main.rand.Next(-32, 33)), Player.velocity,
                        Mod.Find<ModGore>("LargeBubble").Type);
                }
            }

            if (bubbleCD == 40)
            {
                for (int i = 0; i < 4; i++)
                {
                    int g1 = Gore.NewGore(Player.GetSource_FromThis(),
                        Player.Center + new Vector2(Main.rand.Next(-32, 33), Main.rand.Next(-32, 33)), Player.velocity,
                        Mod.Find<ModGore>("SmallBubble").Type);
                }

                bubbleCD = 0;
            }
            #endregion bubble timer and spawn bubble gores/sound

            #region down
            if (Player.controlDown && Player.controlJump)
            {
                Player.wingsLogic = 0;
                Player.rocketBoots = 0;
                if (Player.controlLeft)
                {
                    Player.velocity.X = -15f;
                }
                else if (Player.controlRight)
                {
                    Player.velocity.X = 15f;
                }
                else
                {
                    Player.velocity.X = 0f;
                }

                Player.velocity.Y = 15f;
                //bubbleBoostActive = true;
            }
            #endregion down
            #region up
            else if (Player.controlUp && Player.controlJump)
            {
                Player.wingsLogic = 0;
                Player.rocketBoots = 0;
                if (Player.controlLeft)
                {
                    Player.velocity.X = -15f;
                }
                else if (Player.controlRight)
                {
                    Player.velocity.X = 15f;
                }
                else
                {
                    Player.velocity.X = 0f;
                }

                Player.velocity.Y = -15f;
                //bubbleBoostActive = true;
            }
            #endregion up
            #region left
            else if (Player.controlLeft && Player.controlJump)
            {
                Player.velocity.X = -15f;
                Player.wingsLogic = 0;
                Player.rocketBoots = 0;
                if (Player.gravDir == 1f && Player.velocity.Y > -Player.gravity)
                {
                    Player.velocity.Y = -(Player.gravity + 1E-06f);
                }
                else if (Player.gravDir == -1f && Player.velocity.Y < Player.gravity)
                {
                    Player.velocity.Y = Player.gravity + 1E-06f;
                }
                //bubbleBoostActive = true;
            }
            #endregion left
            #region right
            else if (Player.controlRight && Player.controlJump)
            {
                Player.velocity.X = 15f;
                Player.wingsLogic = 0;
                Player.rocketBoots = 0;
                if (Player.gravDir == 1f && Player.velocity.Y > -Player.gravity)
                {
                    Player.velocity.Y = -(Player.gravity + 1E-06f);
                }
                else if (Player.gravDir == -1f && Player.velocity.Y < Player.gravity)
                {
                    Player.velocity.Y = Player.gravity + 1E-06f;
                }

                //bubbleBoostActive = true;
            }
            #endregion right

            StayInBounds(Player.position);
        }
        #endregion bubble boost

        #region duratanium armor setbonus
        if (DuraArmor)
        {
            bool flag = false;
            for (int num22 = 0; num22 < 22; num22++)
            {
                if (Main.debuff[Player.buffType[num22]] && Player.buffType[num22] != BuffID.Horrified &&
                    Player.buffType[num22] != BuffID.PotionSickness && Player.buffType[num22] != BuffID.Merfolk &&
                    Player.buffType[num22] != BuffID.Werewolf && Player.buffType[num22] != BuffID.TheTongue &&
                    Player.buffType[num22] != BuffID.ManaSickness && Player.buffType[num22] != BuffID.Wet &&
                    Player.buffType[num22] != BuffID.Slimed &&
                    Player.buffType[num22] != ModContent.BuffType<StaminaDrain>())
                {
                    flag = true;
                    break;
                }
            }

            if (flag)
            {
                DuraArmorBonusDef = 12; // defDebuffBonusDef is here to avoid the def buff sticking around 24/7 because of terraria code jank
            }
            else
            {
                DuraArmorBonusDef = 0;
            }
        }

        Player.statDefense += DuraArmorBonusDef; // outside of the if statement to remove extra defense
        #endregion duratanium armor setbonus
    }
    public override void PostUpdate()
    {
        Vector2 pposTile = Player.Center / 16;
        for (int xpos = (int)pposTile.X - 4; xpos <= (int)pposTile.X + 4; xpos++)
        {
            for (int ypos = (int)pposTile.Y - 4; ypos <= (int)pposTile.Y + 4; ypos++)
            {
                if (Main.tile[xpos, ypos].TileType == (ushort)ModContent.TileType<TritanoriumOre>() ||
                    Main.tile[xpos, ypos].TileType == (ushort)ModContent.TileType<PyroscoricOre>())
                {
                    if (!LuckTome && !BlahWings)
                    {
                        Player.AddBuff(ModContent.BuffType<Melting>(), 60);
                    }
                }
            }
        }
        if (FrenzyStanceActive)
        {
            Player.GetCritChance(DamageClass.Melee) -= 12;
        }
        if (CaesiumBoostActive)
        {
            Player.endurance += 0.2f;
            Player.accRunSpeed *= 0.3f;
            Player.maxRunSpeed *= 0.3f;
        }
        if (!FrenzyStance)
        {
            FrenzyStanceActive = false;
        }
        if (!CaesiumBoost)
        {
            CaesiumBoostActive = false;
        }
    }
    public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
    {
        if (GoBerserk)
        {
            if (damage > 50)
            {
                Player.AddBuff(ModContent.BuffType<Berserk>(), 180);
            }
        }
    }
    public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
    {
        if (Player.whoAmI == Main.myPlayer && CobShield)
        {
            int time = 300;
            if (CobOmegaShield)
            {
                time = 600;
            }

            Player.AddBuff(BuffID.Ironskin, time);
        }

        if (Player.whoAmI == Main.myPlayer && PallShield)
        {
            int hpHealed = 5;
            if (PallOmegaShield)
            {
                hpHealed = 10;
            }

            Player.statLife += hpHealed;
            Player.HealEffect(hpHealed);
        }
    }
    /// <summary>
    /// Keep the player in the bounds of the world.
    /// </summary>
    /// <param name="pos"></param>
    public static void StayInBounds(Vector2 pos)
    {
        if (pos.X > Main.maxTilesX - 100)
        {
            pos.X = Main.maxTilesX - 100;
        }

        if (pos.X < 100f)
        {
            pos.X = 100f;
        }

        if (pos.Y > Main.maxTilesY)
        {
            pos.Y = Main.maxTilesY;
        }

        if (pos.Y < 100f)
        {
            pos.Y = 100f;
        }
    }
}
