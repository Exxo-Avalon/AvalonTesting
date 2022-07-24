using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Avalon.DropConditions;

public class Superhardmode : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return CanShowItemDropInUI();
    }

    public bool CanShowItemDropInUI()
    {
        return ModContent.GetInstance<AvalonWorld>().SuperHardmode;
    }

    public string GetConditionDescription()
    {
        return null;
    }
}
