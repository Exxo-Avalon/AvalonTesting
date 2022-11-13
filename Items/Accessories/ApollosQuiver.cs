using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

class ApollosQuiver : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Apollo's Quiver");
        Tooltip.SetDefault("20% chance to not consume arrows and 15% increased arrow damage\nIncreases arrow speed by 10% and critical strike chance by 5%\nIncreases critical strike damage by 25%");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 7);
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.DestroyerEmblem)
            .AddIngredient(ItemID.MagicQuiver)
            .AddTile(TileID.TinkerersWorkbench).Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.magicQuiver = true;
        player.arrowDamage += 0.15f;
        player.GetCritChance(DamageClass.Ranged) += 5;
        player.GetModPlayer<Players.ExxoPlayer>().CritDamageMult += 0.25f;
    }
}
