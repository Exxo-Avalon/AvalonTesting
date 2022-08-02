using Avalon.Players;
using Terraria.GameContent.ItemDropRules;

namespace Avalon.DropConditions;

public class OutpostDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return info.player.GetModPlayer<ExxoBiomePlayer>().ZoneTuhrtlOutpost && info.npc.value > 0 && !info.IsInSimulation;
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
