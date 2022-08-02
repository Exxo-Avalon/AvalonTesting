using Terraria.GameContent.ItemDropRules;

namespace Avalon.DropConditions;

public class ZoneDungeon : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return info.player.ZoneDungeon && !info.IsInSimulation && info.npc.value > 0;
    }

    public bool CanShowItemDropInUI()
    {
        return true;
    }

    public string GetConditionDescription()
    {
        return "Drops in the dungeon";
    }
}
