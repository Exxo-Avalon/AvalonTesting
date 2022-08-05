using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

[AutoloadEquip(EquipType.Face)]
class SurgicalMask : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Surgical Mask");
        Tooltip.SetDefault("Immunity to Infected\nCurrently foes nothing");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = 100000;
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.buffImmune[ModContent.BuffType<Buffs.BacteriaInfection>()] = true;
    }
}
