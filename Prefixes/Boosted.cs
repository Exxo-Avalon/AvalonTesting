using Terraria;

namespace AvalonTesting.Prefixes;

public class Boosted : ArmorPrefix
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
        player.moveSpeed += 0.04f;
    }
}
