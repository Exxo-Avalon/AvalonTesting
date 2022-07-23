using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Material;

class FourLeafClover : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Four Leaf Clover");
        Tooltip.SetDefault("You are very lucky to have found this!");
        SacrificeTotal = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = Item.sellPrice(5);
        Item.height = dims.Height;
    }
}
