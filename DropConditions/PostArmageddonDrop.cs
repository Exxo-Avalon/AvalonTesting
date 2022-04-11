using AvalonTesting.Systems;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace AvalonTesting.DropConditions;

public class PostArmageddonDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return ModContent.GetInstance<AvalonTestingWorld>().SuperHardmode && ModContent.GetInstance<DownedBossSytem>().DownedArmageddon &&
               !ModContent.GetInstance<DownedBossSytem>().DownedMechasting;
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
