using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Material;

class YuckyBit : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Yucky Bit");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = 750;
        Item.height = dims.Height;
    }
}
