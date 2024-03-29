using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Beam;

class CrimstoneColumn : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Crimstone Column");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.CrimstoneColumn>();
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
        Terraria.Recipe.Create(Type, 2)
            .AddIngredient(ItemID.CrimstoneBlock)
            .AddTile(TileID.Sawmill).Register();
    }
}
