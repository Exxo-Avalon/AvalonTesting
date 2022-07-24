using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace Avalon.DropConditions;

public class HardmodeOnly : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return CanShowItemDropInUI();
    }

    public bool CanShowItemDropInUI()
    {
        return Main.hardMode;
    }

    public string GetConditionDescription()
    {
        return null;
    }
}
