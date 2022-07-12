using AvalonTesting.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Placeable.Furniture;

internal class CoughwoodChair : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Coughwood Chair");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.CoughwoodChair>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 99;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        Recipe.Create(ModContent.ItemType<CoughwoodChair>())
            .AddIngredient(ModContent.ItemType<Coughwood>(), 4)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
