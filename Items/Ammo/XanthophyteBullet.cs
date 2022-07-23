using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Ammo;

class XanthophyteBullet : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Xanthophyte Bullet");
        SacrificeTotal = 300;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.shootSpeed = 15f;
        Item.damage = 12;
        Item.ammo = AmmoID.Bullet;
        Item.DamageType = DamageClass.Ranged;
        Item.consumable = true;
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.knockBack = 4f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.XanthophyteBullet>();
        Item.maxStack = 2000;
        Item.value = 200;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(60)
            .AddIngredient(ItemID.MusketBall, 60)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.XanthophyteBar>())
            .AddTile(TileID.MythrilAnvil).Register();
    }
}
