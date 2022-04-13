using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Accessories;

internal class CloudGloves : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Cloud Glove");
        Tooltip.SetDefault("The wearer can place blocks and walls in midair");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 1);
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonTestingGlobalItemInstance>().UpdateInvisibleVanity = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.Avalon().cloudGloves = true;
    }

    public override void UpdateVanity(Player player)
    {
        player.Avalon().cloudGloves = true;
    }

    public override void AddRecipes()
    {
        CreateRecipe().AddIngredient(ItemID.Silk, 15).AddIngredient(ItemID.Cloud, 25)
            .AddIngredient(ItemID.SoulofFlight, 5).AddRecipeGroup("AvalonTesting:GoldBar", 5)
            .AddIngredient(ItemID.SunplateBlock, 10).AddTile(TileID.TinkerersWorkbench).Register();
    }
}
