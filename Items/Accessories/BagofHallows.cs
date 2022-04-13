using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Accessories;

internal class BagofHallows : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bag of Hallows");
        Tooltip.SetDefault("Hallow particles cover you when you move");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightPurple;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.vanity = true;
        Item.value = Item.sellPrice(0, 2);
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

    public override void UpdateVanity(Player player)
    {
        if (!(player.velocity.Length() > 0))
        {
            return;
        }

        int dust = Dust.NewDust(player.position, player.width + 20, player.height, DustID.Enchanted_Gold, 0f, 0f,
            100, Color.White, 2f);
        Main.dust[dust].noGravity = true;
    }
}
