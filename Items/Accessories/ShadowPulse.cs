using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

class ShadowPulse : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Shadow Pulse");
        Tooltip.SetDefault("The holder has pulsing outlines\nThe holder has afterimages when moving");
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
        player.GetModPlayer<ExxoEquipEffectPlayer>().ShadowCharm = true;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<ExxoEquipEffectPlayer>().PulseCharm = true;
        player.GetModPlayer<ExxoEquipEffectPlayer>().ShadowCharm = true;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<ShadowCharm>())
            .AddIngredient(ModContent.ItemType<PulseCharm>())
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
}
