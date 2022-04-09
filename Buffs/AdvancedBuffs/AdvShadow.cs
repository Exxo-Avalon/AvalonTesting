using AvalonTesting.Players;
using AvalonTesting.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Buffs.AdvancedBuffs;

public class AdvShadow : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Advanced Shadows");
        Description.SetDefault("You can teleport to the cursor, press V");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        if (ModContent.GetInstance<KeybindSystem>().ShadowHotkey.JustPressed &&
            player.GetModPlayer<ExxoBuffPlayer>().ShadowCooldown >= 300 && !Main.editChest && !Main.editSign &&
            !Main.drawingPlayerChat)
        {
            player.GetModPlayer<ExxoBuffPlayer>().ResetShadowCooldown();

            for (int num10 = 0; num10 < 70; num10++)
            {
                Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, player.velocity.X * 0.5f,
                    player.velocity.Y * 0.5f, 150, default, 1.1f);
            }

            player.position.X = Main.mouseX + Main.screenPosition.X;
            player.position.Y = Main.mouseY + Main.screenPosition.Y;
            for (int num11 = 0; num11 < 70; num11++)
            {
                Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, default,
                    1.1f);
            }
        }
    }
}
