using Terraria.GameContent.ItemDropRules;

namespace Avalon.DropConditions;
public class ZoneTropicsToken : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return info.player.GetModPlayer<Players.ExxoBiomePlayer>().ZoneTropics;
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

