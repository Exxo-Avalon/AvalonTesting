using Avalon.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs;

// TODO: IMPLEMENT
public class Reckoning : ModBuff
{
    private int stacks = 1;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Reckoning");
        Description.SetDefault("Reckoning level: ");
    }

    public override void ModifyBuffTip(ref string tip, ref int rare)
    {
        tip += stacks;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<ExxoPlayer>().ReckoningBonus = true;
        stacks = player.GetModPlayer<ExxoPlayer>().reckoningLevel;
        if (player.buffTime[buffIndex] == 0)
        {
            player.GetModPlayer<ExxoPlayer>().reckoningLevel = 1;
        }
    }
}
