using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace Avalon.DropConditions;

public class IsEclipse :
    IItemDropRuleCondition,
    IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return Main.hardMode && Main.dayTime && Main.eclipse && !info.IsInSimulation;
    }

    public bool CanShowItemDropInUI()
    {
        return Main.hardMode;
    }

    public string GetConditionDescription()
    {
        return "Drops during an Eclipse";
    }
}
