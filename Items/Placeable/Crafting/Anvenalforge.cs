using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Crafting;

class Anvenalforge : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Anvenalforge");
        Tooltip.SetDefault("Used to craft almost anything");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Anvenalforge>();
        Item.rare = ModContent.RarityType<Rarities.AvalonRarity>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 99;
        Item.value = 100000;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<CaesiumForge>()).AddIngredient(ModContent.ItemType<SolariumAnvil>()).AddRecipeGroup("AvalonTesting:WorkBenches").AddIngredient(ModContent.ItemType<DemonAltar>()).AddIngredient(ModContent.ItemType<HallowedAltar>()).AddIngredient(ItemID.LunarCraftingStation).AddIngredient(ModContent.ItemType<Material.SoulofTorture>(), 10).AddTile(TileID.TinkerersWorkbench).Register();
        CreateRecipe(1).AddIngredient(ModContent.ItemType<CaesiumForge>()).AddIngredient(ModContent.ItemType<SolariumAnvil>()).AddRecipeGroup("AvalonTesting:WorkBenches").AddIngredient(ModContent.ItemType<CrimsonAltar>()).AddIngredient(ModContent.ItemType<HallowedAltar>()).AddIngredient(ItemID.LunarCraftingStation).AddIngredient(ModContent.ItemType<Material.SoulofTorture>(), 10).AddTile(TileID.TinkerersWorkbench).Register();
        CreateRecipe(1).AddIngredient(ModContent.ItemType<CaesiumForge>()).AddIngredient(ModContent.ItemType<SolariumAnvil>()).AddRecipeGroup("AvalonTesting:WorkBenches").AddIngredient(ModContent.ItemType<IckyAltar>()).AddIngredient(ModContent.ItemType<HallowedAltar>()).AddIngredient(ItemID.LunarCraftingStation).AddIngredient(ModContent.ItemType<Material.SoulofTorture>(), 10).AddTile(TileID.TinkerersWorkbench).Register();
    }
}
