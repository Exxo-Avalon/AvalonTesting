using AvalonTesting.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Weapons.Melee;

internal class VirulentKnives : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Virulent Knives");
        Tooltip.SetDefault("Throws homing knives");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item1;
        Item.damage = 46;
        Item.noUseGraphic = true;
        Item.autoReuse = true;
        Item.shootSpeed = 11f;
        Item.noMelee = true;
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.useTime = 18;
        Item.knockBack = 3f;
        Item.shoot = ModContent.ProjectileType<YuckyKnife>();
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 20);
        Item.useAnimation = 18;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item39;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
                               int type, int damage, float knockback)
    {
        int numberProjectiles = Main.rand.Next(1, 5);
        for (int i = 0; i < numberProjectiles; i++)
        {
            Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(20));
            Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage,
                knockback, player.whoAmI);
        }

        return false;
    }
}
