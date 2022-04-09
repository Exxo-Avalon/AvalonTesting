using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Tools;

class BronzeAxe : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bronze Axe");
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.TinAxe);
    }
}
