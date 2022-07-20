using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Weapons.Ranged;

class HallowedBlowpipe : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Hallowed Blowpipe");
        Tooltip.SetDefault("Fires a spread of ten seeds\nAllows the collection of seeds for ammo");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 17;
        Item.autoReuse = true;
        Item.useAmmo = AmmoID.Dart;
        Item.UseSound = SoundID.Item63;
        Item.shootSpeed = 11f;
        Item.DamageType = DamageClass.Ranged;
        Item.rare = ItemRarityID.Pink;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 30;
        Item.knockBack = 3.25f;
        Item.shoot = ProjectileID.PurificationPowder;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 10000;
        Item.useAnimation = 30;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.HallowedBar, 13)
            .AddTile(TileID.MythrilAnvil).Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        for (int num200 = 0; num200 < 10; num200++)
        {
            float num201 = velocity.X;
            float num202 = velocity.Y;
            num201 += Main.rand.Next(-35, 36) * 0.05f;
            num202 += Main.rand.Next(-35, 36) * 0.05f;
            Projectile.NewProjectile(source, position.X, position.Y, num201, num202, type, damage, knockback, player.whoAmI, 0f, 0f);
        }

        return false;
    }
}
