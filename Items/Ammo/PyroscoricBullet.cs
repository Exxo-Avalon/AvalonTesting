using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

class PyroscoricBullet : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Pyroscoric Bullet");
        Tooltip.SetDefault("Explodes periodically");
        SacrificeTotal = 99;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.shootSpeed = 9f;
        Item.damage = 19;
        Item.ammo = AmmoID.Bullet;
        Item.DamageType = DamageClass.Ranged;
        Item.consumable = true;
        Item.rare = ModContent.RarityType<MagentaRarity>();
        Item.width = dims.Width;
        Item.knockBack = 4f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.PyroscoricBullet>();
        Item.maxStack = 2000;
        Item.value = 1200;
        Item.height = dims.Height;
    }
}
