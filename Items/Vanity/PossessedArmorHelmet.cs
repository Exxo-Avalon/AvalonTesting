using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Vanity;

[AutoloadEquip(EquipType.Head)]
class PossessedArmorHelmet : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Possessed Armor Helmet");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.vanity = true;
        Item.value = Item.sellPrice(0, 0, 20, 0);
        Item.height = dims.Height;
    }
}
