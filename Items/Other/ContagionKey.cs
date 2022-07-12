using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Other;

internal class ContagionKey : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Contagion Key");
        Tooltip.SetDefault("Opens a Contagion Chest in the Dungeon");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.scale = 1f;
        Item.maxStack = 999;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.TempleKey)
            .AddIngredient(ModContent.ItemType<ContagionKeyMold>())
            .AddIngredient(ItemID.SoulofFright, 5)
            .AddIngredient(ItemID.SoulofMight, 5)
            .AddIngredient(ItemID.SoulofSight, 5)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
