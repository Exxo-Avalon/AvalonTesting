using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Material;

class RottenFlesh : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Rotten Flesh");
        SacrificeTotal = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.value = 10;
        Item.maxStack = 999;
        Item.height = dims.Height;
    }
}
