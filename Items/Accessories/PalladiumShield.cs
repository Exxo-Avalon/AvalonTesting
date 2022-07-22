using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Accessories;

class PalladiumShield : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Palladium Shield");
        Tooltip.SetDefault("Regenerates health when struck");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 2;
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.value = 54000;
        Item.accessory = true;
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<Players.ExxoEquipEffectPlayer>().PallShield = true;
        player.GetModPlayer<Players.ExxoEquipEffectPlayer>().SpikeImmune = true;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.PalladiumBar, 15)
            .AddTile(TileID.Anvils).Register();
    }
}
