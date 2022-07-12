using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace AvalonTesting.Items.Other;

class Quack : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Quack");
        Tooltip.SetDefault("'May annoy others'");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.noUseGraphic = true;
        Item.rare = ModContent.RarityType<Rarities.AvalonRarity>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 30;
        Item.useStyle = 10;
        Item.value = 100;
        Item.useAnimation = 30;
        Item.height = dims.Height;
    }

    public override bool? UseItem(Player player)
    {
        SoundEngine.PlaySound(SoundID.Zombie12, player.position);
        return true;
    }
}
