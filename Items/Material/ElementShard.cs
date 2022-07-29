using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class ElementShard : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Element Shard");
        Tooltip.SetDefault("'A fragment of the elements'");
        SacrificeTotal = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = 3000;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type, 10)
            .AddIngredient(ModContent.ItemType<BlastShard>(), 3)
            .AddIngredient(ModContent.ItemType<TornadoShard>(), 3)
            .AddIngredient(ModContent.ItemType<VenomShard>(), 3)
            .AddIngredient(ModContent.ItemType<WickedShard>(), 3)
            .AddIngredient(ModContent.ItemType<SacredShard>(), 3)
            .AddIngredient(ModContent.ItemType<CoreShard>(), 3)
            .AddIngredient(ModContent.ItemType<TorrentShard>(), 3)
            .AddIngredient(ModContent.ItemType<DemonicShard>(), 3)
            .AddIngredient(ModContent.ItemType<FrigidShard>(), 3)
            .AddTile(TileID.MythrilAnvil).Register();
    }
}
