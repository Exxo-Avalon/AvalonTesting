using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Bar;

class CorruptedBar : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Corrupted Bar");
        SacrificeTotal = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.maxStack = 999;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.PlacedBars>();
        Item.placeStyle = 24;
        Item.rare = ItemRarityID.Pink;
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.value = Item.sellPrice(0, 1, 0, 0);
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type, 3)
            .AddIngredient(ModContent.ItemType<Tile.WickedShard>(), 2)
            .AddIngredient(ModContent.ItemType<Tile.HallowedOre>(), 12)
            .AddTile(ModContent.TileType<Tiles.CaesiumForge>())
            .Register();
    }
}
