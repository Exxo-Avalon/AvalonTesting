using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class CoreShard : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Core Shard");
        Tooltip.SetDefault("'A fragment of deep earth creatures'");
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
        Item.placeStyle = 3;
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 0, 12, 0);
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
}
