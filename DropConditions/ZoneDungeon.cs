using Terraria.GameContent.ItemDropRules;

namespace AvalonTesting.DropConditions;

public class ZoneDungeon : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return info.player.ZoneDungeon;
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
