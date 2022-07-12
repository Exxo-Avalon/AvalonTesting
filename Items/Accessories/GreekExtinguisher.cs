using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Accessories;

class GreekExtinguisher : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Greek Extinguisher");
        Tooltip.SetDefault("Immunity to Cursed Inferno");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightPurple;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = 100000;
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.CursedInferno] = true;
    }
}
