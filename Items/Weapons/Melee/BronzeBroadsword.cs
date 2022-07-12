using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Weapons.Melee;

class BronzeBroadsword : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bronze Broadsword");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.TinBroadsword);
    }
}