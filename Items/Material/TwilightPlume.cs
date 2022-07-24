using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class TwilightPlume : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Twilight Plume");
        SacrificeTotal = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = 100;
        Item.height = dims.Height;
    }
}
