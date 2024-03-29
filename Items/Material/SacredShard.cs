using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class SacredShard : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Sacred Shard");
        Tooltip.SetDefault("'A fragment of hallow creatures'");
        SacrificeTotal = 10;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.maxStack = 999;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Shards>();
        Item.placeStyle = 8;
        Item.rare = ItemRarityID.LightPurple;
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 0, 12, 0);
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<ArcaneShard>(), 2)
            .AddIngredient(ItemID.SoulofLight, 2)
            .AddTile(TileID.MythrilAnvil).Register();
    }
}
