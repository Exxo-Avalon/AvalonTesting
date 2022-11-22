using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class MysticalTomePage : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Mystical Tome Page");
        Tooltip.SetDefault("Used to craft tomes");
        SacrificeTotal = 20;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = Item.sellPrice(0, 0, 2, 0);
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().TomeMaterial = true;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ItemID.FallenStar, 2).AddRecipeGroup("IronBar").AddRecipeGroup("Wood", 3).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
    }
}
