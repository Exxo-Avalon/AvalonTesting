using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class Patella : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Patella");
        SacrificeTotal = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.value = 90;
        Item.maxStack = 999;
        Item.height = dims.Height;
    }
}
