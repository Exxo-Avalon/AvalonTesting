using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Accessories;

internal class BagofFrost : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bag of Frost");
        Tooltip.SetDefault("Frostburn particles cover you when you move");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightPurple;
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
            for (int num30 = 0; num30 < 5; num30++)
            {
                float num31 = player.velocity.X / 3f * num30;
                float num32 = player.velocity.Y / 3f * num30;
                int num33 = 4;
                int num34 = Dust.NewDust(new Vector2(player.position.X + num33, player.position.Y + num33),
                    player.width - (num33 * 2), player.height - (num33 * 2), DustID.IceTorch, 0f, 0f, 100, default,
                    1.8f);
                Main.dust[num34].noGravity = true;
                Main.dust[num34].velocity *= 0.1f;
                Main.dust[num34].velocity += player.velocity * 0.1f;
                Dust expr_4AFD_cp_0 = Main.dust[num34];
                expr_4AFD_cp_0.position.X = expr_4AFD_cp_0.position.X - num31;
                Dust expr_4B18_cp_0 = Main.dust[num34];
                expr_4B18_cp_0.position.Y = expr_4B18_cp_0.position.Y - num32;
            }

            if (Main.rand.Next(3) == 0)
            {
                int num35 = 4;
                int num36 = Dust.NewDust(new Vector2(player.position.X + num35, player.position.Y + num35),
                    player.width - (num35 * 2), player.height - (num35 * 2), DustID.MagicMirror, 0f, 0f, 100, default,
                    0.6f);
                Main.dust[num36].noGravity = true;
                Main.dust[num36].velocity *= 0.25f;
                Main.dust[num36].velocity += player.velocity * 0.5f;
            }
        }

        if (player.controlLeft)
        {
            for (int num37 = 0; num37 < 5; num37++)
            {
                float num38 = player.velocity.X / 3f * num37;
                float num39 = player.velocity.Y / 3f * num37;
                int num40 = 4;
                int num41 = Dust.NewDust(new Vector2(player.position.X + num40, player.position.Y + num40),
                    player.width - (num40 * 2), player.height - (num40 * 2), DustID.IceTorch, 0f, 0f, 100, default,
                    1.8f);
                Main.dust[num41].noGravity = true;
                Main.dust[num41].velocity *= 0.1f;
                Main.dust[num41].velocity += player.velocity * 0.1f;
                Dust expr_4CFB_cp_0 = Main.dust[num41];
                expr_4CFB_cp_0.position.X = expr_4CFB_cp_0.position.X - num38;
                Dust expr_4D16_cp_0 = Main.dust[num41];
                expr_4D16_cp_0.position.Y = expr_4D16_cp_0.position.Y - num39;
            }

            if (Main.rand.Next(3) == 0)
            {
                int num42 = 4;
                int num43 = Dust.NewDust(new Vector2(player.position.X + num42, player.position.Y + num42),
                    player.width - (num42 * 2), player.height - (num42 * 2), DustID.MagicMirror, 0f, 0f, 100, default,
                    0.6f);
                Main.dust[num43].noGravity = true;
                Main.dust[num43].velocity *= 0.25f;
                Main.dust[num43].velocity += player.velocity * 0.5f;
            }
        }

        if (player.controlJump)
        {
            int num44 = Dust.NewDust(player.position, player.width - 20, player.height, DustID.IceTorch, 0f, 0f, 100,
                Color.White, 2f);
            Main.dust[num44].noGravity = true;
        }

        if (player.controlRight)
        {
            for (int num73 = 0; num73 < 5; num73++)
            {
                float num74 = player.velocity.X / 3f * num73;
                float num75 = player.velocity.Y / 3f * num73;
                int num76 = 4;
                int num77 = Dust.NewDust(new Vector2(player.position.X + num76, player.position.Y + num76),
                    player.width - (num76 * 2), player.height - (num76 * 2), DustID.IceTorch, 0f, 0f, 100, default,
                    1.8f);
                Main.dust[num77].noGravity = true;
                Main.dust[num77].velocity *= 0.1f;
                Main.dust[num77].velocity += player.velocity * 0.1f;
                Dust expr_78A2_cp_0 = Main.dust[num77];
                expr_78A2_cp_0.position.X = expr_78A2_cp_0.position.X - num74;
                Dust expr_78BD_cp_0 = Main.dust[num77];
                expr_78BD_cp_0.position.Y = expr_78BD_cp_0.position.Y - num75;
            }

            if (Main.rand.Next(3) == 0)
            {
                int num78 = 4;
                int num79 = Dust.NewDust(new Vector2(player.position.X + num78, player.position.Y + num78),
                    player.width - (num78 * 2), player.height - (num78 * 2), DustID.MagicMirror, 0f, 0f, 100, default,
                    0.6f);
                Main.dust[num79].noGravity = true;
                Main.dust[num79].velocity *= 0.25f;
                Main.dust[num79].velocity += player.velocity * 0.5f;
            }
        }

        if (player.controlLeft)
        {
            for (int num80 = 0; num80 < 5; num80++)
            {
                float num81 = player.velocity.X / 3f * num80;
                float num82 = player.velocity.Y / 3f * num80;
                int num83 = 4;
                int num84 = Dust.NewDust(new Vector2(player.position.X + num83, player.position.Y + num83),
                    player.width - (num83 * 2), player.height - (num83 * 2), DustID.IceTorch, 0f, 0f, 100, default,
                    1.8f);
                Main.dust[num84].noGravity = true;
                Main.dust[num84].velocity *= 0.1f;
                Main.dust[num84].velocity += player.velocity * 0.1f;
                Dust expr_7AA0_cp_0 = Main.dust[num84];
                expr_7AA0_cp_0.position.X = expr_7AA0_cp_0.position.X - num81;
                Dust expr_7ABB_cp_0 = Main.dust[num84];
                expr_7ABB_cp_0.position.Y = expr_7ABB_cp_0.position.Y - num82;
            }

            if (Main.rand.Next(3) == 0)
            {
                int num85 = 4;
                int num86 = Dust.NewDust(new Vector2(player.position.X + num85, player.position.Y + num85),
                    player.width - (num85 * 2), player.height - (num85 * 2), DustID.MagicMirror, 0f, 0f, 100, default,
                    0.6f);
                Main.dust[num86].noGravity = true;
                Main.dust[num86].velocity *= 0.25f;
                Main.dust[num86].velocity += player.velocity * 0.5f;
            }
        }

        if (player.controlJump)
        {
            int num87 = Dust.NewDust(player.position, player.width - 20, player.height, DustID.IceTorch, 0f, 0f, 100,
                Color.White, 2f);
            Main.dust[num87].noGravity = true;
        }
    }

    public override void UpdateVanity(Player player)
    {
        if (player.controlRight)
        {
            for (int num30 = 0; num30 < 5; num30++)
            {
                float num31 = player.velocity.X / 3f * num30;
                float num32 = player.velocity.Y / 3f * num30;
                int num33 = 4;
                int num34 = Dust.NewDust(new Vector2(player.position.X + num33, player.position.Y + num33),
                    player.width - (num33 * 2), player.height - (num33 * 2), DustID.IceTorch, 0f, 0f, 100, default,
                    1.8f);
                Main.dust[num34].noGravity = true;
                Main.dust[num34].velocity *= 0.1f;
                Main.dust[num34].velocity += player.velocity * 0.1f;
                Dust expr_4AFD_cp_0 = Main.dust[num34];
                expr_4AFD_cp_0.position.X = expr_4AFD_cp_0.position.X - num31;
                Dust expr_4B18_cp_0 = Main.dust[num34];
                expr_4B18_cp_0.position.Y = expr_4B18_cp_0.position.Y - num32;
            }

            if (Main.rand.Next(3) == 0)
            {
                int num35 = 4;
                int num36 = Dust.NewDust(new Vector2(player.position.X + num35, player.position.Y + num35),
                    player.width - (num35 * 2), player.height - (num35 * 2), DustID.MagicMirror, 0f, 0f, 100, default,
                    0.6f);
                Main.dust[num36].noGravity = true;
                Main.dust[num36].velocity *= 0.25f;
                Main.dust[num36].velocity += player.velocity * 0.5f;
            }
        }

        if (player.controlLeft)
        {
            for (int num37 = 0; num37 < 5; num37++)
            {
                float num38 = player.velocity.X / 3f * num37;
                float num39 = player.velocity.Y / 3f * num37;
                int num40 = 4;
                int num41 = Dust.NewDust(new Vector2(player.position.X + num40, player.position.Y + num40),
                    player.width - (num40 * 2), player.height - (num40 * 2), DustID.IceTorch, 0f, 0f, 100, default,
                    1.8f);
                Main.dust[num41].noGravity = true;
                Main.dust[num41].velocity *= 0.1f;
                Main.dust[num41].velocity += player.velocity * 0.1f;
                Dust expr_4CFB_cp_0 = Main.dust[num41];
                expr_4CFB_cp_0.position.X = expr_4CFB_cp_0.position.X - num38;
                Dust expr_4D16_cp_0 = Main.dust[num41];
                expr_4D16_cp_0.position.Y = expr_4D16_cp_0.position.Y - num39;
            }

            if (Main.rand.Next(3) == 0)
            {
                int num42 = 4;
                int num43 = Dust.NewDust(new Vector2(player.position.X + num42, player.position.Y + num42),
                    player.width - (num42 * 2), player.height - (num42 * 2), DustID.MagicMirror, 0f, 0f, 100, default,
                    0.6f);
                Main.dust[num43].noGravity = true;
                Main.dust[num43].velocity *= 0.25f;
                Main.dust[num43].velocity += player.velocity * 0.5f;
            }
        }

        if (player.controlJump)
        {
            int num44 = Dust.NewDust(player.position, player.width - 20, player.height, DustID.IceTorch, 0f, 0f, 100,
                Color.White, 2f);
            Main.dust[num44].noGravity = true;
        }

        if (player.controlRight)
        {
            for (int num73 = 0; num73 < 5; num73++)
            {
                float num74 = player.velocity.X / 3f * num73;
                float num75 = player.velocity.Y / 3f * num73;
                int num76 = 4;
                int num77 = Dust.NewDust(new Vector2(player.position.X + num76, player.position.Y + num76),
                    player.width - (num76 * 2), player.height - (num76 * 2), DustID.IceTorch, 0f, 0f, 100, default,
                    1.8f);
                Main.dust[num77].noGravity = true;
                Main.dust[num77].velocity *= 0.1f;
                Main.dust[num77].velocity += player.velocity * 0.1f;
                Dust expr_78A2_cp_0 = Main.dust[num77];
                expr_78A2_cp_0.position.X = expr_78A2_cp_0.position.X - num74;
                Dust expr_78BD_cp_0 = Main.dust[num77];
                expr_78BD_cp_0.position.Y = expr_78BD_cp_0.position.Y - num75;
            }

            if (Main.rand.Next(3) == 0)
            {
                int num78 = 4;
                int num79 = Dust.NewDust(new Vector2(player.position.X + num78, player.position.Y + num78),
                    player.width - (num78 * 2), player.height - (num78 * 2), DustID.MagicMirror, 0f, 0f, 100, default,
                    0.6f);
                Main.dust[num79].noGravity = true;
                Main.dust[num79].velocity *= 0.25f;
                Main.dust[num79].velocity += player.velocity * 0.5f;
            }
        }

        if (player.controlLeft)
        {
            for (int num80 = 0; num80 < 5; num80++)
            {
                float num81 = player.velocity.X / 3f * num80;
                float num82 = player.velocity.Y / 3f * num80;
                int num83 = 4;
                int num84 = Dust.NewDust(new Vector2(player.position.X + num83, player.position.Y + num83),
                    player.width - (num83 * 2), player.height - (num83 * 2), DustID.IceTorch, 0f, 0f, 100, default,
                    1.8f);
                Main.dust[num84].noGravity = true;
                Main.dust[num84].velocity *= 0.1f;
                Main.dust[num84].velocity += player.velocity * 0.1f;
                Dust expr_7AA0_cp_0 = Main.dust[num84];
                expr_7AA0_cp_0.position.X = expr_7AA0_cp_0.position.X - num81;
                Dust expr_7ABB_cp_0 = Main.dust[num84];
                expr_7ABB_cp_0.position.Y = expr_7ABB_cp_0.position.Y - num82;
            }

            if (Main.rand.Next(3) == 0)
            {
                int num85 = 4;
                int num86 = Dust.NewDust(new Vector2(player.position.X + num85, player.position.Y + num85),
                    player.width - (num85 * 2), player.height - (num85 * 2), DustID.MagicMirror, 0f, 0f, 100, default,
                    0.6f);
                Main.dust[num86].noGravity = true;
                Main.dust[num86].velocity *= 0.25f;
                Main.dust[num86].velocity += player.velocity * 0.5f;
            }
        }

        if (player.controlJump)
        {
            int num87 = Dust.NewDust(player.position, player.width - 20, player.height, DustID.IceTorch, 0f, 0f, 100,
                Color.White, 2f);
            Main.dust[num87].noGravity = true;
        }
    }
}
