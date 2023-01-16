using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

class ChaoticDestroyerEye : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Chaotic Destroyer Eye");
        Tooltip.SetDefault("25% increased critical strike damage\n" +
                            "10% increased damage\n" +
                            "Critical strike chance increases as your health drops\n" +
                            "8% increased critical strike chance");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Cyan;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 9, 75);
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.Avalon().AllCritDamage(0.25f);
        player.GetDamage(DamageClass.Generic) += 0.1f;
        player.GetModPlayer<Players.ExxoEquipEffectPlayer>().ChaosCharm = true;
        player.GetCritChance(DamageClass.Generic) += 8;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<ChaosEye>())
            .AddIngredient(ModContent.ItemType<HellsteelEmblem>())
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
}
