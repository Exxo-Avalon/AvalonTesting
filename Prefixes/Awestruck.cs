using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Prefixes;

public class Awestruck : ModPrefix
{
    public override float RollChance(Item item)
    {
        return 3f;
    }
    public override PrefixCategory Category { get { return PrefixCategory.Melee; } }

    public override bool CanRoll(Item item)
    {
        return true;
    }
    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        knockbackMult = 1.2f;
        damageMult = 1.19f;
        critBonus = 6;
        useTimeMult = 0.85f;
        scaleMult = 1.13f;
    }
}
