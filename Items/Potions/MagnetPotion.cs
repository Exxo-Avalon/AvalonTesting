using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions;

class MagnetPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Magnet Potion");
        Tooltip.SetDefault("Increases item grab range");
        SacrificeTotal = 20;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.Magnet>();
        Item.consumable = true;
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.useTime = 15;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.maxStack = 100;
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.buffTime = 4 * 3600;
        Item.UseSound = SoundID.Item3;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.BottledLava>())
            .AddRecipeGroup(RecipeGroupID.IronBar)
            .AddIngredient(ItemID.Ebonkoi)
            .AddIngredient(ItemID.Blinkroot)
            .AddTile(TileID.Bottles)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.BottledLava>())
            .AddRecipeGroup(RecipeGroupID.IronBar)
            .AddIngredient(ItemID.Hemopiranha)
            .AddIngredient(ItemID.Blinkroot)
            .AddTile(TileID.Bottles)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.BottledLava>())
            .AddRecipeGroup(RecipeGroupID.IronBar)
            .AddIngredient(ModContent.ItemType<Fish.SicklyTrout>())
            .AddIngredient(ItemID.Blinkroot)
            .AddTile(TileID.Bottles)
            .Register();
    }
}
