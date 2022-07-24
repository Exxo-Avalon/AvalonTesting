using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Bar;

public class XanthophyteBar : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Xanthophyte Bar");
        SacrificeTotal = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.PlacedBars>();
        Item.rare = ItemRarityID.Yellow;
        Item.placeStyle = 23;
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.useTurn = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 999;
        Item.value = Item.sellPrice(0, 0, 15, 0);
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Tile.XanthophyteOre>(), 5).AddTile(TileID.AdamantiteForge).Register();
    }
}
