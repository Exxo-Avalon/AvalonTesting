using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class Kunzite : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Kunzite");
        SacrificeTotal = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.maxStack = 999;
        Item.createTile = ModContent.TileType<Tiles.PlacedGems>();
        Item.placeStyle = 2;
        Item.consumable = true;
        Item.rare = ItemRarityID.Purple;
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.value = 50000;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
}
