using Terraria;

namespace Avalon.Prefixes;

public class Loaded : ExxoPrefix
{
    public override bool CanRoll(Item item) => item.IsArmor();

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.15f;

    public override void UpdateOwnerPlayer(Player player) => player.statDefense++;
}
