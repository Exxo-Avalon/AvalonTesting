using AvalonTesting.Players;
using AvalonTesting.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AvalonTesting.Items.Armor;

[AutoloadEquip(EquipType.Head)]
internal class AeroforceGuardia : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Aeroforce Guardia");
        Tooltip.SetDefault("6% increased minion damage"
                           + "\nIncreases your max number of minions by 1"
                           + "\n[c/C39FDD:10th Anniversary Contest Winner - Crabby]");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 8;
        Item.rare = ModContent.RarityType<CrabbyRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 3);
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.HallowedBar, 15)
            .AddIngredient(ItemID.Feather, 16)
            .AddIngredient(ItemID.SoulofFlight, 10)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.CaesiumBar>(), 5)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<AeroforceProtector>() && legs.type == ModContent.ItemType<AeroforceLeggings>();
    }

    public override void UpdateArmorSet(Player player)
    {
        ExxoPlayer modPlayer = player.Avalon();
        player.setBonus = "Blessing of the Sky"
                          + "\nIncreases your max number of minions by 1"
                          + "\nGrants a chance for a Sky Insignia to drop when your minions damage an enemy";

        modPlayer.skyBlessing = true;
    }

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Summon) += 0.08f;
        player.maxMinions++;
    }
}
