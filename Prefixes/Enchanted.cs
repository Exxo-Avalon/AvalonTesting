using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Prefixes;

public class Enchanted : ModPrefix
{
    public override PrefixCategory Category => PrefixCategory.Accessory;

    public override void ModifyValue(ref float valueMult)
    {
        valueMult *= 1.25f;
    }
    public override float RollChance(Item item)
    {
        return 3f;
    }
    public override bool CanRoll(Terraria.Item item)
    {
        return true;
    }
    public override void Apply(Item item)
    {
        Main.player[Main.myPlayer].statManaMax2 += 20;
        Main.player[Main.myPlayer].moveSpeed += 0.03f;
        Main.player[Main.myPlayer].statDefense++;
    }
}
