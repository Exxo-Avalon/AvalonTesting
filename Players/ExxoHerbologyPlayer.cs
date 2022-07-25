using System.Collections.Generic;
using System.Linq;
using Avalon.Data;
using Avalon.Items.Potions;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Players;

public class ExxoHerbologyPlayer : ModPlayer
{
    public Dictionary<int, int> herbCounts = new();

    public HerbTier herbTier;
    public int herbTotal;
    public int herbX;
    public int herbY;
    public int potionTotal;

    public enum HerbTier
    {
        Novice,
        Apprentice,
        Expert,
        Master,
    }

    public bool DisplayHerbologyMenu { get; set; }

    /// <inheritdoc />
    public override void PostUpdate()
    {
        // Herbology bench distance check
        if (DisplayHerbologyMenu)
        {
            int num9 = (int)((Player.position.X + (Player.width * 0.5)) / 16.0);
            int num10 = (int)((Player.position.Y + (Player.height * 0.5)) / 16.0);
            if (num9 < herbX - Player.lastTileRangeX || num9 > herbX + Player.lastTileRangeX + 1 ||
                num10 < herbY - Player.lastTileRangeY || num10 > herbY + Player.lastTileRangeY + 1)
            {
                SoundEngine.PlaySound(SoundID.MenuClose);
                DisplayHerbologyMenu = false;
                Player.dropItemCheck();
            }
        }

        if (!Main.playerInventory)
        {
            DisplayHerbologyMenu = false;
        }
    }

    public bool PurchaseItem(Item item, int amount)
    {
        int charge = HerbologyData.GetItemCost(item, amount);

        if (charge <= 0)
        {
            return false;
        }

        if (HerbologyData.ItemIsHerb(item))
        {
            int herbType = ItemID.None;
            bool chargeInventory = false;
            if (HerbologyData.LargeHerbSeedIdByHerbSeedId.ContainsKey(item.type))
            {
                herbType = HerbologyData.HerbIdByLargeHerbId[
                    HerbologyData.LargeHerbIdByLargeHerbSeedId[HerbologyData.LargeHerbSeedIdByHerbSeedId[item.type]]];
            }
            else if (HerbologyData.LargeHerbSeedIdByHerbSeedId.ContainsValue(item.type))
            {
                chargeInventory = true;
                herbType = HerbologyData.HerbIdByLargeHerbId[HerbologyData.LargeHerbIdByLargeHerbSeedId[item.type]];
            }

            if (herbTotal < charge)
            {
                return false;
            }

            if (chargeInventory)
            {
                if (herbType != ItemID.None && herbCounts.ContainsKey(herbType) &&
                    herbCounts[herbType] > charge)
                {
                    herbCounts[herbType] -= charge;
                }
                else
                {
                    return false;
                }
            }

            herbTotal -= charge;
            Main.mouseItem = item.Clone();
            Main.mouseItem.stack = amount;
            return true;
        }

        if (potionTotal < charge)
        {
            return false;
        }

        potionTotal -= charge;
        Main.mouseItem = item.Clone();
        Main.mouseItem.stack = amount;
        return true;
    }

    public bool SellItem(Item item)
    {
        if (item.stack <= 0 || item.type == ItemID.None)
        {
            return false;
        }

        int herbAddition = 0;
        int herbType = ItemID.None;
        if (HerbologyData.HerbIdByLargeHerbId.ContainsValue(item.type))
        {
            herbAddition = HerbologyData.HerbSellPrice;
            herbType = item.type;
        }
        else if (HerbologyData.LargeHerbSeedIdByHerbId.ContainsValue(item.type))
        {
            herbAddition = HerbologyData.LargeHerbSeedSellPrice;
            herbType = HerbologyData.HerbIdByLargeHerbId[HerbologyData.LargeHerbIdByLargeHerbSeedId[item.type]];
        }
        else if (HerbologyData.LargeHerbIdByLargeHerbSeedId.ContainsValue(item.type))
        {
            herbAddition = HerbologyData.LargeHerbSellPrice;
            herbType = HerbologyData.HerbIdByLargeHerbId[item.type];
        }

        if (herbAddition > 0 && herbType != ItemID.None)
        {
            if (!herbCounts.ContainsKey(herbType))
            {
                herbCounts.Add(herbType, 0);
            }

            herbCounts[herbType] += herbAddition * item.stack;
            herbTotal += herbAddition * item.stack;
        }

        int potionAddition = 0;

        if (HerbologyData.PotionIds.Contains(item.type))
        {
            potionAddition = HerbologyData.PotionSellPrice;
        }
        else if (HerbologyData.ElixirIds.Contains(item.type))
        {
            potionAddition = HerbologyData.ElixirSellPrice;
        }
        else if (item.type == ModContent.ItemType<BlahPotion>())
        {
            potionAddition = HerbologyData.BlahPotionSellPrice;
        }

        if (HerbologyData.RestorationIDs.Contains(item.type))
        {
            potionAddition = HerbologyData.RestorationPotionCost;
        }

        if (potionAddition > 0)
        {
            potionTotal += potionAddition * item.stack;
        }

        UpdateHerbTier();

        PopupText.NewText(PopupTextContext.Advanced, item, item.stack);
        // SoundEngine.PlaySound(SoundID.Item, -1, -1,
        //     SoundLoader.GetSoundSlot(global::Avalon.Mod, "Sounds/Item/HerbConsume"));
        return true;
    }

    public void UpdateHerbTier()
    {
        HerbTier newHerbTier;
        if (herbTotal >= HerbologyData.HerbTier4Threshold && Main.hardMode &&
            ModContent.GetInstance<AvalonWorld>().SuperHardmode)
        {
            newHerbTier = HerbTier.Master; // tier 4; Blah Potion exchange
        }
        else if (herbTotal >= HerbologyData.HerbTier3Threshold && Main.hardMode)
        {
            newHerbTier = HerbTier.Expert; // tier 3; allows you to obtain elixirs
        }
        else if (herbTotal >= HerbologyData.HerbTier2Threshold)
        {
            newHerbTier = HerbTier.Apprentice; // tier 2; allows for large herb seeds
        }
        else
        {
            newHerbTier =
                HerbTier.Novice; // tier 1; allows for exchanging one herb for another
        }

        if (newHerbTier > herbTier)
        {
            herbTier = newHerbTier;
        }
    }
}
