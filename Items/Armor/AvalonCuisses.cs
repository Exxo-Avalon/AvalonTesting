using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Armor;

[AutoloadEquip(EquipType.Legs)]
class AvalonCuisses : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Avalon Cuisses");
        Tooltip.SetDefault("30% increased critical damage"
                           + "\n10% increased melee speed"
                           + "\nLightning strikes when damaged");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 38;
        Item.rare = ModContent.RarityType<Rarities.AvalonRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 41, 0, 0);
        Item.height = dims.Height;
    }
    public override void UpdateEquip(Player player)
    {
        player.moveSpeed += 0.15f;
        player.GetAttackSpeed(DamageClass.Melee) += 0.10f;
        player.Avalon().LightningInABottle = true;
    }
}
