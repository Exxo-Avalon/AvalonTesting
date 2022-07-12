using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Material;

class Echoplasm : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Echoplasm");
        Tooltip.SetDefault("Used to craft Ectoplasm furniture");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = Item.sellPrice(0, 0, 5, 0);
        Item.height = dims.Height;
    }
}
