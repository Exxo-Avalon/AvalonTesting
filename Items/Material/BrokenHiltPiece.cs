using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class BrokenHiltPiece : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Broken Hilt Piece");
        SacrificeTotal = 5;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.value = 50;
        Item.maxStack = 999;
        Item.height = dims.Height;
    }
}
