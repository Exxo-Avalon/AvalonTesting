using Terraria;

namespace AvalonTesting.Prefixes;

public class Busted : ExxoPrefix
{
    public override bool CanRoll(Item item) => item.IsArmor();

    public override void ModifyValue(ref float valueMult) => valueMult *= 0.9f;

    public override void UpdateOwnerPlayer(Player player) => player.statDefense--;
}
