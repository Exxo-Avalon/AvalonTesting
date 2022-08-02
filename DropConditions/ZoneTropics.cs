using Terraria.GameContent.ItemDropRules;

namespace Avalon.DropConditions;
public class ZoneTropics : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return info.player.GetModPlayer<Players.ExxoBiomePlayer>().ZoneTropics && !info.IsInSimulation && info.npc.value > 0;
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

