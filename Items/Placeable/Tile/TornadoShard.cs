using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

class TornadoShard : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Tornado Shard");
        Tooltip.SetDefault("'A fragment of flying creatures'");
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
        Item.placeStyle = 4;
        Item.rare = ItemRarityID.Green;
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
            .AddIngredient(ModContent.ItemType<Material.BreezeShard>(), 2)
            .AddIngredient(ItemID.SoulofFlight)
            .AddTile(TileID.MythrilAnvil).Register();
    }
}
