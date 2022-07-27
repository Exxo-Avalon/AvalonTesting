using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;

namespace Avalon.Items.Material;

class Root : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Root");
        SacrificeTotal = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = Item.sellPrice(silver: 1);
        Item.height = dims.Height;
    }
}
