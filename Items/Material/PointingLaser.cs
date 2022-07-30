using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class PointingLaser : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Pointing Laser");
        Tooltip.SetDefault("Used for crafting the Eye of Oblivion");
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
        Item.shootSpeed = 15f;
        Item.width = dims.Width;
        Item.channel = true;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.maxStack = 999;
        Item.value = 0;
        Item.height = dims.Height;
    }
}
