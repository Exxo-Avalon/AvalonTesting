using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

class ObsidianGlove : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Obsidian Glove");
        Tooltip.SetDefault("The wearer can place blocks and walls in midair and in lava\nProvides immunity to fire blocks");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 2, 0, 0);
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().UpdateInvisibleVanity = true;
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
        CreateRecipe(1).AddIngredient(ModContent.ItemType<CloudGloves>()).AddIngredient(ItemID.ObsidianSkull).AddTile(TileID.TinkerersWorkbench).Register();
    }
}
