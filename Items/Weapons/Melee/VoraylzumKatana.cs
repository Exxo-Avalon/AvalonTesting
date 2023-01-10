using Avalon.Projectiles.Melee;
using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class VoraylzumKatana : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Vorazylcum Katana");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 38;
        Item.height = 40;
        Item.damage = 111;
        Item.autoReuse = true;
        Item.rare = ModContent.RarityType<FireOrangeRarity>();
        Item.knockBack = 4f;
        Item.useTime = 17;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 10, 90, 0);
        Item.useAnimation = 17;
        Item.UseSound = SoundID.Item1;
        Item.scale = 1f;
        Item.shoot = ModContent.ProjectileType<VorazylcumKatanaSlash>();
        Item.reuseDelay = 2;
        Item.noMelee = true;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        int p1 = Projectile.NewProjectile(source, position, new Vector2(player.direction, 0), ModContent.ProjectileType<VorazylcumKatanaSlash>(), damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax);
        return false;
    }
}

//TODO When the recipe gets put here add like 7 kunzite to it kthxbye
