using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class ElementShard : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Element Shard");
        Tooltip.SetDefault("'A fragment of the elements'");
        SacrificeTotal = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = 3000;
        Item.height = dims.Height;
    }
}
