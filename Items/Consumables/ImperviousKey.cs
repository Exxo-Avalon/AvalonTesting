using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

public class ImperviousKey : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Impervious Key");
        Tooltip.SetDefault("Opens the Hellcastle doors");
        SacrificeTotal = 4;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.maxStack = 999;
        Item.width = dims.Width;
        Item.value = 0;
        Item.height = dims.Height;
        Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
    }
    public override void AddRecipes()
    {
       CreateRecipe(2).AddIngredient(ItemID.TempleKey, 2).AddIngredient(ItemID.Ectoplasm, 10).AddIngredient(ModContent.ItemType<Placeable.Bar.BeetleBar>()).AddIngredient(ItemID.LunarBar, 5).AddTile(TileID.MythrilAnvil).Register();
    }
}
