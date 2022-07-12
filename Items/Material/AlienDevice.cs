using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Material;

public class AlienDevice : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Alien Device");
        Tooltip.SetDefault("Used for crafting the Eye of Oblivion");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 10;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Cyan;
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = 0;
        Item.height = dims.Height;
    }
}
