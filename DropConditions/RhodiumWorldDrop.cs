using Avalon.Systems;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Avalon.DropConditions;

public class RhodiumWorldDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return ModContent.GetInstance<ExxoWorldGen>().RhodiumOre == ExxoWorldGen.RhodiumVariant.Rhodium;
    }

    public bool CanShowItemDropInUI()
    {
        return ModContent.GetInstance<ExxoWorldGen>().RhodiumOre == ExxoWorldGen.RhodiumVariant.Rhodium;
    }

    public string GetConditionDescription()
    {
        return null;
    }
}
