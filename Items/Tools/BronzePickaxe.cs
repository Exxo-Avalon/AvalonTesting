using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

public class BronzePickaxe : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bronze Pickaxe");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.TinPickaxe);
    }
}
