using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

class ManaCompromise : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Mana Compromise");
        Tooltip.SetDefault("\n12% decreased magic damage and 8% decreased mana usage\nAutomatically use mana potions when needed\nProvides immunity to Mana Sickness");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightPurple;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 6, 70, 0);
        Item.accessory = true;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ItemID.ManaFlower)
            .AddIngredient(ItemID.ManaRegenerationBand)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.ManaSickness] = true;
        player.manaFlower = true;
        player.GetDamage(DamageClass.Magic) -= 0.12f;
        player.manaCost -= 0.08f;
    }
}
