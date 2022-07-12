using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Material;

class CarbonSteel : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Carbon Steel");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 0, 2, 0);
        Item.maxStack = 999;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(10).AddIngredient(ItemID.IronOre, 30).AddTile(TileID.Hellforge).Register();
        CreateRecipe(10).AddIngredient(ItemID.LeadOre, 30).AddTile(TileID.Hellforge).Register();
        CreateRecipe(10).AddIngredient(ModContent.ItemType<Placeable.Tile.NickelOre>(), 30).AddTile(TileID.Hellforge).Register();
    }
}
