using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Buffs.AdvancedBuffs;

public class AdvRage : ModBuff
{
    private const int PercentIncrease = 15;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Advanced Rage");
        Description.SetDefault($"{PercentIncrease}% increased critical strike chance");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetCritChance<GenericDamageClass>() += PercentIncrease;
    }
}
