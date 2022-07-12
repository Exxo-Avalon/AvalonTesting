using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Armor;

[AutoloadEquip(EquipType.Body)]
class AncientTitaniumPlateMail : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Ancient Titanium Plate Mail");
        Tooltip.SetDefault("10% increased melee damage and speed");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 7;
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.value = 100000;
        Item.height = dims.Height;
    }

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Melee) += 0.1f;
        player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
    }
}
