using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class YuckyBit : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Yucky Bit");
        SacrificeTotal = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = 750;
        Item.height = dims.Height;
    }
}
