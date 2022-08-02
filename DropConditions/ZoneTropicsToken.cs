using Terraria.GameContent.ItemDropRules;

namespace Avalon.DropConditions;
public class ZoneTropicsToken : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return info.player.GetModPlayer<Players.ExxoBiomePlayer>().ZoneTropics && info.npc.value > 0 && !info.IsInSimulation;
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

