using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class TimeShift : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Time Shift");
        Description.SetDefault("Time is slowed down");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<Players.ExxoBuffPlayer>().TimeSlowCounter++;
        if (player.GetModPlayer<Players.ExxoBuffPlayer>().TimeSlowCounter % 2 == 0)
        {
            Main.time--;
        }
        if (player.buffTime[buffIndex] == 0)
        {
            player.GetModPlayer<Players.ExxoBuffPlayer>().TimeSlowCounter = 0;
        }
    }
}
