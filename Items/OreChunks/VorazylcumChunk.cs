using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace Avalon.Items.OreChunks;

class VorazylcumChunk : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Vorazylcum Chunk");
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
        Recipe.Create(ModContent.ItemType<Placeable.Bar.VorazylcumBar>())
            .AddIngredient(Type, 6)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
