using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Material;

class IceGel : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Ice Gel");
        Tooltip.SetDefault("'Sticky and slippery!'");
        SacrificeTotal = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.White;
        Item.width = dims.Width;
        Item.value = 700;
        Item.maxStack = 999;
        Item.height = dims.Height;
    }
}
