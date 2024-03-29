﻿using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class SpikedBlastShell : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Spiked Blast Shell");
        SacrificeTotal = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ModContent.RarityType<Rarities.RainbowRarity>();
        Item.width = dims.Width;
        Item.value = 5000;
        Item.maxStack = 999;
        Item.height = dims.Height;
    }
}
