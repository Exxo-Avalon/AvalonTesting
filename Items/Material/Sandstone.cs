using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class Sandstone : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Sandstone");
        Tooltip.SetDefault("Finish used to produce tomes");
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
        Recipe.Create(Type, 5)
            .AddIngredient(ItemID.SandBlock, 10)
            .AddIngredient(ItemID.StoneBlock, 10)
            .AddTile(TileID.Hellforge).Register();
    }
}
