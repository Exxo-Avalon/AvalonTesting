using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

[AutoloadEquip(EquipType.Shield)]
public class AegisofAges : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Aegis of Ages");
        Tooltip.SetDefault(
            "+20 defense, +5 life regeneration, +20% damage\nEffects are only active when below 33% life");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ModContent.RarityType<Rarities.TealRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 25);
        Item.accessory = true;
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (player.statLife <= player.statLifeMax2 * 0.33)
        {
            player.statDefense += 20;
            player.lifeRegen += 5;
            player.GetDamage(DamageClass.Generic) += 0.2f;
        }
    }
}
