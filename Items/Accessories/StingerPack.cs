using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

class StingerPack : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Stinger Pack");
        Tooltip.SetDefault("Releases a 360 degree spread of stingers when struck");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ModContent.RarityType<Rarities.FireOrangeRarity>();
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 10);
        Item.height = dims.Height;
        Item.defense = 3;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.Avalon().stingerPack = true;
    }
}
