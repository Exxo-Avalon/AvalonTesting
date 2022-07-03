using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Prefixes;

public class Messy : ExxoPrefix
{
    public override bool CanRoll(Item item) => item.IsArmor();

    public override void ModifyValue(ref float valueMult) => valueMult *= 0.9f;

    public override void UpdateOwnerPlayer(Player player) => player.GetDamage(DamageClass.Generic) -= 0.05f;
}
