using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

class GoblinToolbelt : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Goblin Toolbelt");
        Tooltip.SetDefault("Increases block placement range by 2, tells time and shows position\nCan craft Tinkerer's Workshop and Work Bench items by hand");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 5, 0, 0);
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.blockRange += 2;
        player.accWatch = 3;
        player.accCompass = 1;
        player.accDepthMeter = 1;
        player.GetModPlayer<ExxoEquipEffectPlayer>().GoblinToolbelt = true;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.Toolbelt)
            .AddIngredient(ItemID.GPS)
            .AddIngredient(ItemID.GoldCoin)
            .AddIngredient(ItemID.TinkerersWorkshop)
            .AddTile(TileID.MythrilAnvil).Register();
    }
}
