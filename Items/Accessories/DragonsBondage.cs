using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Accessories;

public class DragonsBondage : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Dragon's Bondage");
        Tooltip.SetDefault("'Your victory weighs on you... literally.'");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = -12;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 15, 0, 0);
        //item.buffType = ModContent.BuffType<Buffs.DragonsChains>();
        Item.height = dims.Height;
        Item.expert = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.Avalon().dragonsBondage = true;
        //player.AddBuff(item.buffType, 2, true);
    }
}
