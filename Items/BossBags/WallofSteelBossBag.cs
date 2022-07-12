using AvalonTesting.Items.Accessories;
using AvalonTesting.Items.Consumables;
using AvalonTesting.Items.Material;
using AvalonTesting.Items.Weapons.Magic;
using AvalonTesting.Items.Weapons.Ranged;
using AvalonTesting.NPCs.Bosses;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.BossBags;

public class WallofSteelBossBag : ModItem
{
    public override int BossBagNPC => ModContent.NPCType<WallofSteel>();

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Treasure Bag (Wall of Steel)");
        Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
    }

    public override void SetDefaults()
    {
        Item.maxStack = 999;
        Item.consumable = true;
        Item.width = 36;
        Item.height = 34;
        Item.rare = ItemRarityID.Purple;
        Item.expert = true;
    }

    public override bool CanRightClick()
    {
        return true;
    }

    public override void OpenBossBag(Player player)
    {
        player.TryGettingDevArmor(player.GetSource_OpenItem(Item.type));

        if ((player.extraAccessorySlots == 0 && !Main.expertMode) ||
            (player.extraAccessorySlots == 1 && Main.expertMode))
        {
            player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<MechanicalHeart>());
        }

        int drop = Main.rand.Next(5);
        if (drop == 0)
        {
            player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<FleshBoiler>());
        }

        if (drop == 1)
        {
            player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<MagicCleaver>());
        }

        if (drop == 2)
        {
            player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<BubbleBoost>());
        }

        player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<SoulofBlight>(),
            Main.rand.Next(40, 56));
        player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<HellsteelPlate>(),
            Main.rand.Next(20, 31));
    }
}
