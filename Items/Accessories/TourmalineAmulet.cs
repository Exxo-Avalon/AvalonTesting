using AvalonTesting.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Accessories;

class TourmalineAmulet : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Tourmaline Amulet");
        Tooltip.SetDefault("5% increased critical strike chance");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 0, 70);
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetCritChance(DamageClass.Generic) += 5;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Tourmaline>(), 12).AddIngredient(ItemID.Chain).AddTile(TileID.Anvils).Register();
    }
}
