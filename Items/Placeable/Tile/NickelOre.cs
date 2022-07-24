﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

class NickelOre : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Nickel Ore");
        SacrificeTotal = 100;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Ores.NickelOre>();
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.useTurn = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 999;
        Item.value = Item.sellPrice(0, 0, 1, 25);
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
}
