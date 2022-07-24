using Avalon.Systems;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Avalon.DropConditions;

public class PostMechastingDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return ModContent.GetInstance<AvalonWorld>().SuperHardmode && ModContent.GetInstance<DownedBossSystem>().DownedMechasting;
    }

    public bool CanShowItemDropInUI()
    {
        return ModContent.GetInstance<AvalonWorld>().SuperHardmode;
    }

    public string GetConditionDescription()
    {
        return "Drops after Mechasting is defeated";
    }
}
