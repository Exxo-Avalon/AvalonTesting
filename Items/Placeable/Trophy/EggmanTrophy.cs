using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Placeable.Trophy;

class EggmanTrophy : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Eggman Trophy");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.maxStack = 99;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.BossTrophy>();
        Item.placeStyle = 5;
        Item.rare = ModContent.RarityType<TealRarity>();
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 1, 0, 0);
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
}
