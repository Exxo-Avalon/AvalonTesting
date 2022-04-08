using Terraria;

namespace AvalonTesting.Prefixes;

public class Fluidic : ArmorPrefix
{
    public override bool CanRoll(Item item)
    {
        return IsArmor(item);
    }
    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 1.25f;
    }
    public override void UpdateEquip(Player player)
    {
        player.moveSpeed += 0.05f;
        player.ignoreWater = true;
    }
}
