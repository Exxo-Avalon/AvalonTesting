using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

class ShadowCharm : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Shadow Charm");
        Tooltip.SetDefault("The holder has afterimages when moving\nWorks in the vanity slot\n[c/C39FDD:10th Anniversary Contest Winner - QuibopWon]");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ModContent.RarityType<Rarities.QuibopsRarity>();
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 0, 45);
        Item.height = dims.Height;
    }
    public override void UpdateVanity(Player player)
    {
        player.GetModPlayer<ExxoEquipEffectPlayer>().ShadowCharm = true;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<ExxoEquipEffectPlayer>().ShadowCharm = true;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ItemID.ShadowScale, 3)
            .AddIngredient(ItemID.DemoniteBar, 5)
            .AddIngredient(ItemID.NinjaHood)
            .AddTile(TileID.Anvils)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ItemID.ShadowScale, 3)
            .AddIngredient(ItemID.DemoniteBar, 5)
            .AddIngredient(ItemID.NinjaShirt)
            .AddTile(TileID.Anvils)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ItemID.ShadowScale, 3)
            .AddIngredient(ItemID.DemoniteBar, 5)
            .AddIngredient(ItemID.NinjaPants)
            .AddTile(TileID.Anvils)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ItemID.TissueSample, 3)
            .AddIngredient(ItemID.CrimtaneBar, 5)
            .AddIngredient(ItemID.NinjaHood)
            .AddTile(TileID.Anvils)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ItemID.TissueSample, 3)
            .AddIngredient(ItemID.CrimtaneBar, 5)
            .AddIngredient(ItemID.NinjaShirt)
            .AddTile(TileID.Anvils)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ItemID.TissueSample, 3)
            .AddIngredient(ItemID.CrimtaneBar, 5)
            .AddIngredient(ItemID.NinjaPants)
            .AddTile(TileID.Anvils)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.Booger>(), 3)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.PandemiteBar>(), 5)
            .AddIngredient(ItemID.NinjaHood)
            .AddTile(TileID.Anvils)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.Booger>(), 3)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.PandemiteBar>(), 5)
            .AddIngredient(ItemID.NinjaShirt)
            .AddTile(TileID.Anvils)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.Booger>(), 3)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.PandemiteBar>(), 5)
            .AddIngredient(ItemID.NinjaPants)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
