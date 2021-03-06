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
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Pink;
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = 0;
        Item.height = dims.Height;
    }
}
