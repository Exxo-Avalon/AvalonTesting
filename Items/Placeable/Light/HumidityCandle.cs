﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Placeable.Light;

class HumidityCandle : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Candle of Humidity");
        Tooltip.SetDefault("Nearby players have cold resistance");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.SoulCandles.HumidityCandle>();
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.useTurn = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 999;
        Item.value = Item.sellPrice(0, 0, 10);
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.SoulofHumidity>(), 15).AddIngredient(ItemID.Candle).AddTile(TileID.MythrilAnvil).Register();
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.SoulofHumidity>(), 15).AddIngredient(ItemID.PlatinumCandle).AddTile(TileID.MythrilAnvil).Register();
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.SoulofHumidity>(), 15).AddIngredient(ModContent.ItemType<Placeable.Light.BismuthCandle>()).AddTile(TileID.MythrilAnvil).Register();
    }
}
