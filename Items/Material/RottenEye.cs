using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Material;

class RottenEye : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Rotten Eye");
        Tooltip.SetDefault("'Looks nasty!'");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.value = 90;
        Item.maxStack = 999;
        Item.height = dims.Height;
    }
}
