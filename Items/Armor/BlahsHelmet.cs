using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor;

[AutoloadEquip(EquipType.Head)]
class BlahsHelmet : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Blah's Helmet");
        Tooltip.SetDefault("29% increased damage\n10% increased critical strike chance");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 50;
        Item.rare = ModContent.RarityType<Rarities.RainbowRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(1, 0, 0, 0);
        Item.height = dims.Height;
    }
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<BlahsBodyarmor>() && legs.type == ModContent.ItemType<BlahsGreaves>();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.GetModPlayer<Players.ExxoEquipEffectPlayer>().BlahArmor = true;
        player.setBonus = "Melee and Ranged Stealth, Attackers also take double full damage, and Spectre Heal and Silence";
        player.Avalon().meleeStealth = true;
        player.shroomiteStealth = true;
        player.GetModPlayer<Players.ExxoEquipEffectPlayer>().DoubleDamage = true;
        player.ghostHeal = true;
        //player.thorns = true;
        player.Avalon().ghostSilence = true;
    }
    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Generic) += 0.29f;
        player.GetCritChance(DamageClass.Generic) += 10;
    }
}
