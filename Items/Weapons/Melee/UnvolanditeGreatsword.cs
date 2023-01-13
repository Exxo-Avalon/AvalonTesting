using Avalon.Projectiles.Melee;
using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class UnvolanditeGreatsword : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Unvolandite Greatsword");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 52;
        Item.height = 54;
        Item.damage = 109;
        Item.autoReuse = true;
        Item.rare = ModContent.RarityType<FireOrangeRarity>();
        Item.knockBack = 7f;
        Item.useTime = 22;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 9, 87, 65);
        Item.useAnimation = 22;
        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<UnvolanditeGreatswordSlash>();
        Item.reuseDelay = 2;
        Item.noMelee = true;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        int p1 = Projectile.NewProjectile(source, position, new Vector2(player.direction, 0), ModContent.ProjectileType<UnvolanditeGreatswordSlashSwirl>(), damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax * 1.4f);

        int p2 = Projectile.NewProjectile(source, position, new Vector2(player.direction, 0), ModContent.ProjectileType<UnvolanditeGreatswordSlash>(), damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax);
        return false;
    }
}
