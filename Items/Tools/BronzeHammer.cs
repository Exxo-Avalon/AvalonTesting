using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

class BronzeHammer : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bronze Hammer");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.TinHammer);
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.BronzeBar>(), 10)
            .AddRecipeGroup(RecipeGroupID.Wood, 3)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
