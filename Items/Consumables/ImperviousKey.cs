using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Consumables;

public class ImperviousKey : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Impervious Key");
        Tooltip.SetDefault("Opens the Hellcastle doors");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 4;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.maxStack = 999;
        Item.width = dims.Width;
        Item.value = 0;
        Item.height = dims.Height;
        Item.rare = ItemRarityID.Cyan;
    }
    public override void AddRecipes()
    {
       CreateRecipe(2).AddIngredient(ItemID.TempleKey, 2).AddIngredient(ItemID.Ectoplasm, 10).AddIngredient(ModContent.ItemType<Placeable.Bar.BeetleBar>()).AddIngredient(ItemID.LunarBar, 5).AddTile(TileID.MythrilAnvil).Register();
    }
}
