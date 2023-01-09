using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions;

class TitanskinPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Titanskin Potion");
        Tooltip.SetDefault("-8% damage, +15 defense");
        SacrificeTotal = 20;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[1]
        {
            Color.LightGray
        };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.Titanskin>();
        Item.consumable = true;
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.useTime = 15;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.maxStack = 100;
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.buffTime = 14400;
        Item.UseSound = SoundID.Item3;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.BottledLava>()).AddIngredient(ModContent.ItemType<Ore.RhodiumOre>()).AddIngredient(ModContent.ItemType<Material.Sweetstem>()).AddIngredient(ItemID.SoulofMight).AddTile(TileID.Bottles).Register();
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.BottledLava>()).AddIngredient(ModContent.ItemType<Ore.OsmiumOre>()).AddIngredient(ModContent.ItemType<Material.Sweetstem>()).AddIngredient(ItemID.SoulofMight).AddTile(TileID.Bottles).Register();
    }
}
