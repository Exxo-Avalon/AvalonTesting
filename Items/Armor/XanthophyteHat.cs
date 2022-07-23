using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Armor;

[AutoloadEquip(EquipType.Head)]
class XanthophyteHat : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Xanthophyte Hat");
        Tooltip.SetDefault("Increases maximum mana by 100 and reduces mana usage by 15%\n18% increased magic damage");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 8;
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 6);
        Item.height = dims.Height;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<XanthophytePlate>() && legs.type == ModContent.ItemType<XanthophyteLeggings>();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "???";
        //player.GetAttackSpeed(DamageClass.Melee) += 0.2f;
    }

    public override void UpdateEquip(Player player)
    {
        player.manaCost -= 0.15f;
        player.statManaMax2 += 100;
        player.GetDamage(DamageClass.Magic) += 0.18f;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.XanthophyteBar>(), 12)
            .AddIngredient(ModContent.ItemType<Placeable.Tile.VenomShard>())
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
