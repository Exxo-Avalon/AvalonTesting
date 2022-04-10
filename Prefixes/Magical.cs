using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Prefixes;

public class Magical : ModPrefix
{
    public override PrefixCategory Category { get { return PrefixCategory.Accessory; } }

    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 1.25f;
    }
    public override bool CanRoll(Item item)
    {
        return true;
    }
    public override void Apply(Item item)
    {
        Main.player[Main.myPlayer].statManaMax2 += 40;
    }

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
    }
}
