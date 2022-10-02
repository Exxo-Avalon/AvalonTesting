using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

class PatellaBullet : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Patella Bullet");
        Tooltip.SetDefault("Slow speed, low range, but high damage");
        SacrificeTotal = 99;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 15;
        Item.shootSpeed = 3f;
        Item.ammo = AmmoID.Bullet;
        Item.DamageType = DamageClass.Ranged;
        Item.consumable = true;
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.knockBack = 3f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.PatellaBullet>();
        Item.value = 10;
        Item.maxStack = 2000;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(25).AddIngredient(ItemID.MusketBall, 25).AddIngredient(ItemID.Vertebrae, 5).AddIngredient(ItemID.Ichor).AddTile(TileID.MythrilAnvil).Register();
    }
}
