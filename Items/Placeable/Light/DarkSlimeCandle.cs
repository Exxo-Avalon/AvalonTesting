using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Light;

class DarkSlimeCandle : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Dark Slime Candle");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.noWet = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.DarkSlime.DarkSlimeCandle>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 99;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
}
