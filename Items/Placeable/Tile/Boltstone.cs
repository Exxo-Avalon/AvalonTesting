using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Placeable.Tile;

class Boltstone : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Boltstone");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Ores.Boltstone>();
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.useTurn = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 999;
        Item.value = Item.sellPrice(0, 0, 0, 50);
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(this, 35).AddTile(TileID.Furnaces).Register();
        CreateRecipe(35).AddIngredient(ModContent.ItemType<Items.Consumables.StaminaCrystal>()).AddTile(TileID.Furnaces).Register();
    }
}
