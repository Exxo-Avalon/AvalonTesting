using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

internal class BagofBlood : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bag of Blood");
        Tooltip.SetDefault("Blood particles cover you when you move\nWorks in the vanity slot");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Green;
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
            .AddIngredient(ItemID.Vertebrae, 15)
            .AddIngredient(ItemID.Ichor, 10)
            .AddIngredient(ItemID.CrimstoneBlock, 50)
            .AddIngredient(ModContent.ItemType<Material.CorruptShard>(), 5)
            .AddTile(TileID.Hellforge).Register();
    }
    public override void UpdateVanity(Player player)
    {
        if (!(player.velocity.Length() > 0))
        {
            return;
        }

        int dust1 = Dust.NewDust(player.position, player.width - 20, player.height, DustID.TheDestroyer, 0f, 0f,
            100,
            Color.White, 0.9f);
        Main.dust[dust1].noGravity = true;
        int dust2 = Dust.NewDust(player.position, player.width - 20, player.height, DustID.Blood, 0f, 0f, 100,
            Color.White,
            1.5f);
        Main.dust[dust2].noGravity = true;
    }
}
