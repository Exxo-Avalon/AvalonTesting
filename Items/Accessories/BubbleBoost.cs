using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

public class BubbleBoost : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bubble Boost");
        Tooltip.SetDefault("Allows the holder to bubble boost\nHold JUMP and a directional key to fly\n'A relic of starbound times past'");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ModContent.RarityType<BlueRarity>();
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 15, 0, 0);
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<Players.ExxoEquipEffectPlayer>().BubbleBoost = true;
        //player.Avalon().activateBubble = true;
        player.noFallDmg = true;
    }
}
