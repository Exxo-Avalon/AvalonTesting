using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Wall;

class HellWall : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Hell Wall");
        SacrificeTotal = 200;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 7;
        Item.createWall = ModContent.WallType<Walls.HellWall>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 999;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe(8).AddIngredient(ItemID.AshBlock).AddIngredient(ItemID.Hellstone).AddTile(TileID.WorkBenches).Register();
    }
}
