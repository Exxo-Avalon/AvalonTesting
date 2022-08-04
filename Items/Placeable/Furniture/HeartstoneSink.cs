using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

public class HeartstoneSink : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Heartstone Sink");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.Heartstone.HeartstoneSink>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 99;
        Item.value = 300;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
}
