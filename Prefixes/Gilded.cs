using Terraria.ModLoader;

namespace AvalonTesting.Prefixes;

public class Gilded : ModPrefix
{
    public override PrefixCategory Category { get { return PrefixCategory.Melee; } }

    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 1.05f;
    }
    public override bool CanRoll(Terraria.Item item)
    {
        return true;
    }

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult = 1.1f;
    }
}
