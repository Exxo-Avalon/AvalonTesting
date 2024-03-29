using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Avalon.Items.Material;

class HellsteelPlate : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Hellsteel Plate");
        SacrificeTotal = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = Item.sellPrice(0, 0, 2);
        Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
        Item.height = dims.Height;
    }
}
