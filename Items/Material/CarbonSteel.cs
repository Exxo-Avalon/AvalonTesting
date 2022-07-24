using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class CarbonSteel : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("High-carbon Steel");
        Tooltip.SetDefault("Metal used for producing tomes");
        SacrificeTotal = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 0, 2, 0);
        Item.maxStack = 999;
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().TomeMaterial = true;
    }
    public override void AddRecipes()
    {
        CreateRecipe(10).AddIngredient(ItemID.IronOre, 30).AddTile(TileID.Hellforge).Register();
        CreateRecipe(10).AddIngredient(ItemID.LeadOre, 30).AddTile(TileID.Hellforge).Register();
        CreateRecipe(10).AddIngredient(ModContent.ItemType<Placeable.Tile.NickelOre>(), 30).AddTile(TileID.Hellforge).Register();
    }
}
