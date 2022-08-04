using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

class PurpleDungeonChair : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Purple Dungeon Chair");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.PurpleDungeon.PurpleDungeonChair>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 99;
        Item.value = 200;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
}