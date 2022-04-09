using AvalonTesting.Players;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Buffs.AdvancedBuffs;

public class AdvBattle : ModBuff
{
    public const float RateMultiplier = 0.35f; // Lower means more spawns as rate is the delay in time
    public const float SpawnMultiplier = 2.5f;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Advanced Battle");
        Description.SetDefault("Increased enemy spawn rate");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<ExxoBuffPlayer>().AdvancedBattle = true;
    }
}
