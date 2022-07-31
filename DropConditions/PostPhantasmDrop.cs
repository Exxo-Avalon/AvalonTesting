using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalon.Systems;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Avalon.DropConditions;
public class PostPhantasmDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return ModContent.GetInstance<DownedBossSystem>().DownedPhantasm;
    }

    public bool CanShowItemDropInUI()
    {
        return false;
    }

    public string GetConditionDescription()
    {
        return null;
    }
}
