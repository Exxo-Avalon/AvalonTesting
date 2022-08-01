using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Summon;

public class ReflectorStaff : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Reflector Staff");
        Tooltip.SetDefault("Summons mirrors to reflect hostile projectiles");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Item.useStyle = 1;
        Item.shootSpeed = 14f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Summon.Reflector>();
        Item.damage = 120;
        Item.width = 38;
        Item.height = 36;
        Item.UseSound = SoundID.Item44;
        Item.buffType = ModContent.BuffType<Buffs.Reflector>();
        Item.useAnimation = 30;
        Item.useTime = 30;
        Item.noMelee = true;
        Item.value = Item.sellPrice(0, 30, 0, 0);
        Item.knockBack = 8.5f;
        Item.rare = 8;
        Item.DamageType = DamageClass.Summon;
        Item.mana = 30;
        //item.buffTime = 3600;
    }
    public override bool CanUseItem(Player player)
    {
        if (player.maxMinions > 6)
            return player.ownedProjectileCounts[Item.shoot] < 6;
        return player.ownedProjectileCounts[Item.shoot] < player.maxMinions;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
                               int type, int damage, float knockback)
    {
        player.AddBuff(Item.buffType, 2);
        player.SpawnMinionOnCursor(source, player.whoAmI, type, damage, knockback);
        return false;
    }
}
