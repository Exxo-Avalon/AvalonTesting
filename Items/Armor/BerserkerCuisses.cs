using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor;

[AutoloadEquip(EquipType.Legs)]
class BerserkerCuisses : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Berserker Cuisses");
        Tooltip.SetDefault("Allows the Frenzy stance, double tap DOWN to activate\nLightning strikes when damaged");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 32;
        Item.rare = ModContent.RarityType<Rarities.TealRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 65, 0, 0);
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Placeable.Bar.BerserkerBar>(), 17).AddIngredient(ModContent.ItemType<AncientLeggings>()).AddIngredient(ModContent.ItemType<Material.Onyx>(), 15).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
    }
    public override void UpdateEquip(Player player)
    {
        player.Avalon().LightningInABottle = true;
        player.GetModPlayer<ExxoEquipEffectPlayer>().FrenzyStance = true;
    }
}
