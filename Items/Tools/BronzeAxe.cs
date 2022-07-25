using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

class BronzeAxe : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bronze Axe");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.TinAxe);
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.BronzeBar>(), 9)
            .AddRecipeGroup(RecipeGroupID.Wood, 3)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
