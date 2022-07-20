using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Armor;

[AutoloadEquip(EquipType.Legs)]
class DesertGreaves : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Desert Greaves");
        Tooltip.SetDefault("5% increased melee damage");
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

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Melee) += 0.05f;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.SandBlock, 50)
            .AddIngredient(ModContent.ItemType<Placeable.Tile.Beak>(), 3)
            .AddIngredient(ItemID.AntlionMandible)
            .AddIngredient(ItemID.Topaz)
            .AddIngredient(ItemID.GoldGreaves)
            .AddTile(TileID.Anvils).Register();

        Recipe.Create(Type)
            .AddIngredient(ItemID.SandBlock, 50)
            .AddIngredient(ModContent.ItemType<Placeable.Tile.Beak>(), 3)
            .AddIngredient(ItemID.AntlionMandible)
            .AddIngredient(ItemID.Topaz, 5)
            .AddIngredient(ItemID.PlatinumGreaves)
            .AddTile(TileID.Anvils).Register();

        Recipe.Create(Type)
            .AddIngredient(ItemID.SandBlock, 50)
            .AddIngredient(ModContent.ItemType<Placeable.Tile.Beak>(), 3)
            .AddIngredient(ItemID.AntlionMandible)
            .AddIngredient(ItemID.Topaz)
            .AddIngredient(ModContent.ItemType<BismuthGreaves>())
            .AddTile(TileID.Anvils).Register();
    }
}
