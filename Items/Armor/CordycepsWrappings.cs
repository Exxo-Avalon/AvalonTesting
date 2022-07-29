using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor;

[AutoloadEquip(EquipType.Body)]
class CordycepsWrappings : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Cordyceps Wrappings");
        Tooltip.SetDefault("Increases minion damage by 4%\nIncreases your max number of minions by 1");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 4;
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(silver: 60);
        Item.height = dims.Height;
    }
    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Summon) += 0.04f;
        player.maxMinions += 1;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.TropicalShroomCap>(), 16)
            .AddIngredient(ModContent.ItemType<Material.MosquitoProboscis>(), 10)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
