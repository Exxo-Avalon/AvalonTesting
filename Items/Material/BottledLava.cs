﻿using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class BottledLava : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bottled Lava");
        Tooltip.SetDefault("'Drinking may be fatal'");
        SacrificeTotal = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.maxStack = 100;
        Item.value = 50;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ItemID.Bottle).AddIngredient(ItemID.Obsidian).Register();
    }
}
