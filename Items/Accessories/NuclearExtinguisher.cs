using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Accessories;

class NuclearExtinguisher : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Nuclear Extinguisher");
        Tooltip.SetDefault("Immunity to Blackout and Cursed Inferno");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = 200000;
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.Blackout] = true;
        player.buffImmune[BuffID.CursedInferno] = true;
    }
}
