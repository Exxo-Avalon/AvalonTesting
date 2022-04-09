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
        if (player.controlRight)
        {
            int num9 = Dust.NewDust(player.position, player.width - 20, player.height, DustID.Torch, 0f, 0f, 100,
                Color.White, 2f);
            Main.dust[num9].noGravity = true;
        }

        if (player.controlLeft)
        {
            int num10 = Dust.NewDust(player.position, player.width + 20, player.height, DustID.Torch, 0f, 0f, 100,
                Color.White, 2f);
            Main.dust[num10].noGravity = true;
        }

        if (player.controlJump)
        {
            int num11 = Dust.NewDust(player.position, player.width + 20, player.height + 20, DustID.Torch, 0f, 0f, 100,
                Color.White, 2f);
            Main.dust[num11].noGravity = true;
        }

        if (player.controlRight)
        {
            int num52 = Dust.NewDust(player.position, player.width - 20, player.height, DustID.Torch, 0f, 0f, 100,
                Color.White, 2f);
            Main.dust[num52].noGravity = true;
        }

        if (player.controlLeft)
        {
            int num53 = Dust.NewDust(player.position, player.width + 20, player.height, DustID.Torch, 0f, 0f, 100,
                Color.White, 2f);
            Main.dust[num53].noGravity = true;
        }

        if (player.controlJump)
        {
            int num54 = Dust.NewDust(player.position, player.width + 20, player.height + 20, DustID.Torch, 0f, 0f, 100,
                Color.White, 2f);
            Main.dust[num54].noGravity = true;
        }
    }

    public override void UpdateVanity(Player player)
    {
        if (player.controlRight)
        {
            int num9 = Dust.NewDust(player.position, player.width - 20, player.height, DustID.Torch, 0f, 0f, 100,
                Color.White, 2f);
            Main.dust[num9].noGravity = true;
        }

        if (player.controlLeft)
        {
            int num10 = Dust.NewDust(player.position, player.width + 20, player.height, DustID.Torch, 0f, 0f, 100,
                Color.White, 2f);
            Main.dust[num10].noGravity = true;
        }

        if (player.controlJump)
        {
            int num11 = Dust.NewDust(player.position, player.width + 20, player.height + 20, DustID.Torch, 0f, 0f, 100,
                Color.White, 2f);
            Main.dust[num11].noGravity = true;
        }

        if (player.controlRight)
        {
            int num52 = Dust.NewDust(player.position, player.width - 20, player.height, DustID.Torch, 0f, 0f, 100,
                Color.White, 2f);
            Main.dust[num52].noGravity = true;
        }

        if (player.controlLeft)
        {
            int num53 = Dust.NewDust(player.position, player.width + 20, player.height, DustID.Torch, 0f, 0f, 100,
                Color.White, 2f);
            Main.dust[num53].noGravity = true;
        }

        if (player.controlJump)
        {
            int num54 = Dust.NewDust(player.position, player.width + 20, player.height + 20, DustID.Torch, 0f, 0f, 100,
                Color.White, 2f);
            Main.dust[num54].noGravity = true;
        }
    }
}
