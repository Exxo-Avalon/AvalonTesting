using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor;

[AutoloadEquip(EquipType.Head)]
class XanthophyteMask : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Xanthophyte Mask");
        Tooltip.SetDefault("20% increased ranged damage\n20% chance to not consume ammo");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 14;
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
        player.ammoCost80 = true;
        player.GetDamage(DamageClass.Ranged) += 0.2f;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.XanthophyteBar>(), 12)
            .AddIngredient(ModContent.ItemType<Material.VenomShard>())
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
