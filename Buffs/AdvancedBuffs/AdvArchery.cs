using Terraria.ModLoader;

namespace AvalonTesting.Buffs.AdvancedBuffs;

public class AdvArchery : ModBuff
{
    public const float PercentageIncrease = 0.30f;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Advanced Archery");
        Description.SetDefault($"{PercentageIncrease * 100}% increased arrow speed and damage");
    }
}
