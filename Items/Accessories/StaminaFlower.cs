using AvalonTesting.Items.Potions;
using AvalonTesting.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Accessories;

internal class StaminaFlower : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Stamina Flower");
        Tooltip.SetDefault("Increases maximum stamina by 90\nAutomatically uses stamina potions when needed");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Pink;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 0, 54);
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe().AddIngredient(ModContent.ItemType<StaminaPotion>())
            .AddIngredient(ModContent.ItemType<BandofStamina>()).AddIngredient(ItemID.JungleRose)
            .AddTile(TileID.TinkerersWorkbench).Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<ExxoStaminaPlayer>().StamFlower = true;
        player.GetModPlayer<ExxoStaminaPlayer>().StatStamMax2 += 90;
    }
}
