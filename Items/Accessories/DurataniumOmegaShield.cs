using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

class DurataniumOmegaShield : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Duratanium Omega Shield");
        Tooltip.SetDefault("Increases defense and regenerates life when struck\nGreatly slows the effects of damage over time debuffs");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 4;
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.value = 100000;
        Item.accessory = true;
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<Players.ExxoEquipEffectPlayer>().CobShield = true;
        player.GetModPlayer<Players.ExxoEquipEffectPlayer>().PallShield = true;
        player.GetModPlayer<Players.ExxoEquipEffectPlayer>().DuraShield = true;
        player.noKnockback = true;
        player.GetModPlayer<Players.ExxoEquipEffectPlayer>().SpikeImmune = true;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<CobaltShieldMarkII>())
            .AddIngredient(ModContent.ItemType<PalladiumShield>())
            .AddIngredient(ModContent.ItemType<DurataniumShield>())
            .AddIngredient(ItemID.SoulofFright, 5)
            .AddTile(TileID.TinkerersWorkbench).Register();
    }
}
