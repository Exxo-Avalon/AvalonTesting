using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor;

[AutoloadEquip(EquipType.Legs)]
class IridiumPants : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Iridium Pants");
        Tooltip.SetDefault("11% increased magic damage");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 8;
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 0, 9, 75);
        Item.height = dims.Height;
    }
    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Magic) += 0.11f;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.IridiumBar>(), 17)
            .AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 5)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
