using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

class BronzeHammer : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bronze Hammer");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.TinHammer);
    }
}
