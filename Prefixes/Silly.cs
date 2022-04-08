using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Prefixes;

public class Silly : ArmorPrefix
{
    public override bool CanRoll(Item item)
    {
        return IsArmor(item);
    }

    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 1.2f;
    }
    public override void UpdateEquip(Player player)
    {
        player.GetCritChance(DamageClass.Generic) += 2;
    }
}
