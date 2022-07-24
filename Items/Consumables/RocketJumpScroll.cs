using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

class RocketJumpScroll : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Rocket Jump Scroll");
        Tooltip.SetDefault("Unlocks stamina rocket jump");
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
        CreateRecipe(1).AddIngredient(ItemID.Book).AddIngredient(ItemID.CloudinaBottle).AddIngredient(ModContent.ItemType<StaminaCrystal>()).AddTile(TileID.Bookcases).Register();
        CreateRecipe(1).AddIngredient(ItemID.Book).AddIngredient(ItemID.BlizzardinaBottle).AddIngredient(ModContent.ItemType<StaminaCrystal>()).AddTile(TileID.Bookcases).Register();
        CreateRecipe(1).AddIngredient(ItemID.Book).AddIngredient(ItemID.SandstorminaBottle).AddIngredient(ModContent.ItemType<StaminaCrystal>()).AddTile(TileID.Bookcases).Register();
        CreateRecipe(1).AddIngredient(ItemID.Book).AddIngredient(ItemID.FartinaJar).AddIngredient(ModContent.ItemType<StaminaCrystal>()).AddTile(TileID.Bookcases).Register();
        CreateRecipe(1).AddIngredient(ItemID.Book).AddIngredient(ItemID.TsunamiInABottle).AddIngredient(ModContent.ItemType<StaminaCrystal>()).AddTile(TileID.Bookcases).Register();
    }
    public override bool CanUseItem(Player player)
    {
        return !player.GetModPlayer<ExxoStaminaPlayer>().RocketJumpUnlocked;
    }
    public override bool? UseItem(Player player)
    {
        player.GetModPlayer<ExxoStaminaPlayer>().RocketJumpUnlocked = true;
        return true;
    }
}
