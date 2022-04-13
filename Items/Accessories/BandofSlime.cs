using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Accessories;

internal class BandofSlime : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Band of Slime");
        Tooltip.SetDefault("Reduces damage taken by 5% and negates fall damage\nAll tiles are slippery");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = 50000;
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.endurance += 0.05f;
        player.noFallDmg = true;
        player.slippy = true;
        player.slippy2 = true;
    }
}
