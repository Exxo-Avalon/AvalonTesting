using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor;

[AutoloadEquip(EquipType.Head)]
class SpectreHeadgear : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Spectre Headgear");
        Tooltip.SetDefault("10% decreased mana usage\n10% increased magic damage");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 11;
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.value = 375000;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.Ectoplasm, 12)
            .AddIngredient(ItemID.ChlorophyteBar, 12)
            .AddTile(TileID.MythrilAnvil).Register();
    }
    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Magic) += 0.1f;
        player.manaCost -= 0.1f;
    }
}
