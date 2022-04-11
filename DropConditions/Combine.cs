using System.Linq;
using Terraria.GameContent.ItemDropRules;

namespace AvalonTesting.DropConditions;

public class Combine : IItemDropRuleCondition, IProvideItemConditionDescription
{
    private readonly bool andConditions;
    private readonly IItemDropRuleCondition[] conditions;
    private readonly string description;

    public Combine(bool andConditions = true, string description = null,
                   params IItemDropRuleCondition[] dropRuleConditions)
    {
        this.andConditions = andConditions;
        this.description = description;
        conditions = dropRuleConditions;
    }

    public bool CanDrop(DropAttemptInfo info)
    {
        return andConditions ? conditions.All(val => val.CanDrop(info)) : conditions.Any(val => val.CanDrop(info));
    }

    public bool CanShowItemDropInUI()
    {
        return andConditions
            ? conditions.All(val => val.CanShowItemDropInUI())
            : conditions.Any(val => val.CanShowItemDropInUI());
    }

    public string GetConditionDescription()
    {
        return description;
    }
}
