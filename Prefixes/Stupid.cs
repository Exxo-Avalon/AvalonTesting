using Terraria.ModLoader;

namespace AvalonTesting.Prefixes;

public class Stupid : ModPrefix
{
    public override PrefixCategory Category => PrefixCategory.Melee;

    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 0.9f;
    }
    public override bool CanRoll(Terraria.Item item)
    {
        return true;
    }
    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        knockbackMult = 1.16f;
        damageMult = 0.7f;
        useTimeMult = 1.05f;
        scaleMult = 1.25f;
    }
}
