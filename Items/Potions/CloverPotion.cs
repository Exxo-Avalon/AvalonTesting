using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Potions;

class CloverPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Clover Potion");
        Tooltip.SetDefault("Doubles rare drop chance");
        SacrificeTotal = 20;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.Luck>();
        Item.consumable = true;
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.useTime = 15;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.maxStack = 100;
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.buffTime = 108000;
        Item.UseSound = SoundID.Item3;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.FakeFourLeafClover>()).AddIngredient(ModContent.ItemType<Material.BottledLava>()).AddIngredient(ModContent.ItemType<Material.Holybird>()).AddIngredient(ItemID.Fireblossom).AddTile(TileID.Bottles).Register();
        CreateRecipe(20).AddIngredient(ModContent.ItemType<Material.FourLeafClover>()).AddIngredient(ModContent.ItemType<Material.BottledLava>(), 20).AddIngredient(ModContent.ItemType<Material.Holybird>(), 20).AddIngredient(ItemID.Fireblossom, 20).AddTile(TileID.Bottles).Register();
    }
}
