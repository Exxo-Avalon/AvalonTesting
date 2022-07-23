using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Placeable.Light;

class SoulofTimeinaBottle : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Soul of Time in a Bottle");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.SoulsinaBottle>();
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.useTurn = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 999;
        Item.value = Item.sellPrice(0, 0, 10);
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.placeStyle = 4;
    }

    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.Bottle)
            .AddIngredient(ModContent.ItemType<Material.SoulofTime>())
            .Register();
    }
}
