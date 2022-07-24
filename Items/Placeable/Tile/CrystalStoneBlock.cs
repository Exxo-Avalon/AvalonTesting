﻿using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

class CrystalStoneBlock : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Crystal Stone Block");
        SacrificeTotal = 100;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.CrystalStone>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 999;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
}
