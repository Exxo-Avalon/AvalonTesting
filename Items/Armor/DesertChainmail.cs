using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor;

[AutoloadEquip(EquipType.Body)]
class DesertChainmail : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Desert Chainmail");
        Tooltip.SetDefault("5% decreased mana usage\nIncreases maximum mana by 20");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 6;
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 1, 0, 0);
        Item.height = dims.Height;
    }

    public override void UpdateEquip(Player player)
    {
        player.manaCost -= 0.05f;
        player.statManaMax2 += 20;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.SandBlock, 100)
            .AddIngredient(ModContent.ItemType<Material.Beak>(), 5)
            .AddIngredient(ItemID.AntlionMandible, 2)
            .AddIngredient(ItemID.Topaz, 2)
            .AddIngredient(ItemID.GoldChainmail)
            .AddTile(TileID.Anvils).Register();

        Recipe.Create(Type)
            .AddIngredient(ItemID.SandBlock, 100)
            .AddIngredient(ModContent.ItemType<Material.Beak>(), 5)
            .AddIngredient(ItemID.AntlionMandible, 2)
            .AddIngredient(ItemID.Topaz, 5)
            .AddIngredient(ItemID.PlatinumChainmail)
            .AddTile(TileID.Anvils).Register();

        Recipe.Create(Type)
            .AddIngredient(ItemID.SandBlock, 100)
            .AddIngredient(ModContent.ItemType<Material.Beak>(), 5)
            .AddIngredient(ItemID.AntlionMandible, 2)
            .AddIngredient(ItemID.Topaz, 2)
            .AddIngredient(ModContent.ItemType<BismuthChainmail>())
            .AddTile(TileID.Anvils).Register();
    }
}
