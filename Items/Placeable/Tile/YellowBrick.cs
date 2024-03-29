using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

class YellowBrick : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Yellow Brick");
        SacrificeTotal = 100;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.YellowBrick>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 999;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    /* public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Wall.YellowBrickWall>(), 4).AddTile(TileID.WorkBenches).Register();
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Wall.YellowSlabWall>(), 4).AddTile(TileID.WorkBenches).Register();
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Wall.YellowTiledWall>(), 4).AddTile(TileID.WorkBenches).Register();
    } */
}
