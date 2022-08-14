using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

class PhantasmalBullet : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Phantasmal Bullet");
        Tooltip.SetDefault("Phases through normal tiles\n[c/C39FDD:10th Anniversary Contest Winner - QuibopWon]");
        SacrificeTotal = 99;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.shootSpeed = 11f;
        Item.damage = 18;
        Item.ammo = AmmoID.Bullet;
        Item.DamageType = DamageClass.Ranged;
        Item.consumable = true;
        Item.rare = ModContent.RarityType<Rarities.QuibopsRarity>();
        Item.width = dims.Width;
        Item.knockBack = 6f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.PhantasmalBullet>();
        Item.maxStack = 2000;
        Item.value = 1200;
        Item.height = dims.Height;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Vector2 pos = player.Center + new Vector2(50, 0).RotatedBy(player.AngleTo(Main.MouseWorld));
        Projectile.NewProjectile(source, pos.X, pos.Y, velocity.X * 3, velocity.Y * 3, Type, damage, knockback);
        return false;
    }
}
