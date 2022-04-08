using Terraria;

namespace AvalonTesting.Prefixes;

public class Protective : ArmorPrefix
{
    public override bool CanRoll(Item item)
    {
        return IsArmor(item);
    }

    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 1.15f;
    }
    public override void UpdateEquip(Player player)
    {
        player.statDefense += 2;
    }
}
