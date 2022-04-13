using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Accessories;

class ChaosCharm : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Chaos Charm");
        Tooltip.SetDefault("Critical strike chance increases as your health drops");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Pink;
        Item.width = dims.Width;
        Item.value = 150000;
        Item.accessory = true;
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.Avalon().chaosCharm = true;
    }
}
