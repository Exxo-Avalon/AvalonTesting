using Avalon.Players;
using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Armor;

[AutoloadEquip(EquipType.Head)]
internal class AvalonHelmet : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Avalon Helmet");
        Tooltip.SetDefault("32% increased damage"
                           + "\n20% decreased mana usage"
                           + "\nIncreases maximum mana by 280"
                           + "\nOccasionally summons a leaf storm when damaged");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 40;
        Item.rare = ModContent.RarityType<AvalonRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 41);
        Item.height = dims.Height;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<AvalonBodyarmor>() && legs.type == ModContent.ItemType<AvalonCuisses>();
    }

    public override void UpdateArmorSet(Player player)
    {
        ExxoPlayer modPlayer = player.Avalon();
        player.setBonus = "Restoration"
                          + "\nDealing a critical hit temporarily gives the 'Blessing of Avalon' buff"
                          + "\nThis buff removes almost all debuffs and greatly increases your stats"
                          + "\n\nRetribution"
                          + "\nEnemies who strike you are marked for their destruction"
                          + "\nThey will take quadruple damage from your next attack";

        player.GetModPlayer<ExxoEquipEffectPlayer>().AvalonRestoration = true;
        player.GetModPlayer<ExxoEquipEffectPlayer>().AvalonRetribution = true;
    }

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Generic) += 0.32f;
        player.manaCost -= 0.20f;
        player.statManaMax2 += 280;
        player.GetModPlayer<ExxoEquipEffectPlayer>().LeafStorm = true;
    }
}
