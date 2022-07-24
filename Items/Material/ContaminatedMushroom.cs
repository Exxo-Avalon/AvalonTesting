using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class ContaminatedMushroom : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Virulent Mushroom");
        SacrificeTotal = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = 50;
        Item.height = dims.Height;
    }
}
