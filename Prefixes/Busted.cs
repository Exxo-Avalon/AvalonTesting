using Terraria;

namespace AvalonTesting.Prefixes;

public class Busted : ArmorPrefix
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
        player.statDefense--;
    }
}
