using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions;

class ImmortalityPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Immortality Potion");
        Tooltip.SetDefault("Resurrect in place with 33% life when you are killed\nHas a 180 second cooldown");
        SacrificeTotal = 20;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.maxStack = 100;
        Item.height = dims.Height;
    }
    //public override void AddRecipes()
    //{
    //    CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.BottledLava>()).AddIngredient(ModContent.ItemType<Material.Sweetstem>()).AddIngredient(ItemID.Blinkroot).AddIngredient(ItemID.SpecularFish).AddTile(TileID.Bottles).Register();
    //}
}
