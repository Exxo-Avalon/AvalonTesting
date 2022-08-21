using System;
using System.Collections.Generic;
using Avalon.Buffs;
using Avalon.Buffs.AdvancedBuffs;
using Avalon.Items.Accessories;
using Avalon.Items.AdvancedPotions;
using Avalon.Items.Ammo;
using Avalon.Items.Consumables;
using Avalon.Items.Material;
using Avalon.Items.Ore;
using Avalon.Items.Placeable.Seed;
using Avalon.Items.Potions;
using Avalon.Network;
using Avalon.Players;
using Avalon.Prefixes;
using Avalon.Systems;
using Avalon.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Avalon;

public class AvalonGlobalItem : GlobalItem
{
    private static readonly int NewMaskPrice = Item.sellPrice(0, 2);

    private static readonly List<int> nonSolidExceptions = new()
    {
        TileID.Cobweb,
        TileID.LivingCursedFire,
        TileID.LivingDemonFire,
        TileID.LivingFire,
        TileID.LivingFrostFire,
        TileID.LivingIchor,
        TileID.LivingUltrabrightFire,
        TileID.ChimneySmoke,
        TileID.Bubble,
        TileID.Rope,
        TileID.SilkRope,
        TileID.VineRope,
        TileID.WebRope,
        ModContent.TileType<LivingLightning>(),
        ModContent.TileType<VineRope>(),
    };
    public override void SetDefaults(Item item)
    {
        if (item.IsArmor())
        {
            ItemID.Sets.CanGetPrefixes[item.type] = true;
        }

        if (item.GetGlobalItem<AvalonGlobalItemInstance>().Tome)
        {
            item.accessory = true;
        }

        if (item.accessory)
        {
            item.canBePlacedInVanityRegardlessOfConditions = true;
            if (item.GetGlobalItem<AvalonGlobalItemInstance>().Tome)
            {
                item.canBePlacedInVanityRegardlessOfConditions = false;
            }
        }

        switch (item.type)
        {
            case ItemID.Mushroom:
                item.potion = false;
                item.healLife = 0;
                item.useAnimation = 15;
                item.useTime = 10;
                item.useTurn = item.autoReuse = true;
                item.createTile = ModContent.TileType<MushroomTile>();
                item.useStyle = ItemUseStyleID.Swing;
                item.UseSound = null;
                break;
            case ItemID.EnchantedBoomerang:
                item.rare = ItemRarityID.Green;
                break;
            case ItemID.RottenChunk:
                item.useStyle = ItemUseStyleID.Swing;
                item.useAnimation = 15;
                item.useTime = 10;
                item.consumable = true;
                item.useTurn = true;
                item.autoReuse = true;
                item.createTile = ModContent.TileType<RottenChunk>();
                break;
            case ItemID.ShadowScale:
                item.useTurn = true;
                item.useStyle = ItemUseStyleID.Swing;
                item.useAnimation = 15;
                item.useTime = 10;
                item.autoReuse = true;
                item.consumable = true;
                item.createTile = ModContent.TileType<ShadowScale>();
                break;
            case ItemID.NightmarePickaxe:
                item.pick = 60;
                break;
            case ItemID.LesserManaPotion:
                item.maxStack = 50;
                break;
            case ItemID.Spike:
            case ItemID.WoodenSpike:
                item.ammo = ItemID.Spike;
                item.notAmmo = true;
                break;
            case ItemID.Hellstone:
                item.value = Item.sellPrice(0, 0, 13, 30);
                break;
            case ItemID.ManaPotion:
                item.maxStack = 75;
                break;
            case ItemID.Vine:
                item.useStyle = ItemUseStyleID.Swing;
                item.useTurn = true;
                item.useAnimation = 15;
                item.useTime = 8;
                item.autoReuse = true;
                item.consumable = true;
                item.createTile = ModContent.TileType<VineRope>();
                item.tileBoost += 3;
                break;
            case ItemID.LesserRestorationPotion:
                item.rare = ItemRarityID.Green;
                item.maxStack = 40;
                break;
            case ItemID.RestorationPotion:
                item.rare = ItemRarityID.Green;
                item.maxStack = 40;
                break;
            case 4695:
            case 4696:
            case 4697:
            case 4698:
            case 4699:
            case 4700:
                item.rare = ItemRarityID.Blue;
                break;
            // case ItemID.Goldfish:
            //    item.makeNPC = (short)ModContent.NPCType<NPCs.ImpactWizard>();
            //    break;
            case ItemID.DivingHelmet:
                item.value = 10000;
                break;
            case ItemID.MagicPowerPotion:
                item.buffTime = 18000;
                break;
            case ItemID.ThornsPotion:
                item.buffTime = 10800;
                break;
            case ItemID.GravitationPotion:
                item.buffTime = 21600;
                break;
            case ItemID.GoblinBattleStandard:
                item.maxStack = 20;
                break;
            case ItemID.PixieDust:
                item.value = 1000;
                break;
            case ItemID.Flamethrower:
                item.damage = 35;
                break;
            case ItemID.SnowGlobe:
                item.maxStack = 20;
                break;
            case ItemID.RainbowBrick:
            case ItemID.RainbowBrickWall:
                item.rare = ItemRarityID.Blue;
                break;
            case ItemID.UnholyTrident:
                item.autoReuse = true;
                break;
            case ItemID.FrostHelmet:
                item.defense = 12;
                break;
            case ItemID.FrostBreastplate:
                item.defense = 22;
                break;
            case ItemID.FrostLeggings:
                item.defense = 15;
                break;
            case ItemID.AmethystStaff:
                item.damage = 15;
                break;
            case ItemID.TopazStaff:
                item.damage = 16;
                break;
            case ItemID.SapphireStaff:
                item.damage = 19;
                break;
            case ItemID.EmeraldStaff:
                item.damage = 20;
                break;
            case ItemID.RubyStaff:
                item.damage = 22;
                break;
            case ItemID.DiamondStaff:
                item.damage = 25;
                break;
            case ItemID.TerraBlade:
                item.noMelee = false;
                item.UseSound = SoundID.Item1;
                break;
            case ItemID.DeathbringerPickaxe:
                item.pick = 64;
                break;
            case ItemID.BlackBelt:
                item.value = 150000;
                break;
            case ItemID.ChlorophyteMask:
                item.defense = 27;
                break;
            case ItemID.ChlorophyteHelmet:
                item.defense = 15;
                break;
            case ItemID.ChlorophyteHeadgear:
                item.defense = 9;
                break;
            case ItemID.ChlorophytePlateMail:
                item.defense = 20;
                break;
            case ItemID.ChlorophyteGreaves:
                item.defense = 15;
                break;
            case ItemID.PossessedHatchet:
                item.shootSpeed = 16f;
                item.useAnimation = 12;
                item.useTime = 12;
                break;
            case ItemID.WaspGun:
                item.damage = 23;
                break;
            case ItemID.SniperRifle:
                item.value = Item.sellPrice(0, 7);
                break;
            case ItemID.Uzi:
                item.value = 250000;
                break;
            case ItemID.Picksaw:
                item.tileBoost++;
                break;
            case ItemID.HeatRay:
                item.mana = 5;
                break;
            case ItemID.TheAxe:
                item.hammer = 95;
                break;
            case ItemID.SlimeStaff:
                item.damage = 11;
                break;
            case ItemID.PirateMap:
                item.maxStack = 20;
                break;
            case ItemID.TurtleHelmet:
                item.defense = 24;
                break;
            case ItemID.TurtleScaleMail:
                item.defense = 29;
                break;
            case ItemID.TurtleLeggings:
                item.defense = 20;
                break;
            case ItemID.BonePickaxe:
                item.pick = 55;
                break;
            case ItemID.Vertebrae:
                item.useStyle = ItemUseStyleID.Swing;
                item.useAnimation = 15;
                item.useTime = 10;
                item.consumable = true;
                item.useTurn = true;
                item.autoReuse = true;
                item.createTile = ModContent.TileType<Vertebrae>();
                break;
            case ItemID.FlaskofGold:
                item.value = 108000;
                break;
            case ItemID.Ectoplasm:
                item.useStyle = ItemUseStyleID.Swing;
                item.useAnimation = 15;
                item.useTime = 10;
                item.consumable = true;
                item.useTurn = true;
                item.autoReuse = true;
                item.createTile = ModContent.TileType<Ectoplasm>();
                break;
            case ItemID.AnkhCharm:
                item.value = Item.sellPrice(0, 10);
                break;
            case ItemID.AnkhShield:
                item.value = Item.sellPrice(0, 13);
                break;
            case ItemID.Coal:
                item.value = 5;
                item.rare = -1;
                break;
            case ItemID.ShroomiteDiggingClaw:
                item.pick = 205;
                break;
            case ItemID.BeetleHelmet:
                item.defense = 25;
                break;
            case ItemID.BeetleScaleMail:
                item.defense = 22;
                break;
            case ItemID.BeetleShell:
                item.defense = 33;
                break;
            case ItemID.BeetleLeggings:
                item.defense = 19;
                break;
            case ItemID.SuperManaPotion:
                item.maxStack = 150;
                break;
            case ItemID.LifeforcePotion:
                item.rare = ItemRarityID.Orange;
                break;
            case ItemID.RagePotion:
                item.rare = ItemRarityID.Green;
                break;
            case ItemID.InfernoPotion:
                item.rare = ItemRarityID.Orange;
                break;
            case ItemID.TeleportationPotion:
                item.rare = ItemRarityID.Green;
                break;
            case ItemID.FishingPotion:
                item.value = 1000;
                break;
            case ItemID.SonarPotion:
                item.buffTime = 19600;
                break;
            case ItemID.CratePotion:
                item.buffTime = 28800;
                break;
            case ItemID.AnglerHat:
                item.defense = 3;
                break;
            case ItemID.AnglerVest:
                item.defense = 5;
                break;
            case ItemID.AnglerPants:
                item.defense = 4;
                break;
            case ItemID.SpiderMask:
                item.defense = 6;
                break;
            case ItemID.SpiderBreastplate:
                item.defense = 9;
                break;
            case ItemID.SpiderGreaves:
                item.defense = 8;
                break;
            case ItemID.Meowmere:
                item.damage = 145;
                break;
            case ItemID.SDMG:
                item.damage = 49;
                break;
            case ItemID.StarWrath:
                item.damage = 85;
                break;
            case ItemID.LastPrism:
                item.damage = 72;
                break;
            case ItemID.Terrarian:
                item.damage = 144;
                break;
            case ItemID.LunarFlareBook:
                item.damage = 75;
                break;
            case ItemID.RainbowCrystalStaff:
                item.damage = 120;
                break;
        }

        if (ItemID.Sets.Torches[item.type])
        {
            item.ammo = ItemID.Torch;
            item.notAmmo = true;
        }

        if (Data.Sets.Item.StackTo2000[item.type])
        {
            item.maxStack = 2000;
        }

        if (Data.Sets.Item.StackTo999[item.type])
        {
            item.maxStack = 999;
        }

        if (Data.Sets.Item.StackTo100[item.type])
        {
            item.maxStack = 100;
        }

        // Alters vanilla setting cost of masks in Item.SetDefaultsX from default
        // this.value = Item.sellPrice(0, 0, 75);
        switch (item.type)
        {
            case ItemID.DeerclopsMask:
            case ItemID.QueenSlimeMask:
            case ItemID.FairyQueenMask:
            case ItemID.BossMaskOgre:
            case ItemID.BossMaskDarkMage:
            case ItemID.BossMaskBetsy:
            case ItemID.BossMaskMoonlord:
            case ItemID.BossMaskCultist:
            case ItemID.DukeFishronMask:
            case ItemID.KingSlimeMask:
            case ItemID.SpiderMask:
            case ItemID.SkeletronMask:
                item.value = NewMaskPrice;
                break;
            default:
                if (item.type is >= ItemID.BrainMask and <= ItemID.DestroyerMask)
                {
                    item.value = NewMaskPrice;
                }

                break;
        }
    }

