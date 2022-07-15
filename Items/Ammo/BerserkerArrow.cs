using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Ammo;

class BerserkerArrow : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Berserker Arrow");
        SacrificeTotal = 300;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 19;
        Item.shootSpeed = 4f;
        Item.ammo = AmmoID.Arrow;
        Item.DamageType = DamageClass.Ranged;
        Item.consumable = true;
        Item.rare = ItemRarityID.Cyan;
        Item.width = dims.Width;
        Item.knockBack = 4f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.BerserkerArrow>();
        Item.value = Item.sellPrice(0, 0, 2, 0);
        Item.maxStack = 2000;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(100)
            .AddIngredient(ItemID.WoodenArrow, 100)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.BerserkerBar>(), 2)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
    }
}
