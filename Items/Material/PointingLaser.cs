using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class PointingLaser : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Pointing Laser");
        Tooltip.SetDefault("Used for crafting the Eye of Oblivion\nCan be pointed");
        SacrificeTotal = 25;
        Terraria.Item.staff[Item.type] = true;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Pink;
        Item.useAnimation = 15;
        Item.useTime = 15;
        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.ArrowBeam>();
        Item.shootSpeed = 6f;
        Item.width = dims.Width;
        Item.channel = true;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.maxStack = 999;
        Item.value = 0;
        Item.height = dims.Height;
    }
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {

    }
}
