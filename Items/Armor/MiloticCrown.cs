using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Armor;

[AutoloadEquip(EquipType.Head)]
class MiloticCrown : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Milotic Crown");
        Tooltip.SetDefault("30% increased minion damage\nIncreases your max number of minions by 3");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 29;
        Item.rare = ModContent.RarityType<TealRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 40, 0, 0);
        Item.height = dims.Height;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<MiloticSkinplate>() && legs.type == ModContent.ItemType<MiloticJodpurs>();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Minions have a chance to freeze your enemies" +
                          "\nFrozen enemies or enemies that cannot be frozen take 10% more damage";
        player.Avalon().minionFreeze = true;
    }

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Summon) += 0.3f;
        player.maxMinions += 3;
    }
}
