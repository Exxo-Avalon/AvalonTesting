using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Crafting;

internal class Catalyzer : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Catalyzer");
        Tooltip.SetDefault("Used to convert items to their counterparts");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Catalyzer>();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 99;
        Item.value = 50000;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddRecipeGroup(RecipeGroupID.Wood, 20)
            .AddIngredient(ModContent.ItemType<Material.Sulphur>(), 30)
            .AddRecipeGroup("IronBar", 15)
            .AddRecipeGroup("Avalon:WorkBenches")
            .AddTile(TileID.Anvils)
            .Register();
    }
}
