using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Prefixes;

public class Slimy : ArmorPrefix
{
    public Slimy()
    {

    }
    public override PrefixCategory Category => PrefixCategory.Custom;
    public override float RollChance(Item item)
    {
        return 3f;
    }
    public override bool CanRoll(Item item)
    {
        return IsArmor(item);
    }

    // public override bool Autoload(ref string name)
    // {
    //     if (base.Autoload(ref name))
    //     {
    //         Mod.AddPrefix("Slimy", new Slimy());
    //     }
    //     return false;
    // }
    public override void UpdateEquip(Player player)
    {
        player.endurance += 0.03f;
    }
}
