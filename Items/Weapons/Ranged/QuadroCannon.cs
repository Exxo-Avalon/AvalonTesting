using AvalonTesting.Items.Material;
using AvalonTesting.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Weapons.Ranged;

internal class QuadroCannon : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Quadro Cannon");
        Tooltip.SetDefault("Four round burst\nOnly the first shot consumes ammo and fires a spread of bullets");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 15;
        Item.autoReuse = true;
        Item.shootSpeed = 14f;
        Item.useAmmo = AmmoID.Bullet;
        Item.DamageType = DamageClass.Ranged;
        Item.rare = ItemRarityID.Yellow;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 4;
        Item.knockBack = 5f;
        Item.shoot = ProjectileID.Bullet;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 780000;
        Item.reuseDelay = 14;
        Item.useAnimation = 16;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item11;
    }

    public override void AddRecipes() => CreateRecipe().AddIngredient(ItemID.ClockworkAssaultRifle)
        .AddIngredient(ItemID.Shotgun).AddIngredient(ModContent.ItemType<DragonScale>(), 10)
        .AddIngredient(ItemID.SoulofFright).AddIngredient(ItemID.SoulofSight).AddIngredient(ItemID.SoulofMight)
        .AddIngredient(ModContent.ItemType<LensApparatus>()).AddIngredient(ModContent.ItemType<Onyx>(), 25)
        .AddTile(TileID.MythrilAnvil).Register();

    public override Vector2? HoldoutOffset() => new Vector2(-10, 0);

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
                               int type, int damage, float knockback)
    {
        // sound is weird sometimes?? idk why tho
        for (int num209 = 0; num209 < 4; num209++)
        {
            float num210 = velocity.X;
            float num211 = velocity.Y;
            num210 += Main.rand.Next(-24, 25) * 0.05f;
            num211 += Main.rand.Next(-24, 25) * 0.05f;
            Projectile.NewProjectile(source, position.X, position.Y, num210, num211, type, damage, knockback,
                player.whoAmI);
            SoundEngine.PlaySound(SoundID.Item11, player.position);
        }

        return false;
    }

    public override bool CanConsumeAmmo(Item ammo, Player player) => player.itemAnimation >= Item.useAnimation - 4;
}
