using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

class Rainbringer : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Rainbringer");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.useTime = 30;
        Item.shoot = ModContent.ProjectileType<Projectiles.Tools.Rainbringer>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 2, 70, 0);
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ItemID.RainCloud, 50).AddRecipeGroup(RecipeGroup.recipeGroupIDs["Avalon:CopperBar"], 10).AddIngredient(ItemID.SoulofNight, 10).AddTile(TileID.MythrilAnvil).Register();
    }
}
