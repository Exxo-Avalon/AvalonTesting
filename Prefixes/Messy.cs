using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Prefixes;

public class Messy : ArmorPrefix
{
    public override bool CanRoll(Item item)
    {
        return IsArmor(item);
    }

    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 0.9f;
    }
    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Generic) -= 0.05f;
    }
}
