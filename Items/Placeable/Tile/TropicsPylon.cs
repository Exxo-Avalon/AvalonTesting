using Terraria.Enums;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class TropicsPylon : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Tropics Pylon");

    }
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.TropicsPylon>());
        Item.SetShopValues(ItemRarityColor.Blue1, Terraria.Item.buyPrice(gold: 10));
    }
}
