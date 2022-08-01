using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged;

class UnvolanditeFusebow : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Unvolandite Fusebow");
        Tooltip.SetDefault("Fires a spread of pulse arrows that explode on the final impact");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 98;
        Item.DamageType = DamageClass.Ranged;
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 16;
        Item.value = Item.sellPrice(0, 20, 0, 0);
        Item.rare = ModContent.RarityType<FireOrangeRarity>();
        Item.UseSound = SoundID.Item75;
        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.UnvolanditeBolt>();
        Item.shootSpeed = 14f;
        Item.useAmmo = AmmoID.Arrow;
        Item.height = dims.Height;
        Item.width = dims.Width;
    }

    //      public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
    //      {
    //if (type == ProjectileID.WoodenArrowFriendly)
    //	{
    //		type = ModContent.ProjectileType<Projectiles.UnvolanditeBolt>();
    //	}
    //return true;
    //}
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        int numberProjectiles = 2 + Main.rand.Next(2);
        for (int i = 0; i < numberProjectiles; i++)
        {
            Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(15));
            Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<Projectiles.Magic.UnvolanditeBolt>(), damage, knockback, player.whoAmI);
        }
        return false;
    }
}
