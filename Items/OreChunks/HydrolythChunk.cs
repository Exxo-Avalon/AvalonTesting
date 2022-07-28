using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace Avalon.Items.OreChunks;

class HydrolythChunk : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Hydrolyth Chunk");
        SacrificeTotal = 200;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = 100;
        Item.height = dims.Height;
        Item.rare = ModContent.RarityType<Rarities.TealRarity>();
    }
    public override void AddRecipes()
    {
        Recipe.Create(ModContent.ItemType<Placeable.Bar.HydrolythBar>())
            .AddIngredient(Type, 5)
            .AddIngredient(ModContent.ItemType<FeroziumChunk>())
            .AddIngredient(ModContent.ItemType<Placeable.Tile.SolariumOre>())
            .AddTile(TileID.WorkBenches)
            .Register();

        Recipe.Create(ModContent.ItemType<Placeable.Bar.HydrolythBar>())
            .AddIngredient(Type, 5)
            .AddIngredient(ModContent.ItemType<Placeable.Tile.FeroziumOre>())
            .AddIngredient(ModContent.ItemType<Placeable.Tile.SolariumOre>())
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
