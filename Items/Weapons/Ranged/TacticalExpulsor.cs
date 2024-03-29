using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged;

class TacticalExpulsor : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Tactical Expulsor");
        Tooltip.SetDefault("Fires a spread of eight bullets");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 42;
        Item.height = 18;
        Item.damage = 35;
        Item.autoReuse = true;
        Item.useTurn = false;
        Item.useAmmo = AmmoID.Bullet;
        Item.shootSpeed = 7f;
        Item.crit++;
        Item.DamageType = DamageClass.Ranged;
        Item.rare = ModContent.RarityType<Rarities.MagentaRarity>();
        Item.noMelee = true;
        Item.knockBack = 3f;
        Item.useTime = 19;
        Item.shoot = ProjectileID.Bullet;
        Item.value = Item.sellPrice(0, 20, 0, 0);
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useAnimation = 19;
        Item.UseSound = SoundID.Item38;

    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.TacticalShotgun)
            .AddIngredient(ItemID.Ectoplasm, 40)
            .AddIngredient(ItemID.ShroomiteBar, 25)
            .AddIngredient(ModContent.ItemType<Material.SoulofBlight>(), 20)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-10f, 0f);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        for (int num194 = 0; num194 < 8; num194++)
        {
            float num195 = velocity.X;
            float num196 = velocity.Y;
            num195 += Main.rand.Next(-40, 41) * 0.05f;
            num196 += Main.rand.Next(-40, 41) * 0.05f;
            Projectile.NewProjectile(source, position.X, position.Y, num195, num196, type, damage, knockback, player.whoAmI, 0f, 0f);
        }

        return false;
    }
}
