using AvalonTesting.Players;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Buffs.AdvancedBuffs;

public class AdvCalming : ModBuff
{
    public const float RateMultiplier = 1.5f; // Lower means more spawns as rate is the delay in time
    public const float SpawnMultiplier = 0.65f;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Advanced Calm");
        Description.SetDefault("Reduced enemy aggression");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<ExxoBuffPlayer>().AdvancedCalming = true;
    }
}
