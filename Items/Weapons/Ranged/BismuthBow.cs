using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Weapons.Ranged;

class BismuthBow : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bismuth Bow");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item5;
        Item.damage = 11;
        Item.scale = 1f;
        Item.shootSpeed = 8f;
        Item.useAmmo = AmmoID.Arrow;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 23;
        Item.knockBack = 0f;
        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 9000;
        Item.useAnimation = 23;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Placeable.Bar.BismuthBar>(), 7).AddTile(TileID.Anvils).Register();
    }
}
