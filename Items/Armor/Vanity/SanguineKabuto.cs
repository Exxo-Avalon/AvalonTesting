using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    class SanguineKabuto : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sanguine Kabuto");
        }
        public override void SetDefaults()
        {
            Item.defense = 3;
            Item.value = Item.sellPrice(0, 1, 20);
            Item.rare = ItemRarityID.Orange;
        }
    }
}
