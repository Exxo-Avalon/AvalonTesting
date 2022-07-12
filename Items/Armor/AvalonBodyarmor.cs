using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Armor;

[AutoloadEquip(EquipType.Body)]
class AvalonBodyarmor : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Avalon Bodyarmor");
        Tooltip.SetDefault("10% increased critical strike chance"
                           + "\nGreatly increases length of invincibility after taking damage"
                           + "\nStars fall when injured");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 42;
        Item.rare = ModContent.RarityType<Rarities.AvalonRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 41, 0, 0);
        Item.height = dims.Height;
    }
    public override void UpdateEquip(Player player)
    {
        player.GetCritChance(DamageClass.Generic) += 10;
        player.Avalon().CritDamageMult += 0.30f;
        player.longInvince = true;
        player.starCloakItem = Item;
    }
}
