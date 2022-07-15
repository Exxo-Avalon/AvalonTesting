using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Ammo;

class TritonBullet : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Triton Bullet");
        SacrificeTotal = 99;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.shootSpeed = 11f;
        Item.damage = 17;
        Item.ammo = AmmoID.Bullet;
        Item.DamageType = DamageClass.Ranged;
        Item.consumable = true;
        Item.rare = ModContent.RarityType<BlueRarity>();
        Item.width = dims.Width;
        Item.knockBack = 20f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.TritonBullet>();
        Item.maxStack = 2000;
        Item.value = 1200;
        Item.height = dims.Height;
    }
}
