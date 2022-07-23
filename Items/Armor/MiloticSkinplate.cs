using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Armor;

[AutoloadEquip(EquipType.Body)]
class MiloticSkinplate : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Milotic Skinplate");
        Tooltip.SetDefault("30% increased minion knockback\nIncreases your max number of minions by 3");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 30;
        Item.rare = ModContent.RarityType<TealRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 40, 0, 0);
        Item.height = dims.Height;
    }

    public override void UpdateEquip(Player player)
    {
        player.GetKnockback(DamageClass.Summon) += 0.3f;
        player.maxMinions += 3;
    }
}
