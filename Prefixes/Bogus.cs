using AvalonTesting.Players;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Prefixes;

public class Bogus : ModPrefix
{
    public override PrefixCategory Category => PrefixCategory.Accessory;

    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 1.25f;
    }
    public override bool CanRoll(Item item)
    {
        return true;
    }
    public override void Apply(Item item)
    {
        Main.player[Main.myPlayer].GetCritChance(DamageClass.Generic) += 3;
        //Main.player[Main.myPlayer].GetModPlayer<ExxoPlayer>().CritDamageMult += 0.03f;
    }
}
