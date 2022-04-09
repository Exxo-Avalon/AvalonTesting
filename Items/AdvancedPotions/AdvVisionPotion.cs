﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.AdvancedPotions;

class AdvVisionPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Vision Elixir");
        Tooltip.SetDefault("Open caves light up");
    }

    public override void SetDefaults()
    {
        Rectangle dims = global::AvalonTesting.GetDims("Items/AdvancedPotions/AdvVisionPotion");
        Item.buffType = ModContent.BuffType<Buffs.AdvancedBuffs.AdvVision>();
        Item.UseSound = SoundID.Item3;
        Item.consumable = true;
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.useTime = 15;
        Item.useStyle = ItemUseStyleID.EatFood;
        Item.maxStack = 100;
        Item.value = Item.sellPrice(0, 0, 4, 0);
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.buffTime = 21600;
    }
}