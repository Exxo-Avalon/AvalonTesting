using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Painting;

class EyeoftheUniverse : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Eye of the Universe");
        Tooltip.SetDefault("'The Nomai symbol for the center of the Universe'");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.rare = ItemRarityID.Pink;
        Item.createTile = ModContent.TileType<Tiles.EyeoftheUniverse>();
        Item.placeStyle = 0;
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
