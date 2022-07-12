using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Placeable.Light;

class OrangeDungeonCandle : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Orange Dungeon Candle");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.noWet = true;
        Item.useTurn = true;
        Item.maxStack = 99;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.OrangeDungeonCandle>();
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
}
