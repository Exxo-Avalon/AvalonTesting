using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged;

class GleamingTwilight : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Gleaming Twilight");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 38;
        Item.height = 26;
        Item.damage = 90;
        Item.autoReuse = true;
        Item.useAmmo = AmmoID.Arrow;
        Item.shootSpeed = 11f;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.rare = ModContent.RarityType<BlueRarity>();
        Item.useTime = 15;
        Item.knockBack = 4.5f;
        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 1000000;
        Item.useAnimation = 15;
        Item.UseSound = SoundID.Item5;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ItemID.ChlorophyteShotbow).AddIngredient(ItemID.HallowedRepeater).AddIngredient(ModContent.ItemType<Material.SoulofBlight>(), 20).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        for (int num188 = 0; num188 < 5; num188++)
        {
            float num189 = velocity.X;
            float num190 = velocity.Y;
            num189 += (float)Main.rand.Next(-40, 41) * 0.05f;
            num190 += (float)Main.rand.Next(-40, 41) * 0.05f;
            Projectile.NewProjectile(source, position.X, position.Y, num189, num190, type, damage, knockback, player.whoAmI, 0f, 0f);
        }
        return false;
    }
}
