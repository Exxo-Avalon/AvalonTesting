using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Armor;

[AutoloadEquip(EquipType.Body)]
class XanthophytePlate : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Xanthophyte Plate");
        Tooltip.SetDefault("7% increased damage\n8% increased critical strike chance");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 19;
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 4, 80);
        Item.height = dims.Height;
    }

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Generic) += 0.07f;
        player.GetCritChance(DamageClass.Generic) += 8;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.XanthophyteBar>(), 24)
            .AddIngredient(ModContent.ItemType<Placeable.Tile.VenomShard>())
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
