using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Avalon.DropConditions;

public class HardmodePreSuperHardmode : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return CanShowItemDropInUI();
    }

    public bool CanShowItemDropInUI()
    {
        return Main.hardMode && !ModContent.GetInstance<AvalonTestingWorld>().SuperHardmode;
    }

    public string GetConditionDescription()
    {
        return null;
    }
}
