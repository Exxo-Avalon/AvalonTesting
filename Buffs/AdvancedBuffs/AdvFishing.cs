using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Buffs.AdvancedBuffs;

public class AdvFishing : ModBuff
{
    private const int FishingIncrease = 30;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Advanced Fishing");
        Description.SetDefault("Increased fishing level");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.fishingSkill += FishingIncrease;
    }
}
