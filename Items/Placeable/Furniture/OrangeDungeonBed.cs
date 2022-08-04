using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

class OrangeDungeonBed : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Orange Bed");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.OrangeDungeon.OrangeDungeonBed>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 99;
        Item.value = 2000;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
}
