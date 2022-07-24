using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

internal class BandofStamina : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Band of Stamina");
        Tooltip.SetDefault("Increases maximum stamina by 90");
        SacrificeTotal = 1;
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
        player.GetModPlayer<ExxoStaminaPlayer>().StatStamMax2 += 90;
    }
}
