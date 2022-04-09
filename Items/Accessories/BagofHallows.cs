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
        if (player.controlRight)
        {
            int num15 = Dust.NewDust(player.position, player.width - 20, player.height, DustID.Enchanted_Gold, 0f, 0f,
                100, Color.White, 2f);
            Main.dust[num15].noGravity = true;
        }

        if (player.controlLeft)
        {
            int num16 = Dust.NewDust(player.position, player.width + 20, player.height, DustID.Enchanted_Gold, 0f, 0f,
                100, Color.White, 2f);
            Main.dust[num16].noGravity = true;
        }

        if (player.controlJump)
        {
            int num17 = Dust.NewDust(player.position, player.width + 20, player.height + 20, DustID.Enchanted_Gold, 0f,
                0f, 100, Color.White, 2f);
            Main.dust[num17].noGravity = true;
        }

        if (player.controlRight)
        {
            int num58 = Dust.NewDust(player.position, player.width - 20, player.height, DustID.Enchanted_Gold, 0f, 0f,
                100, Color.White, 2f);
            Main.dust[num58].noGravity = true;
        }

        if (player.controlLeft)
        {
            int num59 = Dust.NewDust(player.position, player.width + 20, player.height, DustID.Enchanted_Gold, 0f, 0f,
                100, Color.White, 2f);
            Main.dust[num59].noGravity = true;
        }

        if (player.controlJump)
        {
            int num60 = Dust.NewDust(player.position, player.width + 20, player.height + 20, DustID.Enchanted_Gold, 0f,
                0f, 100, Color.White, 2f);
            Main.dust[num60].noGravity = true;
        }
    }

    public override void UpdateVanity(Player player)
    {
        if (player.controlRight)
        {
            int num15 = Dust.NewDust(player.position, player.width - 20, player.height, DustID.Enchanted_Gold, 0f, 0f,
                100, Color.White, 2f);
            Main.dust[num15].noGravity = true;
        }

        if (player.controlLeft)
        {
            int num16 = Dust.NewDust(player.position, player.width + 20, player.height, DustID.Enchanted_Gold, 0f, 0f,
                100, Color.White, 2f);
            Main.dust[num16].noGravity = true;
        }

        if (player.controlJump)
        {
            int num17 = Dust.NewDust(player.position, player.width + 20, player.height + 20, DustID.Enchanted_Gold, 0f,
                0f, 100, Color.White, 2f);
            Main.dust[num17].noGravity = true;
        }

        if (player.controlRight)
        {
            int num58 = Dust.NewDust(player.position, player.width - 20, player.height, DustID.Enchanted_Gold, 0f, 0f,
                100, Color.White, 2f);
            Main.dust[num58].noGravity = true;
        }

        if (player.controlLeft)
        {
            int num59 = Dust.NewDust(player.position, player.width + 20, player.height, DustID.Enchanted_Gold, 0f, 0f,
                100, Color.White, 2f);
            Main.dust[num59].noGravity = true;
        }

        if (player.controlJump)
        {
            int num60 = Dust.NewDust(player.position, player.width + 20, player.height + 20, DustID.Enchanted_Gold, 0f,
                0f, 100, Color.White, 2f);
            Main.dust[num60].noGravity = true;
        }
    }
}
