using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Awestruck : ExxoPrefix
{
    public override PrefixCategory Category => PrefixCategory.Melee;

    public override float RollChance(Item item) => 3f;

    public override bool CanRoll(Item item) => true;

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult,
                                  ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        knockbackMult = 1.2f;
        damageMult = 1.19f;
        critBonus = 6;
        useTimeMult = 0.85f;
        scaleMult = 1.13f;
    }
}
