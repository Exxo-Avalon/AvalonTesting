using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor;

[AutoloadEquip(EquipType.Legs)]
class MosquitoLeggings : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Mosquito Leggings");
        Tooltip.SetDefault("Increases minion damage by 4%\nIncreases your max number of minions by 1");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 3;
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(silver: 60);
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.TropicalShroomCap>(), 8)
            .AddIngredient(ModContent.ItemType<Material.Root>(), 2)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
