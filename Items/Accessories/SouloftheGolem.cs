using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

class SouloftheGolem : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Soul of the Golem");
        Tooltip.SetDefault("Grants a 15% chance for enemies to drop a platinum heart");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Cyan;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 3, 0, 0);
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<Players.ExxoEquipEffectPlayer>().EthHeart = true;
        player.GetModPlayer<Players.ExxoEquipEffectPlayer>().HeartGolem = true;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<EtherealHeart>())
            .AddIngredient(ModContent.ItemType<HeartoftheGolem>())
            .AddTile(TileID.TinkerersWorkbench).Register();
    }
}
