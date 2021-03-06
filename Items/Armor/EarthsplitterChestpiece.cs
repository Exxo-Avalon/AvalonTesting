using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor;

[AutoloadEquip(EquipType.Body)]
class EarthsplitterChestpiece : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Earthsplitter Chestpiece");
        Tooltip.SetDefault("Night Owl potion effect\nGrappling hooks can grab onto 5 tiles and have 25% increased range" // add other things from post
                           + "\n[c/C39FDD:10th Anniversary Contest Winner - Crispy]");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 9;
        Item.rare = ModContent.RarityType<Rarities.CrispyRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 3);
        Item.height = dims.Height;
    }
    public override void UpdateEquip(Player player)
    {
        player.nightVision = true;
        player.GetModPlayer<Players.ExxoPlayer>().HookBonus = true;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 4)
            .AddIngredient(ItemID.ShadowScalemail)
            .AddIngredient(ItemID.MiningShirt)
            .AddTile(TileID.Anvils)
            .Register();

        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 4)
            .AddIngredient(ItemID.AncientShadowScalemail)
            .AddIngredient(ItemID.MiningShirt)
            .AddTile(TileID.Anvils)
            .Register();

        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 4)
            .AddIngredient(ItemID.CrimsonScalemail)
            .AddIngredient(ItemID.MiningShirt)
            .AddTile(TileID.Anvils)
            .Register();

        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 4)
            .AddIngredient(ModContent.ItemType<ViruthornScalemail>())
            .AddIngredient(ItemID.MiningShirt)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
