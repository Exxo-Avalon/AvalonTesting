using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor;

[AutoloadEquip(EquipType.Head)]
class DesertHelmet : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Desert Helmet");
        Tooltip.SetDefault("5% decreased mana usage\n5% increased ranged damage");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 5;
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 1, 0, 0);
        Item.height = dims.Height;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<DesertChainmail>() && legs.type == ModContent.ItemType<DesertGreaves>();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "15% increased magic damage";
        player.GetDamage(DamageClass.Magic) += 0.15f;
    }

    public override void UpdateEquip(Player player)
    {
        player.manaCost -= 0.05f;
        player.GetDamage(DamageClass.Ranged) += 0.05f;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.SandBlock, 75)
            .AddIngredient(ModContent.ItemType<Placeable.Tile.Beak>(), 2)
            .AddIngredient(ItemID.AntlionMandible, 2)
            .AddIngredient(ItemID.Topaz, 2)
            .AddIngredient(ItemID.GoldHelmet)
            .AddTile(TileID.Anvils).Register();

        Recipe.Create(Type)
            .AddIngredient(ItemID.SandBlock, 75)
            .AddIngredient(ModContent.ItemType<Placeable.Tile.Beak>(), 2)
            .AddIngredient(ItemID.AntlionMandible, 2)
            .AddIngredient(ItemID.Topaz, 10)
            .AddIngredient(ItemID.PlatinumHelmet)
            .AddTile(TileID.Anvils).Register();

        Recipe.Create(Type)
            .AddIngredient(ItemID.SandBlock, 75)
            .AddIngredient(ModContent.ItemType<Placeable.Tile.Beak>(), 2)
            .AddIngredient(ItemID.AntlionMandible, 2)
            .AddIngredient(ItemID.Topaz, 2)
            .AddIngredient(ModContent.ItemType<BismuthHelmet>())
            .AddTile(TileID.Anvils).Register();
    }
}
