using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

class FeroziumArrow : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Ferozium Arrow");
        SacrificeTotal = 99;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 15;
        Item.shootSpeed = 4f;
        Item.ammo = AmmoID.Arrow;
        Item.DamageType = DamageClass.Ranged;
        Item.consumable = true;
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.knockBack = 4f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.FeroziumArrow>();
        Item.value = Item.sellPrice(0, 0, 1, 0);
        Item.maxStack = 2000;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type, 70)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.FeroziumBar>())
            .AddIngredient(ItemID.WoodenArrow, 70)
            .AddTile(TileID.MythrilAnvil).Register();
    }
}
