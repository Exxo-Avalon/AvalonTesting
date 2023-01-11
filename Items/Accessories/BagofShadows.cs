using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

internal class BagofShadows : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bag of Shadows");
        Tooltip.SetDefault("Shadow particles cover you when you move\nWorks in the vanity slot");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.vanity = true;
        Item.value = Item.sellPrice(0, 1);
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().UpdateInvisibleVanity = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (!hideVisual)
        {
            UpdateVanity(player);
        }
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.DemoniteBar, 15)
            .AddIngredient(ItemID.CursedFlame, 5)
            .AddIngredient(ItemID.EbonstoneBlock, 50)
            .AddIngredient(ModContent.ItemType<Material.CorruptShard>(), 5)
            .AddTile(TileID.Hellforge).Register();
    }
    public override void UpdateVanity(Player player)
    {
        if (!(player.velocity.Length() > 0))
        {
            return;
        }

        int dust = Dust.NewDust(player.position, player.width - 20, player.height, DustID.Shadowflame, 0f, 0f, 100,
            Color.White, 2f);
        Main.dust[dust].noGravity = true;
    }
}
