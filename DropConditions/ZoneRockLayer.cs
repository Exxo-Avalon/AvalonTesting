using Terraria.GameContent.ItemDropRules;

namespace AvalonTesting.DropConditions;

public class ZoneRockLayer : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return info.player.ZoneRockLayerHeight;
    }

    public bool CanShowItemDropInUI()
    {
        return true;
    }

    public string GetConditionDescription()
    {
        return "Drops in the underground";
    }
}
