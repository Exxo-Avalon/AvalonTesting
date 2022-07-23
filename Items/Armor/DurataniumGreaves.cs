using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Armor;

[AutoloadEquip(EquipType.Legs)]
class DurataniumGreaves : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Duratanium Greaves");
        Tooltip.SetDefault("5% increased movement speed");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 8;
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 1, 30, 0);
        Item.height = dims.Height;
    }

    public override void UpdateEquip(Player player)
    {
        player.moveSpeed += 0.05f;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.DurataniumBar>(), 15)
            .AddTile(TileID.Anvils).Register();
    }
}
