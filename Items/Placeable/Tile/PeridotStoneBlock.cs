using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace AvalonTesting.Items.Placeable.Tile;

class PeridotStoneBlock : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Peridot Stone Block");
        SacrificeTotal = 100;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Ores.Peridot>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 999;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Peridot>())
            .AddIngredient(ItemID.StoneBlock)
            .AddTile(TileID.HeavyWorkBench)
            .AddCondition(Recipe.Condition.InGraveyardBiome)
            .Register();
    }
}
