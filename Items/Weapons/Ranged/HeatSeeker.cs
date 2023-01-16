using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged;

class HeatSeeker : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Heat Seeker");
        Tooltip.SetDefault("Rockets turn into heat-seeking missiles");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 98;
        Item.autoReuse = true;
        Item.useTurn = false;
        Item.useAmmo = AmmoID.Rocket;
        Item.shootSpeed = 10f;
        Item.crit += 3;
        Item.DamageType = DamageClass.Ranged;
        Item.rare = ModContent.RarityType<Rarities.YellowGreenRarity>();
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.knockBack = 5f;
        Item.useTime = 15;
        Item.shoot = ProjectileID.RocketI;
        Item.value = Item.sellPrice(1, 0, 0, 0);
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item11;

    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-10f, 0f);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectile(source, position + new Vector2(60,-6 * player.direction).RotatedBy(velocity.ToRotation()),velocity, ModContent.ProjectileType<Projectiles.Ranged.HomingRocketFriendly>(), damage, knockback, player.whoAmI, 0f, 0f);
        return false;
    }
}
