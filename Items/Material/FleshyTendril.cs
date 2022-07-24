using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class FleshyTendril : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Fleshy Tendril");
        SacrificeTotal = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.value = 50;
        Item.maxStack = 999;
        Item.height = dims.Height;
    }
}
