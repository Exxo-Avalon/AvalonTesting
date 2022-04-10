using Terraria.GameContent.ItemDropRules;

namespace AvalonTesting.DropConditions;
public class DungeonDrop : IItemDropRuleCondition, IProvideItemConditionDescription
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
        return "Drops in the Dungeon in Hardmode";
    }
}

