using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Material;

internal class BrokenVigilanteTome : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Broken Vigilante Tome");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.maxStack = 99;
        Item.value = Item.sellPrice(0, 2);
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.BrokenHeroSword)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>())
            .Register();
    }
}
