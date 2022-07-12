using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Accessories;

class UndeadTalisman : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Undead Talisman");
        Tooltip.SetDefault("Provides 20 defense against undead monsters");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 0, 50, 0);
        Item.accessory = true;
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.Avalon().undeadTalisman = true;
    }
}
