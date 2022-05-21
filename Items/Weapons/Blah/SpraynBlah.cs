using System;
using AvalonTesting.Items.Material;
using AvalonTesting.Items.Placeable.Bar;
using AvalonTesting.Items.Weapons.Ranged;
using AvalonTesting.Rarities;
using AvalonTesting.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Weapons.Blah;

internal class SpraynBlah : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Spray 'n' Blah");
        Tooltip.SetDefault("Fires very inaccurately\n30% chance to not consume ammo\n'Spray 'n' Pray'");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 131;
        Item.autoReuse = true;
        Item.useTurn = false;
        Item.useAmmo = AmmoID.Bullet;
        Item.shootSpeed = 13f;
        Item.crit += 4;
        Item.DamageType = DamageClass.Ranged;
        Item.rare = ModContent.RarityType<BlahRarity>();
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.knockBack = 3f;
        Item.useTime = 4;
        Item.shoot = ProjectileID.Bullet;
        Item.value = Item.sellPrice(1);
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useAnimation = 4;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item41;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<Placeable.Tile.Phantoplasm>(), 45)
        .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 40)
        .AddIngredient(ModContent.ItemType<SoulofTorture>(), 45).AddIngredient(ModContent.ItemType<PlanterasFury>())
        .AddIngredient(ItemID.ChainGun).AddIngredient(ItemID.Megashark).AddTile(ModContent.TileType<SolariumAnvil>())
        .Register();

    public override Vector2? HoldoutOffset() => new Vector2(-10f, 0f);

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
                               int type, int damage, float knockback)
    {
        float num78 = velocity.X + (Main.rand.Next(-200, 201) * 0.05f);
        float num79 = velocity.Y + (Main.rand.Next(-200, 201) * 0.05f);
        Projectile.NewProjectile(source, position.X, position.Y, num78, num79, type, damage, knockback, player.whoAmI);
        return false;
    }

    public override void HoldItem(Player player)
    {
        var vector = new Vector2(player.position.X + (player.width * 0.5f), player.position.Y + (player.height * 0.5f));
        float num70 = Main.mouseX + Main.screenPosition.X - vector.X;
        float num71 = Main.mouseY + Main.screenPosition.Y - vector.Y;
        if (player.gravDir == -1f)
        {
            num71 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector.Y;
        }

        float num72 = (float)Math.Sqrt((num70 * num70) + (num71 * num71));
        float num73 = num72;
        num72 = player.inventory[player.selectedItem].shootSpeed / num72;
        if (player.inventory[player.selectedItem].type == Item.type)
        {
            num70 += Main.rand.Next(-100, 101) * 0.03f / num72;
            num71 += Main.rand.Next(-100, 101) * 0.03f / num72;
        }

        num70 *= num72;
        num71 *= num72;
        player.itemRotation = (float)Math.Atan2(num71 * player.direction, num70 * player.direction);
    }

    public override bool CanConsumeAmmo(Item ammo, Player player) => Main.rand.Next(10) >= 3;
}
