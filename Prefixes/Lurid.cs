using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Prefixes;

public class Lurid : ModPrefix
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
    public override void Apply(Item item)
    {
        //Main.player[Main.myPlayer].GetCritChance(DamageClass.Generic) += 2;
        //item.defense += 2;
    }
}
