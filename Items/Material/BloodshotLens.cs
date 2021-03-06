using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class BloodshotLens : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bloodshot Lens");
        SacrificeTotal = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = Item.sellPrice(0, 0, 20, 0);
        Item.height = dims.Height;
    }
}
