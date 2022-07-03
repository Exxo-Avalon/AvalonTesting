using Terraria;

namespace AvalonTesting.Prefixes;

public class Protective : ExxoPrefix
{
    public override bool CanRoll(Item item) => item.IsArmor();

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.15f;

    public override void UpdateOwnerPlayer(Player player) => player.statDefense += 2;
}
