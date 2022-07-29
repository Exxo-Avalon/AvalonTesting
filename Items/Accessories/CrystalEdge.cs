using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

class CrystalEdge : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Crystal Edge");
        Tooltip.SetDefault("Increases damage by 15");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 4, 0, 0);
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.Avalon().crystalEdge = true;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.CrystalShard, 50)
            .AddIngredient(ItemID.SoulofMight, 10)
            .AddTile(TileID.TinkerersWorkbench).Register();
    }
}
