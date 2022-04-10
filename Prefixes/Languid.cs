using AvalonTesting.Players;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Prefixes;

public class Languid : ModPrefix
{
    public override PrefixCategory Category => PrefixCategory.Accessory;

    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 0.9f;
    }
    public override bool CanRoll(Item item)
    {
        return true;
    }
    public override void Apply(Item item)
    {
    }
}
