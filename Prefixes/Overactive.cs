using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Prefixes;

public class Overactive : ModPrefix
{
    public override bool CanRoll(Item item)
    {
        return true;
    }

    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 1.1f;
    }

    public override void Apply(Item item)
    {
        Main.player[Main.myPlayer].statManaMax2 += 20;
        Main.player[Main.myPlayer].manaCost += 0.04f;
    }

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
    }
}
