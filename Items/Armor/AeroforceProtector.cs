using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Armor;

[AutoloadEquip(EquipType.Body)]
class AeroforceProtector : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Aeroforce Protector");
        Tooltip.SetDefault("10% increased minion damage"
                           + "\nIncreases your max number of minions by 1"
                           + "\n[c/C39FDD:10th Anniversary Contest Winner - Crabby]");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 12;
        Item.rare = ModContent.RarityType<Rarities.CrabbyRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 3);
        Item.height = dims.Height;
    }
    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Summon) += 0.1f;
        player.maxMinions++;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.HallowedBar, 30)
            .AddIngredient(ItemID.Feather, 24)
            .AddIngredient(ItemID.SoulofFlight, 16)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.CaesiumBar>(), 5)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
