using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace AvalonTesting.Items.Weapons.Summon;

public class UltrablivionStaff : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Ultrablivion Staff");
        Tooltip.SetDefault("Summons a mini Ultrablivion to fight for you");
        Item.staff[Item.type] = true;
    }

    public override void SetDefaults()
    {
        Item.DamageType = DamageClass.Summon;
        Item.mana = 5;
        Item.width = 152;
        Item.height = 152;
        Item.useAnimation = 30;
        Item.useTime = 30;
        Item.useStyle = 1;
        Item.noMelee = true;
        Item.damage = 100;
        Item.knockBack = 4;
        Item.value = Item.buyPrice(10, 0, 0, 0);
        Item.rare = 11;
        Item.expert = true;
        Item.UseSound = SoundID.Item44;
        Item.shoot = ModContent.ProjectileType<Projectiles.Summon.UltraHMinion>();
        Item.shootSpeed = 10f;
    }

        
    public override bool AltFunctionUse(Player player)
    {
        return true;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (player.altFunctionUse != 2)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.Summon.UltraHMinion>(), damage, knockback, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.Summon.UltraRMinion>(), damage, knockback, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(source, position + new Vector2(100, 0), velocity, ModContent.ProjectileType<Projectiles.Summon.UltraLMinion>(), damage, knockback, player.whoAmI, 0f, 0f);
        }
        return false;
    }

    public override bool? UseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
            player.MinionNPCTargetAim(true);
        }
        return base.UseItem(player);
    }
}
