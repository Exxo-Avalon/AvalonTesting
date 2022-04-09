using AvalonTesting.Items.Consumables;
using AvalonTesting.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Accessories;

internal class RestorationBand : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Restoration Band");
        Tooltip.SetDefault("Provides life, mana, and stamina regeneration");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 1);
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe().AddIngredient(ItemID.LifeCrystal).AddIngredient(ItemID.ManaCrystal)
            .AddIngredient(ModContent.ItemType<StaminaCrystal>()).AddIngredient(ItemID.Shackle, 2)
            .AddTile(TileID.TinkerersWorkbench).Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.lifeRegen++;
        player.manaRegen++;
        player.GetModPlayer<ExxoStaminaPlayer>().StaminaRegenCost = 1700;
    }
}
