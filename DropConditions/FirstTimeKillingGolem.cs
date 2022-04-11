using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace AvalonTesting.DropConditions;

public class FirstTimeKillingGolem : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return !NPC.downedGolemBoss;
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
