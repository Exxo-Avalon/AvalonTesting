using Terraria.ModLoader;
using Terraria;

namespace AvalonTesting.Prefixes;

public class Needled : ModPrefix
{
    public override PrefixCategory Category { get { return PrefixCategory.AnyWeapon; } }

    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 1.1f;
    }
    public override bool CanRoll(Item item)
    {
        return true;
    }
    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult = 1.15f;
    }
}
