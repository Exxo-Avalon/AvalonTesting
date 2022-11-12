using Avalon.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Bogus : ExxoPrefix
{
    public override PrefixCategory Category => PrefixCategory.Accessory;

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.25f;
    public override bool CanRoll(Item item) => true;

    public override void UpdateOwnerPlayer(Player player)
    {
        player.GetModPlayer<ExxoPlayer>().CritDamageMult += 0.06f;
        player.GetCritChance(DamageClass.Generic) += 2;
    }
}
