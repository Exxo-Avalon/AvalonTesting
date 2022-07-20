using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Accessories;

internal class BagofFire : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bag of Fire");
        Tooltip.SetDefault("Flame particles cover you when you move");
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
        Item.GetGlobalItem<AvalonTestingGlobalItemInstance>().UpdateInvisibleVanity = true;
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
            .AddIngredient(ItemID.Fireblossom, 15)
            .AddIngredient(ItemID.HellstoneBar, 10)
            .AddIngredient(ItemID.AshBlock, 50)
            .AddIngredient(ModContent.ItemType<Material.FireShard>(), 5)
            .AddTile(TileID.Hellforge).Register();
    }
    public override void UpdateVanity(Player player)
    {
        if (!(player.velocity.Length() > 0))
        {
            return;
        }

        int dust = Dust.NewDust(player.position, player.width + 20, player.height + 20, DustID.Torch, 0f, 0f, 100,
            Color.White, 2f);
        Main.dust[dust].noGravity = true;
    }
}
