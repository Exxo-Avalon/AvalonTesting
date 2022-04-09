using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Weapons.Ranged;

class SackofToys : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Sack of Toys");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 45;
        Item.color = Color.Red;
        Item.shootSpeed = 14f;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.rare = ItemRarityID.Pink;
        Item.width = dims.Width;
        Item.useTime = 24;
        Item.knockBack = 4.5f;
        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 1, 0, 0);
        Item.useAnimation = 24;
        Item.height = dims.Height;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        int num164 = Main.rand.Next(14);
        if (num164 == 0)
        {
            for (int num165 = 0; num165 < 2; num165++)
            {
                float num166 = velocity.X;
                float num167 = velocity.Y;
                num166 += (float)Main.rand.Next(-30, 31) * 0.05f;
                num167 += (float)Main.rand.Next(-30, 31) * 0.05f;
                Projectile.NewProjectile(source, position.X, position.Y, num166, num167, ProjectileID.WoodenArrowFriendly, damage, knockback, player.whoAmI, 0f, 0f);
            }
        }
        else if (num164 == 1)
        {
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileID.FireArrow, damage, knockback, player.whoAmI, 0f, 0f);
        }
        else if (num164 == 2)
        {
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileID.Shuriken, damage, knockback, player.whoAmI, 0f, 0f);
        }
        else if (num164 == 3)
        {
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileID.JestersArrow, damage, knockback, player.whoAmI, 0f, 0f);
        }
        else if (num164 == 4)
        {
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileID.EnchantedBoomerang, damage, knockback, player.whoAmI, 0f, 0f);
        }
        else if (num164 == 5)
        {
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileID.Bullet, damage, knockback, player.whoAmI, 0f, 0f);
        }
        else if (num164 == 6)
        {
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileID.BallofFire, damage, knockback, player.whoAmI, 0f, 0f);
        }
        else if (num164 == 7)
        {
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileID.BallOHurt, damage, knockback, player.whoAmI, 0f, 0f);
        }
        else if (num164 == 8)
        {
            for (int num168 = 0; num168 < 2; num168++)
            {
                float num169 = velocity.X;
                float num170 = velocity.Y;
                num169 += (float)Main.rand.Next(-30, 31) * 0.05f;
                num170 += (float)Main.rand.Next(-30, 31) * 0.05f;
                Projectile.NewProjectile(source, position.X, position.Y, num169, num170, ProjectileID.WaterBolt, damage, knockback, player.whoAmI, 0f, 0f);
            }
        }
        else if (num164 == 9)
        {
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileID.Grenade, damage, knockback, player.whoAmI, 0f, 0f);
        }
        else if (num164 == 10)
        {
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileID.ThornChakram, damage, knockback, player.whoAmI, 0f, 0f);
        }
        else if (num164 == 11)
        {
            int num171 = Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileID.HarpyFeather, damage, knockback, player.whoAmI, 0f, 0f);
            Main.projectile[num171].hostile = false;
            Main.projectile[num171].friendly = true;
        }
        else if (num164 == 12)
        {
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileID.DemonScythe, damage, knockback, player.whoAmI, 0f, 0f);
        }
        else if (num164 == 13)
        {
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileID.PoisonedKnife, damage, knockback, player.whoAmI, 0f, 0f);
        }

        return false;
    }
}
