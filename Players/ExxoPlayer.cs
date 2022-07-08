using System;
using System.Collections.Generic;
using System.Linq;
using AvalonTesting.Buffs;
using AvalonTesting.Dusts;
using AvalonTesting.Items.Accessories;
using AvalonTesting.Items.Consumables;
using AvalonTesting.Items.Fish;
using AvalonTesting.Items.Other;
using AvalonTesting.Items.Tomes;
using AvalonTesting.Items.Tools;
using AvalonTesting.Items.Weapons.Ranged;
using AvalonTesting.Logic;
using AvalonTesting.NPCs.Bosses;
using AvalonTesting.Prefixes;
using AvalonTesting.Projectiles;
using AvalonTesting.Projectiles.Melee;
using AvalonTesting.Systems;
using AvalonTesting.Tiles;
using AvalonTesting.Tiles.Ores;
using AvalonTesting.Walls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AvalonTesting.Players;

public class ExxoPlayer : ModPlayer
{
    public static Asset<Texture2D>[] spectrumArmorTextures;
    protected override bool CloneNewInstances => false;

    public static void stayInBounds(Vector2 pos)
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

    public static int NumHookProj() => Main.projectile.Count(p =>
        Main.projHook[p.type] && p.active && p.ai[0] == 2f && p.owner == Main.myPlayer);

    public override void ResetEffects()
    {
        //Main.NewText("" + trapImmune.ToString());
        //Main.NewText("" + slimeBand.ToString());
        Player.defaultItemGrabRange = 38;
        oreDupe = false;
        skyBlessing = false;
        inertiaBoots = false;
        luckTome = false;
        blahWings = false;
        spikeImmune = false;
        snotOrb = false;
        shockWave = false;
        quackJump = false;
        bOfBacteria = false;
        stingerPack = false;
        crystalEdge = false;
        tpStam = true;
        riftGoggles = false;
        trapImmune = false;
        undeadTalisman = false;
        vampireTeeth = false;
        cOmega = false;
        pOmega = false;
        slimeBand = false;
        noSticky = false;
        lucky = false;
        enemySpawns2 = false;
        bloodCast = false;
        magnet = false;
        bubbleBoost = false;
        darkInferno = false;
        melting = false;
        dragonsBondage = false;
        necroticAura = false;
        defDebuff = false;
        defDebuffBonusDef = 0;
        frozen = false;
        LightningInABottle = false;
        reckoning = false;
        hyperMagic = false;
        hyperMelee = false;
        hyperRanged = false;
        ancientLessCost = false;
        ancientGunslinger = false;
        ancientMinionGuide = false;
        ancientSandVortex = false;
        oblivionKill = false;
        goBerserk = false;
        splitProj = false;
        spectrumSpeed = false;
        spectrumBlur = false;
        minionFreeze = false;
        leafStorm = false;
        thornMagic = false;
        roseMagic = false;
        avalonRestoration = false;
        avalonRetribution = false;
        curseOfIcarus = false;
        malaria = false;
        primeMinion = false;
        hungryMinion = false;
        gastroMinion = false;
        reflectorMinion = false;
        iceGolem = false;
        caesiumPoison = false;
        goldDagger = false;
        platinumDagger = false;
        bismuthDagger = false;
        adamantiteDagger = false;
        titaniumDagger = false;
        troxiniumDagger = false;
        UltraHMinion = false;
        UltraRMinion = false;
        UltraLMinion = false;
        cloudGloves = false;
        ReckoningBonus = false;
        bonusKB = 1f;
        miniArma = false;

        if (shmAcc)
        {
            Player.extraAccessory = true;
            Player.extraAccessorySlots++;
        }

        CritDamageMult = 1f;

        Player.GetModPlayer<ExxoStaminaPlayer>().StatStamMax2 = Player.GetModPlayer<ExxoStaminaPlayer>().StatStamMax;
        if (Player.whoAmI == Main.myPlayer)
        {
            MousePosition = Main.MouseWorld;
        }
    }

