using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Ammo;

class SpectralBullet : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Spectral Bullet");
        Tooltip.SetDefault("Passes through normal tiles\n[c/C39FDD:10th Anniversary Contest Winner - QuibopWon]");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.shootSpeed = 11f;
        Item.damage = 12;
        Item.ammo = AmmoID.Bullet;
        Item.DamageType = DamageClass.Ranged;
        Item.consumable = true;
        Item.rare = ModContent.RarityType<Rarities.QuibopsRarity>();
        Item.width = dims.Width;
        Item.knockBack = 6f;
        Item.shoot = ModContent.ProjectileType<Projectiles.SpectralBullet>();
        Item.maxStack = 2000;
        Item.value = 1200;
        Item.height = dims.Height;
    }
}
