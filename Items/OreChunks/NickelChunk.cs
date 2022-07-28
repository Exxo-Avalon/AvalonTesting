using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace Avalon.Items.OreChunks;

class NickelChunk : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Nickel Chunk");
        SacrificeTotal = 200;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = 100;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(ModContent.ItemType<Placeable.Bar.NickelBar>())
            .AddIngredient(Type, 3)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