    public override void PostUpdateEquips()
    {
        for (int i = 0; i <= 9; i++)
        {
            Item item = Player.armor[i];
            (PrefixLoader.GetPrefix(item.prefix) as ExxoPrefix)?.UpdateOwnerPlayer(Player);
        }

        //player.statMana = statManaMax3;
        //statManaMax2 = player.statManaMax2;
        if (meleeStealth && armorStealth)
        {
            if (Player.itemAnimation > 0)
            {
                Player.stealthTimer = 5;
            }

            if (Player.velocity.X > -0.1 && Player.velocity.X < 0.1 && Player.velocity.Y > -0.1 &&
                Player.velocity.Y < 0.1)
            {
                if (Player.stealthTimer == 0)
                {
                    Player.stealth -= 0.015f;
                    if (Player.stealth < 0.0)
                    {
                        Player.stealth = 0f;
                    }
                }
            }
            else
            {
                float num23 = Math.Abs(Player.velocity.X) + Math.Abs(Player.velocity.Y);
                Player.stealth += num23 * 0.0075f;
                if (Player.stealth > 1f)
                {
                    Player.stealth = 1f;
                }
            }

            Player.GetDamage(DamageClass.Melee) += (1f - Player.stealth) * 0.4f;
            Player.GetCritChance(DamageClass.Melee) += (int)((1f - Player.stealth) * 8f);
            Player.GetDamage(DamageClass.Ranged) += (1f - Player.stealth) * 0.6f;
            Player.GetCritChance(DamageClass.Ranged) += (int)((1f - Player.stealth) * 10f);
            Player.aggro -= (int)((1f - Player.stealth) * 750f);
            if (Player.stealthTimer > 0)
            {
                Player.stealthTimer--;
            }
        }
        else if (armorStealth)
        {
            if (Player.itemAnimation > 0)
            {
                Player.stealthTimer = 5;
            }

            if (Player.velocity.X > -0.1 && Player.velocity.X < 0.1 && Player.velocity.Y > -0.1 &&
                Player.velocity.Y < 0.1)
            {
                if (Player.stealthTimer == 0)
                {
                    Player.stealth -= 0.015f;
                    if (Player.stealth < 0.0)
                    {
                        Player.stealth = 0f;
                    }
                }
            }
            else
            {
                float num24 = Math.Abs(Player.velocity.X) + Math.Abs(Player.velocity.Y);
                Player.stealth += num24 * 0.0075f;
                if (Player.stealth > 1f)
                {
                    Player.stealth = 1f;
                }
            }

            Player.GetDamage(DamageClass.Ranged) += (1f - Player.stealth) * 0.6f;
            Player.GetCritChance(DamageClass.Ranged) += (int)((1f - Player.stealth) * 10f);
            Player.aggro -= (int)((1f - Player.stealth) * 750f);
            if (Player.stealthTimer > 0)
            {
                Player.stealthTimer--;
            }
        }
        else if (meleeStealth)
        {
            if (Player.itemAnimation > 0)
            {
                Player.stealthTimer = 5;
            }

            if (Player.velocity.X > -0.1 && Player.velocity.X < 0.1 && Player.velocity.Y > -0.1 &&
                Player.velocity.Y < 0.1)
            {
                if (Player.stealthTimer == 0)
                {
                    Player.stealth -= 0.015f;
                    if (Player.stealth < 0.0)
                    {
                        Player.stealth = 0f;
                    }
                }
            }
            else
            {
                float num25 = Math.Abs(Player.velocity.X) + Math.Abs(Player.velocity.Y);
                Player.stealth += num25 * 0.0075f;
                if (Player.stealth > 1f)
                {
                    Player.stealth = 1f;
                }
            }

            Player.GetDamage(DamageClass.Melee) += (1f - Player.stealth) * 0.4f;
            Player.GetCritChance(DamageClass.Melee) += (int)((1f - Player.stealth) * 8f);
            Player.aggro -= (int)((1f - Player.stealth) * 750f);
            if (Player.stealthTimer > 0)
            {
                Player.stealthTimer--;
            }
        }
        else
        {
            Player.stealth = 1f;
        }

        if (inertiaBoots || blahWings)
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

        #region bubble boost
        if (bubbleBoost && activateBubble && !Player.IsOnGround() && !Player.releaseJump &&
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
                    SoundEngine.PlaySound(new SoundStyle($"{nameof(AvalonTesting)}/Sounds/Item/Bubbles"),
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
                bubbleBoostActive = true;
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
                bubbleBoostActive = true;
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

                bubbleBoostActive = true;
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

                bubbleBoostActive = true;
            }
            #endregion right

            stayInBounds(Player.position);
        }
        #endregion bubble boost

        if (chaosCharm)
        {
            int modCrit = 2 * (int)Math.Floor((Player.statLifeMax2 - (double)Player.statLife) /
                Player.statLifeMax2 * 10.0);
            Player.GetCritChance(DamageClass.Generic) += modCrit;
        }

        if (defDebuff)
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
                defDebuffBonusDef =
                    12; // defDebuffBonusDef is here to avoid the def buff sticking around 24/7 because of terraria code jank
            }
            else
            {
                defDebuffBonusDef = 0;
            }
        }

        Player.statDefense += defDebuffBonusDef; // outside of the if statement to remove extra defense

        if (teleportV || tpStam)
        {
            if (tpCD > 300)
            {
                tpCD = 300;
            }

            tpCD++;
        }

        Player.GetModPlayer<ExxoStaminaPlayer>().FlightRestoreCooldown++;
        if (Player.GetModPlayer<ExxoStaminaPlayer>().FlightRestoreCooldown > 3600)
        {
            Player.GetModPlayer<ExxoStaminaPlayer>().FlightRestoreCooldown = 3600;
        }

        if (curseOfIcarus)
        {
            Player.wingsLogic = 0;
            if (Player.mount.CanFly() ||
                Player.mount.CanHover()) // Setting player.mount._flyTime does not work for all mounts. Bye-bye mounts!
            {
                Player.mount.Dismount(Player);
            }

            // Alternative code which limits flight time instead of disabling it
            /*
            if (player.wingTime > 30)
                player.wingTime = 30;
            */
        }

        actualStatManaMax2 = Player.statManaMax2;
    }

    public override void OnEnterWorld(Player player)
    {
        if (tomeItem.type <= ItemID.None)
        {
            tomeItem.SetDefaults();
        }

        Main.NewText("You are using Exxo Avalon: Origins " + AvalonTesting.Mod.Version);
        Main.NewText("Please note that Exxo Avalon: Origins is in Beta; it may have many bugs");
        Main.NewText("Please also note that Exxo Avalon: Origins will interact strangely with other large mods");
    }

    public override void UpdateEquips()
    {
        if (tomeItem.stack > 0)
        {
            Player.VanillaUpdateEquip(tomeItem);
        }
    }

    public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
                               int type, int damage, float knockback)
    {
        #region spike cannon logic
        if (item.type == ModContent.ItemType<Items.Weapons.Ranged.SpikeCannon>() ||
            item.type == ModContent.ItemType<SpikeRailgun>())
        {
            var item2 = new Item();
            bool flag7 = false;
            bool inAmmoSlots = false;
            for (int i = 54; i < 58; i++)
            {
                if (Player.inventory[i].ammo == Player.inventory[Player.selectedItem].useAmmo &&
                    Player.inventory[i].stack > 0)
                {
                    item2 = Player.inventory[i];
                    flag7 = true;
                    inAmmoSlots = true;
                    break;
                }
            }

            if (!inAmmoSlots)
            {
                for (int i = 0; i < 54; i++)
                {
                    if (Player.inventory[i].ammo == Player.inventory[Player.selectedItem].useAmmo &&
                        Player.inventory[i].stack > 0)
                    {
                        item2 = Player.inventory[i];
                        flag7 = true;
                        //Main.NewText(item2.Name);
                        break;
                    }
                }
            }

            if (flag7)
            {
                if (Player.inventory[Player.selectedItem].useAmmo == ItemID.Spike)
                {
                    int t = 0;
                    int dmgAdd = 0;
                    if (item2.type == ItemID.Spike)
                    {
                        t = ModContent.ProjectileType<Projectiles.SpikeCannon>();
                        dmgAdd = 11;
                    }
                    else if (item2.type == ModContent.ItemType<Items.Placeable.Tile.DemonSpikeScale>())
                    {
                        t = ModContent.ProjectileType<Projectiles.DemonSpikeScale>();
                        dmgAdd = 17;
                    }
                    else if (item2.type == ModContent.ItemType<Items.Placeable.Tile.BloodiedSpike>())
                    {
                        t = ModContent.ProjectileType<Projectiles.BloodiedSpike>();
                        dmgAdd = 17;
                    }
                    else if (item2.type == ModContent.ItemType<Items.Placeable.Tile.NastySpike>())
                    {
                        t = ModContent.ProjectileType<Projectiles.NastySpike>();
                        dmgAdd = 18;
                    }
                    else if (item2.type == ItemID.WoodenSpike)
                    {
                        t = ModContent.ProjectileType<WoodenSpike>();
                        dmgAdd = 30;
                    }
                    else if (item2.type == ModContent.ItemType<Items.Placeable.Tile.VenomSpike>())
                    {
                        t = ModContent.ProjectileType<Projectiles.VenomSpike>();
                        dmgAdd = 39;
                    }
                    else if (item2.type == ModContent.ItemType<Items.Placeable.Tile.PoisonSpike>())
                    {
                        t = ModContent.ProjectileType<Projectiles.PoisonSpike>();
                        dmgAdd = 15;
                    }

                    if (t > 0)
                    {
                        if (item.type == ModContent.ItemType<SpikeRailgun>())
                        {
                            float num87 = MathHelper.Pi / 10;
                            int num88 = 3;
                            Vector2 vector2 = velocity;
                            vector2.Normalize();
                            vector2 *= 40f;
                            for (int num89 = 0; num89 < num88; num89++)
                            {
                                float num90 = num89 - ((num88 - 1f) / 2f);
                                Vector2 vector3 = vector2.Rotate(num87 * num90);
                                int num91 = Projectile.NewProjectile(
                                    Player.GetSource_ItemUse_WithPotentialAmmo(
                                        ModContent.GetInstance<SpikeRailgun>().Item,
                                        ModContent.GetInstance<SpikeRailgun>().Item.ammo), position.X + vector3.X,
                                    position.Y + vector3.Y, velocity.X, velocity.Y, t, damage + dmgAdd, knockback,
                                    Player.whoAmI);
                            }

                            Main.NewText(t);
                            return false;
                        }

                        Projectile.NewProjectile(
                            Player.GetSource_ItemUse_WithPotentialAmmo(
                                ModContent.GetInstance<Items.Weapons.Ranged.SpikeCannon>().Item,
                                ModContent.GetInstance<Items.Weapons.Ranged.SpikeCannon>().Item.ammo), position,
                            velocity, t, damage + dmgAdd, knockback, Player.whoAmI);
                        return false;
                    }
                }
            }
        }
        #endregion
        #region torch launcher logic
        if (item.type == ModContent.ItemType<TorchLauncher>())
        {
            var item2 = new Item();
            bool flag7 = false;
            bool inAmmoSlots = false;
            for (int i = 54; i < 58; i++)
            {
                if (Player.inventory[i].ammo == Player.inventory[Player.selectedItem].useAmmo &&
                    Player.inventory[i].stack > 0)
                {
                    item2 = Player.inventory[i];
                    flag7 = true;
                    inAmmoSlots = true;
                    break;
                }
            }

            if (!inAmmoSlots)
            {
                for (int i = 0; i < 54; i++)
                {
                    if (Player.inventory[i].ammo == Player.inventory[Player.selectedItem].useAmmo &&
                        Player.inventory[i].stack > 0)
                    {
                        item2 = Player.inventory[i];
                        flag7 = true;
                        break;
                    }
                }
            }

            if (flag7)
            {
                if (Player.inventory[Player.selectedItem].useAmmo == 8)
                {
                    int t = 0;
                    if (torches.TryGetValue(item2.type, out t))
                    {
                        Projectile.NewProjectile(
                            Player.GetSource_ItemUse_WithPotentialAmmo(ModContent.GetInstance<TorchLauncher>().Item,
                                ModContent.GetInstance<TorchLauncher>().Item.ammo), position,
                            new Vector2(velocity.X, velocity.Y), t, 0, 0);
                        return false;
                    }

                    return base.Shoot(item, source, position, velocity, type, damage, knockback);
                }
            }
        }
        #endregion
        return base.Shoot(item, source, position, velocity, type, damage, knockback);
    }

    public override bool CanConsumeAmmo(Item weapon, Item ammo)
    {
        bool consume = true;

        if (tomeItem.type == ModContent.ItemType<CreatorsTome>() && Main.rand.NextBool(4))
        {
            consume = false;
        }

        if ((tomeItem.type == ModContent.ItemType<TomeofDistance>() ||
             tomeItem.type == ModContent.ItemType<Dominance>() ||
             tomeItem.type == ModContent.ItemType<LoveUpandDown>()) && Main.rand.NextBool(5))
        {
            consume = false;
        }

        if ((tomeItem.type == ModContent.ItemType<ThePlumHarvest>() ||
             tomeItem.type == ModContent.ItemType<Emperor>()) && Main.rand.Next(10) < 3)
        {
            consume = false;
        }

        if (!consume)
        {
            return false;
        }

        return base.CanConsumeAmmo(weapon, ammo);
    }

    public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
    {
        if (ReckoningBonus && item.DamageType == DamageClass.Ranged)
        {
            reckoningHit++;
            if (reckoningHit >= 4)
            {
                Player.AddBuff(ModContent.BuffType<Reckoning>(), 60 * 5);
                reckoningHit = 0;
            }
        }
        if (terraClaws && item.DamageType == DamageClass.Melee)
        {
            switch (Main.rand.Next(5))
            {
                case 0:
                    target.AddBuff(BuffID.OnFire, 9 * 60);
                    break;
                case 1:
                    target.AddBuff(BuffID.Poisoned, 9 * 60);
                    break;
                case 2:
                    target.AddBuff(BuffID.Venom, 9 * 60);
                    break;
                case 3:
                    target.AddBuff(BuffID.Frostburn, 9 * 60);
                    break;
                case 4:
                    target.AddBuff(BuffID.Ichor, 9 * 60);
                    break;
            }
        }

        if (ancientSandVortex && Main.rand.NextBool(10))
        {
            Projectile.NewProjectile(Player.GetSource_OnHit(target), target.position, Vector2.Zero,
                ModContent.ProjectileType<AncientSandnado>(), 0, 0);
        }

        if (vampireTeeth && item.DamageType == DamageClass.Melee)
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

        if (crit)
        {
            if (Main.rand.NextBool(8) && Player.whoAmI == Main.myPlayer && reckoningTimeLeft > 0 && reckoningLevel < 10)
            {
                reckoningLevel++;
            }

            if (avalonRestoration)
            {
                Player.AddBuff(ModContent.BuffType<BlessingofAvalon>(), 120);
            }
        }
    }

    public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
    {
        if (reckoning && proj.DamageType == DamageClass.Ranged && proj.owner == Main.myPlayer)
        {
            reckoningHit++;
            if (reckoningHit >= 4)
            {
                Player.AddBuff(ModContent.BuffType<Reckoning>(), 60 * 8);
                reckoningHit = 0;
            }
        }
        if (minionFreeze)
        {
            if (proj.minion || Data.Sets.Projectile.MinionProjectiles[proj.type])
            {
                if (CanBeFrozen.CanFreeze(target))
                {
                    target.AddBuff(ModContent.BuffType<MinionFrozen>(), 60);
                }
            }
        }

        if (roseMagic && proj.DamageType == DamageClass.Magic && Main.rand.NextBool(8) && roseMagicCooldown <= 0)
        {
            int num36 = Item.NewItem(Player.GetSource_OnHit(target), (int)target.position.X,
                (int)target.position.Y, target.width, target.height, ModContent.ItemType<Rosebud>());
            Main.item[num36].velocity.Y = Main.rand.Next(-20, 1) * 0.2f;
            Main.item[num36].velocity.X = Main.rand.Next(10, 31) * 0.2f * Player.direction;
            roseMagicCooldown = 20;
        }

        if (target.life <= 0 && ancientGunslinger && proj.owner == Main.myPlayer && proj.DamageType == DamageClass.Ranged)
        {
            Projectile.NewProjectile(Player.GetSource_OnHit(target), target.position, Vector2.Zero,
                ModContent.ProjectileType<SandyExplosion>(), damage * 2, knockback);
        }

        if (crit)
        {
            if (Main.rand.NextBool(8) && Player.whoAmI == Main.myPlayer && reckoningTimeLeft > 0 && reckoningLevel < 10)
            {
                reckoningLevel++;
            }

            if (avalonRestoration)
            {
                Player.AddBuff(ModContent.BuffType<BlessingofAvalon>(), 120);
            }
        }
    }

    public override void OnHitPvp(Item item, Player target, int damage, bool crit)
    {
        if (crit)
        {
            if (Main.rand.Next(8) == 0)
            {
                if (Player.whoAmI == Main.myPlayer && reckoningTimeLeft > 0 && reckoningLevel < 10)
                {
                    reckoningLevel += 1;
                }
            }

            if (avalonRestoration)
            {
                Player.AddBuff(ModContent.BuffType<BlessingofAvalon>(), 120);
            }
        }
    }

    public override void OnHitPvpWithProj(Projectile proj, Player target, int damage, bool crit)
    {
        if (minionFreeze)
        {
            if (proj.minion || Data.Sets.Projectile.MinionProjectiles[proj.type])
            {
                target.AddBuff(ModContent.BuffType<MinionFrozen>(), 60);
            }
        }

        if (crit)
        {
            if (Main.rand.Next(8) == 0)
            {
                if (Player.whoAmI == Main.myPlayer && reckoningTimeLeft > 0 && reckoningLevel < 10)
                {
                    reckoningLevel += 1;
                }
            }

            if (avalonRestoration)
            {
                Player.AddBuff(ModContent.BuffType<BlessingofAvalon>(), 120);
            }
        }
    }

    public override void OnHitByNPC(NPC npc, int damage, bool crit)
    {
        if (stingerPack)
        {
            float shootSpeed = 6f;
            Vector2 center = Player.Center;
            SoundEngine.PlaySound(SoundID.Item17, Player.position);
            float num572 = (float)Math.Atan2(center.Y - npc.Center.Y, center.X - npc.Center.X);
            for (float f = 0f; f <= 3.6f; f += 0.4f)
            {
                int proj = Projectile.NewProjectile(npc.GetSource_FromThis(), center.X, center.Y,
                    (float)(Math.Cos(num572 + f) * shootSpeed * -1), (float)(Math.Sin(num572 + f) * shootSpeed * -1.0),
                    ProjectileID.Stinger, 60, 0f, 0);
                Main.projectile[proj].timeLeft = 600;
                Main.projectile[proj].tileCollide = false;
                Main.projectile[proj].hostile = false;
                Main.projectile[proj].friendly = true;
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.FromLiteral(""), proj);
                }

                proj = Projectile.NewProjectile(npc.GetSource_FromThis(), center.X, center.Y,
                    (float)(Math.Cos(num572 - f) * shootSpeed * -1), (float)(Math.Sin(num572 - f) * shootSpeed * -1.0),
                    ProjectileID.Stinger, 60, 0f, 0);
                Main.projectile[proj].timeLeft = 600;
                Main.projectile[proj].tileCollide = false;
                Main.projectile[proj].hostile = false;
                Main.projectile[proj].friendly = true;
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.FromLiteral(""), proj);
                }
            }
        }

        if (auraThorns && !Player.immune && !npc.dontTakeDamage)
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

        if (doubleDamage && !Player.immune && !npc.dontTakeDamage)
        {
            npc.StrikeNPC(npc.damage * 2, 2f, 1);
        }

        if (avalonRetribution && damage > 0)
        {
            npc.AddBuff(ModContent.BuffType<CurseofAvalon>(), 100);
        }
    }

    public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
    {
        if (crystalEdge)
        {
            damage += 15;
        }

        if (target.HasBuff(ModContent.BuffType<CurseofAvalon>()))
        {
            damage *= 4;
            target.DelBuff(target.FindBuffIndex(ModContent.BuffType<CurseofAvalon>()));
        }

        if (hyperMelee)
        {
            hyperBar++;
            if (hyperBar > 15 && hyperBar <= 25)
            {
                crit = true;
                if (hyperBar == 25)
                {
                    hyperBar = 0;
                }
            }
        }

        if (confusionTal && Main.rand.Next(100) <= 12)
        {
            target.AddBuff(BuffID.Confused, 540);
        }

        if (crit)
        {
            damage += MultiplyCritDamage(damage);
        }
    }

    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback,
                                              ref bool crit, ref int hitDirection)
    {
        if (crystalEdge)
        {
            damage += 15;
        }

        if (target.HasBuff(ModContent.BuffType<CurseofAvalon>()) &&
            proj.type != ProjectileID.HallowStar &&
            proj.type != ModContent.ProjectileType<Leaves>() &&
            proj.type != ModContent.ProjectileType<LightningBolt>() &&
            proj.type != ModContent.ProjectileType<LightningTrail>())
        {
            damage *= 4;
            target.DelBuff(target.FindBuffIndex(ModContent.BuffType<CurseofAvalon>()));
        }

        if (hyperMelee && proj.DamageType == DamageClass.Melee)
        {
            hyperBar++;
            if (hyperBar > 15 && hyperBar <= 25)
            {
                crit = true;
                if (hyperBar == 25)
                {
                    hyperBar = 0;
                }
            }
        }

        if (hyperRanged && proj.DamageType == DamageClass.Ranged)
        {
            hyperBar++;
            if (hyperBar > 15 && hyperBar <= 25)
            {
                crit = true;
                if (hyperBar == 25)
                {
                    hyperBar = 0;
                }
            }
        }

        if (hyperMagic && proj.DamageType == DamageClass.Magic)
        {
            hyperBar++;
            if (hyperBar > 15 && hyperBar <= 25)
            {
                crit = true;
                if (hyperBar == 25)
                {
                    hyperBar = 0;
                }
            }
        }

        if (minionFreeze)
        {
            if (proj.minion || Data.Sets.Projectile.MinionProjectiles[proj.type])
            {
                if (target.HasBuff(ModContent.BuffType<MinionFrozen>()) || !CanBeFrozen.CanFreeze(target))
                {
                    damage = (int)(damage * 1.10f);
                }
            }
        }

        if (crit)
        {
            damage += MultiplyCritDamage(damage);
        }
    }

    public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit)
    {
        if (crit)
        {
            damage += MultiplyCritDamage(damage);
        }
    }

    public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit)
    {
        if (minionFreeze)
        {
            if (proj.minion || Data.Sets.Projectile.MinionProjectiles[proj.type])
            {
                if (target.HasBuff(ModContent.BuffType<MinionFrozen>()))
                {
                    damage = (int)(damage * 1.10f);
                }
            }
        }

        if (crit)
        {
            damage += MultiplyCritDamage(damage);
        }
    }

    public override void SaveData(TagCompound tag) => tag["CrystalHealth"] = CrystalHealth;

    // tag = new TagCompound
    // {
    //     { "AvalonTesting:TomeSlot", ItemIO.Save(tomeItem) },
    //     { "AvalonTesting:CrystalHealth", CrystalHealth },
    //     { "AvalonTesting:Stamina", Player.GetModPlayer<ExxoStaminaPlayer>().StatStamMax},
    //     { "AvalonTesting:SHMAcc", shmAcc },
    //     { "AvalonTesting:HerbTier", (int)herbTier },
    //     { "AvalonTesting:HerbTotal", herbTotal },
    //     { "AvalonTesting:PotionTotal", potionTotal },
    //     { "AvalonTesting:HerbCounts", herbCounts.Save() },
    //     { "AvalonTesting:SpiritPoppyUseCount", spiritPoppyUseCount },
    //     { "AvalonTesting:RocketJumpUnlocked", Player.GetModPlayer<ExxoStaminaPlayer>().RocketJumpUnlocked },
    //     { "AvalonTesting:TeleportUnlocked", Player.GetModPlayer<ExxoStaminaPlayer>().TeleportUnlocked},
    //     { "AvalonTesting:SwimmingUnlocked", Player.GetModPlayer<ExxoStaminaPlayer>().SwimmingUnlocked },
    //     { "AvalonTesting:SprintUnlocked", Player.GetModPlayer<ExxoStaminaPlayer>().SprintUnlocked },
    //     { "AvalonTesting:FlightRestoreUnlocked", Player.GetModPlayer<ExxoStaminaPlayer>().FlightRestoreUnlocked },
    // };
    public override void LoadData(TagCompound tag)
    {
        if (tag.ContainsKey("CrystalHealth"))
        {
            CrystalHealth = tag.Get<int>("CrystalHealth");
            Player.statLifeMax += CrystalHealth * 25;
            Player.statLifeMax2 += CrystalHealth * 25;
            Player.statLife += CrystalHealth * 25;
        }
        // if (tag.ContainsKey("AvalonTesting:TomeSlot"))
        // {
        //     tomeItem = ItemIO.Load(tag.Get<TagCompound>("AvalonTesting:TomeSlot"));
        // }
        // if (tag.ContainsKey("AvalonTesting:CrystalHealth"))
        // {
        //     CrystalHealth = tag.GetAsInt("AvalonTesting:CrystalHealth");
        //     if (CrystalHealth > 4)
        //     {
        //         CrystalHealth = 4;
        //     }
        //
        //     if (Player.statLifeMax == 500)
        //     {
        //         Player.statLifeMax += CrystalHealth *= 25;
        //         Player.statLifeMax2 += CrystalHealth *= 25;
        //     }
        // }
        //
        // if (tag.ContainsKey("AvalonTesting:Stamina"))
        // {
        //     Player.GetModPlayer<ExxoStaminaPlayer>().StatStamMax = tag.GetAsInt("AvalonTesting:Stamina");
        // }
        // if (tag.ContainsKey("AvalonTesting:SHMAcc"))
        // {
        //     shmAcc = tag.Get<bool>("AvalonTesting:SHMAcc");
        // }
        // if (tag.ContainsKey("AvalonTesting:HerbTier"))
        // {
        //     herbTier = (HerbTier)tag.GetAsInt("AvalonTesting:HerbTier");
        // }
        // if (tag.ContainsKey("AvalonTesting:HerbTotal"))
        // {
        //     herbTotal = tag.GetAsInt("AvalonTesting:HerbTotal");
        // }
        // if (tag.ContainsKey("AvalonTesting:PotionTotal"))
        // {
        //     potionTotal = tag.GetAsInt("AvalonTesting:PotionTotal");
        // }
        // if (tag.ContainsKey("AvalonTesting:HerbCounts"))
        // {
        //     try
        //     {
        //         herbCounts.Load(tag.Get<TagCompound>("AvalonTesting:HerbCounts"));
        //     }
        //     catch
        //     {
        //         herbCounts = new Dictionary<int, int>();
        //     }
        // }
        // if (tag.ContainsKey("AvalonTesting:SpiritPoppyUseCount"))
        // {
        //     spiritPoppyUseCount = tag.Get<int>("AvalonTesting:SpiritPoppyUseCount");
        // }
        // if (tag.ContainsKey("AvalonTesting:RocketJumpUnlocked"))
        // {
        //     Player.GetModPlayer<ExxoStaminaPlayer>().RocketJumpUnlocked = tag.Get<bool>("AvalonTesting:RocketJumpUnlocked");
        // }
        // if (tag.ContainsKey("AvalonTesting:TeleportUnlocked"))
        // {
        //     Player.GetModPlayer<ExxoStaminaPlayer>().TeleportUnlocked = tag.Get<bool>("AvalonTesting:TeleportUnlocked");
        // }
        // if (tag.ContainsKey("AvalonTesting:SwimmingUnlocked"))
        // {
        //     Player.GetModPlayer<ExxoStaminaPlayer>().SwimmingUnlocked = tag.Get<bool>("AvalonTesting:SwimmingUnlocked");
        // }
        // if (tag.ContainsKey("AvalonTesting:SprintUnlocked"))
        // {
        //     Player.GetModPlayer<ExxoStaminaPlayer>().SprintUnlocked = tag.Get<bool>("AvalonTesting:SprintUnlocked");
        // }
        // if (tag.ContainsKey("AvalonTesting:FlightRestoreUnlocked"))
        // {
        //     Player.GetModPlayer<ExxoStaminaPlayer>().FlightRestoreUnlocked = tag.Get<bool>("AvalonTesting:FlightRestoreUnlocked");
        // }
    }

    public override void PostUpdate()
    {
        //Main.worldRate = 7;
        #region player sensor
        int pposX = (int)(Player.position.X / 16);
        int pposY = (int)(Player.position.Y / 16);
        int pposXOld = (int)(Player.oldPosition.X / 16);
        int pposYOld = (int)(Player.oldPosition.Y / 16);
        // x, y
        if (Main.tile[pposX, pposY].TileType == ModContent.TileType<PlayerSensor>())
        {
            if (!pSensor[0])
            {
                SoundEngine.PlaySound(SoundID.Mech, new Vector2(pposX * 16, pposY * 16));
                Wiring.TripWire(pposX, pposY, 1, 1);
                pSensor[0] = true;
            }
        }
        else
        {
            pSensor[0] = false;
        }

        // x + 1, y
        if (Main.tile[pposX + 1, pposY].TileType == ModContent.TileType<PlayerSensor>())
        {
            if (!pSensor[1])
            {
                SoundEngine.PlaySound(SoundID.Mech, new Vector2((pposX + 1) * 16, pposY * 16));
                Wiring.TripWire(pposX + 1, pposY, 1, 1);
                pSensor[1] = true;
            }
        }
        else
        {
            pSensor[1] = false;
        }

        // x, y + 1
        if (Main.tile[pposX, pposY + 1].TileType == ModContent.TileType<PlayerSensor>())
        {
            if (!pSensor[2])
            {
                SoundEngine.PlaySound(SoundID.Mech, new Vector2(pposX * 16, (pposY + 1) * 16));
                Wiring.TripWire(pposX, pposY + 1, 1, 1);
                pSensor[2] = true;
            }
        }
        else
        {
            pSensor[2] = false;
        }

        // x + 1, y + 1
        if (Main.tile[pposX + 1, pposY + 1].TileType == ModContent.TileType<PlayerSensor>())
        {
            if (!pSensor[3])
            {
                SoundEngine.PlaySound(SoundID.Mech, new Vector2((pposX + 1) * 16, (pposY + 1) * 16));
                Wiring.TripWire(pposX + 1, pposY + 1, 1, 1);
                pSensor[3] = true;
            }
        }
        else
        {
            pSensor[3] = false;
        }

        // x, y + 2
        if (Main.tile[pposX, pposY + 2].TileType == ModContent.TileType<PlayerSensor>())
        {
            if (!pSensor[4])
            {
                SoundEngine.PlaySound(SoundID.Mech, new Vector2(pposX * 16, (pposY + 2) * 16));
                Wiring.TripWire(pposX, pposY + 2, 1, 1);
                pSensor[4] = true;
            }
        }
        else
        {
            pSensor[4] = false;
        }

        // x + 1, y + 1
        if (Main.tile[pposX + 1, pposY + 2].TileType == ModContent.TileType<PlayerSensor>())
        {
            if (!pSensor[5])
            {
                SoundEngine.PlaySound(SoundID.Mech, new Vector2((pposX + 1) * 16, (pposY + 2) * 16));
                Wiring.TripWire(pposX + 1, pposY + 2, 1, 1);
                pSensor[5] = true;
            }
        }
        else
        {
            pSensor[5] = false;
        }
        #endregion
        if (ReckoningBonus)
        {
            Player.GetCritChance(DamageClass.Ranged) += reckoningLevel * 3;
        }
        if (Player.GetModPlayer<ExxoBuffPlayer>().SkyBlessing)
        {
            switch (Player.GetModPlayer<ExxoBuffPlayer>().SkyStacks)
            {
                case 1:
                    Player.GetDamage(DamageClass.Summon) += 0.02f;
                    break;
                case 2:
                    Player.GetDamage(DamageClass.Summon) += 0.04f;
                    break;
                case 3:
                    Player.GetDamage(DamageClass.Summon) += 0.06f;
                    break;
                case 4:
                    Player.GetDamage(DamageClass.Summon) += 0.08f;
                    break;
                case 5:
                    Player.GetDamage(DamageClass.Summon) += 0.1f;
                    break;
                case 6:
                    Player.GetDamage(DamageClass.Summon) += 0.12f;
                    break;
                case 7:
                    Player.GetDamage(DamageClass.Summon) += 0.14f;
                    break;
                case 8:
                    Player.GetDamage(DamageClass.Summon) += 0.16f;
                    break;
                case 9:
                    Player.GetDamage(DamageClass.Summon) += 0.18f;
                    break;
                case 10:
                    Player.GetDamage(DamageClass.Summon) += 0.25f;
                    break;
            }
            //if (Player.GetModPlayer<ExxoBuffPlayer>().SkyStacks < 10)
            //{
            //    Player.GetDamage(DamageClass.Summon) += (float)(Player.GetModPlayer<ExxoBuffPlayer>().SkyStacks * 2) / 100;
            //}
            //else Player.GetDamage(DamageClass.Summon) += 0.25f;
        }
        if (screenShakeTimer == 1)
        {
            SoundEngine.PlaySound(new SoundStyle($"{nameof(AvalonTesting)}/Sounds/Item/Stomp"), Player.position);
        }

        if (screenShakeTimer > 0)
        {
            screenShakeTimer--;
        }

        Vector2 pposTile = Player.Center / 16;
        for (int xpos = (int)pposTile.X - 4; xpos <= (int)pposTile.X + 4; xpos++)
        {
            for (int ypos = (int)pposTile.Y - 4; ypos <= (int)pposTile.Y + 4; ypos++)
            {
                if (Main.tile[xpos, ypos].TileType == (ushort)ModContent.TileType<TritanoriumOre>() ||
                    Main.tile[xpos, ypos].TileType == (ushort)ModContent.TileType<PyroscoricOre>())
                {
                    if (!luckTome && !blahWings)
                    {
                        Player.AddBuff(ModContent.BuffType<Melting>(), 60);
                    }
                }
            }
        }

        if (ZoneFlight)
        {
            Player.slowFall = true; // doesn't work
        }

        if (ZoneFright)
        {
            Player.statDefense += 5;
        }

        if (ZoneIceSoul)
        {
            slimeBand = true; // doesn't work
        }

        if (ZoneMight)
        {
            Player.GetDamage(DamageClass.Generic) += 0.06f;
        }

        if (ZoneNight)
        {
            Player.wolfAcc = true;
        }

        if (ZoneTime)
        {
            Player.accWatch = 3;
        }

        if (ZoneTorture)
        {
            Player.GetCritChance(DamageClass.Generic) += 6;
        }

        if (ZoneSight)
        {
            Player.detectCreature = Player.dangerSense = Player.nightVision = true;
        }

        if (ZoneDelight)
        {
            Player.lifeRegen += 3;
        }

        if (ZoneHumidity)
        {
            Player.resistCold = true;
        }

        if (ZoneBlight)
        {
            Player.GetArmorPenetration(DamageClass.Generic) += 10;
        }

        #region rift goggles
        /*if (Player.ZoneCrimson || Player.ZoneCorrupt || Player.GetModPlayer<ExxoBiomePlayer>().ZoneContagion)
        {
            if (Main.rand.Next(3000) == 0 && riftGoggles)
            {
                Vector2 pposTile2 = Player.position + new Vector2(Main.rand.Next(-20 * 16, 21 * 16), Main.rand.Next(-20 * 16, 21 * 16));
                Point pt = pposTile2.ToTileCoordinates();
                if (!Main.tile[pt.X, pt.Y].HasTile)
                {
                    int proj = NPC.NewNPC(Player.GetSource_TileInteraction(pt.X, pt.Y), pt.X * 16, pt.Y * 16, ModContent.NPCType<NPCs.Rift>(), 0);
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, proj);
                    }

                    for (int i = 0; i < 20; i++)
                    {
                        int num893 = Dust.NewDust(Main.npc[proj].position, Main.npc[proj].width, Main.npc[proj].height, DustID.Enchanted_Pink, 0f, 0f, 0, default, 1f);
                        Main.dust[num893].velocity *= 2f;
                        Main.dust[num893].scale = 0.9f;
                        Main.dust[num893].noGravity = true;
                        Main.dust[num893].fadeIn = 3f;
                    }
                }
            }
        }
        if (riftGoggles && Main.rand.Next(5000) == 0)
        {
            if (Player.ZoneRockLayerHeight)
            {
                Vector2 pposTile2 = Player.position + new Vector2(Main.rand.Next(-35 * 16, 35 * 16), Main.rand.Next(-35 * 16, 35 * 16));
                Point pt = pposTile2.ToTileCoordinates();
                int proj = NPC.NewNPC(Player.GetSource_TileInteraction(pt.X, pt.Y), pt.X * 16, pt.Y * 16, ModContent.NPCType<NPCs.Rift>(), ai1: 1);
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, proj);
                }

                for (int i = 0; i < 20; i++)
                {
                    int num893 = Dust.NewDust(Main.npc[proj].position, Main.npc[proj].width, Main.npc[proj].height, DustID.Enchanted_Pink, 0f, 0f, 0, default, 1f);
                    Main.dust[num893].velocity *= 2f;
                    Main.dust[num893].scale = 0.9f;
                    Main.dust[num893].noGravity = true;
                    Main.dust[num893].fadeIn = 3f;
                }
            }
        }*/
        #endregion rift goggles

        // Herbology bench distance check
        if (herb)
        {
            int num9 = (int)((Player.position.X + (Player.width * 0.5)) / 16.0);
            int num10 = (int)((Player.position.Y + (Player.height * 0.5)) / 16.0);
            if (num9 < herbX - Player.tileRangeX || num9 > herbX + Player.tileRangeX + 1 ||
                num10 < herbY - Player.tileRangeY || num10 > herbY + Player.tileRangeY + 1)
            {
                SoundEngine.PlaySound(SoundID.MenuClose);
                Player.Avalon().herb = false;
                Player.dropItemCheck();
            }
        }

        if (!Main.playerInventory)
        {
            Player.Avalon().herb = false;
        }

        slimeImmune = false;
        if (Player.tongued)
        {
            bool flag21 = false;
            if (AvalonTestingWorld.WallOfSteel >= 0)
            {
                float num159 = Main.npc[AvalonTestingWorld.WallOfSteel].position.X +
                               (Main.npc[AvalonTestingWorld.WallOfSteel].width / 2);
                num159 += Main.npc[AvalonTestingWorld.WallOfSteel].direction * 200;
                float num160 = Main.npc[AvalonTestingWorld.WallOfSteel].position.Y +
                               (Main.npc[AvalonTestingWorld.WallOfSteel].height / 2);
                var vector5 = new Vector2(Player.position.X + (Player.width * 0.5f),
                    Player.position.Y + (Player.height * 0.5f));
                float num161 = num159 - vector5.X;
                float num162 = num160 - vector5.Y;
                float num163 = (float)Math.Sqrt((num161 * num161) + (num162 * num162));
                float num164 = 11f;
                float num165;
                if (num163 > num164)
                {
                    num165 = num164 / num163;
                }
                else
                {
                    num165 = 1f;
                    flag21 = true;
                }

                num161 *= num165;
                num162 *= num165;
                Player.velocity.X = num161;
                Player.velocity.Y = num162;
                Player.position += Player.velocity;
            }

            if (flag21 && Main.myPlayer == Player.whoAmI)
            {
                for (int num166 = 0; num166 < 22; num166++)
                {
                    if (Player.buffType[num166] == 38)
                    {
                        Player.DelBuff(num166);
                    }
                }
            }
        }

        // Large gem inventory checking
        Player.gemCount = 0;
        gemCount++;
        if (gemCount >= 10)
        {
            Player.gem = -1;
            ownedLargeGems = new bool[10];
            gemCount = 0;
            for (int num27 = 0; num27 <= 58; num27++)
            {
                if (Player.inventory[num27].type == ItemID.None || Player.inventory[num27].stack == 0)
                {
                    Player.inventory[num27].TurnToAir();
                }

                // Vanilla gems
                if (Player.inventory[num27].type >= ItemID.LargeAmethyst &&
                    Player.inventory[num27].type <= ItemID.LargeDiamond)
                {
                    Player.gem = Player.inventory[num27].type - 1522;
                    ownedLargeGems[Player.gem] = true;
                }
                else if (Player.inventory[num27].type == ItemID.LargeAmber)
                {
                    Player.gem = 6;
                    ownedLargeGems[Player.gem] = true;
                }
                // Modded gems
                else if (Player.inventory[num27].type == ModContent.ItemType<LargeZircon>())
                {
                    Player.gem = 7;
                    ownedLargeGems[Player.gem] = true;
                }
                else if (Player.inventory[num27].type == ModContent.ItemType<LargeTourmaline>())
                {
                    Player.gem = 8;
                    ownedLargeGems[Player.gem] = true;
                }
                else if (Player.inventory[num27].type == ModContent.ItemType<LargePeridot>())
                {
                    Player.gem = 9;
                    ownedLargeGems[Player.gem] = true;
                }
            }
        }

        if (necroticAura)
        {
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.townNPC)
                {
                    continue;
                }

                if (!npc.active ||
                    npc.dontTakeDamage ||
                    npc.friendly ||
                    npc.life < 1)
                {
                    continue;
                }

                if (npc.Center.X >= Player.Center.X - 320 &&
                    npc.Center.X <= Player.Center.X + 320 &&
                    npc.Center.Y >= Player.Center.Y - 320 &&
                    npc.Center.Y <= Player.Center.Y + 320)
                {
                    int count = 0;
                    if (count++ % 50 == 0)
                    {
                        foreach (NPC target in Main.npc)
                        {
                            if (target.Center.X >= Player.Center.X - 320 &&
                                target.Center.X <= Player.Center.X + 320 &&
                                target.Center.Y >= Player.Center.Y - 320 &&
                                target.Center.Y <= Player.Center.Y + 320)
                            {
                                if (!target.active ||
                                    target.dontTakeDamage ||
                                    target.townNPC ||
                                    target.life < 1 ||
                                    target.boss ||
                                    //target.type == ModContent.NPCType<NPCs.Juggernaut>() ||
                                    target.realLife >= 0)
                                {
                                    continue;
                                }

                                target.AddBuff(ModContent.BuffType<NecroticDrain>(), 2);
                            }
                        }

                        count = 0;
                    }
                }
            }
        }

        //if (reckoning)
        //{
        //    if (Player.whoAmI == Main.myPlayer)
        //    {
        //        if (reckoningTimeLeft > 0)
        //        {
        //            reckoningTimeLeft--;
        //        }
        //        else
        //        {
        //            if (reckoningLevel > 1)
        //            {
        //                reckoningLevel--;
        //            }

        //            reckoningTimeLeft = 120;
        //        }

        //        if (reckoningLevel < 1)
        //        {
        //            reckoningLevel = 1;
        //        }

        //        Player.GetCritChance(DamageClass.Ranged) += 3 * reckoningLevel;
        //    }
        //    else
        //    {
        //        Main.NewText("Good job dummy, you broke the Reckoning set bonus");
        //    }
        //}
        //else
        //{
        //    reckoningLevel = 0;
        //    reckoningTimeLeft = 0;
        //}
    }

    public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
    {
        if (Player.HasItemInArmor(ModContent.ItemType<ShadowRing>()))
        {
            drawInfo.shadow = 0f;
        }

        if (blahArmor)
        {
            drawInfo.shadow = 0f;
        }

        if (spectrumBlur)
        {
            Player.eocDash = 1;
        }
    }

    public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit,
                                 ref bool customDamage, ref bool playSound, ref bool genGore,
                                 ref PlayerDeathReason damageSource) =>
        //if (AvalonTesting.GodMode)
        //{
        //    return false;
        //}
        true;

    public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
    {
        if (Main.myPlayer == Player.whoAmI)
        {
            Player.trashItem.SetDefaults();
            if (Player.difficulty == 0)
            {
                for (int i = 0; i < 59; i++)
                {
                    if (Player.inventory[i].stack > 0 &&
                        (Player.inventory[i].type == ModContent.ItemType<LargeZircon>() ||
                         Player.inventory[i].type == ModContent.ItemType<LargeTourmaline>() ||
                         Player.inventory[i].type == ModContent.ItemType<LargePeridot>()))
                    {
                        int num = Item.NewItem(Player.GetSource_Death(), (int)Player.position.X, (int)Player.position.Y,
                            Player.width, Player.height, Player.inventory[i].type);
                        Main.item[num].netDefaults(Player.inventory[i].netID);
                        Main.item[num].Prefix(Player.inventory[i].prefix);
                        Main.item[num].stack = Player.inventory[i].stack;
                        Main.item[num].velocity.Y = Main.rand.Next(-20, 1) * 0.2f;
                        Main.item[num].velocity.X = Main.rand.Next(-20, 21) * 0.2f;
                        Main.item[num].noGrabDelay = 100;
                        Main.item[num].favorited = false;
                        Main.item[num].newAndShiny = false;
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, num);
                        }

                        Player.inventory[i].SetDefaults();
                    }
                }
            }
        }
    }

    public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
    {
        if (Player.whoAmI == Main.myPlayer && incDef)
        {
            int time = 300;
            if (cOmega)
            {
                time = 600;
            }

            Player.AddBuff(BuffID.Ironskin, time);
        }

        if (Player.whoAmI == Main.myPlayer && regenStrike)
        {
            int hpHealed = 5;
            if (pOmega)
            {
                hpHealed = 10;
            }

            Player.statLife += hpHealed;
            Player.HealEffect(hpHealed);
        }
    }

    public override void PostUpdateMiscEffects()
    {
        Player.statManaMax2 = actualStatManaMax2;
        DashMovement();
        DoubleJumps();
        if (noSticky)
        {
            Player.sticky = false;
        }

        if (Player.HasItem(ModContent.ItemType<SonicScrewdriverMkI>()))
        {
            Player.findTreasure = Player.detectCreature = true;
        }

        if (Player.HasItem(ModContent.ItemType<SonicScrewdriverMkII>()))
        {
            Player.findTreasure = Player.detectCreature = true;
            Player.accWatch = 3;
            Player.accDepthMeter = 1;
            Player.accCompass = 1;
        }

        if (Player.HasItem(ModContent.ItemType<SonicScrewdriverMkIII>()))
        {
            Player.findTreasure = Player.detectCreature = Player.dangerSense = openLocks = true;
            Player.accWatch = 3;
            Player.accDepthMeter = 1;
            Player.accCompass = 1;
        }

        if (bloodCast)
        {
            Player.statManaMax2 += Player.statLifeMax2;
        }

        if (dragonsBondage)
        {
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<DragonBall>()] == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    int newBall = Projectile.NewProjectile(
                        Player.GetSource_Accessory(new Item(ModContent.ItemType<DragonsBondage>())), Player.Center,
                        Vector2.Zero, ModContent.ProjectileType<DragonBall>(), Player.HeldItem.damage / 2 * 3, 1f,
                        Player.whoAmI);
                    Main.projectile[newBall].localAI[0] = i;
                }
            }
        }
        else
        {
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<DragonBall>()] != 0)
            {
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    if (Main.projectile[i].type == ModContent.ProjectileType<DragonBall>() &&
                        Main.projectile[i].owner == Player.whoAmI)
                    {
                        Main.projectile[i].Kill();
                    }
                }
            }
        }

        if (spectrumSpeed)
        {
            float damagePercent;
            float maxSpeed;

            if (noSticky)
            {
                maxSpeed = 10f;
            }
            else
            {
                maxSpeed = Player.maxRunSpeed;
            }

            damagePercent = (-25f * (Math.Abs(Player.velocity.X) / maxSpeed)) + 25f;

            if (damagePercent < 0)
            {
                damagePercent = 0;
            }

            if (Math.Abs(Player.velocity.X) >= maxSpeed)
            {
                Player.AddBuff(ModContent.BuffType<SpectrumBlur>(), 5);
            }

            Player.GetDamage(DamageClass.Ranged) += damagePercent / 100f;
        }

        if (roseMagic)
        {
            if (roseMagicCooldown > 0)
            {
                roseMagicCooldown--;
            }
            else
            {
                roseMagicCooldown = 0;
            }
        }

        // Broken completely. If you wanna fix be my guest.
        /*
        if (ancientGunslinger)
        {
            if (player.controlUseItem && !oldLeftClick) // acts as justPressed
            {
                baseUseTime = player.HeldItem.useTime;
                baseUseAnim = player.HeldItem.useAnimation;
            }
            oldLeftClick = player.controlUseItem;
            if (player.controlUseItem && baseUseTime != -1)
            {
                if (player.HeldItem.ranged)
                {
                    ancientGunslingerTimer++;

                    ancientGunslingerStatAdd = ancientGunslingerTimer / 30;

                    if (ancientGunslingerStatAdd > player.HeldItem.useTime - 5)
                        ancientGunslingerStatAdd = player.HeldItem.useTime - 5;

                    if (ancientGunslingerTimer > 600)
                        ancientGunslingerTimer = 600;
                }
            }
            if (!player.controlUseItem)
            {
                player.HeldItem.useTime = baseUseTime;
                player.HeldItem.useAnimation = baseUseAnim;
                baseUseTime = -1;
                baseUseAnim = -1;
                ancientGunslingerStatAdd = 0;
                ancientGunslingerTimer = 0;
            }

            player.HeldItem.useTime -= ancientGunslingerStatAdd;
            player.HeldItem.useAnimation -= ancientGunslingerStatAdd;
        }
        else
        {
            player.HeldItem.useTime = baseUseTime;
            player.HeldItem.useAnimation += baseUseAnim;
            baseUseTime = -1;
            baseUseAnim = -1;
            ancientGunslingerStatAdd = 0;
            ancientGunslingerTimer = 0;
        }
        Main.NewText(player.HeldItem.useTime);
        Main.NewText(player.HeldItem.useAnimation);
        */
        UpdateMana();
    }

    public override void PreUpdate()
    {
        WOSTongue();
        tpStam = !teleportV;
        if (teleportV)
        {
            teleportV = false;
            teleportVWasTriggered = true;
        }

        Player.breathMax = 200;
        if (ZoneFlight)
        {
            Player.slowFall = true;
        }

        // Large gem trashing logic
        if (Player.whoAmI == Main.myPlayer)
        {
            if (
                Player.trashItem.type == ModContent.ItemType<LargeZircon>() ||
                Player.trashItem.type == ModContent.ItemType<LargeTourmaline>() ||
                Player.trashItem.type == ModContent.ItemType<LargePeridot>()
            )
            {
                Player.trashItem.SetDefaults();
            }
        }
    }

    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        if (KeybindSystem.QuickStaminaHotkey.JustPressed)
        {
            Player.GetModPlayer<ExxoStaminaPlayer>().QuickStamina();
        }

        if (KeybindSystem.FlightTimeRestoreHotkey.JustPressed && Player.wingsLogic > 0 && Player.wingTime == 0 &&
            Player.GetModPlayer<ExxoStaminaPlayer>().FlightRestoreUnlocked &&
            Player.GetModPlayer<ExxoStaminaPlayer>().FlightRestoreCooldown >= 60 * 60)
        {
            int amt = 150;
            if (Player.GetModPlayer<ExxoStaminaPlayer>().StaminaDrain)
            {
                amt *= (int)(Player.GetModPlayer<ExxoStaminaPlayer>().StaminaDrainStacks *
                             Player.GetModPlayer<ExxoStaminaPlayer>().StaminaDrainMult);
            }

            if (Player.GetModPlayer<ExxoStaminaPlayer>().StatStam >= amt)
            {
                Player.GetModPlayer<ExxoStaminaPlayer>().StatStam -= amt;
                Player.GetModPlayer<ExxoStaminaPlayer>().FlightRestoreCooldown = 0;
                Player.wingTime = Player.wingTimeMax;
            }
            else if (Player.GetModPlayer<ExxoStaminaPlayer>().StamFlower)
            {
                Player.GetModPlayer<ExxoStaminaPlayer>().QuickStamina(amt);
                if (Player.GetModPlayer<ExxoStaminaPlayer>().StatStam >= amt)
                {
                    Player.GetModPlayer<ExxoStaminaPlayer>().StatStam -= amt;
                    Player.GetModPlayer<ExxoStaminaPlayer>().FlightRestoreCooldown = 0;
                    Player.wingTime = Player.wingTimeMax;
                }
            }
        }

        if (KeybindSystem.ShadowHotkey.JustPressed && tpStam && tpCD >= 300 &&
            Player.GetModPlayer<ExxoStaminaPlayer>().TeleportUnlocked)
        {
            int amt = 90;
            if (Player.GetModPlayer<ExxoStaminaPlayer>().StaminaDrain)
            {
                amt *= (int)(Player.GetModPlayer<ExxoStaminaPlayer>().StaminaDrainStacks *
                             Player.GetModPlayer<ExxoStaminaPlayer>().StaminaDrainMult);
            }

            if (Player.GetModPlayer<ExxoStaminaPlayer>().StatStam >= amt)
            {
                Player.GetModPlayer<ExxoStaminaPlayer>().StatStam -= amt;
                tpCD = 0;
                if (Main.tile[(int)(Main.mouseX + Main.screenPosition.X) / 16,
                        (int)(Main.mouseY + Main.screenPosition.Y) / 16].WallType !=
                    ModContent.WallType<ImperviousBrickWallUnsafe>() &&
                    Main.tile[(int)(Main.mouseX + Main.screenPosition.X) / 16,
                        (int)(Main.mouseY + Main.screenPosition.Y) / 16].WallType != WallID.LihzahrdBrickUnsafe)
                {
                    for (int m = 0; m < 70; m++)
                    {
                        Dust.NewDust(Player.position, Player.width, Player.height, DustID.MagicMirror,
                            Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 150, default, 1.1f);
                    }

                    Player.position.X = Main.mouseX + Main.screenPosition.X;
                    Player.position.Y = Main.mouseY + Main.screenPosition.Y;
                    for (int n = 0; n < 70; n++)
                    {
                        Dust.NewDust(Player.position, Player.width, Player.height, DustID.MagicMirror, 0f, 0f, 150,
                            default, 1.1f);
                    }
                }
            }
            else if (Player.GetModPlayer<ExxoStaminaPlayer>().StamFlower)
            {
                Player.GetModPlayer<ExxoStaminaPlayer>().QuickStamina(amt);
                if (Player.GetModPlayer<ExxoStaminaPlayer>().StatStam >= amt)
                {
                    Player.GetModPlayer<ExxoStaminaPlayer>().StatStam -= amt;
                    tpCD = 0;
                    if (Main.tile[(int)(Main.mouseX + Main.screenPosition.X) / 16,
                            (int)(Main.mouseY + Main.screenPosition.Y) / 16].WallType !=
                        ModContent.WallType<ImperviousBrickWallUnsafe>() &&
                        Main.tile[(int)(Main.mouseX + Main.screenPosition.X) / 16,
                            (int)(Main.mouseY + Main.screenPosition.Y) / 16].WallType != WallID.LihzahrdBrickUnsafe &&
                        !Main.wallDungeon[
                            Main.tile[(int)(Main.mouseX + Main.screenPosition.X) / 16,
                                (int)(Main.mouseY + Main.screenPosition.Y) / 16].WallType])
                    {
                        for (int m = 0; m < 70; m++)
                        {
                            Dust.NewDust(Player.position, Player.width, Player.height, DustID.MagicMirror,
                                Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 150, default, 1.1f);
                        }

                        Player.position.X = Main.mouseX + Main.screenPosition.X;
                        Player.position.Y = Main.mouseY + Main.screenPosition.Y;
                        for (int n = 0; n < 70; n++)
                        {
                            Dust.NewDust(Player.position, Player.width, Player.height, DustID.MagicMirror, 0f, 0f, 150,
                                default, 1.1f);
                        }
                    }
                }
            }
        }
        else if (KeybindSystem.ShadowHotkey.JustPressed && (teleportV || teleportVWasTriggered) && tpCD >= 300)
        {
            teleportVWasTriggered = false;
            tpCD = 0;
            if (Main.tile[(int)(Main.mouseX + Main.screenPosition.X) / 16,
                    (int)(Main.mouseY + Main.screenPosition.Y) / 16].WallType !=
                ModContent.WallType<ImperviousBrickWallUnsafe>() &&
                Main.tile[(int)(Main.mouseX + Main.screenPosition.X) / 16,
                    (int)(Main.mouseY + Main.screenPosition.Y) / 16].WallType != WallID.LihzahrdBrickUnsafe)
            {
                for (int m = 0; m < 70; m++)
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, DustID.MagicMirror,
                        Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 150, default, 1.1f);
                }

                Player.position.X = Main.mouseX + Main.screenPosition.X;
                Player.position.Y = Main.mouseY + Main.screenPosition.Y;
                for (int n = 0; n < 70; n++)
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, DustID.MagicMirror, 0f, 0f, 150, default,
                        1.1f);
                }
            }
        }

        #region other hotkeys
        if (KeybindSystem.RocketJumpHotkey.JustPressed && Player.GetModPlayer<ExxoStaminaPlayer>().RocketJumpUnlocked)
        {
            activateRocketJump = !activateRocketJump;
            Main.NewText(!activateRocketJump ? "Rocket Jump Off" : "Rocket Jump On");
        }

        if (KeybindSystem.SprintHotkey.JustPressed && Player.GetModPlayer<ExxoStaminaPlayer>().SprintUnlocked)
        {
            activateSprint = !activateSprint;
            Main.NewText(!activateSprint ? "Sprinting Off" : "Sprinting On");
        }

        if (KeybindSystem.DashHotkey.JustPressed)
        {
            stamDashKey = !stamDashKey;
            Main.NewText(!stamDashKey ? "Dashing Off" : "Dashing On");
        }

        if (KeybindSystem.QuintupleHotkey.JustPressed)
        {
            quintJump = !quintJump;
            Main.NewText(!quintJump ? "Quintuple Jump Off" : "Quintuple Jump On");
        }

        if (KeybindSystem.SwimHotkey.JustPressed && Player.GetModPlayer<ExxoStaminaPlayer>().SwimmingUnlocked)
        {
            activateSwim = !activateSwim;
            Main.NewText(!activateSwim ? "Swimming Off" : "Swimming On");
        }

        if (KeybindSystem.WallSlideHotkey.JustPressed)
        {
            activateSlide = !activateSlide;
            Main.NewText(!activateSlide ? "Wall Sliding Off" : "Wall Sliding On");
        }

        if (KeybindSystem.BubbleBoostHotkey.JustPressed)
        {
            activateBubble = !activateBubble;
            Main.NewText(!activateBubble ? "Bubble Boost Off" : "Bubble Boost On");
        }
        #endregion
        if (Player.inventory[Player.selectedItem].type == ModContent.ItemType<Items.Tools.AccelerationDrill>() &&
            KeybindSystem.ModeChangeHotkey.JustPressed)
        {
            speed = !speed;
            if (!speed)
            {
                Main.NewText("Acceleration Drill Mode: Normal");
            }
            else
            {
                Main.NewText("Acceleration Drill Mode: Speed");
            }
        }

        if (Main.netMode != NetmodeID.SinglePlayer &&
            Player.inventory[Player.selectedItem].type == ModContent.ItemType<EideticMirror>() &&
            KeybindSystem.ModeChangeHotkey.JustPressed)
        {
            int newPlayer = teleportToPlayer;
            int numPlayersCounted = 0;
            while (true)
            {
                newPlayer++;
                if (newPlayer >= 255)
                {
                    newPlayer -= 255;
                }

                if (Main.player[newPlayer].active && Player.whoAmI != newPlayer && !Main.player[newPlayer].dead &&
                    Main.player[Main.myPlayer].team > 0 &&
                    Main.player[Main.myPlayer].team == Main.player[newPlayer].team)
                {
                    Main.NewText("Teleporting to " + Main.player[newPlayer].name + " ready.", 250, 250, 0);
                    teleportToPlayer = newPlayer;
                    break;
                }

                numPlayersCounted++;
                if (numPlayersCounted >= 255)
                {
                    Main.NewText("There are no valid players on your team.", 250, 0, 0);
                    teleportToPlayer = -1;
                    break;
                }
            }
        }

        if (Player.inventory[Player.selectedItem].type == ModContent.ItemType<ShadowMirror>())
        {
            Player.noFallDmg = true; //TODO: Replace with better anti-fall-damage mechanism.
            if (KeybindSystem.ModeChangeHotkey.JustPressed)
            {
                shadowWP++;
                shadowWP %= 7;
                switch (shadowWP)
                {
                    case 0:
                        Main.NewText("Mirror Mode: Spawn point");
                        break;

                    case 1:
                        Main.NewText("Mirror Mode: Dungeon");
                        break;

                    case 2:
                        Main.NewText("Mirror Mode: Jungle/Tropics");
                        break;

                    case 3:
                        Main.NewText("Mirror Mode: Left Ocean");
                        break;

                    case 4:
                        Main.NewText("Mirror Mode: Right Ocean");
                        break;

                    case 5:
                        Main.NewText("Mirror Mode: Underworld");
                        break;

                    case 6:
                        Main.NewText("Mirror Mode: Random");
                        break;

                    default:
                        throw new IndexOutOfRangeException(
                            "Not quite sure how you've managed this, but your shadow mirror's teleportation function is just wrong. Please contact the devs of Endo Avalon, and give a full bug report of all the details of the circumstances leading up to this error.");
                }
            }
        }

        bool flag = true;
        if (Player.mount != null && Player.mount.Active)
        {
            flag = Player.mount.BlockExtraJumps;
        }

        bool flag2 = (!Player.hasJumpOption_Cloud || !Player.canJumpAgain_Cloud) &&
                     (!Player.hasJumpOption_Sandstorm || !Player.canJumpAgain_Sandstorm) &&
                     (!Player.hasJumpOption_Blizzard || !Player.canJumpAgain_Blizzard) &&
                     (!Player.hasJumpOption_Fart || !Player.canJumpAgain_Fart) &&
                     (!Player.hasJumpOption_Sail || !Player.canJumpAgain_Sail) &&
                     (!Player.hasJumpOption_Unicorn || !Player.canJumpAgain_Unicorn) && NumHookProj() <= 0 && flag;
        if (!(PlayerInput.Triggers.JustPressed.Jump && Player.position.Y != Player.oldPosition.Y && flag2))
        {
            return;
        }

        if (quackJump && jumpAgainQuack)
        {
            jumpAgainQuack = false;
            int h = Player.height;
            if (Player.gravDir == -1)
            {
                h = -6;
            }

            SoundEngine.PlaySound(SoundID.Zombie12, Player.position);
            Player.velocity.Y = -Player.jumpSpeed * Player.gravDir;
            Player.jump = (int)(Player.jumpHeight * 1.25);
            int num8 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + h), Player.width + 8, 4,
                DustID.FartInAJar, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, DustID.FartInAJar, default,
                1.5f);
            Main.dust[num8].velocity.X = (Main.dust[num8].velocity.X * 0.5f) - (Player.velocity.X * 0.1f);
            Main.dust[num8].velocity.Y = (Main.dust[num8].velocity.Y * 0.5f) - (Player.velocity.Y * 0.3f);
            Main.dust[num8].velocity *= 0.5f;

            int g = Main.rand.Next(2);
            if (g == 0)
            {
                g = Mod.Find<ModGore>("QuackGore1").Type;
            }

            if (g == 1)
            {
                g = Mod.Find<ModGore>("QuackGore2").Type;
            }

            int g2 = Main.rand.Next(2);
            if (g2 == 0)
            {
                g2 = Mod.Find<ModGore>("QuackGore1").Type;
            }

            if (g2 == 1)
            {
                g2 = Mod.Find<ModGore>("QuackGore2").Type;
            }

            int g3 = Main.rand.Next(2);
            if (g3 == 0)
            {
                g3 = Mod.Find<ModGore>("QuackGore1").Type;
            }

            if (g3 == 1)
            {
                g3 = Mod.Find<ModGore>("QuackGore2").Type;
            }

            int num3 = Gore.NewGore(Player.GetSource_FromThis(),
                new Vector2(Player.position.X + (Player.width / 2) - 16f, Player.position.Y + h - 16f),
                new Vector2(-Player.velocity.X, -Player.velocity.Y), g);
            Main.gore[num3].velocity.X = (Main.gore[num3].velocity.X * 0.1f) - (Player.velocity.X * 0.1f);
            Main.gore[num3].velocity.Y = (Main.gore[num3].velocity.Y * 0.1f) - (Player.velocity.Y * 0.05f);
            Main.gore[num3].sticky = false;
            Main.gore[num3].rotation += 0.1f;
            num3 = Gore.NewGore(Player.GetSource_FromThis(),
                new Vector2(Player.position.X - 36f, Player.position.Y + h - 16f),
                new Vector2(-Player.velocity.X, -Player.velocity.Y), g2);
            Main.gore[num3].velocity.X = (Main.gore[num3].velocity.X * 0.1f) - (Player.velocity.X * 0.1f);
            Main.gore[num3].velocity.Y = (Main.gore[num3].velocity.Y * 0.1f) - (Player.velocity.Y * 0.05f);
            Main.gore[num3].sticky = false;
            Main.gore[num3].rotation += 0.1f;
            num3 = Gore.NewGore(Player.GetSource_FromThis(),
                new Vector2(Player.position.X + Player.width + 4f, Player.position.Y + h - 16f),
                new Vector2(-Player.velocity.X, -Player.velocity.Y), g3);
            Main.gore[num3].velocity.X = (Main.gore[num3].velocity.X * 0.1f) - (Player.velocity.X * 0.1f);
            Main.gore[num3].velocity.Y = (Main.gore[num3].velocity.Y * 0.1f) - (Player.velocity.Y * 0.05f);
            Main.gore[num3].sticky = false;
            Main.gore[num3].rotation += 0.1f;
        }
    }

    public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
    {
        if (damage > 0)
        {
            if (LightningInABottle)
            {
                var cloudPosition = new Vector2(Player.Center.X + 0f, Player.Center.Y - 150f);
                var targetPosition = new Vector2(Player.Center.X /* + (-20f * hitDirection)*/, Player.Center.Y);
                var targetPosition2 = new Vector2(Player.Center.X + Main.rand.Next(-40, -20), Player.Center.Y);
                var targetPosition3 = new Vector2(Player.Center.X + Main.rand.Next(-40, -20), Player.Center.Y);
                if (Main.rand.Next(2) == 0)
                {
                    targetPosition2 = new Vector2(Player.Center.X + Main.rand.Next(20, 40), Player.Center.Y);
                }

                if (Main.rand.Next(2) == 0)
                {
                    targetPosition3 = new Vector2(Player.Center.X + Main.rand.Next(20, 40), Player.Center.Y);
                }

                Projectile.NewProjectile(
                    Player.GetSource_Accessory(new Item(ModContent.ItemType<LightninginaBottle>())), cloudPosition,
                    Vector2.Zero, ModContent.ProjectileType<LightningCloud>(), 0, 0f, Player.whoAmI);

                for (int i = 0; i < 1; i++)
                {
                    Vector2 vectorBetween = targetPosition - cloudPosition;
                    float randomSeed = Main.rand.Next(100);
                    Vector2 startVelocity = Vector2.Normalize(vectorBetween.RotatedByRandom(0.78539818525314331)) * 27f;
                    Projectile.NewProjectile(
                        Player.GetSource_Accessory(new Item(ModContent.ItemType<LightninginaBottle>())), cloudPosition,
                        startVelocity, ModContent.ProjectileType<Lightning>(), 47, 0f, Main.myPlayer,
                        vectorBetween.ToRotation(), randomSeed);
                }

                for (int i = 0; i < 1; i++)
                {
                    Vector2 vectorBetween = targetPosition2 - cloudPosition;
                    float randomSeed = Main.rand.Next(100);
                    Vector2 startVelocity = Vector2.Normalize(vectorBetween.RotatedByRandom(0.78539818525314331)) * 27f;
                    Projectile.NewProjectile(
                        Player.GetSource_Accessory(new Item(ModContent.ItemType<LightninginaBottle>())), cloudPosition,
                        startVelocity, ModContent.ProjectileType<Lightning>(), 47, 0f, Main.myPlayer,
                        vectorBetween.ToRotation(), randomSeed);
                }

                for (int i = 0; i < 1; i++)
                {
                    Vector2 vectorBetween = targetPosition3 - cloudPosition;
                    float randomSeed = Main.rand.Next(100);
                    Vector2 startVelocity = Vector2.Normalize(vectorBetween.RotatedByRandom(0.78539818525314331)) * 27f;
                    Projectile.NewProjectile(
                        Player.GetSource_Accessory(new Item(ModContent.ItemType<LightninginaBottle>())), cloudPosition,
                        startVelocity, ModContent.ProjectileType<Lightning>(), 47, 0f, Main.myPlayer,
                        vectorBetween.ToRotation(), randomSeed);
                }
            }

            if (goBerserk)
            {
                if (damage > 50)
                {
                    Player.AddBuff(ModContent.BuffType<Berserk>(), 180);
                }
            }

            if (leafStorm)
            {
                if (damage > 0 && Main.rand.Next(5) == 0)
                {
                    var pos = new Vector2(Player.Center.X + Main.rand.Next(-500, 501), Player.Center.Y);
                    while (Main.tile[(int)(pos.X / 16), (int)(pos.Y / 16)].HasTile)
                    {
                        pos.Y--;
                    }

                    Projectile.NewProjectile(Player.GetSource_FromThis(), pos, Vector2.Zero,
                        ModContent.ProjectileType<LeafStorm>(), 80, 0.6f, Main.myPlayer);
                }
            }
        }
    }

    public override void UpdateDead() => jumpAgainQuack = false;

    public override void PostUpdateRunSpeeds()
    {
        //Main.NewText("PostUpdateRunSpeeds " + slimeBand.ToString());
        FloorVisualsAvalon(Player.velocity.Y > Player.gravity);
        if (activateRocketJump && Player.GetModPlayer<ExxoStaminaPlayer>().RocketJumpUnlocked)
        {
            if (Player.controlUp && Player.releaseUp)
            {
                if (Player.IsOnGround())
                {
                    int amt = 70;
                    if (Player.GetModPlayer<ExxoStaminaPlayer>().StaminaDrain)
                    {
                        amt *= (int)(Player.GetModPlayer<ExxoStaminaPlayer>().StaminaDrainStacks *
                                     Player.GetModPlayer<ExxoStaminaPlayer>().StaminaDrainMult);
                    }

                    if (Player.GetModPlayer<ExxoStaminaPlayer>().StatStam >= amt)
                    {
                        Player.GetModPlayer<ExxoStaminaPlayer>().StatStam -= amt;
                        float yDestination = Player.position.Y - 360f;
                        int num6 = Gore.NewGore(Player.GetSource_FromThis(),
                            new Vector2(Player.position.X + (Player.width / 2) - 16f,
                                Player.position.Y + (Player.gravDir == -1 ? 0 : Player.height) - 16f),
                            new Vector2(-Player.velocity.X, -Player.velocity.Y), Main.rand.Next(11, 14));
                        Main.gore[num6].velocity.X = (Main.gore[num6].velocity.X * 0.1f) - (Player.velocity.X * 0.1f);
                        Main.gore[num6].velocity.Y = (Main.gore[num6].velocity.Y * 0.1f) - (Player.velocity.Y * 0.05f);
                        num6 = Gore.NewGore(Player.GetSource_FromThis(),
                            new Vector2(Player.position.X - 36f,
                                Player.position.Y + (Player.gravDir == -1 ? 0 : Player.height) - 16f),
                            new Vector2(-Player.velocity.X, -Player.velocity.Y), Main.rand.Next(11, 14));
                        Main.gore[num6].velocity.X = (Main.gore[num6].velocity.X * 0.1f) - (Player.velocity.X * 0.1f);
                        Main.gore[num6].velocity.Y = (Main.gore[num6].velocity.Y * 0.1f) - (Player.velocity.Y * 0.05f);
                        num6 = Gore.NewGore(Player.GetSource_FromThis(),
                            new Vector2(Player.position.X + Player.width + 4f,
                                Player.position.Y + (Player.gravDir == -1 ? 0 : Player.height) - 16f),
                            new Vector2(-Player.velocity.X, -Player.velocity.Y), Main.rand.Next(11, 14));
                        Main.gore[num6].velocity.X = (Main.gore[num6].velocity.X * 0.1f) - (Player.velocity.X * 0.1f);
                        Main.gore[num6].velocity.Y = (Main.gore[num6].velocity.Y * 0.1f) - (Player.velocity.Y * 0.05f);
                        SoundEngine.PlaySound(SoundID.Item11, Player.Center);
                        Player.velocity.Y -= 16.5f;
                    }
                    else if (Player.GetModPlayer<ExxoStaminaPlayer>().StamFlower)
                    {
                        Player.GetModPlayer<ExxoStaminaPlayer>().QuickStamina(amt);
                        if (Player.GetModPlayer<ExxoStaminaPlayer>().StatStam >= amt)
                        {
                            Player.GetModPlayer<ExxoStaminaPlayer>().StatStam -= amt;
                            float yDestination = Player.position.Y - 360f;
                            int num6 = Gore.NewGore(Player.GetSource_FromThis(),
                                new Vector2(Player.position.X + (Player.width / 2) - 16f,
                                    Player.position.Y + (Player.gravDir == -1 ? 0 : Player.height) - 16f),
                                new Vector2(-Player.velocity.X, -Player.velocity.Y), Main.rand.Next(11, 14));
                            Main.gore[num6].velocity.X =
                                (Main.gore[num6].velocity.X * 0.1f) - (Player.velocity.X * 0.1f);
                            Main.gore[num6].velocity.Y =
                                (Main.gore[num6].velocity.Y * 0.1f) - (Player.velocity.Y * 0.05f);
                            num6 = Gore.NewGore(Player.GetSource_FromThis(),
                                new Vector2(Player.position.X - 36f,
                                    Player.position.Y + (Player.gravDir == -1 ? 0 : Player.height) - 16f),
                                new Vector2(-Player.velocity.X, -Player.velocity.Y), Main.rand.Next(11, 14));
                            Main.gore[num6].velocity.X =
                                (Main.gore[num6].velocity.X * 0.1f) - (Player.velocity.X * 0.1f);
                            Main.gore[num6].velocity.Y =
                                (Main.gore[num6].velocity.Y * 0.1f) - (Player.velocity.Y * 0.05f);
                            num6 = Gore.NewGore(Player.GetSource_FromThis(),
                                new Vector2(Player.position.X + Player.width + 4f,
                                    Player.position.Y + (Player.gravDir == -1 ? 0 : Player.height) - 16f),
                                new Vector2(-Player.velocity.X, -Player.velocity.Y), Main.rand.Next(11, 14));
                            Main.gore[num6].velocity.X =
                                (Main.gore[num6].velocity.X * 0.1f) - (Player.velocity.X * 0.1f);
                            Main.gore[num6].velocity.Y =
                                (Main.gore[num6].velocity.Y * 0.1f) - (Player.velocity.Y * 0.05f);
                            SoundEngine.PlaySound(SoundID.Item11, Player.Center);
                            Player.velocity.Y -= 16.5f;
                        }
                    }
                }
            }

            if (Player.velocity.Y < 0)
            {
                for (int x = 0; x < 5; x++)
                {
                    int d = Dust.NewDust(new Vector2(Player.Center.X, Player.position.Y + Player.height), 10, 10,
                        DustID.Smoke);
                }
            }
        }

        if (Player.wet && Player.velocity != Vector2.Zero && !Player.accMerman && activateSwim &&
            Player.GetModPlayer<ExxoStaminaPlayer>().SwimmingUnlocked)
        {
            bool flag15 = true;
            staminaCD++;
            Player.GetModPlayer<ExxoStaminaPlayer>().StaminaRegenCount = 0;
            if (staminaCD >= 10)
            {
                int amt = 1;
                if (Player.GetModPlayer<ExxoStaminaPlayer>().StaminaDrain)
                {
                    amt *= (int)(Player.GetModPlayer<ExxoStaminaPlayer>().StaminaDrainStacks *
                                 Player.GetModPlayer<ExxoStaminaPlayer>().StaminaDrainMult);
                }

                if (Player.GetModPlayer<ExxoStaminaPlayer>().StatStam >= amt)
                {
                    Player.GetModPlayer<ExxoStaminaPlayer>().StatStam -= amt;
                }
                else if (Player.GetModPlayer<ExxoStaminaPlayer>().StamFlower)
                {
                    Player.GetModPlayer<ExxoStaminaPlayer>().QuickStamina();
                    if (Player.GetModPlayer<ExxoStaminaPlayer>().StatStam >= amt)
                    {
                        Player.GetModPlayer<ExxoStaminaPlayer>().StatStam -= amt;
                    }
                }

                if (Player.GetModPlayer<ExxoStaminaPlayer>().StatStam <= 0)
                {
                    Player.GetModPlayer<ExxoStaminaPlayer>().StatStam = 0;
                    flag15 = false;
                }

                staminaCD = 0;
            }

            if (flag15)
            {
                Player.accFlipper = true;
            }
        }

        if (activateSprint)
        {
            if ((Player.controlRight || Player.controlLeft) && Player.velocity.X != 0f)
            {
                bool flag17 = true;
                staminaCD2++;
                Player.GetModPlayer<ExxoStaminaPlayer>().StaminaRegenCount = 0;
                if (staminaCD2 >= 30)
                {
                    int amt = 2;
                    if (Player.GetModPlayer<ExxoStaminaPlayer>().StaminaDrain)
                    {
                        amt *= (int)(Player.GetModPlayer<ExxoStaminaPlayer>().StaminaDrainStacks *
                                     Player.GetModPlayer<ExxoStaminaPlayer>().StaminaDrainMult);
                    }

                    if (Player.GetModPlayer<ExxoStaminaPlayer>().StatStam >= amt)
                    {
                        Player.GetModPlayer<ExxoStaminaPlayer>().StatStam -= amt;
                    }
                    else if (Player.GetModPlayer<ExxoStaminaPlayer>().StamFlower)
                    {
                        Player.GetModPlayer<ExxoStaminaPlayer>().QuickStamina();
                        if (Player.GetModPlayer<ExxoStaminaPlayer>().StatStam >= amt)
                        {
                            Player.GetModPlayer<ExxoStaminaPlayer>().StatStam -= amt;
                        }
                    }

                    if (Player.GetModPlayer<ExxoStaminaPlayer>().StatStam <= 0)
                    {
                        Player.GetModPlayer<ExxoStaminaPlayer>().StatStam = 0;
                        flag17 = false;
                    }

                    staminaCD2 = 0;
                }

                if (flag17)
                {
                    if (!Player.HasItemInArmor(ItemID.HermesBoots) && !Player.HasItemInArmor(ItemID.FlurryBoots) &&
                        !Player.HasItemInArmor(ItemID.SpectreBoots) &&
                        !Player.HasItemInArmor(ItemID.LightningBoots) &&
                        !Player.HasItemInArmor(ItemID.FrostsparkBoots) &&
                        !Player.HasItemInArmor(ItemID.SailfishBoots) &&
                        !inertiaBoots && !blahWings)
                    {
                        Player.accRunSpeed = 6f;
                    }
                    else if (!Player.HasItemInArmor(ItemID.LightningBoots) &&
                             !Player.HasItemInArmor(ItemID.FrostsparkBoots) &&
                             !inertiaBoots && !blahWings)
                    {
                        Player.accRunSpeed = 6.75f;
                    }
                    else if (!inertiaBoots && !blahWings)
                    {
                        Player.accRunSpeed = 10.29f;
                        if ((Player.velocity.X < 4f && Player.controlRight) ||
                            (Player.velocity.X > -4f && Player.controlLeft))
                        {
                            Player.velocity.X = Player.velocity.X + (Player.controlRight ? 0.31f : -0.31f);
                        }
                        else if ((Player.velocity.X < 8f && Player.controlRight) ||
                                 (Player.velocity.X > -8f && Player.controlLeft))
                        {
                            Player.velocity.X = Player.velocity.X + (Player.controlRight ? 0.29f : -0.29f);
                        }
                    }
                    else
                    {
                        Player.accRunSpeed = 14.29f;
                        if ((Player.velocity.X < 5f && Player.controlRight) ||
                            (Player.velocity.X > -5f && Player.controlLeft))
                        {
                            Player.velocity.X = Player.velocity.X + (Player.controlRight ? 0.41f : -0.41f);
                        }
                        else if ((Player.velocity.X < 14f && Player.controlRight) ||
                                 (Player.velocity.X > -14f && Player.controlLeft))
                        {
                            Player.velocity.X = Player.velocity.X + (Player.controlRight ? 0.39f : -0.39f);
                        }
                    }
                }
            }
        }
    }

    public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn,
                                   ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
    {
        #region Contagion Fish
        if (Player.GetModPlayer<ExxoBiomePlayer>().ZoneContagion && Main.rand.NextBool(10))
        {
            itemDrop = ModContent.ItemType<NauSeaFish>();
        }
        #endregion Contagion Fish
    }

    public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
    {
        if (undeadTalisman)
        {
            int dmgPlaceholder = npc.damage;
            if (Data.Sets.NPC.Undead[npc.type])
            {
                if (damage - ((Player.statDefense / 2) - 10) <= 0)
                {
                    damage = 0;
                    Player.immune = true;
                    Player.immuneAlpha = 0;
                }
                else
                {
                    damage = dmgPlaceholder - ((Player.statDefense / 2) - 10);
                }
            }
        }

        if (Player.HasBuff(ModContent.BuffType<ShadowCurse>()))
        {
            damage *= 2;
        }

        if (caesiumPoison)
        {
            damage = (int)(damage * 1.15f);
        }
    }

    public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
    {
        if (Player.HasBuff(ModContent.BuffType<ShadowCurse>()))
        {
            damage *= 2;
        }
    }

    public void WOSTongue()
    {
        if (AvalonTestingWorld.WallOfSteel >= 0 && Main.npc[AvalonTestingWorld.WallOfSteel].active)
        {
            float num = Main.npc[AvalonTestingWorld.WallOfSteel].position.X + 40f;
            if (Main.npc[AvalonTestingWorld.WallOfSteel].direction > 0)
            {
                num -= 96f;
            }

            if (Player.position.X + Player.width > num && Player.position.X < num + 140f && Player.gross)
            {
                Player.noKnockback = false;
                Player.Hurt(PlayerDeathReason.ByNPC(AvalonTestingWorld.WallOfSteel), 50,
                    Main.npc[AvalonTestingWorld.WallOfSteel].direction);
            }

            if (!Player.gross && Player.position.Y > (Main.maxTilesY - 250) * 16 && Player.position.X > num - 1920f &&
                Player.position.X < num + 1920f)
            {
                Player.AddBuff(37, 10);
                //Main.PlaySound(4, (int)Main.npc[AvalonTestingWorld.wos].position.X, (int)Main.npc[AvalonTestingWorld.wos].position.Y, 10);
            }

            if (Player.gross)
            {
                if (Player.position.Y < (Main.maxTilesY - 200) * 16)
                {
                    Player.AddBuff(38, 10);
                }

                if (Main.npc[AvalonTestingWorld.WallOfSteel].direction < 0)
                {
                    if (Player.position.X + (Player.width / 2) > Main.npc[AvalonTestingWorld.WallOfSteel].position.X +
                        (Main.npc[AvalonTestingWorld.WallOfSteel].width / 2) + 40f)
                    {
                        Player.AddBuff(38, 10);
                    }
                }
                else if (Player.position.X + (Player.width / 2) < Main.npc[AvalonTestingWorld.WallOfSteel].position.X +
                         (Main.npc[AvalonTestingWorld.WallOfSteel].width / 2) - 40f)
                {
                    Player.AddBuff(38, 10);
                }
            }

            if (Player.tongued)
            {
                Player.controlHook = false;
                Player.controlUseItem = false;
                for (int i = 0; i < 1000; i++)
                {
                    if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer &&
                        Main.projectile[i].aiStyle == 7)
                    {
                        Main.projectile[i].Kill();
                    }
                }

                var vector = new Vector2(Player.position.X + (Player.width * 0.5f),
                    Player.position.Y + (Player.height * 0.5f));
                float num2 = Main.npc[AvalonTestingWorld.WallOfSteel].position.X +
                    (Main.npc[AvalonTestingWorld.WallOfSteel].width / 2) - vector.X;
                float num3 = Main.npc[AvalonTestingWorld.WallOfSteel].position.Y +
                    (Main.npc[AvalonTestingWorld.WallOfSteel].height / 2) - vector.Y;
                float num4 = (float)Math.Sqrt((num2 * num2) + (num3 * num3));
                if (num4 > 3000f)
                {
                    //player.lastPosBeforeDeath = this.position;
                    Player.KillMe(PlayerDeathReason.ByNPC(AvalonTestingWorld.WallOfSteel), 1000.0, 0);
                    return;
                }

                if (Main.npc[AvalonTestingWorld.WallOfSteel].position.X < 608f ||
                    Main.npc[AvalonTestingWorld.WallOfSteel].position.X > (Main.maxTilesX - 38) * 16)
                {
                    //this.lastPosBeforeDeath = this.position;
                    Player.KillMe(PlayerDeathReason.ByNPC(AvalonTestingWorld.WallOfSteel), 1000.0, 0);
                }
            }
        }
    }

    public int MultiplyCritDamage(int dmg) // dmg = damage befor crit application
    {
        int bonusDmg = -dmg;
        bonusDmg += (int)(dmg * (CritDamageMult + 1f) / 2);
        return bonusDmg;
    }

    public void DoubleJumps()
    {
        if (NumHookProj() > 0 || Player.sliding || (Player.autoJump && Player.justJumped))
        {
            jumpAgainQuack = true;
            return;
        }

        bool flag = true;
        if (Player.mount != null && Player.mount.Active)
        {
            flag = Player.mount.BlockExtraJumps;
        }

        bool flag2 = Player.carpet ? Player.carpetTime <= 0 && Player.canCarpet : true;
        if (Player.position.Y == Player.oldPosition.Y && flag && flag2)
        {
            jumpAgainQuack = true;
        }
    }

    public void UpdateMana() => Player.statManaMax2 += spiritPoppyUseCount * 20;

    public void DashMovement()
    {
        if (Player.dashDelay > 0)
        {
            Player.dashDelay--;
            return;
        }

        int amt = 60;
        if (Player.GetModPlayer<ExxoStaminaPlayer>().StaminaDrain)
        {
            amt *= (int)(Player.GetModPlayer<ExxoStaminaPlayer>().StaminaDrainStacks *
                         Player.GetModPlayer<ExxoStaminaPlayer>().StaminaDrainMult);
        }

        if (stamDashKey && Player.dash == 0 && Player.dashDelay >= 0)
        {
            if (Player.GetModPlayer<ExxoStaminaPlayer>().StatStam > amt)
            {
                int num2 = 0;
                bool flag = false;
                if (Player.dashTime > 0)
                {
                    Player.dashTime--;
                }

                if (Player.dashTime < 0)
                {
                    Player.dashTime++;
                }

                if (Player.controlRight && Player.releaseRight)
                {
                    if (Player.dashTime > 0)
                    {
                        num2 = 1;
                        flag = true;
                        Player.dashTime = 0;
                        Player.GetModPlayer<ExxoStaminaPlayer>().StatStam -= amt;
                    }
                    else
                    {
                        Player.dashTime = 15;
                    }
                }
                else if (Player.controlLeft && Player.releaseLeft)
                {
                    if (Player.dashTime < 0)
                    {
                        num2 = -1;
                        flag = true;
                        Player.dashTime = 0;
                        Player.GetModPlayer<ExxoStaminaPlayer>().StatStam -= amt;
                    }
                    else
                    {
                        Player.dashTime = -15;
                    }
                }

                if (flag)
                {
                    Player.velocity.X = 15.9f * num2;
                    Player.dashDelay = -1;
                    for (int j = 0; j < 20; j++)
                    {
                        int num3 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width,
                            Player.height, DustID.Smoke, 0f, 0f, 100, default, 2f);
                        Dust expr_336_cp_0 = Main.dust[num3];
                        expr_336_cp_0.position.X = expr_336_cp_0.position.X + Main.rand.Next(-5, 6);
                        Dust expr_35D_cp_0 = Main.dust[num3];
                        expr_35D_cp_0.position.Y = expr_35D_cp_0.position.Y + Main.rand.Next(-5, 6);
                        Main.dust[num3].velocity *= 0.2f;
                        Main.dust[num3].scale *= 1f + (Main.rand.Next(20) * 0.01f);
                    }

                    int num4 = Gore.NewGore(Player.GetSource_FromThis(),
                        new Vector2(Player.position.X + (Player.width / 2) - 24f,
                            Player.position.Y + (Player.height / 2) - 34f), default, Main.rand.Next(61, 64));
                    Main.gore[num4].velocity.X = Main.rand.Next(-50, 51) * 0.01f;
                    Main.gore[num4].velocity.Y = Main.rand.Next(-50, 51) * 0.01f;
                    Main.gore[num4].velocity *= 0.4f;
                    num4 = Gore.NewGore(Player.GetSource_FromThis(),
                        new Vector2(Player.position.X + (Player.width / 2) - 24f,
                            Player.position.Y + (Player.height / 2) - 14f), default, Main.rand.Next(61, 64));
                    Main.gore[num4].velocity.X = Main.rand.Next(-50, 51) * 0.01f;
                    Main.gore[num4].velocity.Y = Main.rand.Next(-50, 51) * 0.01f;
                    Main.gore[num4].velocity *= 0.4f;
                }
            }
            else if (Player.GetModPlayer<ExxoStaminaPlayer>().StamFlower)
            {
                Player.GetModPlayer<ExxoStaminaPlayer>().QuickStamina(amt);
                if (Player.GetModPlayer<ExxoStaminaPlayer>().StatStam > amt)
                {
                    int num2 = 0;
                    bool flag = false;
                    if (Player.dashTime > 0)
                    {
                        Player.dashTime--;
                    }

                    if (Player.dashTime < 0)
                    {
                        Player.dashTime++;
                    }

                    if (Player.controlRight && Player.releaseRight)
                    {
                        if (Player.dashTime > 0)
                        {
                            num2 = 1;
                            flag = true;
                            Player.dashTime = 0;
                            Player.GetModPlayer<ExxoStaminaPlayer>().StatStam -= amt;
                        }
                        else
                        {
                            Player.dashTime = 15;
                        }
                    }
                    else if (Player.controlLeft && Player.releaseLeft)
                    {
                        if (Player.dashTime < 0)
                        {
                            num2 = -1;
                            flag = true;
                            Player.dashTime = 0;
                            Player.GetModPlayer<ExxoStaminaPlayer>().StatStam -= amt;
                        }
                        else
                        {
                            Player.dashTime = -15;
                        }
                    }

                    if (flag)
                    {
                        Player.velocity.X = 15.9f * num2;
                        Player.dashDelay = -1;
                        for (int j = 0; j < 20; j++)
                        {
                            int num3 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width,
                                Player.height, DustID.Smoke, 0f, 0f, 100, default, 2f);
                            Dust expr_336_cp_0 = Main.dust[num3];
                            expr_336_cp_0.position.X = expr_336_cp_0.position.X + Main.rand.Next(-5, 6);
                            Dust expr_35D_cp_0 = Main.dust[num3];
                            expr_35D_cp_0.position.Y = expr_35D_cp_0.position.Y + Main.rand.Next(-5, 6);
                            Main.dust[num3].velocity *= 0.2f;
                            Main.dust[num3].scale *= 1f + (Main.rand.Next(20) * 0.01f);
                        }

                        int num4 = Gore.NewGore(Player.GetSource_FromThis(),
                            new Vector2(Player.position.X + (Player.width / 2) - 24f,
                                Player.position.Y + (Player.height / 2) - 34f), default, Main.rand.Next(61, 64));
                        Main.gore[num4].velocity.X = Main.rand.Next(-50, 51) * 0.01f;
                        Main.gore[num4].velocity.Y = Main.rand.Next(-50, 51) * 0.01f;
                        Main.gore[num4].velocity *= 0.4f;
                        num4 = Gore.NewGore(Player.GetSource_FromThis(),
                            new Vector2(Player.position.X + (Player.width / 2) - 24f,
                                Player.position.Y + (Player.height / 2) - 14f), default, Main.rand.Next(61, 64));
                        Main.gore[num4].velocity.X = Main.rand.Next(-50, 51) * 0.01f;
                        Main.gore[num4].velocity.Y = Main.rand.Next(-50, 51) * 0.01f;
                        Main.gore[num4].velocity *= 0.4f;
                    }
                }
            }
        }
    }

    public void FloorVisualsAvalon(bool falling)
    {
        int num = (int)((Player.position.X + (Player.width / 2)) / 16f);
        int num2 = (int)((Player.position.Y + Player.height) / 16f);
        int num3 = -1;
        if (Main.tile[num, num2].HasUnactuatedTile && Main.tileSolid[Main.tile[num, num2].TileType])
        {
            num3 = Main.tile[num, num2].TileType;
        }
        else if (Main.tile[num - 1, num2].HasUnactuatedTile && Main.tileSolid[Main.tile[num - 1, num2].TileType])
        {
            num3 = Main.tile[num - 1, num2].TileType;
        }
        else if (Main.tile[num + 1, num2].HasUnactuatedTile && Main.tileSolid[Main.tile[num + 1, num2].TileType])
        {
            num3 = Main.tile[num + 1, num2].TileType;
        }

        if (num3 > -1)
        {
            if (num3 == 229 && !noSticky)
            {
                Player.sticky = true;
            }
            else
            {
                Player.sticky = false;
            }

            if (slimeBand || ZoneIceSoul)
            {
                Player.slippy = true;
                Player.slippy2 = true;
            }
            else
            {
                Player.slippy = false;
                Player.slippy2 = false;
            }
        }
    }

    /// <summary>
    ///     Teleports the player to the given mode's teleport. Used for the Shadow Mirror, Magic Conch, and Demon Conch.
    ///     Kills the player if they have the Horrified debuff from Wall of Flesh or Wall of Steel.
    /// </summary>
    /// <param name="mode">
    ///     The mode of the teleport - 0: Spawnpoint, 1: Dungeon, 2: Jungle/Tropics, 3: Left Ocean, 4: Right Ocean,
    ///     5: Underworld, 6: Random.
    /// </param>
    /// <param name="pid">Unused.</param>
    public void ShadowTP(int mode, int pid)
    {
        if (Player.HasBuff(37))
        {
            Player.KillMe(PlayerDeathReason.ByCustomReason(" tried to escape..."), 3000000, 0);
            return;
        }

        switch (mode)
        {
            case 0:
                Player.Spawn(PlayerSpawnContext.RecallFromItem);
                break;
            case 1: // dungeon
                Player.noFallDmg = true;
                Player.immuneTime = 100;
                ShadowTeleport.Teleport();
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.PlayerControls, number: Player.whoAmI);
                }

                Player.noFallDmg = false;
                break;

            case 2: // jungle
                Player.noFallDmg = true;
                Player.immuneTime = 100;
                Vector2 prePos = Player.position;
                Vector2 pos = prePos;
                ShadowTeleport.Teleport(1);
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.PlayerControls, number: Player.whoAmI);
                }

                Player.noFallDmg = false;
                break;

            case 3: // left ocean
                Player.noFallDmg = true;
                Player.immuneTime = 300;
                ShadowTeleport.Teleport(2);
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.PlayerControls, number: Player.whoAmI);
                }

                Player.noFallDmg = false;
                break;

            case 4: // right ocean
                Player.noFallDmg = true;
                Player.immuneTime = 300;
                ShadowTeleport.Teleport(3);
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.PlayerControls, number: Player.whoAmI);
                }

                Player.noFallDmg = false;
                break;

            case 5: // hell
                Player.noFallDmg = true;
                Player.immuneTime = 100;
                ShadowTeleport.Teleport(4);
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.PlayerControls, number: Player.whoAmI);
                }

                Player.noFallDmg = false;
                break;

            case 6: // random
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Player.TeleportationPotion();
                }
                else if (Main.netMode == NetmodeID.MultiplayerClient && Player.whoAmI == Main.myPlayer)
                {
                    NetMessage.SendData(MessageID.Teleport, -1, -1, NetworkText.Empty);
                }

                break;
            }
        }

        int d = 15;
        switch (mode)
        {
            case 0:
                d = DustID.MagicMirror;
                break;
            case 1:
                d = ModContent.DustType<DungeonTeleportDust>();
                break;
            case 2:
                d = ModContent.DustType<JungleTeleportDust>();
                break;
            case 3:
            case 4:
                d = ModContent.DustType<OceanTeleportDust>();
                break;
            case 5:
                d = ModContent.DustType<DemonConchDust>();
                break;
            default:
                d = DustID.MagicMirror;
                break;
        }

        for (int i = 0; i < 70; i++)
        {
            Dust.NewDust(Player.position, Player.width, Player.height, d, 0f, 0f, 150, default, 1.5f);
        }
    }

    #region fields
    // stamina abilities
    //public bool sprintUnlocked = false;
    //public bool rocketJumpUnlocked = false;
    //public bool swimmingUnlocked = false;
    //public bool teleportUnlocked = false;
    //public bool flightRestoreUnlocked = false;

    //public int stamFlightRestoreCD = 0;
    //public bool releaseQuickStamina;
    //public int stamRegen;
    //public int stamRegenCount;
    //public int stamRegenDelay;
    //public int staminaRegen = 1000;
    //public int StatStamMax = 30;
    //public int StatStamMax2;
    //public bool stamFlower = false;
    //public bool staminaDrain = false;
    //public int staminaDrainStacks = 1;
    //public float staminaDrainMult = 1.2f;
    //public int StatStam = 100;
    //public static int staminaDrainTime = 10 * 60;
    // end stamina stuff

    // buff stuff
    public bool beeRepel = false;
    public bool lucky;
    public bool melting;
    public bool enemySpawns2;
    public int crimsonCount = 0;
    public bool darkInferno;
    public int deliriumCount = 0;
    public bool forceField = true;
    public int fAlevel = 0;
    public int fAlastRecord = 0;
    public int shadowPotCd = 0;
    public bool shockWave;
    public int fallStart_old = 0;
    public bool vision = false;

    public const int deliriumFreq = 600;
    // end buff stuff

    // minion stuff
    public bool gastroMinion;
    public bool hungryMinion;
    public bool iceGolem;
    public bool goldDagger;
    public bool platinumDagger;
    public bool bismuthDagger;
    public bool adamantiteDagger;
    public bool titaniumDagger;
    public bool troxiniumDagger;
    public bool primeMinion;

    public bool reflectorMinion;

    public bool skyBlessing;

    // end minion stuff
    public bool[] pSensor = new bool[6];
    public int spiritPoppyUseCount;
    public bool shmAcc = false;
    public bool herb;
    public bool teleportVWasTriggered;
    public int screenShakeTimer;
    public bool snotOrb;
    public bool miniArma;

    public enum ShadowMirrorModes
    {
        Spawn,
        Checkpoints,
        Team,
    }

    public static Dictionary<int, int> torches;

    public bool inertiaBoots;
    public bool blahWings;
    public bool spikeImmune;
    public bool luckTome;
    public bool thornHeartAmulet = false;
    public bool quackJump;
    public bool jumpAgainQuack;
    public bool armorStealth = false;
    public int shadowCheckPointNum = 0;
    public int shadowPlayerNum = 0;
    public bool slimeImmune;
    public int infectTimer = 0;
    public int infectDmg = 0;
    public bool weaponMinion = false; // remove
    public bool earthInsig = false;
    public int CrystalHealth;
    public Item tomeItem = new();
    public int pl = -1;
    public bool openLocks;
    public bool chaosCharm;
    public short thunderTimer;
    public bool terraClaws;
    public bool thunderBolt;
    public bool incDef;
    public bool regenStrike;
    public bool bOfBacteria;
    public bool duraShield = false;
    public bool slimeBand;
    public bool defDebuff;
    public int defDebuffBonusDef;
    public float rot;
    public byte qsMode = 1;
    public byte qsTimer;
    public bool qsDone;
    public bool qsIsNDown;
    public bool trapImmune;
    public bool hyperMelee;
    public bool hyperMagic;
    public bool hyperRanged;
    public int hyperBar;
    public bool auraThorns;
    public bool speed;
    public bool Nd;
    public bool oldNd;
    public bool Fd;
    public bool oldFd;
    public bool Bud;
    public bool oldBud;
    public bool Ld;
    public bool oldLd;
    public bool Gd;
    public bool oldGd;
    public bool Brd;
    public bool oldBrd;
    public bool Kd;
    public bool oldKd;
    public bool activateBubble;
    public bool activateSprint;
    public bool activateSwim;
    public bool activateSlide;
    public bool activateRocketJump;
    public bool stamDashKey;
    public bool quintJump;
    public bool shadowRing;
    public static bool SpawnDL = false;
    public bool fleshLaser;
    public int teleportToPlayer = -1;
    public bool dashIntoMob;
    public bool dashTemp;
    public bool bubbleBoost;
    public bool bubbleBoostActive;
    public int teamLen;
    public bool rocketJumpRO = true;
    public bool heartGolem;
    public bool ethHeart;
    public byte staminaCD;
    public byte staminaCD2;
    public byte staminaCD3;
    public bool blahArmor;
    public bool shadowTele;
    public bool teleportV;
    public bool tpStam = true;
    public int tpCD;
    public int bubbleCD;
    public bool ancientLessCost;
    public bool ancientGunslinger;
    public bool ancientMinionGuide;
    public bool ancientSandVortex;
    public int ancientGunslingerTimer;
    public int ancientGunslingerStatAdd;
    public int baseUseTime;
    public int baseUseAnim;
    public bool oldLeftClick;
    public bool oblivionKill;
    public bool goBerserk;
    public bool splitProj;
    public bool spectrumSpeed;
    public bool spectrumBlur;
    public bool minionFreeze;
    public bool leafStorm;
    public bool thornMagic;
    public bool roseMagic;
    public int roseMagicCooldown;
    public bool avalonRestoration;
    public bool avalonRetribution;
    public int deliriumDuration = 300;
    public int quadroCount;
    public int shadowWP;
    public bool confusionTal;
    public bool shadowRO;
    public bool isNDown;
    public bool magnet;
    public Item tome;
    public bool ghostSilence;
    public bool ZoneTime;
    public bool ZoneBlight;
    public bool ZoneFright;
    public bool ZoneMight;
    public bool ZoneNight;
    public bool ZoneTorture;
    public bool ZoneIceSoul;
    public bool ZoneFlight;
    public bool ZoneHumidity;
    public bool ZoneDelight;
    public bool ZoneSight;
    public bool meleeStealth;

    public bool ammoCost70;
    public bool LightningInABottle;
    public bool longInvince2;
    public float kbIncrease;
    public bool accDivingSuit;
    public bool accDivingPants;
    public bool doubleJump5;
    public bool jumpAgain5;
    public bool dJumpEffect5;
    public bool doubleDamage;
    public bool frozen;
    public Color baseSkinTone;
    public bool bloodCast;
    public bool necroticAura;
    public bool reckoning;
    public int reckoningLevel;
    public int reckoningTimeLeft;
    public bool ReckoningBonus;
    public int reckoningHit;
    public bool curseOfIcarus;
    public bool undeadTalisman;
    public bool cOmega;
    public bool pOmega;
    public bool noSticky;
    public bool vampireTeeth;
    public bool riftGoggles;
    public bool malaria;
    public bool caesiumPoison;
    public int caesiumTimer;
    public bool cloudGloves;
    public bool crystalEdge;
    public float bonusKB = 1f;
    public bool stingerPack;
    public bool UltraHMinion;
    public bool UltraRMinion;
    public bool UltraLMinion;
    private int actualStatManaMax2;
    public bool oreDupe;

    public Vector2 MousePosition;

    #region Dragon's Bondage AI vars
    public bool dragonsBondage;
    #endregion Dragon's Bondage AI vars

    public int herbX;
    public int herbY;
    public int potionTotal;
    public int herbTotal;
    public Dictionary<int, int> herbCounts = new();
    private int gemCount;
    public bool[] ownedLargeGems = new bool[10];

    // Crit damage multiplyer vars
    public float CritDamageMult = 1f;

    public enum HerbTier
    {
        Novice,
        Apprentice,
        Expert,
        Master,
    }

    public HerbTier herbTier;
    #endregion fields
}
