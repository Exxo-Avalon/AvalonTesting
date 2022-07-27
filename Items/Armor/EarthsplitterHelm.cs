using Avalon.Players;
using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Avalon.Items.Armor;

[AutoloadEquip(EquipType.Head)]
internal class EarthsplitterHelm : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Earthsplitter Helm");
        Tooltip.SetDefault("Shine potion effect"
                           + "\n[c/C39FDD:10th Anniversary Contest Winner - Crispy]");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 10;
        Item.rare = ModContent.RarityType<CrispyRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 3);
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 2)
            .AddIngredient(ItemID.ShadowHelmet)
            .AddIngredient(ItemID.MiningHelmet)
            .AddTile(TileID.Anvils)
            .Register();

        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 2)
            .AddIngredient(ItemID.AncientShadowHelmet)
            .AddIngredient(ItemID.MiningHelmet)
            .AddTile(TileID.Anvils)
            .Register();

        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 2)
            .AddIngredient(ItemID.CrimsonHelmet)
            .AddIngredient(ItemID.MiningHelmet)
            .AddTile(TileID.Anvils)
            .Register();

        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 2)
            .AddIngredient(ModContent.ItemType<ViruthornHelmet>())
            .AddIngredient(ItemID.MiningHelmet)
            .AddTile(TileID.Anvils)
            .Register();
    }
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<EarthsplitterChestpiece>() && legs.type == ModContent.ItemType<EarthsplitterLeggings>();
    }

    public override void UpdateArmorSet(Player player)
    {
        ExxoPlayer modPlayer = player.Avalon();
        player.setBonus = "Ore blocks have a 33% chance to drop double ore";

        modPlayer.oreDupe = true;
    }

    public override void UpdateEquip(Player player)
    {
        Lighting.AddLight(player.position, 0.8f, 0.8f, 0.8f);
    }
}
