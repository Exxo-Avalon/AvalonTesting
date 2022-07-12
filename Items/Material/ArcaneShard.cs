using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Material;

class ArcaneShard : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Arcane Shard");
        Tooltip.SetDefault("'A fragment of light creatures'");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightPurple;
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = Item.sellPrice(0, 0, 6, 0);
        Item.height = dims.Height;
    }
}
