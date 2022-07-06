using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Placeable.Crafting;

class CaesiumForge : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Caesium Forge");
        Tooltip.SetDefault("Used to smelt high-end ore");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.CaesiumForge>();
        Item.rare = ItemRarityID.Pink;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 99;
        Item.value = 50000;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(1)
            .AddIngredient(ItemID.AdamantiteForge)
            .AddIngredient(ModContent.ItemType<Placeable.Tile.CaesiumOre>(), 40)
            .AddTile(TileID.MythrilAnvil).Register();

        Recipe.Create(1)
            .AddIngredient(ItemID.TitaniumForge)
            .AddIngredient(ModContent.ItemType<Placeable.Tile.CaesiumOre>(), 40)
            .AddTile(TileID.MythrilAnvil).Register();

        Recipe.Create(1)
            .AddIngredient(ModContent.ItemType<TroxiniumForge>())
            .AddIngredient(ModContent.ItemType<Placeable.Tile.CaesiumOre>(), 40)
            .AddTile(TileID.MythrilAnvil).Register();
    }
}
