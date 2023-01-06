using Avalon.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvLuck : ModBuff
{
    public const float PercentIncrease = 0.10f;

    // TODO: OUTDATED DESCRIPTION
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Advanced Clover");
        Description.SetDefault("Doubles rare drop chance");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.enemySpawns = true;
        player.GetModPlayer<ExxoBuffPlayer>().Lucky = true;
        player.GetModPlayer<ExxoBuffPlayer>().AdvancedBattle = true;
    }
}
