using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Light;

class ResistantWoodCandle : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Resistant Wood Candle");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.noWet = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.ResistantWood.ResistantWoodCandle>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 99;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
}
