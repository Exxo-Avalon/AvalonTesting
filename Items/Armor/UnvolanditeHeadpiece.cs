using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Armor;

[AutoloadEquip(EquipType.Head)]
class UnvolanditeHeadpiece : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Unvolandite Headpiece");
        Tooltip.SetDefault("16% increased damage\n6% increased critical strike chance");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 32;
        Item.rare = ModContent.RarityType<FireOrangeRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 50, 0, 0);
        Item.height = dims.Height;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<UnvolanditeBodyplate>() && legs.type == ModContent.ItemType<UnvolanditeLeggings>();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.onHitPetal = true;
        player.GetModPlayer<Players.ExxoEquipEffectPlayer>().HyperMelee = true;
        player.GetModPlayer<Players.ExxoEquipEffectPlayer>().HyperMagic = true;
        player.GetModPlayer<Players.ExxoEquipEffectPlayer>().HyperRanged = true;
        player.setBonus = "Petals attack your enemies and Hyper Damage";
    }

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Generic) += 0.16f;
        player.GetCritChance(DamageClass.Generic) += 6;
    }
}
