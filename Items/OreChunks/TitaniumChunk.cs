using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace Avalon.Items.OreChunks;

class TitaniumChunk : ModItem
{
    public override void SetStaticDefaults()
    {
        SacrificeTotal = 200;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = 100;
        Item.height = dims.Height;
        Item.rare = ItemRarityID.Orange;
    }
    public override void AddRecipes()
    {
        Recipe.Create(ItemID.TitaniumBar)
            .AddIngredient(Type, 5)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
