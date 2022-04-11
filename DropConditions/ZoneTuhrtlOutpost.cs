using AvalonTesting.Players;
using Terraria.GameContent.ItemDropRules;

namespace AvalonTesting.DropConditions;

public class OutpostDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return info.player.GetModPlayer<ExxoBiomePlayer>().ZoneTuhrtlOutpost;
    }

    public bool CanShowItemDropInUI()
    {
        return true;
    }

    public string GetConditionDescription()
    {
        return "Drops in the Tuhrtl Outpost";
    }
}
