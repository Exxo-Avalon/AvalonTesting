using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions;

class SupersonicPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Supersonic Potion");
        Tooltip.SetDefault("Increases movement speed to the maximum");
        SacrificeTotal = 20;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[1]
        {
            Color.Gray
        };
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
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Holybird>())
            .AddIngredient(ItemID.Cobweb, 5)
            .AddIngredient(ItemID.Cloud)
            .AddIngredient(ModContent.ItemType<Material.LifeDew>())
            .AddIngredient(ModContent.ItemType<Material.BottledLava>())
            .AddTile(TileID.Bottles)
            .Register();
    }
}
