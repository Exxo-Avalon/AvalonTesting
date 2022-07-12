using AvalonTesting.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Placeable.Crafting;

internal class CoughwoodWorkBench : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Coughwood Work Bench");
        Tooltip.SetDefault("Used for basic crafting");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<CoughwoodWorkbench>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 99;
        Item.value = 150;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        Recipe.Create(ModContent.ItemType<CoughwoodWorkBench>())
            .AddIngredient(ModContent.ItemType<Tile.Coughwood>(), 10).Register();
    }
}
