using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Prefixes;

public class Robust : ModPrefix
{
    public override PrefixCategory Category { get { return PrefixCategory.Accessory; } }

    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 1.25f;
    }
    public override float RollChance(Item item)
    {
        return 3f;
    }
    public override bool CanRoll(Item item)
    {
        return true;
    }
    //public override void ValidateItem(Item item, ref bool invalid)
    //{
    //    invalid = !item.accessory;
    //}
    public override void Apply(Item item)
    {
        //item.defense += 3;
        //Main.player[Main.myPlayer].GetDamage(DamageClass.Generic) += 0.03f;
    }
    //public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    //{
    //    damageMult += 0.03f;
    //}
}
