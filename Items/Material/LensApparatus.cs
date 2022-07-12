using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Material;

class LensApparatus : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Lens Apparatus");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 10;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = Item.sellPrice(0, 0, 60, 0);
        Item.height = dims.Height;
    }
}
