﻿using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Placeable.Crafting;

class CrimsonAltar : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Crimson Altar");
        Tooltip.SetDefault("The spirit of Cthulhu guards this altar");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.EvilAltarsPlaced>();
        Item.placeStyle = 1;
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.useTime = 20;
        Item.maxStack = 99;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 20;
        Item.height = dims.Height;
    }
}
