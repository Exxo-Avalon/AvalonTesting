using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Buffs.AdvancedBuffs;

public class AdvFury : ModBuff
{
    private const int PercentIncrease = 30;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Advanced Fury");
        Description.SetDefault($"{PercentIncrease}% increased critical damage");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetCritChance<GenericDamageClass>() += PercentIncrease;
    }
}
