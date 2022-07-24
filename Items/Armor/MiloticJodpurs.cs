using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor;

[AutoloadEquip(EquipType.Legs)]
class MiloticJodpurs : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Milotic Jodpurs");
        Tooltip.SetDefault("Increases your max number of minions by 2\n10% increased movement speed");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 28;
        Item.rare = ModContent.RarityType<TealRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 40, 0, 0);
        Item.height = dims.Height;
    }

    public override void UpdateEquip(Player player)
    {
        player.moveSpeed += 0.1f;
        player.maxMinions += 2;
    }
}
