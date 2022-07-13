using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Placeable.Wall;

class ApocalyptusWoodWall : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Apocalyptus Wood Wall");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 400;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 7;
        Item.createWall = ModContent.WallType<Walls.ApocalyptusWoodWall>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 999;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(4).AddIngredient(ModContent.ItemType<Tile.ApocalyptusWood>()).AddTile(TileID.WorkBenches).Register();
        CreateRecipe(1).AddIngredient(this, 4).AddTile(TileID.WorkBenches).Register();
    }
}