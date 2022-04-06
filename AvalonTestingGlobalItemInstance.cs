using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AvalonTesting;
public class AvalonTestingGlobalItemInstance : GlobalItem
{
    public override bool InstancePerEntity => true;
    public override GlobalItem Clone(Item item, Item itemClone)
    {
        return base.Clone(item, itemClone);
    }
    Dictionary<int, int> allowedPrefixes = new Dictionary<int, int>()
    {
        { 0, ModContent.PrefixType<Prefixes.Slimy>() }

    };
    public bool IsArmor(Item item)
    {
        if (item.headSlot != -1 || item.bodySlot != -1 || item.legSlot != -1)
        {
            return !item.vanity;
        }
        return false;
    }
    public override bool? PrefixChance(Item item, int pre, UnifiedRandom rand)
    {
        if (IsArmor(item) && pre == -3)
        {
            return true;
        }
        return base.PrefixChance(item, pre, rand);
    }
    public override int ChoosePrefix(Item item, UnifiedRandom rand)
    {
        if (IsArmor(item))
        {
            return allowedPrefixes[rand.Next(allowedPrefixes.Count)];
        }
        return base.ChoosePrefix(item, rand);
    }
    public override void PostReforge(Item item)
    {
        if (IsArmor(item))
        {
            switch (Main.rand.Next(1))
            {
                case 0:
                    item.prefix = ModContent.PrefixType<Prefixes.Slimy>();
                    break;
            }
        }
    }
}
