using System;
using System.Collections.Generic;
using System.Linq;
using Avalon.Buffs;
using Avalon.Dusts;
using Avalon.Items.Accessories;
using Avalon.Items.Consumables;
using Avalon.Items.Fish;
using Avalon.Items.Other;
using Avalon.Items.Tomes;
using Avalon.Items.Tools;
using Avalon.Items.Weapons.Ranged;
using Avalon.Logic;
using Avalon.Prefixes;
using Avalon.Projectiles;
using Avalon.Projectiles.Melee;
using Avalon.Systems;
using Avalon.Tiles;
using Avalon.Walls;
using Avalon.NPCs.Bosses;
using Avalon.Tiles.Ores;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Renderers;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Avalon.Players;

public class ExxoPlayer : ModPlayer
{
    public static Asset<Texture2D>[] spectrumArmorTextures;
    protected override bool CloneNewInstances => false;



    public static int NumHookProj() => Main.projectile.Count(p =>
        Main.projHook[p.type] && p.active && p.ai[0] == 2f && p.owner == Main.myPlayer);

    public override void ModifyScreenPosition()
    {
        if (screenShakeTimer > 0)
        {
            Main.screenPosition += Main.rand.NextVector2Circular(20, 20);
        }
    }

    public override void ResetEffects()
    {
        //Main.NewText("" + trapImmune.ToString());
        //Main.NewText("" + slimeBand.ToString());
        HookBonus = false;
        if (DarkMatterTimeOut-- < 0)
        {
            DarkMatterMonolith = false;
        }

        oreDupe = false;
        skyBlessing = false;
        snotOrb = false;
        shockWave = false;
        quackJump = false;
        stingerPack = false;
        crystalEdge = false;
        tpStam = true;
        riftGoggles = false;
        noSticky = false;
        lucky = false;
        enemySpawns2 = false;
        bloodCast = false;
        magnet = false;
        darkInferno = false;
        dragonsBondage = false;
        necroticAura = false;
        defDebuff = false;
        defDebuffBonusDef = 0;
        frozen = false;
        reckoning = false;


        oblivionKill = false;
        goBerserk = false;
        splitProj = false;
        spectrumSpeed = false;
        spectrumBlur = false;
        minionFreeze = false;
        thornMagic = false;
        roseMagic = false;
        avalonRestoration = false;
        avalonRetribution = false;
        curseOfIcarus = false;
        malaria = false;
        hungryMinion = false;
        gastroMinion = false;
        reflectorMinion = false;
        iceGolem = false;
        UltraHMinion = false;
        UltraRMinion = false;
        UltraLMinion = false;
        cloudGloves = false;
        ReckoningBonus = false;
        bonusKB = 1f;
        miniArma = false;

        CritDamageMult = 1f;

        Player.GetModPlayer<ExxoStaminaPlayer>().StatStamMax2 = Player.GetModPlayer<ExxoStaminaPlayer>().StatStamMax;
        if (Player.whoAmI == Main.myPlayer)
        {
            MousePosition = Main.MouseWorld;
        }
    }

    public override void PostUpdateEquips()
    {
        //Main.NewText(EquipLoader.GetEquipTexture(EquipType.Head, Player.head).Name);
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
        player.GetModPlayer<ExxoEquipEffectPlayer>().AstralCooldown = 0;
        Main.NewText("You are using Exxo Avalon: Origins " + Avalon.Mod.Version);
        Main.NewText("Please note that Exxo Avalon: Origins is in beta - it will have many bugs");
        Main.NewText("Please also note that Exxo Avalon: Origins will interact strangely with other large mods");
    }

    public override void UpdateEquips()
    {
        //Main.NewText(Main.ScreenSize.Y);
        if (tomeItem.stack > 0)
        {
            Player.VanillaUpdateEquip(tomeItem);
        }
    }

