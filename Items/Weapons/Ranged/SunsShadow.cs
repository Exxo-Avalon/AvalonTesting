using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Weapons.Ranged;

class SunsShadow : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Sun's Shadow");
        Tooltip.SetDefault("Fires a spread of twelve seeds\nAllows the collection of seeds for ammo");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 21;
        Item.autoReuse = true;
        Item.useAmmo = AmmoID.Dart;
        Item.UseSound = SoundID.Item63;
        Item.shootSpeed = 11f;
        Item.DamageType = DamageClass.Ranged;
        Item.rare = ItemRarityID.Yellow;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 28;
        Item.knockBack = 4f;
        Item.shoot = ProjectileID.PurificationPowder;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 10000;
        Item.useAnimation = 28;
        Item.height = dims.Height;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        for (int num203 = 0; num203 < 12; num203++)
        {
            float num204 = velocity.X;
            float num205 = velocity.Y;
            num204 += Main.rand.Next(-35, 36) * 0.05f;
            num205 += Main.rand.Next(-35, 36) * 0.05f;
            Projectile.NewProjectile(source, position.X, position.Y, num204, num205, type, damage, knockback, player.whoAmI, 0f, 0f);
        }

        return false;
    }
}
