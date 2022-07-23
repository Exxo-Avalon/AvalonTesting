using AvalonTesting.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Consumables;

class FlightTimeScroll : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Flight Time Scroll");
        Tooltip.SetDefault("Unlocks stamina flight time restoration");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.consumable = true;
        Item.width = dims.Width;
        Item.useTime = 20;
        Item.rare = ItemRarityID.Green;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.UseSound = new SoundStyle($"{nameof(AvalonTesting)}/Sounds/Item/Scroll");
        Item.useAnimation = 20;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ItemID.Book).AddIngredient(ItemID.Feather, 20).AddIngredient(ItemID.SoulofFlight, 15).AddIngredient(ModContent.ItemType<StaminaCrystal>()).AddTile(TileID.Bookcases).Register();
    }
    public override bool CanUseItem(Player player)
    {
        return !player.GetModPlayer<ExxoStaminaPlayer>().FlightRestoreUnlocked;
    }
    public override bool? UseItem(Player player)
    {
        player.GetModPlayer<ExxoStaminaPlayer>().FlightRestoreUnlocked = true;
        return true;
    }
}
