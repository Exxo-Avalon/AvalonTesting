using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Potions;

class SupersonicPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Supersonic Potion");
        Tooltip.SetDefault("Increases movement speed to the maximum");
        SacrificeTotal = 20;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.Supersonic>();
        Item.consumable = true;
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.useTime = 15;
        Item.value = 2000;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.maxStack = 100;
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.buffTime = 6 * 3600;
        Item.UseSound = SoundID.Item3;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Holybird>()).AddIngredient(ItemID.Cobweb, 5).AddIngredient(ItemID.Cloud).AddIngredient(ItemID.SoulofLight).AddIngredient(ModContent.ItemType<Material.BottledLava>()).AddTile(TileID.Bottles).Register();
    }
}
