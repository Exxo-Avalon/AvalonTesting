using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Material;

class BrokenHiltPiece : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Broken Hilt Piece");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;
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
