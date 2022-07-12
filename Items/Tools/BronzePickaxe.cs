using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Tools;

public class BronzePickaxe : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bronze Pickaxe");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.TinPickaxe);
    }
}
