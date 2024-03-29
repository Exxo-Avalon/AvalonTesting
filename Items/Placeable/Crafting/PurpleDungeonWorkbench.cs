using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Crafting;

class PurpleDungeonWorkbench : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Purple Dungeon Work Bench");
        Tooltip.SetDefault("Used for basic crafting");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.PurpleDungeon.PurpleDungeonWorkbench>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 99;
        Item.value = 150;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
}
