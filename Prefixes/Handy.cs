using Terraria;

namespace AvalonTesting.Prefixes;

public class Handy : ExxoPrefix
{
    public override bool CanRoll(Item item) => item.IsArmor();

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.2f;

    public override void UpdateOwnerPlayer(Player player) => player.blockRange++;
}
