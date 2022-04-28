using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Prefixes;

public class Bloated : ArmorPrefix
{
    public override bool CanRoll(Item item)
    {
        return IsArmor(item);
    }
    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Melee) += 0.05f;
        player.GetAttackSpeed(DamageClass.Melee) -= 0.02f;
    }
}
