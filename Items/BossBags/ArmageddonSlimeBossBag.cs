//using AvalonTesting.Items.Consumables;
//using AvalonTesting.Items.Placeable.Tile;
using AvalonTesting.Items.Placeable.Tile;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.BossBags;

public class ArmageddonSlimeBossBag : ModItem
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
        player.TryGettingDevArmor(player.GetSource_OpenItem(Item.type));

        player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<DarkMatterSoilBlock>(), Main.rand.Next(100, 210));
    }

    public override int BossBagNPC => ModContent.NPCType<NPCs.Bosses.ArmageddonSlime>();
}
