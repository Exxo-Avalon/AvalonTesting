using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Ammo;

class PhantasmalBullet : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Phantasmal Bullet");
        Tooltip.SetDefault("Passes through normal tiles\n[c/C39FDD:10th Anniversary Contest Winner - QuibopWon]");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
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
        Item.shoot = ModContent.ProjectileType<Projectiles.PhantasmalBullet>();
        Item.maxStack = 2000;
        Item.value = 1200;
        Item.height = dims.Height;
    }
}
