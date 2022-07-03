﻿using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Placeable.Tile.Ancient;

public class AncientSilverBrick : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Ancient Silver Brick");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Ancient.AncientSilverBrick>();
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.useTurn = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 999;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ItemID.SilverBrick).AddTile(ModContent.TileType<Tiles.Ancient.AncientWorkbench>()).Register();
        CreateRecipe(1).AddIngredient(this).AddTile(ModContent.TileType<Tiles.Ancient.AncientWorkbench>()).Register();
    }
}