    public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
                               int type, int damage, float knockback)
    {
        #region spike cannon logic
        if (item.type == ModContent.ItemType<SpikeCannon>()) //  || item.type == ModContent.ItemType<SpikeRailgun>()
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
                if (Player.inventory[Player.selectedItem].useAmmo == ItemID.Spike)
                {
                    int t = 0;
                    int dmgAdd = 0;
                    if (item2.type == ItemID.Spike)
                    {
                        t = ModContent.ProjectileType<Projectiles.Ranged.SpikeCannon>();
                        dmgAdd = 11;
                    }
                    else if (item2.type == ModContent.ItemType<Items.Placeable.Tile.DemonSpikeScale>())
                    {
                        t = ModContent.ProjectileType<Projectiles.Ranged.DemonSpikeScale>();
                        dmgAdd = 17;
                    }
                    else if (item2.type == ModContent.ItemType<Items.Placeable.Tile.BloodiedSpike>())
                    {
                        t = ModContent.ProjectileType<Projectiles.Ranged.BloodiedSpike>();
                        dmgAdd = 17;
                    }
                    else if (item2.type == ModContent.ItemType<Items.Placeable.Tile.NastySpike>())
                    {
                        t = ModContent.ProjectileType<Projectiles.Ranged.NastySpike>();
                        dmgAdd = 18;
                    }
                    else if (item2.type == ItemID.WoodenSpike)
                    {
                        t = ModContent.ProjectileType<Projectiles.Ranged.WoodenSpike>();
                        dmgAdd = 30;
                    }
                    else if (item2.type == ModContent.ItemType<Items.Placeable.Tile.VenomSpike>())
                    {
                        t = ModContent.ProjectileType<Projectiles.Ranged.VenomSpike>();
                        dmgAdd = 39;
                    }
                    else if (item2.type == ModContent.ItemType<Items.Placeable.Tile.PoisonSpike>())
                    {
                        t = ModContent.ProjectileType<Projectiles.Ranged.PoisonSpike>();
                        dmgAdd = 15;
                    }

                    if (t > 0)
                    {
                        //if (item.type == ModContent.ItemType<SpikeRailgun>())
                        //{
                        //    float num87 = MathHelper.Pi / 10;
                        //    int num88 = 3;
                        //    Vector2 vector2 = velocity;
                        //    vector2.Normalize();
                        //    vector2 *= 40f;
                        //    for (int num89 = 0; num89 < num88; num89++)
                        //    {
                        //        float num90 = num89 - ((num88 - 1f) / 2f);
                        //        Vector2 vector3 = vector2.Rotate(num87 * num90);
                        //        int num91 = Projectile.NewProjectile(
                        //            Player.GetSource_ItemUse_WithPotentialAmmo(
                        //                ModContent.GetInstance<SpikeRailgun>().Item,
                        //                ModContent.GetInstance<SpikeRailgun>().Item.ammo), position.X + vector3.X,
                        //            position.Y + vector3.Y, velocity.X, velocity.Y, t, damage + dmgAdd, knockback,
                        //            Player.whoAmI);
                        //    }
                        //    return false;
                        //}

                        Projectile.NewProjectile(
                            Player.GetSource_ItemUse_WithPotentialAmmo(
                                ModContent.GetInstance<SpikeCannon>().Item,
                                ModContent.GetInstance<SpikeCannon>().Item.ammo), position,
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
                    if (TorchLauncher.TorchProjectile.TryGetValue(item2.type, out int t))
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
        #region terra blade nerf
        if (item.type == ItemID.TerraBlade)
        {
            damage = (int)((int)(Player.GetDamage(DamageClass.Melee).ApplyTo(item.damage)) * 1.25f);
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Player.whoAmI);
            return false;
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
        if (Player.meleeEnchant == 9)
        {
            target.AddBuff(ModContent.BuffType<Virulent>(), 60 * 9);
        }
        if (crit)
        {
            if (Main.rand.NextBool(8) && Player.whoAmI == Main.myPlayer && reckoningTimeLeft > 0 && reckoningLevel < 10)
            {
                reckoningLevel++;
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

        if (roseMagic && proj.DamageType == DamageClass.Magic && Main.rand.NextBool(8) && roseMagicCooldown <= 0 && target.lifeMax > 5 && target.type != NPCID.TargetDummy)
        {
            int num36 = Item.NewItem(Player.GetSource_OnHit(target), (int)target.position.X,
                (int)target.position.Y, target.width, target.height, ModContent.ItemType<Rosebud>());
            Main.item[num36].velocity.Y = Main.rand.Next(-20, 1) * 0.2f;
            Main.item[num36].velocity.X = Main.rand.Next(10, 31) * 0.2f * Player.direction;
            roseMagicCooldown = 20;
        }



        if (crit)
        {
            if (Main.rand.NextBool(8) && Player.whoAmI == Main.myPlayer && reckoningTimeLeft > 0 && reckoningLevel < 10)
            {
                reckoningLevel++;
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
                    reckoningLevel++;
                }
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

        if (target.HasBuff(ModContent.BuffType<Virulent>()) && crit)
        {
            damage += MultiplyCritDamage(damage, 2f);
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

        if (target.HasBuff(ModContent.BuffType<Virulent>()) && crit)
        {
            //int bonusDmg = -damage;
            //bonusDmg += (int)(damage * (2.5f + 1f) / 2);
            damage += MultiplyCritDamage(damage, 2f);
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

    public override void SaveData(TagCompound tag)
    {
        tag["CrystalHealth"] = CrystalHealth;
        tag["SHMAcc"] = shmAcc;
        tag["SpiritPoppyUseCount"] = spiritPoppyUseCount;
    }

    public override void LoadData(TagCompound tag)
    {
        if (tag.ContainsKey("CrystalHealth"))
        {
            CrystalHealth = tag.Get<int>("CrystalHealth");
            Player.statLifeMax += CrystalHealth * 25;
            Player.statLifeMax2 += CrystalHealth * 25;
            Player.statLife += CrystalHealth * 25;
        }
        if (tag.ContainsKey("SHMAcc"))
        {
            shmAcc = tag.Get<bool>("SHMAcc");
        }
        if (tag.ContainsKey("SpiritPoppyUseCount"))
        {
            spiritPoppyUseCount = tag.Get<int>("SpiritPoppyUseCount");
        }

    }
    public override void PostUpdate()
    {
        //Main.worldRate = 7;
        //Main.NewText(Player.position.Y);
        //if (Player.GetModPlayer<ExxoBiomePlayer>().ZoneSkyFortress && Player.ZoneSkyHeight)
        //{
        //    //Main.NewText(Player.gravity);

        //    // CODE FOR ARMA MAYBE?

        //    //float num39 = Main.maxTilesX / 4200;
        //    //num39 *= num39;
        //    //float gravity = (float)((double)(Player.position.Y / 16f - (60f + 10f * num39)) / (Main.worldSurface / 6.0));
        //    //Main.NewText(-gravity);
        //    //Player.velocity.Y += -gravity * 3;

        //    // END

        //}
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
                    Player.GetDamage(DamageClass.Summon) += 0.04f;
                    break;
                case 2:
                    Player.GetDamage(DamageClass.Summon) += 0.08f;
                    break;
                case 3:
                    Player.GetDamage(DamageClass.Summon) += 0.12f;
                    break;
                case 4:
                    Player.GetDamage(DamageClass.Summon) += 0.16f;
                    break;
                case 5:
                    Player.GetDamage(DamageClass.Summon) += 0.2f;
                    break;
                case 6:
                    Player.GetDamage(DamageClass.Summon) += 0.24f;
                    break;
                case 7:
                    Player.GetDamage(DamageClass.Summon) += 0.28f;
                    break;
                case 8:
                    Player.GetDamage(DamageClass.Summon) += 0.32f;
                    break;
                case 9:
                    Player.GetDamage(DamageClass.Summon) += 0.36f;
                    break;
                case 10:
                    Player.GetDamage(DamageClass.Summon) += 0.45f;
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
            SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/Stomp"), Player.position);
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
                if (Main.tile[xpos, ypos].TileType == (ushort)ModContent.TileType<TritanoriumOre>() || Main.tile[xpos, ypos].TileType == (ushort)ModContent.TileType<PyroscoricOre>())
                {
                    if (!Player.GetModPlayer<ExxoEquipEffectPlayer>().LuckTome && !Player.GetModPlayer<ExxoEquipEffectPlayer>().BlahWings)
                    {
                        Player.AddBuff(ModContent.BuffType<Melting>(), 60);
                    }
                }
            }
        }


        if (Player.GetModPlayer<ExxoBiomePlayer>().ZoneFlight)
        {
            Player.slowFall = true; // doesn't work
        }

        if (Player.GetModPlayer<ExxoBiomePlayer>().ZoneFright)
        {
            Player.statDefense += 5;
        }

        if (Player.GetModPlayer<ExxoBiomePlayer>().ZoneIceSoul)
        {
            Player.GetModPlayer<ExxoEquipEffectPlayer>().SlimeBand = true; // doesn't work
        }

        if (Player.GetModPlayer<ExxoBiomePlayer>().ZoneMight)
        {
            Player.GetDamage(DamageClass.Generic) += 0.06f;
        }

        if (Player.GetModPlayer<ExxoBiomePlayer>().ZoneNight)
        {
            Player.wolfAcc = true;
        }

        if (Player.GetModPlayer<ExxoBiomePlayer>().ZoneTime)
        {
            Player.accWatch = 3;
        }

        if (Player.GetModPlayer<ExxoBiomePlayer>().ZoneTorture)
        {
            Player.GetCritChance(DamageClass.Generic) += 6;
        }

        if (Player.GetModPlayer<ExxoBiomePlayer>().ZoneSight)
        {
            Player.detectCreature = Player.dangerSense = Player.nightVision = true;
        }

        if (Player.GetModPlayer<ExxoBiomePlayer>().ZoneDelight)
        {
            Player.lifeRegen += 3;
        }

        if (Player.GetModPlayer<ExxoBiomePlayer>().ZoneHumidity)
        {
            Player.resistCold = true;
        }

        if (Player.GetModPlayer<ExxoBiomePlayer>().ZoneBlight)
        {
            Player.GetArmorPenetration(DamageClass.Generic) += 10;
        }

        if (Player.tongued)
        {
            bool flag21 = false;
            if (AvalonWorld.WallOfSteel >= 0)
            {
                float num159 = Main.npc[AvalonWorld.WallOfSteel].position.X +
                               (Main.npc[AvalonWorld.WallOfSteel].width / 2);
                num159 += Main.npc[AvalonWorld.WallOfSteel].direction * 200;
                float num160 = Main.npc[AvalonWorld.WallOfSteel].position.Y +
                               (Main.npc[AvalonWorld.WallOfSteel].height / 2);
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

        if (spectrumBlur)
        {
            Player.eocDash = 1;
        }
    }

    public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit,
                                 ref bool customDamage, ref bool playSound, ref bool genGore,
                                 ref PlayerDeathReason damageSource, ref int cooldownCounter) =>
        //if (Avalon.GodMode)
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



    public override void PostUpdateMiscEffects()
    {
        WOSTongue();
        Player.statManaMax2 = actualStatManaMax2;
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
        if (Player.GetModPlayer<ExxoBiomePlayer>().ZoneSkyFortress)
        {
            float num39 = Main.maxTilesX / 4200;
            num39 *= num39;
            float gravity = (float)((double)(Player.position.Y / 16f - (60f + 10f * num39)) / (Main.worldSurface / 6.0));
            if ((double)gravity < 0.25)
            {
                gravity = 0.25f;
            }
            if (gravity > 1f)
            {
                gravity = 1f;
            }
            Player.gravity /= gravity;
        }
        WOSTongue();
        tpStam = !teleportV;
        if (teleportV)
        {
            teleportV = false;
            teleportVWasTriggered = true;
        }

        Player.breathMax = 200;
        if (Player.GetModPlayer<ExxoBiomePlayer>().ZoneFlight)
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
        if (KeybindSystem.QuintupleHotkey.JustPressed)
        {
            quintJump = !quintJump;
            Main.NewText(!quintJump ? "Quintuple Jump Off" : "Quintuple Jump On");
        }

        //if (KeybindSystem.BubbleBoostHotkey.JustPressed)
        //{
        //    activateBubble = !activateBubble;
        //    Main.NewText(!activateBubble ? "Bubble Boost Off" : "Bubble Boost On");
        //}
        #endregion
        if ((Player.inventory[Player.selectedItem].type == ModContent.ItemType<AccelerationDrill>() ||
            Player.inventory[Player.selectedItem].type == ModContent.ItemType<AccelerationPickaxe>()) &&
            KeybindSystem.ModeChangeHotkey.JustPressed)
        {
            AccelerationSpeed = !AccelerationSpeed;
            if (!AccelerationSpeed)
            {
                Main.NewText("Acceleration Drill Mode: Normal");
            }
            else
            {
                Main.NewText("Acceleration Drill Mode: Speed");
            }
        }

        if (Main.netMode != NetmodeID.SinglePlayer &&
            Player.inventory[Player.selectedItem].type == ModContent.ItemType<TeamMirror>() &&
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

    public override void UpdateDead() => jumpAgainQuack = false;

    public override void PostUpdateRunSpeeds()
    {
        //Main.NewText("PostUpdateRunSpeeds " + slimeBand.ToString());
        FloorVisualsAvalon(Player.velocity.Y > Player.gravity);
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

    public void WOSTongue()
    {
        if (AvalonWorld.WallOfSteel >= 0 && Main.npc[AvalonWorld.WallOfSteel].active)
        {
            float num = Main.npc[AvalonWorld.WallOfSteel].position.X + 40f;
            if (Main.npc[AvalonWorld.WallOfSteel].direction > 0)
            {
                num -= 96f;
            }
            if (Player.position.X + Player.width > num && Player.position.X < num + 140f && Player.gross)
            {
                Player.noKnockback = false;
                Player.Hurt(PlayerDeathReason.ByNPC(AvalonWorld.WallOfSteel), 50,
                    Main.npc[AvalonWorld.WallOfSteel].direction);
            }

            if (!Player.gross && Player.position.Y > (Main.maxTilesY - 250) * 16 && Player.position.X > num - 1920f &&
                Player.position.X < num + 1920f)
            {
                Player.AddBuff(37, 10);
                Player.gross = true;
                //Main.PlaySound(4, (int)Main.npc[AvalonWorld.wos].position.X, (int)Main.npc[AvalonWorld.wos].position.Y, 10);
            }

            if (Player.gross)
            {
                if (Player.position.Y < (Main.maxTilesY - 200) * 16)
                {
                    Player.AddBuff(38, 10);
                }

                if (Main.npc[AvalonWorld.WallOfSteel].direction < 0)
                {
                    if (Player.position.X + (Player.width / 2) > Main.npc[AvalonWorld.WallOfSteel].position.X +
                        (Main.npc[AvalonWorld.WallOfSteel].width / 2) + 40f)
                    {
                        Player.AddBuff(38, 10);
                    }
                }
                else if (Player.position.X + (Player.width / 2) < Main.npc[AvalonWorld.WallOfSteel].position.X +
                         (Main.npc[AvalonWorld.WallOfSteel].width / 2) - 40f)
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
                float num2 = Main.npc[AvalonWorld.WallOfSteel].position.X +
                    (Main.npc[AvalonWorld.WallOfSteel].width / 2) - vector.X;
                float num3 = Main.npc[AvalonWorld.WallOfSteel].position.Y +
                    (Main.npc[AvalonWorld.WallOfSteel].height / 2) - vector.Y;
                float num4 = (float)Math.Sqrt((num2 * num2) + (num3 * num3));
                if (num4 > 3000f)
                {
                    Player.lastDeathPostion = Player.position;
                    Player.KillMe(PlayerDeathReason.ByNPC(AvalonWorld.WallOfSteel), 1000.0, 0);
                    return;
                }

                if (Main.npc[AvalonWorld.WallOfSteel].position.X < 608f ||
                    Main.npc[AvalonWorld.WallOfSteel].position.X > (Main.maxTilesX - 38) * 16)
                {
                    Player.lastDeathPostion = Player.position;
                    Player.KillMe(PlayerDeathReason.ByNPC(AvalonWorld.WallOfSteel), 1000.0, 0);
                }
            }
        }
    }

    public int MultiplyCritDamage(int dmg, float mult = 0f) // dmg = damage before crit application
    {
        int bonusDmg = -dmg;
        bonusDmg += (int)(dmg * ((mult == 0f ? CritDamageMult : mult) + 1f) / 2);
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

            if (Player.GetModPlayer<ExxoEquipEffectPlayer>().SlimeBand || Player.GetModPlayer<ExxoBiomePlayer>().ZoneIceSoul)
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

    public bool reflectorMinion;

    public bool skyBlessing;

    // end minion stuff
    public bool[] pSensor = new bool[6];
    public int spiritPoppyUseCount;
    public bool shmAcc = false;
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
    public bool DarkMatterMonolith { get; set; }
    public int DarkMatterTimeOut = 20;
    public bool quackJump;
    public bool jumpAgainQuack;
    public bool armorStealth;
    public int shadowCheckPointNum = 0;
    public int shadowPlayerNum = 0;
    public int infectTimer = 0;
    public int infectDmg = 0;

    public int CrystalHealth;
    public Item tomeItem = new();
    public int pl = -1;
    public bool openLocks;

    public bool defDebuff;
    public int defDebuffBonusDef;
    public float rot;
    public byte qsMode = 1;
    public byte qsTimer;
    public bool qsDone;
    public bool qsIsNDown;

    public bool AccelerationSpeed;
    public bool activateBubble;

    public bool quintJump;
    public bool shadowRing;
    public static bool SpawnDL = false;
    public bool FleshArmor;
    public int teleportToPlayer = -1;
    public bool dashIntoMob;
    public bool dashTemp;
    public bool rocketJumpRO = true;


    public byte staminaCD3;

    public bool shadowTele;
    public bool teleportV;
    public bool tpStam = true;
    public int tpCD;

    public int baseUseTime;
    public int baseUseAnim;
    public bool oldLeftClick;
    public bool oblivionKill;
    public bool goBerserk;
    public bool splitProj;
    public bool spectrumSpeed;
    public bool spectrumBlur;
    public bool minionFreeze;
    public bool thornMagic;
    public bool roseMagic;
    public int roseMagicCooldown;
    public bool avalonRestoration;
    public bool avalonRetribution;
    public int deliriumDuration = 300;
    public int quadroCount;
    public int shadowWP;
    public bool shadowRO;
    public bool isNDown;
    public bool magnet;
    public Item tome;
    public bool ghostSilence;
    public bool meleeStealth;

    public bool ammoCost70;
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
    public bool noSticky;
    public bool riftGoggles;
    public bool malaria;
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
    public bool HookBonus;

    public Vector2 MousePosition;

    #region Dragon's Bondage AI vars
    public bool dragonsBondage;
    #endregion Dragon's Bondage AI vars


    private int gemCount;
    public bool[] ownedLargeGems = new bool[10];

    // Crit damage multiplyer vars
    public float CritDamageMult = 1f;


    #endregion fields
}
