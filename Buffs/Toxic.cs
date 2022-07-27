using Avalon.Players;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class Toxic : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Toxic");
        Description.SetDefault("Losing life");
        Main.debuff[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        if (player.GetModPlayer<ExxoBuffPlayer>().FrameCount % 60 == 0)
        {
            if (player.GetModPlayer<ExxoBuffPlayer>().InfectDamage < 16)
            {
                player.GetModPlayer<ExxoBuffPlayer>().InfectDamage *= 2;
            }
            else
            {
                player.GetModPlayer<ExxoBuffPlayer>().InfectDamage = 16;
            }

            player.Hurt(PlayerDeathReason.ByCustomReason(" was infected."),
                player.GetModPlayer<ExxoBuffPlayer>().InfectDamage + player.statDefense / 2, 0);
        }
    }
}
