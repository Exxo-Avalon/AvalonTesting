using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Accessories;

internal class BagofShadows : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bag of Shadows");
        Tooltip.SetDefault("Shadow particles cover you when you move");
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
        Item.GetGlobalItem<AvalonTestingGlobalItemInstance>().UpdateInvisibleVanity = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (player.controlRight)
        {
            int num12 = Dust.NewDust(player.position, player.width - 20, player.height, DustID.Shadowflame, 0f, 0f, 100,
                Color.White, 2f);
            Main.dust[num12].noGravity = true;
        }

        if (player.controlLeft)
        {
            int num13 = Dust.NewDust(player.position, player.width + 20, player.height, DustID.Shadowflame, 0f, 0f, 100,
                Color.White, 2f);
            Main.dust[num13].noGravity = true;
        }

        if (player.controlJump)
        {
            int num14 = Dust.NewDust(player.position, player.width + 20, player.height + 20, DustID.Shadowflame, 0f, 0f,
                100, Color.White, 2f);
            Main.dust[num14].noGravity = true;
        }

        if (player.controlRight)
        {
            int num55 = Dust.NewDust(player.position, player.width - 20, player.height, DustID.Shadowflame, 0f, 0f, 100,
                Color.White, 2f);
            Main.dust[num55].noGravity = true;
        }

        if (player.controlLeft)
        {
            int num56 = Dust.NewDust(player.position, player.width + 20, player.height, DustID.Shadowflame, 0f, 0f, 100,
                Color.White, 2f);
            Main.dust[num56].noGravity = true;
        }

        if (player.controlJump)
        {
            int num57 = Dust.NewDust(player.position, player.width + 20, player.height + 20, DustID.Shadowflame, 0f, 0f,
                100, Color.White, 2f);
            Main.dust[num57].noGravity = true;
        }
    }

    public override void UpdateVanity(Player player)
    {
        if (player.controlRight)
        {
            int num12 = Dust.NewDust(player.position, player.width - 20, player.height, DustID.Shadowflame, 0f, 0f, 100,
                Color.White, 2f);
            Main.dust[num12].noGravity = true;
        }

        if (player.controlLeft)
        {
            int num13 = Dust.NewDust(player.position, player.width + 20, player.height, DustID.Shadowflame, 0f, 0f, 100,
                Color.White, 2f);
            Main.dust[num13].noGravity = true;
        }

        if (player.controlJump)
        {
            int num14 = Dust.NewDust(player.position, player.width + 20, player.height + 20, DustID.Shadowflame, 0f, 0f,
                100, Color.White, 2f);
            Main.dust[num14].noGravity = true;
        }

        if (player.controlRight)
        {
            int num55 = Dust.NewDust(player.position, player.width - 20, player.height, DustID.Shadowflame, 0f, 0f, 100,
                Color.White, 2f);
            Main.dust[num55].noGravity = true;
        }

        if (player.controlLeft)
        {
            int num56 = Dust.NewDust(player.position, player.width + 20, player.height, DustID.Shadowflame, 0f, 0f, 100,
                Color.White, 2f);
            Main.dust[num56].noGravity = true;
        }

        if (player.controlJump)
        {
            int num57 = Dust.NewDust(player.position, player.width + 20, player.height + 20, DustID.Shadowflame, 0f, 0f,
                100, Color.White, 2f);
            Main.dust[num57].noGravity = true;
        }
    }
}
