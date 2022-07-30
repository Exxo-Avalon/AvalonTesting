using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor;

[AutoloadEquip(EquipType.Legs)]
class XanthophyteLeggings : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Xanthophyte Leggings");
        Tooltip.SetDefault("9% increased critical strike chance\n10% increased movements peed");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 13;
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 3, 60);
        Item.height = dims.Height;
    }

    public override void UpdateEquip(Player player)
    {
        player.GetCritChance(DamageClass.Generic) += 9;
        player.moveSpeed += 0.1f;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.XanthophyteBar>(), 18)
            .AddIngredient(ModContent.ItemType<Material.VenomShard>())
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
