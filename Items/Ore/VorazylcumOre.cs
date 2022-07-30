using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ore;

class VorazylcumOre : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Vorazylcum Ore");
        SacrificeTotal = 100;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.createTile = ModContent.TileType<Tiles.Ores.VorazylcumOre>();
        Item.consumable = true;
        Item.rare = ModContent.RarityType<TealRarity>();
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.useTurn = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 0, 50, 0);
        Item.maxStack = 999;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
}
