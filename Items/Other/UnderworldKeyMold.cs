﻿using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Other;

class UnderworldKeyMold : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Underworld Key Mold");
        Tooltip.SetDefault("Used for crafting an Underworld Key");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Purple;
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.height = dims.Height;
    }
}
