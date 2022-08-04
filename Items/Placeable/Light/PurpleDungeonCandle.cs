using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Light;

class PurpleDungeonCandle : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Purple Dungeon Candle");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.noWet = true;
        Item.useTurn = true;
        Item.maxStack = 99;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.PurpleDungeon.PurpleDungeonCandle>();
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
}
