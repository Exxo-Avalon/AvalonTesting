using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class DesertFeather : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Desert Feather");
        SacrificeTotal = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 0, 1, 0);
        Item.maxStack = 999;
        Item.height = dims.Height;
    }
}
