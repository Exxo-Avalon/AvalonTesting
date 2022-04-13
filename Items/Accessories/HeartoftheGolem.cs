using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Accessories;

class HeartoftheGolem : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Heart of the Golem");
        Tooltip.SetDefault("Grants a 5% chance for enemies to drop a platinum heart");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 1, 0, 0);
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.Avalon().heartGolem = true;
    }
}
