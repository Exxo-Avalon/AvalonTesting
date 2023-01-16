using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

class ChaosEmblem : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Chaos Emblem");
        Tooltip.SetDefault("35% increased critical strike damage\n10% increased damage");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 6, 0, 0);
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.Avalon().AllCritDamage(0.35f);
        player.GetDamage(DamageClass.Generic) += 0.1f;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<ChaosCrystal>())
            .AddIngredient(ItemID.AvengerEmblem)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
}
