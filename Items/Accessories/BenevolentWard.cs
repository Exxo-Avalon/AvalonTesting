using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

class BenevolentWard : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Benevolent Ward");
        Tooltip.SetDefault("You have a chance to absorb all damage for 8 seconds when injured\n" +
            "After 8 seconds, you will take damage over time for 20 seconds\n" +
            "45 second cooldown");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = 100000;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.CobaltShield)
            .AddIngredient(ItemID.LunarBar, 10)
            .AddIngredient(ModContent.ItemType<Material.LifeDew>(), 5)
            .AddTile(TileID.LunarCraftingStation)
            .Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<ExxoEquipEffectPlayer>().BenevolentWard = true;
    }
}
