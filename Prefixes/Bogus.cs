using AvalonTesting.Players;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Prefixes;

public class Bogus : ExxoPrefix
{
    public override PrefixCategory Category => PrefixCategory.Accessory;

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.25f;
    public override bool CanRoll(Item item) => true;
    public override void Apply(Item item) => Main.player[Main.myPlayer].GetCritChance(DamageClass.Generic) += 3;

    public override void UpdateOwnerPlayer(Player player)
    {
        player.GetModPlayer<ExxoPlayer>().CritDamageMult += 0.02f;
        player.GetCritChance(DamageClass.Generic) += 0.02f;
    }
}
