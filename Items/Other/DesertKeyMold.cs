using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Other;

class DesertKeyMold : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Desert Key Mold");
        Tooltip.SetDefault("Used for crafting a Desert Key");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Cyan;
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.height = dims.Height;
    }
}
