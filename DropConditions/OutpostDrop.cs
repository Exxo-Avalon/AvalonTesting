using Terraria.GameContent.ItemDropRules;
using Terraria;

namespace AvalonTesting.DropConditions;
public class OutpostDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return Main.hardMode && info.player.GetModPlayer<Players.ExxoBiomePlayer>().ZoneTuhrtlOutpost;
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

