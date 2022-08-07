using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Armor;

[AutoloadEquip(EquipType.Body)]
class BerserkerBodyarmor : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Berserker Bodyarmor");
        Tooltip.SetDefault("Enemies are more likely to target you"
                           + "\nTaking heavy damage will give you the 'Berserk!' buff"
                           + "\nThis buff greatly increases the critical strike damage of true melee weapons\n");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 42;
        Item.rare = ModContent.RarityType<Rarities.TealRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 60, 0, 0);
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Placeable.Bar.BerserkerBar>(), 23).AddIngredient(ModContent.ItemType<AncientBodyplate>()).AddIngredient(ModContent.ItemType<Material.Onyx>(), 15).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
    }
    public override void UpdateEquip(Player player)
    {
        player.aggro += 600;
        player.GetModPlayer<ExxoEquipEffectPlayer>().GoBerserk = true;
    }
}
