using Terraria.GameContent.ItemDropRules;

namespace Avalon.DropConditions;

public class ZoneSnow : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return info.player.ZoneSnow;
    }

    public bool CanShowItemDropInUI()
    {
        return true;
    }

    public string GetConditionDescription()
    {
        return "Drops in the snow";
    }
}
