using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvEndurance : ModBuff
{
    private const float PercentIncrease = 0.15f;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Advanced Endurance");
        Description.SetDefault($"{PercentIncrease * 100}% reduced damage taken");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.endurance += PercentIncrease;
    }
}