    public override void PostUpdate(Item item)
    {
        if (item.lavaWet && item.position.Y / 16 > Main.maxTilesY - 190)
        {
            if (item.type == ModContent.ItemType<HellboundRemote>() && Main.hardMode &&
                ModContent.GetInstance<DownedBossSystem>().DownedPhantasm)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    item.active = false;
                    item.type = ItemID.None;
                    item.stack = 0;
                    if (Main.hardMode && ModContent.GetInstance<DownedBossSystem>().DownedPhantasm)
                    {
                        AvalonGlobalNPC.SpawnWOS(item.position);
                        SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/WoS"), item.position);
                    }

                    NetMessage.SendData(Terraria.ID.MessageID.SyncItem, -1, -1, NetworkText.Empty, item.whoAmI);
                }
            }
        }
    }
    public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
    {
        if (item.GetGlobalItem<AvalonGlobalItemInstance>().Tome && slot < 19 && !modded)
        {
            return false;
        }

        return !item.IsArmor() && base.CanEquipAccessory(item, player, slot, modded);
    }

    public override void PickAmmo(Item weapon, Item ammo, Player player, ref int type,
                                  ref float speed, ref StatModifier damage, ref float knockback)
    {
        if (ammo.ammo == AmmoID.Arrow && speed < 20f && player.HasBuff<AdvArchery>())
        {
            speed *= 1 + AdvArchery.PercentageIncrease;
            speed = MathHelper.Min(speed, 20f);
        }

        base.PickAmmo(weapon, ammo, player, ref type, ref speed, ref damage, ref knockback);
    }

    public override bool? UseItem(Item item, Player player)
    {
        if (player.Avalon().cloudGloves && player.whoAmI == Main.myPlayer)
        {
            bool inrange =
                (player.position.X / 16f) - Player.tileRangeX - player.inventory[player.selectedItem].tileBoost -
                player.blockRange <= Player.tileTargetX &&
                ((player.position.X + player.width) / 16f) + Player.tileRangeX +
                player.inventory[player.selectedItem].tileBoost - 1f + player.blockRange >= Player.tileTargetX &&
                (player.position.Y / 16f) - Player.tileRangeY - player.inventory[player.selectedItem].tileBoost -
                player.blockRange <= Player.tileTargetY &&
                ((player.position.Y + player.height) / 16f) + Player.tileRangeY +
                player.inventory[player.selectedItem].tileBoost - 2f + player.blockRange >= Player.tileTargetY;
            if (item.createTile > -1 &&
                (Main.tileSolid[item.createTile] || nonSolidExceptions.Contains(item.createTile)) &&
                (Main.tile[Player.tileTargetX, Player.tileTargetY].LiquidType != LiquidID.Lava ||
                 player.HasItemInArmor(ModContent.ItemType<ObsidianGlove>())) &&
                !Main.tile[Player.tileTargetX, Player.tileTargetY].HasTile && inrange)
            {
                bool subtractFromStack = WorldGen.PlaceTile(Player.tileTargetX, Player.tileTargetY, item.createTile);
                if (Main.tile[Player.tileTargetX, Player.tileTargetY].HasTile &&
                    Main.netMode != NetmodeID.SinglePlayer && subtractFromStack)
                {
                    NetMessage.SendData(Terraria.ID.MessageID.TileManipulation, -1, -1, null, 1, Player.tileTargetX,
                        Player.tileTargetY, item.createTile);
                }

                if (subtractFromStack)
                {
                    item.stack--;
                }
            }

            if (item.createWall > 0 && Main.tile[Player.tileTargetX, Player.tileTargetY].WallType == 0 && inrange)
            {
                WorldGen.PlaceWall(Player.tileTargetX, Player.tileTargetY, item.createWall);
                if (Main.tile[Player.tileTargetX, Player.tileTargetY].WallType != 0 &&
                    Main.netMode != NetmodeID.SinglePlayer)
                {
                    NetMessage.SendData(Terraria.ID.MessageID.TileManipulation, -1, -1, null, 3, Player.tileTargetX,
                        Player.tileTargetY, item.createWall);
                }

                //Main.PlaySound(0, Player.tileTargetX * 16, Player.tileTargetY * 16, 1);
                item.stack--;
            }
        }

        return base.UseItem(item, player);
    }

    public override void HoldItem(Item item, Player player)
    {
        #region wire disable in sky fortress
        var tempWireItem = new Item();
        tempWireItem.netDefaults(item.netID);
        tempWireItem = tempWireItem.CloneWithModdedDataFrom(item);
        tempWireItem.stack = item.stack;
        if (player.GetModPlayer<ExxoBiomePlayer>().ZoneSkyFortress &&
            !ModContent.GetInstance<DownedBossSystem>().DownedDragonLord)
        {
            player.InfoAccMechShowWires = false;
            if (item.mech)
            {
                item.mech = false;
                item.useStyle = 0;
                item.GetGlobalItem<AvalonGlobalItemInstance>().WasWiring = true;
            }
        }

        if (item.GetGlobalItem<AvalonGlobalItemInstance>().WasWiring &&
            !player.GetModPlayer<ExxoBiomePlayer>().ZoneSkyFortress)
        {
            item.netDefaults(tempWireItem.netID);
            item.stack = tempWireItem.stack;
            item.GetGlobalItem<AvalonGlobalItemInstance>().WasWiring = false;
        }
        #endregion wire disable in sky fortress

        #region wire disable in hellcastle pre-phantasm
        var tempWireItemHC = new Item();
        tempWireItemHC.netDefaults(item.netID);
        tempWireItemHC = tempWireItemHC.CloneWithModdedDataFrom(item);
        tempWireItemHC.stack = item.stack;
        if (player.GetModPlayer<ExxoBiomePlayer>().ZoneNearHellcastle &&
            !ModContent.GetInstance<DownedBossSystem>().DownedPhantasm)
        {
            player.InfoAccMechShowWires = false;
            if (item.mech)
            {
                item.mech = false;
                item.useStyle = 0;
                item.GetGlobalItem<AvalonGlobalItemInstance>().WasWiring = true;
            }
        }

        if (item.GetGlobalItem<AvalonGlobalItemInstance>().WasWiring &&
            !player.GetModPlayer<ExxoBiomePlayer>().ZoneNearHellcastle)
        {
            item.netDefaults(tempWireItemHC.netID);
            item.stack = tempWireItemHC.stack;
            item.GetGlobalItem<AvalonGlobalItemInstance>().WasWiring = false;
        }
        #endregion wire disable in hellcastle pre-phantasm

        #region barbaric prefix logic
        var tempItem = new Item();
        tempItem.netDefaults(item.netID);
        tempItem = tempItem.CloneWithModdedDataFrom(item);
        float kbDiff = 0f;
        if (item.prefix == PrefixID.Superior || item.prefix == PrefixID.Savage || item.prefix == PrefixID.Bulky ||
            item.prefix == PrefixID.Taboo || item.prefix == PrefixID.Celestial ||
            item.prefix == ModContent.PrefixType<Horrific>())
        {
            kbDiff = 0.1f;
        }
        else if (item.prefix == PrefixID.Forceful || item.prefix == PrefixID.Strong ||
                 item.prefix == PrefixID.Unpleasant ||
                 item.prefix == PrefixID.Godly || item.prefix == PrefixID.Heavy || item.prefix == PrefixID.Legendary ||
                 item.prefix == PrefixID.Intimidating || item.prefix == PrefixID.Staunch ||
                 item.prefix == PrefixID.Unreal ||
                 item.prefix == PrefixID.Furious || item.prefix == PrefixID.Mythical)
        {
            kbDiff = 0.15f;
        }
        else if (item.prefix == PrefixID.Broken || item.prefix == PrefixID.Weak || item.prefix == PrefixID.Shameful ||
                 item.prefix == PrefixID.Awkward)
        {
            kbDiff = -0.2f;
        }
        else if (item.prefix == PrefixID.Nasty || item.prefix == PrefixID.Ruthless || item.prefix == PrefixID.Unhappy ||
                 item.prefix == PrefixID.Light || item.prefix == PrefixID.Awful || item.prefix == PrefixID.Deranged ||
                 item.prefix == ModContent.PrefixType<Excited>())
        {
            kbDiff = -0.1f;
        }
        else if (item.prefix == PrefixID.Shoddy || item.prefix == PrefixID.Terrible)
        {
            kbDiff = -0.15f;
        }
        else if (item.prefix == PrefixID.Deadly || item.prefix == PrefixID.Masterful)
        {
            kbDiff = 0.05f;
        }

        item.knockBack = tempItem.knockBack * (1 + kbDiff);
        item.knockBack *= player.Avalon().bonusKB;
        #endregion barbaric prefix logic

        #region herb seed block swap
        if (Data.Sets.Item.HerbSeeds[item.type])
        {
            Vector2 mousePosition = Main.MouseWorld;
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                player.Avalon().MousePosition = mousePosition;
                CursorPosition.SendPacket(mousePosition, player.whoAmI);
            }
            else if (Main.netMode == NetmodeID.SinglePlayer)
            {
                player.Avalon().MousePosition = mousePosition;
            }

            Point mpTile = player.Avalon().MousePosition.ToTileCoordinates();

            if ((Main.tile[mpTile.X, mpTile.Y].TileType == TileID.BloomingHerbs ||
                 (Main.tile[mpTile.X, mpTile.Y].TileType == ModContent.TileType<Tiles.Herbs.Barfbush>() &&
                  Main.tile[mpTile.X, mpTile.Y].TileFrameX == 36) ||
                 (Main.tile[mpTile.X, mpTile.Y].TileType == ModContent.TileType<Tiles.Herbs.Bloodberry>() &&
                  Main.tile[mpTile.X, mpTile.Y].TileFrameX == 36) ||
                 (Main.tile[mpTile.X, mpTile.Y].TileType == ModContent.TileType<Tiles.Herbs.Sweetstem>() &&
                  Main.tile[mpTile.X, mpTile.Y].TileFrameX == 36) ||
                 (Main.tile[mpTile.X, mpTile.Y].TileType == ModContent.TileType<Tiles.Herbs.Holybird>() &&
                  Main.tile[mpTile.X, mpTile.Y].TileFrameX == 36)) &&
                (Main.tile[mpTile.X, mpTile.Y + 1].TileType == TileID.ClayPot ||
                 Main.tile[mpTile.X, mpTile.Y + 1].TileType == TileID.PlanterBox) && Main.mouseLeft)
            {
                WorldGen.KillTile(mpTile.X, mpTile.Y);
                if (!Main.tile[mpTile.X, mpTile.Y].HasTile && Main.netMode != NetmodeID.SinglePlayer)
                {
                    NetMessage.SendData(Terraria.ID.MessageID.TileManipulation, -1, -1, null, 0, mpTile.X, mpTile.Y);
                }

                WorldGen.PlaceTile(mpTile.X, mpTile.Y, item.createTile, style: item.placeStyle);
                if (Main.tile[mpTile.X, mpTile.Y].HasTile && Main.netMode != NetmodeID.SinglePlayer)
                {
                    NetMessage.SendData(Terraria.ID.MessageID.TileManipulation, -1, -1, null, 1, mpTile.X, mpTile.Y,
                        item.createTile, item.placeStyle);
                }

                item.stack--;
            }
        }
        #endregion herb seed block swap

        #region ancient minion guiding
        if (player.GetModPlayer<ExxoEquipEffectPlayer>().AncientMinionGuide)
        {
            if (item.DamageType == DamageClass.Summon && KeybindSystem.MinionGuidingHotkey.Current)
            {
                foreach (Projectile proj in Main.projectile)
                {
                    if (proj.owner == player.whoAmI && proj.minion)
                    {
                        float posX = Main.mouseX + Main.screenPosition.X - proj.Center.X;
                        float posY = Main.mouseY + Main.screenPosition.Y - proj.Center.Y;
                        if (player.gravDir == -1)
                        {
                            posY = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - proj.Center.Y;
                        }

                        proj.velocity.X = posX;
                        proj.velocity.Y = posY;
                    }
                }
            }
        }
        #endregion ancient minion guiding
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        TooltipLine? tooltipLine = tooltips.Find(x => x.Name == "ItemName" && x.Mod == "Terraria");
        TooltipLine? tooltipMat = tooltips.Find(x => x.Name == "Material" && x.Mod == "Terraria");
        TooltipLine? tooltipEquip = tooltips.Find(x => x.Name == "Equipable" && x.Mod == "Terraria");
        if (tooltipEquip != null)
        {
            if (item.GetGlobalItem<AvalonGlobalItemInstance>().Tome)
            {
                tooltips.RemoveAt(tooltips.FindIndex(x => x.Name == "Equipable" && x.Mod == "Terraria"));
            }
        }

        if (tooltipMat != null)
        {
            if (item.GetGlobalItem<AvalonGlobalItemInstance>().TomeMaterial)
            {
                tooltipMat.Text = Language.GetTextValue("Mods.Avalon.CommonItemTooltip.TomeMaterial");
            }
        }

        if (tooltipLine != null && ModContent.GetInstance<AvalonConfig>().VanillaRenames)
        {
            if (item.type == ItemID.CoinGun)
            {
                tooltipLine.Text = "Spend Shot";
            }
            if (item.type == ItemID.PurpleMucos)
            {
                tooltipLine.Text = "Purple Mucus";
            }
            if (item.type == ItemID.HighTestFishingLine)
            {
                tooltipLine.Text = tooltipLine.Text.Replace("Test", "Tensile");
            }
            if (item.type == ItemID.BlueSolution)
            {
                tooltipLine.Text = "Cyan Solution";
            }
            if (item.type == ItemID.DarkBlueSolution)
            {
                tooltipLine.Text = "Blue Solution";
            }
            if (item.type == ItemID.FrostsparkBoots)
            {
                tooltipLine.Text = tooltipLine.Text.Replace("Frostspark", "Sparkfrost");
            }
        }

        if (!item.social && PrefixLoader.GetPrefix(item.prefix) is ExxoPrefix exxoPrefix)
        {
            if (exxoPrefix.Category is PrefixCategory.Accessory or PrefixCategory.Custom)
            {
                tooltips.AddRange(exxoPrefix.TooltipLines);
            }
        }

        switch (item.type)
        {
            case ItemID.Vine:
                tooltips.Add(new TooltipLine(Mod, "Rope", "Can be climbed on"));
                break;
            case ItemID.DeathbringerPickaxe:
            case ItemID.NightmarePickaxe:
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (tooltip.Name == "Tooltip0")
                    {
                        tooltip.Text = "Can mine Rhodium, Osmium, and Iridium";
                    }
                }
                break;
            case ItemID.Seed:
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (tooltip.Name == "Tooltip0")
                    {
                        tooltip.Text = "For use with Blowpipes";
                    }
                }
                break;
            case ItemID.TempleKey:
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (tooltip.Name == "Tooltip0")
                    {
                        tooltip.Text = "Opens the lihzahrd temple door";
                    }
                }
                break;
            case ItemID.PoisonDart:
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (tooltip.Name == "Tooltip1")
                    {
                        tooltip.Text = "For use with Blowpipes and Blowgun";
                    }
                }
                break;
            case ItemID.CoinGun:
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (tooltip.Name == "Tooltip0")
                    {
                        tooltip.Text = "Uses coins for ammo - Higher valued coins do more damage";
                    }
                    if (tooltip.Name == "Tooltip1")
                    {
                        tooltip.Text = "'Knocks some cents into your enemies'";
                    }
                }
                break;
            case ItemID.PickaxeAxe:
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (tooltip.Name == "Tooltip0")
                    {
                        tooltip.Text = "'Not to be confused with a hamdrill'";
                    }
                    if (tooltip.Name == "Tooltip1")
                    {
                        tooltip.Text = "Can mine Chlorophyte, Xanthophyte, and Caesium Ore";
                    }
                }
                break;
            case ItemID.Drax:
                foreach (TooltipLine tooltip in tooltips)
                {
                    if (tooltip.Name == "Tooltip0")
                    {
                        tooltip.Text = "'Not to be confused with a picksaw'";
                    }
                    if (tooltip.Name == "Tooltip1")
                    {
                        tooltip.Text = "Can mine Chlorophyte, Xanthophyte, and Caesium Ore";
                    }
                }
                break;
        }
    }

    public override void UpdateVanity(Item item, Player player)
    {
        if (item.type == ItemID.HighTestFishingLine)
        {
            player.accFishingLine = true;
        }
    }


    public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
    {
        if (ItemID.Sets.BossBag[item.type])
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<StaminaCrystal>(), 4));
        }
        if (item.type == ItemID.KingSlimeBossBag)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<BandofSlime>(), 3));
        }
        if (item.type == ItemID.PlanteraBossBag)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<LifeDew>(), 1, 15, 22));
            itemLoot.Add(ItemDropRule.Common(ItemID.ChlorophyteOre, 1, 60, 121));
        }
    }


    public override void OpenVanillaBag(string context, Player player, int arg)
    {
        IEntitySource openItemSource = player.GetSource_OpenItem(arg);

        if (!context.Equals("bossBag", StringComparison.Ordinal))
        {
            return;
        }

        switch (arg)
        {
            case ItemID.EyeOfCthulhuBossBag:
            {
                if (ModContent.GetInstance<ExxoWorldGen>().WorldEvil == ExxoWorldGen.EvilBiome.Contagion)
                {
                    NPCLoader.blockLoot.Add(ItemID.UnholyArrow);
                    NPCLoader.blockLoot.Add(ItemID.DemoniteOre);
                    NPCLoader.blockLoot.Add(ItemID.CorruptSeeds);

                    int randomQuantity = Main.rand.Next(20) + 10;
                    randomQuantity += Main.rand.Next(20) + 10;
                    randomQuantity += Main.rand.Next(20) + 10;
                    player.QuickSpawnItem(openItemSource, ModContent.ItemType<PandemiteOre>(), randomQuantity);

                    randomQuantity = Main.rand.Next(3) + 1;
                    player.QuickSpawnItem(openItemSource, ModContent.ItemType<IckgrassSeeds>(), randomQuantity);

                    // TODO: Unholy arrow variant for contagion
                    // randomQuantity = Main.rand.Next(30) + 20;
                    // player.QuickSpawnItem(openItemSource, ModContent.ItemType<ShitArrow>(), randomQuantity);
                }

                if (WorldGen.crimson)
                {
                    player.QuickSpawnItem(openItemSource, ModContent.ItemType<BloodyArrow>(), Main.rand.Next(30) + 20);
                }

                switch (Main.hardMode)
                {
                    case false when !ModContent.GetInstance<AvalonWorld>().SuperHardmode &&
                                    Main.rand.Next(10) < 3:
                    case true when !ModContent.GetInstance<AvalonWorld>().SuperHardmode &&
                                   Main.rand.Next(100) < 15:
                    case true
                        when ModContent.GetInstance<AvalonWorld>().SuperHardmode && Main.rand.Next(100) < 7:
                        player.QuickSpawnItem(openItemSource, ModContent.ItemType<BloodyAmulet>());
                        break;
                }

                break;
            }
        }
    }

    public override bool CanUseItem(Item item, Player player)
    {
        #region potion disallow
        if ((item.type == ModContent.ItemType<AdvAmmoReservationPotion>() && player.HasBuff(BuffID.AmmoReservation)) ||
            (item.type == ItemID.AmmoReservationPotion && player.HasBuff(ModContent.BuffType<AdvAmmoReservation>())) ||
            (item.type == ModContent.ItemType<AdvArcheryPotion>() && player.HasBuff(BuffID.Archery)) ||
            (item.type == ItemID.ArcheryPotion && player.HasBuff(ModContent.BuffType<AdvArchery>())) ||
            (item.type == ModContent.ItemType<AdvBattlePotion>() && player.HasBuff(BuffID.Battle)) ||
            (item.type == ItemID.BattlePotion && player.HasBuff(ModContent.BuffType<AdvBattle>())) ||
            (item.type == ModContent.ItemType<AdvBuilderPotion>() && player.HasBuff(BuffID.Builder)) ||
            (item.type == ItemID.BuilderPotion && player.HasBuff(ModContent.BuffType<AdvBuilder>())) ||
            (item.type == ModContent.ItemType<AdvCalmingPotion>() && player.HasBuff(BuffID.Calm)) ||
            (item.type == ItemID.CalmingPotion && player.HasBuff(ModContent.BuffType<AdvCalming>())) ||
            (item.type == ModContent.ItemType<AdvCratePotion>() && player.HasBuff(BuffID.Crate)) ||
            (item.type == ItemID.CratePotion && player.HasBuff(ModContent.BuffType<AdvCrate>())) ||
            (item.type == ModContent.ItemType<AdvDangersensePotion>() && player.HasBuff(BuffID.Dangersense)) ||
            (item.type == ItemID.TrapsightPotion && player.HasBuff(ModContent.BuffType<AdvDangersense>())) ||
            (item.type == ModContent.ItemType<AdvEndurancePotion>() && player.HasBuff(BuffID.Endurance)) ||
            (item.type == ItemID.EndurancePotion && player.HasBuff(ModContent.BuffType<AdvEndurance>())) ||
            (item.type == ModContent.ItemType<AdvFeatherfallPotion>() && player.HasBuff(BuffID.Featherfall)) ||
            (item.type == ItemID.FeatherfallPotion && player.HasBuff(ModContent.BuffType<AdvFeatherfall>())) ||
            (item.type == ModContent.ItemType<AdvFishingPotion>() && player.HasBuff(BuffID.Fishing)) ||
            (item.type == ItemID.FishingPotion && player.HasBuff(ModContent.BuffType<AdvFishing>())) ||
            (item.type == ModContent.ItemType<AdvFlipperPotion>() && player.HasBuff(BuffID.Flipper)) ||
            (item.type == ItemID.FlipperPotion && player.HasBuff(ModContent.BuffType<AdvFlipper>())) ||
            (item.type == ModContent.ItemType<AdvGillsPotion>() && player.HasBuff(BuffID.Gills)) ||
            (item.type == ItemID.GillsPotion && player.HasBuff(ModContent.BuffType<AdvGills>())) ||
            (item.type == ModContent.ItemType<AdvGravitationPotion>() && player.HasBuff(BuffID.Gravitation)) ||
            (item.type == ItemID.GravitationPotion && player.HasBuff(ModContent.BuffType<AdvGravitation>())) ||
            (item.type == ModContent.ItemType<AdvHeartreachPotion>() && player.HasBuff(BuffID.Heartreach)) ||
            (item.type == ItemID.HeartreachPotion && player.HasBuff(ModContent.BuffType<AdvHeartreach>())) ||
            (item.type == ModContent.ItemType<AdvHunterPotion>() && player.HasBuff(BuffID.Hunter)) ||
            (item.type == ItemID.HunterPotion && player.HasBuff(ModContent.BuffType<AdvHunter>())) ||
            (item.type == ModContent.ItemType<AdvInfernoPotion>() && player.HasBuff(BuffID.Inferno)) ||
            (item.type == ItemID.InfernoPotion && player.HasBuff(ModContent.BuffType<AdvInferno>())) ||
            (item.type == ModContent.ItemType<AdvInvisibilityPotion>() && player.HasBuff(BuffID.Invisibility)) ||
            (item.type == ItemID.InvisibilityPotion && player.HasBuff(ModContent.BuffType<AdvInvisibility>())) ||
            (item.type == ModContent.ItemType<AdvIronskinPotion>() && player.HasBuff(BuffID.Ironskin)) ||
            (item.type == ItemID.IronskinPotion && player.HasBuff(ModContent.BuffType<AdvIronskin>())) ||
            (item.type == ModContent.ItemType<AdvLifeforcePotion>() && player.HasBuff(BuffID.Lifeforce)) ||
            (item.type == ItemID.LifeforcePotion && player.HasBuff(ModContent.BuffType<AdvLifeforce>())) ||
            (item.type == ModContent.ItemType<AdvMagicPowerPotion>() && player.HasBuff(BuffID.MagicPower)) ||
            (item.type == ItemID.MagicPowerPotion && player.HasBuff(ModContent.BuffType<AdvMagicPower>())) ||
            (item.type == ModContent.ItemType<AdvManaRegenerationPotion>() &&
             player.HasBuff(BuffID.ManaRegeneration)) ||
            (item.type == ItemID.ManaRegenerationPotion &&
             player.HasBuff(ModContent.BuffType<AdvManaRegeneration>())) ||
            (item.type == ModContent.ItemType<AdvMiningPotion>() && player.HasBuff(BuffID.Mining)) ||
            (item.type == ItemID.MiningPotion && player.HasBuff(ModContent.BuffType<AdvMining>())) ||
            (item.type == ModContent.ItemType<AdvNightOwlPotion>() && player.HasBuff(BuffID.NightOwl)) ||
            (item.type == ItemID.NightOwlPotion && player.HasBuff(ModContent.BuffType<AdvNightOwl>())) ||
            (item.type == ModContent.ItemType<AdvObsidianSkinPotion>() && player.HasBuff(BuffID.ObsidianSkin)) ||
            (item.type == ItemID.ObsidianSkinPotion && player.HasBuff(ModContent.BuffType<AdvObsidianSkin>())) ||
            (item.type == ModContent.ItemType<AdvRagePotion>() && player.HasBuff(BuffID.Rage)) ||
            (item.type == ItemID.RagePotion && player.HasBuff(ModContent.BuffType<AdvRage>())) ||
            (item.type == ModContent.ItemType<AdvRegenerationPotion>() && player.HasBuff(BuffID.Regeneration)) ||
            (item.type == ItemID.RegenerationPotion && player.HasBuff(ModContent.BuffType<AdvRegeneration>())) ||
            (item.type == ModContent.ItemType<AdvShinePotion>() && player.HasBuff(BuffID.Shine)) ||
            (item.type == ItemID.ShinePotion && player.HasBuff(ModContent.BuffType<AdvShine>())) ||
            (item.type == ModContent.ItemType<AdvSonarPotion>() && player.HasBuff(BuffID.Sonar)) ||
            (item.type == ItemID.SonarPotion && player.HasBuff(ModContent.BuffType<AdvSonar>())) ||
            (item.type == ModContent.ItemType<AdvSpelunkerPotion>() && player.HasBuff(BuffID.Spelunker)) ||
            (item.type == ItemID.SpelunkerPotion && player.HasBuff(ModContent.BuffType<AdvSpelunker>())) ||
            (item.type == ModContent.ItemType<AdvSummoningPotion>() && player.HasBuff(BuffID.Summoning)) ||
            (item.type == ItemID.SummoningPotion && player.HasBuff(ModContent.BuffType<AdvSummoning>())) ||
            (item.type == ModContent.ItemType<AdvSwiftnessPotion>() && player.HasBuff(BuffID.Swiftness)) ||
            (item.type == ItemID.SwiftnessPotion && player.HasBuff(ModContent.BuffType<AdvSwiftness>())) ||
            (item.type == ModContent.ItemType<AdvThornsPotion>() && player.HasBuff(BuffID.Thorns)) ||
            (item.type == ItemID.ThornsPotion && player.HasBuff(ModContent.BuffType<AdvThorns>())) ||
            (item.type == ModContent.ItemType<AdvTitanPotion>() && player.HasBuff(BuffID.Titan)) ||
            (item.type == ItemID.TitanPotion && player.HasBuff(ModContent.BuffType<AdvTitan>())) ||
            (item.type == ModContent.ItemType<AdvWarmthPotion>() && player.HasBuff(BuffID.Warmth)) ||
            (item.type == ItemID.WarmthPotion && player.HasBuff(ModContent.BuffType<AdvWarmth>())) ||
            (item.type == ModContent.ItemType<AdvWaterWalkingPotion>() && player.HasBuff(BuffID.WaterWalking)) ||
            (item.type == ItemID.WaterWalkingPotion && player.HasBuff(ModContent.BuffType<AdvWaterWalking>())) ||
            (item.type == ModContent.ItemType<AdvWrathPotion>() && player.HasBuff(BuffID.Wrath)) ||
            (item.type == ItemID.WrathPotion && player.HasBuff(ModContent.BuffType<AdvWrath>())) ||
            (item.type == ModContent.ItemType<AdvBloodCastPotion>() &&
             player.HasBuff(ModContent.BuffType<BloodCast>())) ||
            (item.type == ModContent.ItemType<BloodCastPotion>() &&
             player.HasBuff(ModContent.BuffType<AdvBloodCast>())) ||
            (item.type == ModContent.ItemType<AdvAuraPotion>() &&
             player.HasBuff(ModContent.BuffType<CrimsonDrain>())) ||
            (item.type == ModContent.ItemType<AuraPotion>() && player.HasBuff(ModContent.BuffType<AdvCrimson>())) ||
            (item.type == ModContent.ItemType<AdvForceFieldPotion>() &&
             player.HasBuff(ModContent.BuffType<ForceField>())) ||
            (item.type == ModContent.ItemType<ForceFieldPotion>() &&
             player.HasBuff(ModContent.BuffType<AdvForceField>())) ||
            (item.type == ModContent.ItemType<AdvFuryPotion>() && player.HasBuff(ModContent.BuffType<Fury>())) ||
            (item.type == ModContent.ItemType<FuryPotion>() && player.HasBuff(ModContent.BuffType<AdvFury>())) ||
            (item.type == ModContent.ItemType<AdvGauntletPotion>() &&
             player.HasBuff(ModContent.BuffType<Gauntlet>())) ||
            (item.type == ModContent.ItemType<GauntletPotion>() &&
             player.HasBuff(ModContent.BuffType<AdvGauntlet>())) ||
            (item.type == ModContent.ItemType<AdvGPSPotion>() && player.HasBuff(ModContent.BuffType<GPS>())) ||
            (item.type == ModContent.ItemType<GPSPotion>() && player.HasBuff(ModContent.BuffType<AdvGPS>())) ||
            (item.type == ModContent.ItemType<AdvLeapingPotion>() && player.HasBuff(ModContent.BuffType<Leaping>())) ||
            (item.type == ModContent.ItemType<LeapingPotion>() && player.HasBuff(ModContent.BuffType<AdvLeaping>())) ||
            (item.type == ModContent.ItemType<AdvLuckPotion>() && player.HasBuff(ModContent.BuffType<Luck>())) ||
            (item.type == ModContent.ItemType<CloverPotion>() && player.HasBuff(ModContent.BuffType<AdvLuck>())) ||
            (item.type == ModContent.ItemType<AdvRoguePotion>() && player.HasBuff(ModContent.BuffType<Rogue>())) ||
            (item.type == ModContent.ItemType<RoguePotion>() && player.HasBuff(ModContent.BuffType<AdvRogue>())) ||
            (item.type == ModContent.ItemType<AdvShadowPotion>() && player.HasBuff(ModContent.BuffType<Shadows>())) ||
            (item.type == ModContent.ItemType<ShadowPotion>() && player.HasBuff(ModContent.BuffType<AdvShadow>())) ||
            (item.type == ModContent.ItemType<AdvShockwavePotion>() &&
             player.HasBuff(ModContent.BuffType<Shockwave>())) ||
            (item.type == ModContent.ItemType<ShockwavePotion>() &&
             player.HasBuff(ModContent.BuffType<AdvShockwave>())) ||
            (item.type == ModContent.ItemType<AdvStarbrightPotion>() &&
             player.HasBuff(ModContent.BuffType<Starbright>())) ||
            (item.type == ModContent.ItemType<StarbrightPotion>() &&
             player.HasBuff(ModContent.BuffType<AdvStarbright>())) ||
            (item.type == ModContent.ItemType<AdvStrengthPotion>() && player.HasBuff(ModContent.BuffType<Strong>())) ||
            (item.type == ModContent.ItemType<StrengthPotion>() &&
             player.HasBuff(ModContent.BuffType<AdvStrength>())) ||
            (item.type == ModContent.ItemType<AdvSupersonicPotion>() &&
             player.HasBuff(ModContent.BuffType<Supersonic>())) ||
            (item.type == ModContent.ItemType<SupersonicPotion>() &&
             player.HasBuff(ModContent.BuffType<AdvSupersonic>())) ||
            (item.type == ModContent.ItemType<AdvTitanskinPotion>() &&
             player.HasBuff(ModContent.BuffType<Titanskin>())) ||
            (item.type == ModContent.ItemType<TitanskinPotion>() &&
             player.HasBuff(ModContent.BuffType<AdvTitanskin>())) ||
            (item.type == ModContent.ItemType<AdvVisionPotion>() && player.HasBuff(ModContent.BuffType<Vision>())) ||
            (item.type == ModContent.ItemType<VisionPotion>() && player.HasBuff(ModContent.BuffType<AdvVision>())) ||
            (item.type == ModContent.ItemType<AdvMagnetPotion>() && player.HasBuff(ModContent.BuffType<Magnet>())) ||
            (item.type == ModContent.ItemType<MagnetPotion>() && player.HasBuff(ModContent.BuffType<AdvMagnet>())) ||
            (item.type == ModContent.ItemType<AdvTimeShiftPotion>() &&
             player.HasBuff(ModContent.BuffType<TimeShift>())) ||
            (item.type == ModContent.ItemType<TimeShiftPotion>() &&
             player.HasBuff(ModContent.BuffType<AdvTimeShift>())) ||
            (item.type == ModContent.ItemType<AdvWisdomPotion>() && player.HasBuff(ModContent.BuffType<Wisdom>())) ||
            (item.type == ModContent.ItemType<WisdomPotion>() && player.HasBuff(ModContent.BuffType<AdvWisdom>())))
        {
            return false;
        }
        #endregion
        if (item.type == ItemID.RodofDiscord &&
            ((player.GetModPlayer<ExxoBiomePlayer>().ZoneSkyFortress &&
              !ModContent.GetInstance<DownedBossSystem>().DownedDragonLord) ||
             (player.GetModPlayer<ExxoBiomePlayer>().ZoneNearHellcastle &&
              !ModContent.GetInstance<DownedBossSystem>().DownedPhantasm)))
        {
            return false;
        }

        if (item.type == ItemID.ActuationRod &&
            ((player.GetModPlayer<ExxoBiomePlayer>().ZoneSkyFortress &&
              !ModContent.GetInstance<DownedBossSystem>().DownedDragonLord) ||
             (player.GetModPlayer<ExxoBiomePlayer>().ZoneNearHellcastle &&
              !ModContent.GetInstance<DownedBossSystem>().DownedPhantasm)))
        {
            return false;
        }

        if (item.useAmmo > 0 && player.HasBuff(ModContent.BuffType<Unloaded>()))
        {
            return false;
        }

        if (item.DamageType == DamageClass.Melee && player.HasBuff(ModContent.BuffType<BrokenWeaponry>()))
        {
            return false;
        }

        return base.CanUseItem(item, player);
    }
    public override bool AllowPrefix(Item item, int pre)
    {
        if (item.GetGlobalItem<AvalonGlobalItemInstance>().Tome)
        {
            return false;
        }
        return base.AllowPrefix(item, pre);
    }
    public override int ChoosePrefix(Item item, UnifiedRandom rand) => item.IsArmor()
        ? Main.rand.Next(ExxoPrefix.ExxoCategoryPrefixes[ExxoPrefixCategory.Armor]).Type
        : base.ChoosePrefix(item, rand);
}
