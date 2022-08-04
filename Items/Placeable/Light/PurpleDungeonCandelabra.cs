using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Light;

class PurpleDungeonCandelabra : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Purple Dungeon Candelabra");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.PurpleDungeon.PurpleDungeonCandelabra>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 99;
        Item.value = 1500;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
}