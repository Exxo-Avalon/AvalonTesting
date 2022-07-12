using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Other;

internal class DesertKey : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Desert Key");
        Tooltip.SetDefault("Unlocks a Desert Chest in the dungeon");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Cyan;
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.TempleKey)
            .AddIngredient(ModContent.ItemType<DesertKeyMold>())
            .AddIngredient(ItemID.SoulofFright, 5)
            .AddIngredient(ItemID.SoulofMight, 5)
            .AddIngredient(ItemID.SoulofSight, 5)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
