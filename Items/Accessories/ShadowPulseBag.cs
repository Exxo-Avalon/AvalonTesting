using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

class ShadowPulseBag : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Shadow Pulse Bag");
        Tooltip.SetDefault("A lot of particles cover you when you move\nThe holder has pulsing outlines\nThe holder has afterimages when moving\nWorks in the vanity slot");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.vanity = true;
        Item.value = Item.sellPrice(0, 2, 0, 0);
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().UpdateInvisibleVanity = true;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Omnibag>()).AddIngredient(ModContent.ItemType<ShadowPulse>()).AddTile(TileID.TinkerersWorkbench).Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<ExxoEquipEffectPlayer>().PulseCharm = true;
        player.GetModPlayer<ExxoEquipEffectPlayer>().ShadowCharm = true;
        if (!hideVisual)
        {
            UpdateVanity(player);
        }
    }

    public override void UpdateVanity(Player player)
    {
        player.GetModPlayer<ExxoEquipEffectPlayer>().PulseCharm = true;
        player.GetModPlayer<ExxoEquipEffectPlayer>().ShadowCharm = true;
        ModContent.GetInstance<Omnibag>().UpdateVanity(player);
    }
}
