using AvalonTesting.Systems;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace AvalonTesting.DropConditions;

public class PostMechastingDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return ModContent.GetInstance<AvalonTestingWorld>().SuperHardmode && DownedBossSystem.downedMechasting;
    }

    public bool CanShowItemDropInUI()
    {
        return ModContent.GetInstance<AvalonTestingWorld>().SuperHardmode;
    }

    public string GetConditionDescription()
    {
        return "Drops after Mechasting is defeated";
    }
}
