using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic;

class HauntedCross : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Haunted Cross");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Magic;
        Item.damage = 100;
        Item.autoReuse = true;
        Item.shootSpeed = 12f;
        Item.mana = 15;
        Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 19;
        Item.useAnimation = 19;
        Item.knockBack = 4.25f;
        Item.shoot = ModContent.ProjectileType<Projectiles.ShadowSpirit>();
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.value = Item.sellPrice(0, 30, 0, 0);
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item43;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        float killCount = player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.ShadowPortal>()];
        killCount = MathHelper.Clamp(killCount, 0f, 4f);
        if (Main.myPlayer == player.whoAmI)
        {
            for (int i = 0; i < 1 + killCount; i++)
            {
                Vector2 random = velocity.RotatedByRandom((1 + killCount) / 5);
                Projectile.NewProjectile(source, position.X, position.Y, random.X, random.Y, type, damage, knockback, player.whoAmI, 0f, 0f);
            }
        }
        return false;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ItemID.InfernoFork)
            .AddIngredient(ItemID.SpectreStaff)
            .AddIngredient(ItemID.ShadowbeamStaff)
            .AddIngredient(ModContent.ItemType<Material.SoulofBlight>(), 20)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>())
            .Register();
    }
}
