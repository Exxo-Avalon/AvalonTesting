using System.Collections.Generic;
using System.Linq;
using AvalonTesting.Buffs.AdvancedBuffs;
using AvalonTesting.Items.Accessories;
using AvalonTesting.Items.Consumables;
using AvalonTesting.Items.Material;
using AvalonTesting.Items.Placeable.Seed;
using AvalonTesting.Items.Placeable.Tile;
using AvalonTesting.Network;
using AvalonTesting.Players;
using AvalonTesting.Prefixes;
using AvalonTesting.Systems;
using AvalonTesting.Tiles;
using AvalonTesting.Tiles.Herbs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting;

public class AvalonTestingGlobalItem : GlobalItem
{
    public override void SetDefaults(Item item)
    {
        if (item.IsArmor())
        {
            ItemID.Sets.CanGetPrefixes[item.type] = true;
        }

        if (item.accessory)
        {
            item.canBePlacedInVanityRegardlessOfConditions = true;
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
            case ItemID.Torch:
                item.ammo = 8;
                //item.GetGlobalItem<ExxoAvalonOriginsGlobalItemInstance>().torch = 0;
                item.notAmmo = true;
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
                item.ammo = 147;
                item.notAmmo = true;
                //item.GetGlobalItem<ExxoAvalonOriginsGlobalItemInstance>().spike = 0;
                break;
            case ItemID.WoodenSpike:
                item.ammo = 147;
                item.notAmmo = true;
                //item.GetGlobalItem<ExxoAvalonOriginsGlobalItemInstance>().spike = 4;
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
            //case ItemID.Goldfish:
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
            case ItemID.BlueTorch:
                item.ammo = 8;
                //item.GetGlobalItem<ExxoAvalonOriginsGlobalItemInstance>().torch = 1;
                break;
            case ItemID.RedTorch:
                item.ammo = 8;
                //item.GetGlobalItem<ExxoAvalonOriginsGlobalItemInstance>().torch = 2;
                break;
            case ItemID.GreenTorch:
                item.ammo = 8;
                //item.GetGlobalItem<ExxoAvalonOriginsGlobalItemInstance>().torch = 3;
                break;
            case ItemID.PurpleTorch:
                item.ammo = 8;
                //item.GetGlobalItem<ExxoAvalonOriginsGlobalItemInstance>().torch = 4;
                break;
            case ItemID.WhiteTorch:
                item.ammo = 8;
                //item.GetGlobalItem<ExxoAvalonOriginsGlobalItemInstance>().torch = 5;
                break;
            case ItemID.YellowTorch:
                item.ammo = 8;
                //item.GetGlobalItem<ExxoAvalonOriginsGlobalItemInstance>().torch = 6;
                break;
            case ItemID.DemonTorch:
                item.ammo = 8;
                //item.GetGlobalItem<ExxoAvalonOriginsGlobalItemInstance>().torch = 7;
                break;
            case ItemID.PixieDust:
                item.value = 1000;
                break;
            case ItemID.Flamethrower:
                item.damage = 35;
                break;
            case ItemID.CursedTorch:
                item.ammo = 8;
                //item.GetGlobalItem<ExxoAvalonOriginsGlobalItemInstance>().torch = 8;
                break;
            case ItemID.SnowGlobe:
                item.maxStack = 20;
                break;
            case ItemID.RainbowBrick:
                item.rare = 1;
                break;
            case ItemID.RainbowBrickWall:
                item.rare = 1;
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
            case ItemID.IceTorch:
                item.ammo = 8;
                //item.GetGlobalItem<AvalonTestingGlobalItemInstance>().torch = 9;
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
            case ItemID.OrangeTorch:
                item.ammo = 8;
                //item.GetGlobalItem<ExxoAvalonOriginsGlobalItemInstance>().torch = 10;
                break;
            case ItemID.SniperRifle:
                item.value = Item.sellPrice(0, 7);
                break;
            case ItemID.Uzi:
                item.value = 250000;
                break;
            case ItemID.SkeletronMask:
                item.value = Item.sellPrice(0, 2);
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
            case ItemID.IchorTorch:
                item.ammo = 8;
                //item.GetGlobalItem<ExxoAvalonOriginsGlobalItemInstance>().torch = 11;
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
            case ItemID.UltrabrightTorch:
                item.ammo = 8;
                //item.GetGlobalItem<ExxoAvalonOriginsGlobalItemInstance>().torch = 12;
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
            case ItemID.KingSlimeMask:
                item.value = Item.sellPrice(0, 2);
                break;
            case ItemID.DukeFishronMask:
                item.value = Item.sellPrice(0, 2);
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
            case ItemID.BoneTorch:
                item.ammo = 8;
                //item.GetGlobalItem<ExxoAvalonOriginsGlobalItemInstance>().torch = 19;
                break;
            case ItemID.RainbowTorch:
                item.ammo = 8;
                //item.GetGlobalItem<ExxoAvalonOriginsGlobalItemInstance>().torch = 20;
                break;
            case ItemID.PinkTorch:
                item.ammo = 8;
                //item.GetGlobalItem<ExxoAvalonOriginsGlobalItemInstance>().torch = 21;
                break;
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

        if (item.type >= ItemID.BrainMask && item.type <= ItemID.DestroyerMask)
        {
            item.value = Item.sellPrice(0, 2);
        }
    }

    public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
    {
        if (item.IsArmor())
        {
            return false;
        }

        return base.CanEquipAccessory(item, player, slot, modded);
    }

    public override void PickAmmo(Item weapon, Item ammo, Player player, ref int type, ref float speed, ref int damage,
                                  ref float knockback)
    {
        if (ammo.ammo == AmmoID.Arrow && speed < 20f && player.HasBuff<AdvArchery>())
        {
            speed *= 1 + AdvArchery.PercentageIncrease;
            speed = MathHelper.Min(speed, 20f);
        }

        base.PickAmmo(weapon, ammo, player, ref type, ref speed, ref damage, ref knockback);
    }

    public override void HoldItem(Item item, Player player)
    {
        #region wire disable in sky fortress

        var tempWireItem = new Item();
        tempWireItem.netDefaults(item.netID);
        tempWireItem = tempWireItem.CloneWithModdedDataFrom(item);
        tempWireItem.stack = item.stack;
        if (player.GetModPlayer<ExxoBiomePlayer>().ZoneSkyFortress && !ModContent.GetInstance<DownedBossSystem>().DownedDragonLord)
        {
            player.InfoAccMechShowWires = false;
            if (item.mech)
            {
                item.mech = false;
                item.useStyle = 0;
                item.GetGlobalItem<AvalonTestingGlobalItemInstance>().WasWiring = true;
            }
        }

        if (item.GetGlobalItem<AvalonTestingGlobalItemInstance>().WasWiring &&
            !player.GetModPlayer<ExxoBiomePlayer>().ZoneSkyFortress)
        {
            item.netDefaults(tempWireItem.netID);
            item.stack = tempWireItem.stack;
            item.GetGlobalItem<AvalonTestingGlobalItemInstance>().WasWiring = false;
        }

        #endregion

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
        else if (item.prefix == ModContent.PrefixType<Fantastic>() ||
                 item.prefix == ModContent.PrefixType<Awestruck>() ||
                 item.prefix == ModContent.PrefixType<Phantasmal>())
        {
            kbDiff = 0.2f;
        }
        else if (item.prefix == ModContent.PrefixType<Drunken>() || item.prefix == ModContent.PrefixType<Hectic>())
        {
            kbDiff = -0.07f;
        }
        else if (item.prefix == ModContent.PrefixType<Stupid>())
        {
            kbDiff = 0.16f;
        }

        item.knockBack = tempItem.knockBack * (1 + kbDiff);
        item.knockBack *= player.Avalon().bonusKB;

        #endregion

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

        #endregion
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        TooltipLine tooltipLine = tooltips.FirstOrDefault(x => x.Name == "ItemName" && x.Mod == "Terraria");
        if (tooltipLine != null)
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

            if (item.type == ItemID.BossMaskCultist)
            {
                tooltipLine.Text = "Lunatic Cultist Mask";
            }

            if (item.type == ItemID.AncientCultistTrophy)
            {
                tooltipLine.Text = "Lunatic Cultist Trophy";
            }
        }

        if (item.IsArmor() && !item.social)
        {
            if (item.prefix == ModContent.PrefixType<Fluidic>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+5% increased movement speed") {IsModifier = true});
                    tooltips.Insert(index + 2,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "Free movement in liquids") {IsModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Barbaric>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+4% damage") {IsModifier = true});
                    tooltips.Insert(index + 2,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+6% knockback") {IsModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Boosted>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+4% increased movement speed") {IsModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Busted>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "-1 defense")
                        {
                            IsModifier = true, IsModifierBad = true
                        });
                }
            }

            if (item.prefix == ModContent.PrefixType<Disgusting>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "-2 defense")
                        {
                            IsModifier = true, IsModifierBad = true
                        });
                    tooltips.Insert(index + 2,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "Stink potion effect")
                        {
                            IsModifier = true, IsModifierBad = true
                        });
                }
            }

            if (item.prefix == ModContent.PrefixType<Glorious>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+4% damage") {IsModifier = true});
                    tooltips.Insert(index + 2,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+1 defense") {IsModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Insane>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "Increased placement speed") {IsModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Loaded>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+1 defense") {IsModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Messy>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "-5% damage")
                        {
                            IsModifier = true, IsModifierBad = true
                        });
                }
            }

            if (item.prefix == ModContent.PrefixType<Mythic>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+20 maximum mana") {IsModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Protective>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+2 defense") {IsModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Silly>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+2% critical strike chance") {IsModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Handy>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+1 block range") {IsModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Slimy>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "Reduces damage taken by 3%") {IsModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Bloated>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+5% melee damage") {IsModifier = true});
                    tooltips.Insert(index + 2,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "-2% melee speed")
                        {
                            IsModifier = true, IsModifierBad = true
                        });
                }
            }
        }

        if (item.accessory && !item.social)
        {
            if (item.prefix == ModContent.PrefixType<Magical>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+40 mana") {IsModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Timid>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMeleeSpeed", "-2% melee speed")
                        {
                            IsModifier = true, IsModifierBad = true
                        });
                }
            }

            if (item.prefix == ModContent.PrefixType<Languid>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMoveSpeed", "-2% movement speed")
                        {
                            IsModifier = true, IsModifierBad = true
                        });
                }
            }

            if (item.prefix == ModContent.PrefixType<Enchanted>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+20 maximum mana") {IsModifier = true});
                    tooltips.Insert(index + 2,
                        new TooltipLine(Mod, "PrefixAccMoveSpeed", "+3% movement speed") {IsModifier = true});
                    tooltips.Insert(index + 3,
                        new TooltipLine(Mod, "PrefixAccDefense", "+1 defense") {IsModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Bogus>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccCritChance", "+2% critical strike chance") {IsModifier = true});
                    tooltips.Insert(index + 2,
                        new TooltipLine(Mod, "PrefixAccCritChance", "+2% critical strike damage") {IsModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Vigorous>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMeleeSpeed", "+3% melee speed") {IsModifier = true});
                    tooltips.Insert(index + 2,
                        new TooltipLine(Mod, "PrefixAccDamage", "+3% damage") {IsModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Overactive>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+20 maximum mana") {IsModifier = true});
                    tooltips.Insert(index + 2,
                        new TooltipLine(Mod, "PrefixAccDamage", "+4% mana cost")
                        {
                            IsModifier = true, IsModifierBad = true
                        });
                }
            }

            if (item.prefix == ModContent.PrefixType<Robust>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccDefense", "+3 defense") {IsModifier = true});
                    tooltips.Insert(index + 2,
                        new TooltipLine(Mod, "PrefixAccDamage", "+3% damage") {IsModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Lurid>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccDefense", "+2 defense") {IsModifier = true});
                    tooltips.Insert(index + 2,
                        new TooltipLine(Mod, "PrefixAccCritChance", "+2% critical strike chance") {IsModifier = true});
                }
            }
        }

        switch (item.type)
        {
            case ItemID.Vine:
                tooltips.Add(new TooltipLine(Mod, "Rope", "Can be climbed on"));
                break;
            case ItemID.Seed:
                for (int i = 0; i < tooltips.Count; i++)
                {
                    if (tooltips[i].Name == "Tooltip0")
                    {
                        tooltips[i].Text = "For use with Blowpipes";
                    }
                }

                break;
            case ItemID.PoisonDart:
                for (int i = 0; i < tooltips.Count; i++)
                {
                    if (tooltips[i].Name == "Tooltip1")
                    {
                        tooltips[i].Text = "For use with Blowpipes and Blowgun";
                    }
                }

                break;
            case ItemID.CoinGun:
                for (int i = 0; i < tooltips.Count; i++)
                {
                    if (tooltips[i].Name == "Tooltip0")
                    {
                        tooltips[i].Text = "Uses coins for ammo - Higher valued coins do more damage";
                    }

                    if (tooltips[i].Name == "Tooltip1")
                    {
                        tooltips[i].Text = "'Knocks some cents into your enemies'";
                    }
                }

                break;
            case ItemID.PickaxeAxe:
                for (int i = 0; i < tooltips.Count; i++)
                {
                    if (tooltips[i].Name == "Tooltip0")
                    {
                        tooltips[i].Text = "'Not to be confused with a hamdrill'";
                    }

                    if (tooltips[i].Name == "Tooltip1")
                    {
                        tooltips[i].Text = "Can mine Chlorophyte, Xanthophyte, and Caesium Ore";
                    }
                }

                break;
            case ItemID.Drax:
                for (int i = 0; i < tooltips.Count; i++)
                {
                    if (tooltips[i].Name == "Tooltip0")
                    {
                        tooltips[i].Text = "'Not to be confused with a picksaw'";
                    }

                    if (tooltips[i].Name == "Tooltip1")
                    {
                        tooltips[i].Text = "Can mine Chlorophyte, Xanthophyte, and Caesium Ore";
                    }
                }

                break;
        }
    }

    public override void OpenVanillaBag(string context, Player player, int arg)
    {
        if (context == "bossBag")
        {
            if (Main.rand.Next(4) == 0)
            {
                player.QuickSpawnItem(player.GetSource_OpenItem(0), ModContent.ItemType<StaminaCrystal>());
            }
        }
        if (context == "bossBag" && arg == ItemID.WallOfFleshBossBag)
        {
            NPCLoader.blockLoot.Add(ItemID.RangerEmblem);
            NPCLoader.blockLoot.Add(ItemID.SummonerEmblem);
            NPCLoader.blockLoot.Add(ItemID.WarriorEmblem);
            NPCLoader.blockLoot.Add(ItemID.SorcererEmblem);
            player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.WallOfFleshBossBag), ModContent.ItemType<NullEmblem>());
        }
        if (context == "bossBag" && arg == ItemID.KingSlimeBossBag)
        {
            if (Main.rand.Next(3) == 0) player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.KingSlimeBossBag), ModContent.ItemType<BandofSlime>());
        }
        if (context == "bossBag" && arg == ItemID.PlanteraBossBag)
        {
            player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.PlanteraBossBag), ModContent.ItemType<LifeDew>(), Main.rand.Next(15, 22));
            player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.PlanteraBossBag), ItemID.ChlorophyteOre, Main.rand.Next(60, 121));
        }
        if (context == "bossBag" && arg == ItemID.EyeOfCthulhuBossBag) //keeping it this way because we might add unholy arrow alt
        {
            /*if (ExxoAvalonOriginsWorld.contagion)
            {
                NPCLoader.blockLoot.Add(ItemID.UnholyArrow);
                NPCLoader.blockLoot.Add(ItemID.DemoniteOre);
                NPCLoader.blockLoot.Add(ItemID.CorruptSeeds);
                player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.EyeOfCthulhuBossBag), ModContent.ItemType<BacciliteOre>(), Main.rand.Next(30, 81));
                player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.EyeOfCthulhuBossBag), ModContent.ItemType<IckgrassSeeds>(), Main.rand.Next(1, 3));
                //player.QuickSpawnItem(ModContent.ItemType<ShitArrow>(), Main.rand.Next(20, 50));
            }*/
            if (WorldGen.crimson)
            {
                player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.EyeOfCthulhuBossBag), ModContent.ItemType<Items.Ammo.BloodyArrow>(), Main.rand.Next(20, 50));
            }
            if (!Main.hardMode && !ModContent.GetInstance<AvalonTestingWorld>().SuperHardmode && Main.rand.Next(10) < 3)
            {
                player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.EyeOfCthulhuBossBag), ModContent.ItemType<BloodyAmulet>());
            }
            else if (Main.hardMode && !ModContent.GetInstance<AvalonTestingWorld>().SuperHardmode && Main.rand.Next(100) < 15)
            {
                player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.EyeOfCthulhuBossBag), ModContent.ItemType<BloodyAmulet>());
            }
            else if (Main.hardMode && ModContent.GetInstance<AvalonTestingWorld>().SuperHardmode && Main.rand.Next(100) < 7)
            {
                player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.EyeOfCthulhuBossBag), ModContent.ItemType<BloodyAmulet>());
            }
        }
    }
}
