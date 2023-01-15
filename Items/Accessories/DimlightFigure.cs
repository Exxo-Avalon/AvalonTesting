using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

class DimlightFigure : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Dimlight Figure");
        Tooltip.SetDefault("You have a chance to gain reduced aggro and become immune to nearby enemies");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 5);
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<Players.ExxoEquipEffectPlayer>().Dimlight = true;
    }

    //public override void AddRecipes()
    //{
    //    CreateRecipe(1).AddIngredient(ModContent.ItemType<AmethystAmulet>()).AddIngredient(ModContent.ItemType<TopazAmulet>()).AddIngredient(ModContent.ItemType<SapphireAmulet>()).AddIngredient(ModContent.ItemType<EmeraldAmulet>()).AddIngredient(ModContent.ItemType<RubyAmulet>()).AddIngredient(ModContent.ItemType<DiamondAmulet>()).AddIngredient(ModContent.ItemType<TourmalineAmulet>()).AddIngredient(ModContent.ItemType<PeridotAmulet>()).AddIngredient(ModContent.ItemType<ZirconAmulet>()).AddTile(TileID.Anvils).Register();
    //}
}
