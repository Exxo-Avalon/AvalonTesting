﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Vanity;

[AutoloadEquip(EquipType.Body)]
class BloodstainedChestplate : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bloodstained Chestplate");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.vanity = true;
        Item.value = Item.sellPrice(0, 0, 50, 0);
        Item.height = dims.Height;
    }
}
