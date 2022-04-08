using Terraria;

namespace AvalonTesting.Prefixes;

public class Mythic : ArmorPrefix
{
    public override bool CanRoll(Item item)
    {
        return IsArmor(item);
    }

    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 1.1f;
    }
    public override void UpdateEquip(Player player)
    {
        player.statManaMax2 += 20;
    }
}
