using AvalonTesting.Items.Material;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Consumables;

class HellboundRemote : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Hellbound Remote");
        Tooltip.SetDefault("Summons the Wall of Steel\nToss into lava in the Underworld");
        SacrificeTotal = 3;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightPurple;
        Item.width = dims.Width;
        Item.maxStack = 1;
        Item.value = 0;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Placeable.Bar.BeetleBar>()).AddIngredient(ItemID.LunarBar, 10).AddIngredient(ModContent.ItemType<GhostintheMachine>()).AddIngredient(ItemID.GuideVoodooDoll).AddIngredient(ModContent.ItemType<FleshyTendril>(), 5).AddTile(ModContent.TileType<Tiles.HallowedAltar>()).Register();
    }
}
