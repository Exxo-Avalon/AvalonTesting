using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class MosquitoProboscis : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Mosquito Proboscis");
        SacrificeTotal = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = Item.sellPrice(0, 0, 0, 40);
        Item.height = dims.Height;
    }
}
