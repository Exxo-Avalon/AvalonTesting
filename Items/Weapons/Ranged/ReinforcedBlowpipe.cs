using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Weapons.Ranged;

class ReinforcedBlowpipe : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Reinforced Blowpipe");
        Tooltip.SetDefault("Fires a spread of two seeds\nAllows the collection of seeds for ammo");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 11;
        Item.autoReuse = true;
        Item.useAmmo = AmmoID.Dart;
        Item.UseSound = SoundID.Item63;
        Item.shootSpeed = 11f;
        Item.DamageType = DamageClass.Ranged;
        Item.rare = ItemRarityID.Blue;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 40;
        Item.knockBack = 3.25f;
        Item.shoot = ProjectileID.PurificationPowder;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 10000;
        Item.useAnimation = 40;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item5;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddRecipeGroup("AvalonTesting:SilverBar", 5).AddIngredient(ItemID.Blowpipe).AddTile(TileID.Anvils).Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        for (int num197 = 0; num197 < 2; num197++)
        {
            float num198 = velocity.X;
            float num199 = velocity.Y;
            num198 += Main.rand.Next(-35, 36) * 0.05f;
            num199 += Main.rand.Next(-35, 36) * 0.05f;
            Projectile.NewProjectile(source, position.X, position.Y, num198, num199, type, damage, knockback, player.whoAmI, 0f, 0f);
        }

        return false;
    }
}
