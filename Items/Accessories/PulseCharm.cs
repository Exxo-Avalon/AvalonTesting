using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AvalonTesting.Players;

namespace AvalonTesting.Items.Accessories;

class PulseCharm : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Pulse Charm");
        Tooltip.SetDefault("The holder has pulsing outlines");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ModContent.RarityType<Rarities.QuibopsRarity>();
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 0, 45);
        Item.height = dims.Height;
    }
    public override void UpdateVanity(Player player)
    {
        player.GetModPlayer<ExxoEquipEffectPlayer>().PulseCharm = true;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<ExxoEquipEffectPlayer>().PulseCharm = true;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ItemID.AdamantiteBar, 4)
            .AddIngredient(ItemID.HallowedBar, 4)
            .AddTile(TileID.MythrilAnvil)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ItemID.TitaniumBar, 4)
            .AddIngredient(ItemID.HallowedBar, 4)
            .AddTile(TileID.MythrilAnvil)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.TroxiniumBar>(), 4)
            .AddIngredient(ItemID.HallowedBar, 4)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
