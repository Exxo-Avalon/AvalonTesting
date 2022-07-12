using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Fish;

class NauSeaFish : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Nau-Sea-a Fish");
        Tooltip.SetDefault("'Get it?'");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.maxStack = 999;
        Item.width = dims.Width;
        Item.height = dims.Height;
        Item.rare = ItemRarityID.Blue;
        Item.value = 7500;
    }
}
