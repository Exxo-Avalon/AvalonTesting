using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class ElementDiamond : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Element Diamond");
        Tooltip.SetDefault("Compound of the five elements");
        SacrificeTotal = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 0, 2, 0);
        Item.maxStack = 999;
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().TomeMaterial = true;
    }
}
