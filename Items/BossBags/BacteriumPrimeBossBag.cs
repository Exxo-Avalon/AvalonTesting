using AvalonTesting.Items.Accessories;
using AvalonTesting.Items.Consumables;
using AvalonTesting.Items.Material;
using AvalonTesting.Items.Placeable.Tile;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.BossBags;

public class BacteriumPrimeBossBag : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Treasure Bag");
        Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
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
        //player.TryGettingDevArmor();

        player.QuickSpawnItem(player.GetItemSource_OpenItem(Item.type), ModContent.ItemType<BacciliteOre>(), Main.rand.Next(15, 41) + Main.rand.Next(15, 41));
        player.QuickSpawnItem(player.GetItemSource_OpenItem(Item.type), ModContent.ItemType<Booger>(), Main.rand.Next(10, 20));
        player.QuickSpawnItem(player.GetItemSource_OpenItem(Item.type), ModContent.ItemType<BadgeOfBacteria>());
    }

    public override int BossBagNPC => ModContent.NPCType<NPCs.BacteriumPrime>();
}
