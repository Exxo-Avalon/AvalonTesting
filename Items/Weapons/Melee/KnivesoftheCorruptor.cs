using Avalon.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

internal class KnivesoftheCorruptor : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Knives of the Corruptor");
        Tooltip.SetDefault("Rapidly throws daggers that explode into tiny eaters");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item1;
        Item.damage = 45;
        Item.noUseGraphic = true;
        Item.autoReuse = true;
        Item.shootSpeed = 15f;
        Item.noMelee = true;
        Item.rare = ItemRarityID.Cyan;
        Item.width = dims.Width;
        Item.useTime = 16;
        Item.knockBack = 5.75f;
        Item.shoot = ModContent.ProjectileType<CorruptKnife>();
        Item.DamageType = DamageClass.MeleeNoSpeed;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 50);
        Item.useAnimation = 16;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item39;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.VampireKnives)
            .AddIngredient(ItemID.ScourgeoftheCorruptor)
            .AddIngredient(ModContent.ItemType<Material.SoulofBlight>(), 30)
            .AddIngredient(ModContent.ItemType<Material.IllegalWeaponInstructions>())
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
                               int type, int damage, float knockback)
    {
        int numberProjectiles = Main.rand.Next(4, 8);
        for (int i = 0; i < numberProjectiles; i++)
        {
            Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(20));
            Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage,
                knockback, player.whoAmI);
        }

        return false;
    }
}
