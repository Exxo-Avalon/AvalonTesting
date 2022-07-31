using Avalon.Systems;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Avalon.DropConditions;

public class PostMechastingTokenDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return ModContent.GetInstance<AvalonWorld>().SuperHardmode && ModContent.GetInstance<DownedBossSystem>().DownedMechasting;
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
