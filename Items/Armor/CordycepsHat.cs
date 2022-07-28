using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor;

[AutoloadEquip(EquipType.Head)]
class CordycepsHat : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Cordyceps Hat");
        Tooltip.SetDefault("Increases minion damage by 4%\nIncreases your max number of minions by 1");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 4;
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(silver: 90);
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.TropicalShroomCap>(), 8)
            .AddTile(TileID.Anvils)
            .Register();
    }
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<CordycepsWrappings>() && legs.type == ModContent.ItemType<CordycepsLeggings>();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "9% increased minion damage";
        player.GetDamage(DamageClass.Summon) += 0.09f;
    }
}
