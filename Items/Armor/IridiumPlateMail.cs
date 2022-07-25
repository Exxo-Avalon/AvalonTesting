using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor;

[AutoloadEquip(EquipType.Body)]
class IridiumPlateMail : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Iridium Plate Mail");
        Tooltip.SetDefault("Increases maximum mana by 40");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 9;
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 1, 40, 0);
        Item.height = dims.Height;
    }
    public override void UpdateEquip(Player player)
    {
        player.statManaMax2 += 40;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.IridiumBar>(), 20)
            .AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 6)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
