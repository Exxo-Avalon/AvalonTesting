using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class Gravel : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Gravel");
        Tooltip.SetDefault("Polish used to produce tomes");
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
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type, 15)
            .AddIngredient(ItemID.SiltBlock, 20)
            .AddIngredient(ItemID.StoneBlock, 5)
            .AddTile(TileID.Anvils).Register();

        Recipe.Create(Type, 15)
            .AddIngredient(ItemID.SlushBlock, 20)
            .AddIngredient(ItemID.StoneBlock, 5)
            .AddTile(TileID.Anvils).Register();
    }
}
