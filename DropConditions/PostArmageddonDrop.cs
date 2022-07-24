using Avalon.Systems;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Avalon.DropConditions;

public class PostArmageddonDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return ModContent.GetInstance<AvalonTestingWorld>().SuperHardmode && ModContent.GetInstance<DownedBossSystem>().DownedArmageddon &&
               !ModContent.GetInstance<DownedBossSystem>().DownedMechasting;
    }

    public bool CanShowItemDropInUI()
    {
        return ModContent.GetInstance<AvalonTestingWorld>().SuperHardmode;
    }

    public string GetConditionDescription()
    {
        return "Drops after Armageddon Slime is defeated";
    }
}
