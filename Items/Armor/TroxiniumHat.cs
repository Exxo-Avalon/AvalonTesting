using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor;

[AutoloadEquip(EquipType.Head)]
class TroxiniumHat : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Troxinium Hat");
        Tooltip.SetDefault("10% increased magic damage\n15% decreased mana usage");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 9;
        Item.rare = ItemRarityID.Pink;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 3, 40, 0);
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.TroxiniumBar>(), 12)
            .AddTile(TileID.MythrilAnvil).Register();
    }
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<TroxiniumBodyarmor>() && legs.type == ModContent.ItemType<TroxiniumCuisses>();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Hyper Damage\nHit mobs 15 times to trigger magic crits for 10 hits";
        player.GetModPlayer<Players.ExxoEquipEffectPlayer>().HyperMagic = true;
    }

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Magic) += 0.1f;
        player.manaCost -= 0.15f;
    }
}
