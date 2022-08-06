using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

class DragonBait : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Dragon Bait");
        Tooltip.SetDefault("Vital in the creation of the Dragon Spine");
        SacrificeTotal = 5;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ModContent.RarityType<Rarities.TealRarity>();
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = 1000;
        Item.height = dims.Height;
    }
}
