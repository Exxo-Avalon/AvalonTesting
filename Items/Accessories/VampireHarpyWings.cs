using AvalonTesting.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Accessories;

[AutoloadEquip(EquipType.Wings)]
class VampireHarpyWings : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Vampire Harpy Wings");
        Tooltip.SetDefault("Allows flight and slow fall and heals life\nOther bonuses apply when in the Dark Matter");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Red;
        Item.width = dims.Width;
        Item.value = 800000;
        Item.accessory = true;
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.wingTimeMax = 210;
        if (player.GetModPlayer<ExxoBiomePlayer>().ZoneDarkMatter)
        {
            player.statDefense += 8;
            player.lifeRegen += 5;
        }
    }
}
