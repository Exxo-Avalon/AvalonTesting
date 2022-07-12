using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Fish;

class Ickfish : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Ickfish");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.value = 10;
        Item.maxStack = 999;
        Item.height = dims.Height;
    }
}
