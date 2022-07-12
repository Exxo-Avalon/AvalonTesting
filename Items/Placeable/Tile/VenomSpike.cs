using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Placeable.Tile;

class VenomSpike : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Venom Spike");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.VenomSpike>();
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.useTurn = true;
        Item.maxStack = 999;
        Item.value = 50;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(40).AddIngredient(ItemID.Spike, 40).AddIngredient(ItemID.FlaskofVenom).AddTile(TileID.Anvils).Register();
    }
}
