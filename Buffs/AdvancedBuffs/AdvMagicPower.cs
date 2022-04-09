using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Buffs.AdvancedBuffs;

public class AdvMagicPower : ModBuff
{
    private const float PercentIncrease = 0.3f;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Advanced Magic Power");
        Description.SetDefault($"{PercentIncrease * 100}% increased magic damage");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetDamage(DamageClass.Magic) += PercentIncrease;
    }
}
