using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Placeable.Painting;

class RingofDisgust : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Ring of Disgust");
        Tooltip.SetDefault("'B. Harold'");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.rare = ItemRarityID.Orange;
        Item.createTile = ModContent.TileType<Tiles.Paintings>();
        Item.placeStyle = 9;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 99;
        Item.value = Item.sellPrice(0, 0, 10, 0);
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
}
