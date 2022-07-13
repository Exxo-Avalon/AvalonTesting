using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Placeable.Tile;

class CoughwoodPlatform : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Coughwood Platform");
        SacrificeTotal = 100;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.createTile = ModContent.TileType<Tiles.CoughwoodPlatform>();
        Item.consumable = true;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 999;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(2).AddIngredient(ModContent.ItemType<Coughwood>()).Register();
        CreateRecipe(1).AddIngredient(this, 2).Register();
    }
}
