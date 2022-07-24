using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

class CobaltShieldMarkII : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Cobalt Shield Mark II");
        Tooltip.SetDefault("Increases defense when struck");
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
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.CobaltBar, 15)
            .AddTile(TileID.Anvils).Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<Players.ExxoEquipEffectPlayer>().CobShield = true;
    }
}
