using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Material;

class DarkMatterGel : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Dark Matter Gel");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 50;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.White;
        Item.width = dims.Width;
        Item.scale = 1f;
        Item.maxStack = 999;
        Item.value = 20;
        Item.height = dims.Height;
    }
}
