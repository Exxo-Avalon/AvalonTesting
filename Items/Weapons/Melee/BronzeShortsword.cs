using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Weapons.Melee;

class BronzeShortsword : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bronze Shortsword");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.TinShortsword);
    }
}