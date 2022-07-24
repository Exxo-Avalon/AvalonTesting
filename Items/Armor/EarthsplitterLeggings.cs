using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor;

[AutoloadEquip(EquipType.Legs)]
class EarthsplitterLeggings : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Earthsplitter Leggings");
        Tooltip.SetDefault("Increases jump height"
                           + "\n15% increased mining speed"
                           + "\n[c/C39FDD:10th Anniversary Contest Winner - Crispy]");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 8;
        Item.rare = ModContent.RarityType<Rarities.CrispyRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 3);
        Item.height = dims.Height;
    }
    public override void UpdateEquip(Player player)
    {
        player.frogLegJumpBoost = true;
        Player.jumpHeight = 30;
        player.pickSpeed -= 0.15f;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 6)
            .AddIngredient(ItemID.ShadowGreaves)
            .AddIngredient(ItemID.MiningPants)
            .AddTile(TileID.Anvils)
            .Register();

        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 6)
            .AddIngredient(ItemID.AncientShadowGreaves)
            .AddIngredient(ItemID.MiningPants)
            .AddTile(TileID.Anvils)
            .Register();

        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 6)
            .AddIngredient(ItemID.CrimsonGreaves)
            .AddIngredient(ItemID.MiningPants)
            .AddTile(TileID.Anvils)
            .Register();

        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 6)
            .AddIngredient(ModContent.ItemType<ViruthornGreaves>())
            .AddIngredient(ItemID.MiningPants)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
