﻿using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

class JungleBomb : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Jungle Bomb");
        Tooltip.SetDefault("Converts tiles to the Jungle in a large radius");
        SacrificeTotal = 5;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.maxStack = 999;
        Item.mech = true;
        Item.createTile = ModContent.TileType<Tiles.BiomeBombs>();
        Item.placeStyle = 2;
        Item.consumable = true;
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.useTime = 15;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
}
