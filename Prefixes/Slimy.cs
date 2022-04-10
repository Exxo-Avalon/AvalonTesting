using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Prefixes;

public class Slimy : ArmorPrefix
{
    public override PrefixCategory Category => PrefixCategory.Custom;
    public override float RollChance(Item item)
    {
        return 3f;
    }
    public override bool CanRoll(Item item)
    {
        return IsArmor(item);
    } 
    public override void UpdateEquip(Player player)
    {
        player.endurance += 0.03f;
    }
}
