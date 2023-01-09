using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions;

class BloodCastPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Blood Cast Potion");
        Tooltip.SetDefault("Adds your max life to your max mana");
        SacrificeTotal = 20;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[1]
        {
            Color.Red
        };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.BloodCast>();
        Item.UseSound = SoundID.Item3;
        Item.consumable = true;
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.useTime = 15;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.maxStack = 100;
        Item.value = Item.sellPrice(0, 0, 5, 0);
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.buffTime = 18000;
    }
    public override void AddRecipes()
    {
        CreateRecipe(6).AddIngredient(ItemID.LifeCrystal).AddIngredient(ItemID.ManaCrystal).AddIngredient(ModContent.ItemType<Material.BottledLava>()).AddIngredient(ItemID.Ectoplasm).AddTile(TileID.Bottles).Register();
    }
}
