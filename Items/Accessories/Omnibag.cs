using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

class Omnibag : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Omnibag");
        Tooltip.SetDefault("A lot of particles cover you when you move\nWorks in the vanity slot");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.vanity = true;
        Item.value = Item.sellPrice(0, 2, 0, 0);
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().UpdateInvisibleVanity = true;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<BagofIck>()).AddIngredient(ModContent.ItemType<BagofBlood>()).AddIngredient(ModContent.ItemType<BagofShadows>()).AddIngredient(ModContent.ItemType<BagofHallows>()).AddIngredient(ModContent.ItemType<BagofFrost>()).AddIngredient(ModContent.ItemType<BagofFire>()).AddTile(TileID.MythrilAnvil).Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (!hideVisual)
        {
            UpdateVanity(player);
        }
    }

    public override void UpdateVanity(Player player)
    {
        ModContent.GetInstance<BagofBlood>().UpdateVanity(player);
        ModContent.GetInstance<BagofFire>().UpdateVanity(player);
        ModContent.GetInstance<BagofFrost>().UpdateVanity(player);
        ModContent.GetInstance<BagofHallows>().UpdateVanity(player);
        ModContent.GetInstance<BagofIck>().UpdateVanity(player);
        ModContent.GetInstance<BagofShadows>().UpdateVanity(player);
    }
}
