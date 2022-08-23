using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

class ChaosCrystal : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Chaos Crystal");
        Tooltip.SetDefault("75% increased critical strike damage");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 3, 0, 0);
        Item.height = dims.Height;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.Avalon().CritDamageMult += 0.75f;
    }
}
