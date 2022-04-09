using System.Collections.Generic;
using System.Linq;
using AvalonTesting.Buffs.AdvancedBuffs;
using AvalonTesting.Prefixes;
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

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        TooltipLine tooltipLine = tooltips.FirstOrDefault(x => x.Name == "ItemName" && x.mod == "Terraria");
        if (tooltipLine != null)
        {
            if (item.type == ItemID.CoinGun)
            {
                tooltipLine.text = "Spend Shot";
            }

            if (item.type == ItemID.PurpleMucos)
            {
                tooltipLine.text = "Purple Mucus";
            }

            if (item.type == ItemID.HighTestFishingLine)
            {
                tooltipLine.text = tooltipLine.text.Replace("Test", "Tensile");
            }

            if (item.type == ItemID.BlueSolution)
            {
                tooltipLine.text = "Cyan Solution";
            }

            if (item.type == ItemID.DarkBlueSolution)
            {
                tooltipLine.text = "Blue Solution";
            }

            if (item.type == ItemID.FrostsparkBoots)
            {
                tooltipLine.text = tooltipLine.text.Replace("Frostspark", "Sparkfrost");
            }

            if (item.type == ItemID.BossMaskCultist)
            {
                tooltipLine.text = "Lunatic Cultist Mask";
            }

            if (item.type == ItemID.AncientCultistTrophy)
            {
                tooltipLine.text = "Lunatic Cultist Trophy";
            }
        }

        if (item.IsArmor() && !item.social)
        {
            if (item.prefix == ModContent.PrefixType<Fluidic>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.mod.Equals("Terraria") || tt.mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+5% increased movement speed") {isModifier = true});
                    tooltips.Insert(index + 2,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "Free movement in liquids") {isModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Barbaric>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.mod.Equals("Terraria") || tt.mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+4% damage") {isModifier = true});
                    tooltips.Insert(index + 2,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+6% knockback") {isModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Boosted>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.mod.Equals("Terraria") || tt.mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+4% increased movement speed") {isModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Busted>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.mod.Equals("Terraria") || tt.mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "-1 defense")
                        {
                            isModifier = true, isModifierBad = true
                        });
                }
            }

            if (item.prefix == ModContent.PrefixType<Disgusting>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.mod.Equals("Terraria") || tt.mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "-2 defense")
                        {
                            isModifier = true, isModifierBad = true
                        });
                    tooltips.Insert(index + 2,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "Stink potion effect")
                        {
                            isModifier = true, isModifierBad = true
                        });
                }
            }

            if (item.prefix == ModContent.PrefixType<Glorious>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.mod.Equals("Terraria") || tt.mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+4% damage") {isModifier = true});
                    tooltips.Insert(index + 2,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+1 defense") {isModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Insane>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.mod.Equals("Terraria") || tt.mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "Increased placement speed") {isModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Loaded>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.mod.Equals("Terraria") || tt.mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+1 defense") {isModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Messy>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.mod.Equals("Terraria") || tt.mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "-5% damage")
                        {
                            isModifier = true, isModifierBad = true
                        });
                }
            }

            if (item.prefix == ModContent.PrefixType<Mythic>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.mod.Equals("Terraria") || tt.mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+20 maximum mana") {isModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Protective>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.mod.Equals("Terraria") || tt.mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+2 defense") {isModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Silly>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.mod.Equals("Terraria") || tt.mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+2% critical strike chance") {isModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Handy>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.mod.Equals("Terraria") || tt.mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+1 block range") {isModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Slimy>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.mod.Equals("Terraria") || tt.mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "Reduces damage taken by 3%") {isModifier = true});
                }
            }

            if (item.prefix == ModContent.PrefixType<Bloated>())
            {
                int index = tooltips.FindLastIndex(tt => (tt.mod.Equals("Terraria") || tt.mod.Equals(Mod.Name))
                                                         && (tt.Name.Equals("Material") ||
                                                             tt.Name.StartsWith("Tooltip") ||
                                                             tt.Name.Equals("Defense") || tt.Name.Equals("Equipable")));
                if (index != -1)
                {
                    tooltips.Insert(index + 1,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "+5% melee damage") {isModifier = true});
                    tooltips.Insert(index + 2,
                        new TooltipLine(Mod, "PrefixAccMaxMana", "-2% melee speed")
                        {
                            isModifier = true, isModifierBad = true
                        });
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
                        tooltips[i].text = "For use with Blowpipes";
                    }
                }

                break;
            case ItemID.PoisonDart:
                for (int i = 0; i < tooltips.Count; i++)
                {
                    if (tooltips[i].Name == "Tooltip1")
                    {
                        tooltips[i].text = "For use with Blowpipes and Blowgun";
                    }
                }

                break;
            case ItemID.CoinGun:
                for (int i = 0; i < tooltips.Count; i++)
                {
                    if (tooltips[i].Name == "Tooltip0")
                    {
                        tooltips[i].text = "Uses coins for ammo - Higher valued coins do more damage";
                    }

                    if (tooltips[i].Name == "Tooltip1")
                    {
                        tooltips[i].text = "'Knocks some cents into your enemies'";
                    }
                }

                break;
            case ItemID.PickaxeAxe:
                for (int i = 0; i < tooltips.Count; i++)
                {
                    if (tooltips[i].Name == "Tooltip0")
                    {
                        tooltips[i].text = "'Not to be confused with a hamdrill'";
                    }

                    if (tooltips[i].Name == "Tooltip1")
                    {
                        tooltips[i].text = "Can mine Chlorophyte, Xanthophyte, and Caesium Ore";
                    }
                }

                break;
            case ItemID.Drax:
                for (int i = 0; i < tooltips.Count; i++)
                {
                    if (tooltips[i].Name == "Tooltip0")
                    {
                        tooltips[i].text = "'Not to be confused with a picksaw'";
                    }

                    if (tooltips[i].Name == "Tooltip1")
                    {
                        tooltips[i].text = "Can mine Chlorophyte, Xanthophyte, and Caesium Ore";
                    }
                }

                break;
        }
    }
}
