using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class Melting : ModBuff
{
    private int timer;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Melting");
        Description.SetDefault("I'm melting...!");
        Main.debuff[Type] = true;
        Main.buffNoTimeDisplay[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        if (player.lifeRegen > 0)
        {
            player.lifeRegen = 0;
        }
        timer++;
        if (timer % 5 == 0)
        {
            player.statLife -= 4;
            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), CombatText.LifeRegen, 4, dramatic: false, dot: true);
            if (player.statLife <= 0)
            {
                player.KillMe(PlayerDeathReason.ByCustomReason(" melted away."), 10, 0);
            }
        }
        player.lifeRegenTime = 0;
        player.GetModPlayer<ExxoBuffPlayer>().Melting = true;
        if (player.buffTime[buffIndex] == 0)
        {
            timer = 0;
        }
    }
}
