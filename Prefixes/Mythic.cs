using Terraria;

namespace AvalonTesting.Prefixes;

public class Mythic : ExxoPrefix
{
    public override bool CanRoll(Item item) => item.IsArmor();

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.1f;

    public override void UpdateOwnerPlayer(Player player) => player.statManaMax2 += 20;
}
