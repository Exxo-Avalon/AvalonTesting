using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class ScrollofTome : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Scroll of Tome");
        Tooltip.SetDefault("Vital in the creation of mid-to-lategame tomes");
        SacrificeTotal = 2;
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
