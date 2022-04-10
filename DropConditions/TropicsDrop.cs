using Terraria.GameContent.ItemDropRules;

namespace AvalonTesting.DropConditions;
public class TropicsDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return info.player.GetModPlayer<Players.ExxoBiomePlayer>().ZoneTropics;
    }
    public bool CanShowItemDropInUI()
    {
        return true;
    }
    public string GetConditionDescription()
    {
        return "Drops in the Tropics";
    }
}

