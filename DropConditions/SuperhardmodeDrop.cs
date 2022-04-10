using AvalonTesting.Systems;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria;

namespace AvalonTesting.DropConditions;
public class SuperhardmodeDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return ModContent.GetInstance<AvalonTestingWorld>().SuperHardmode && Main.hardMode;
    }
    public bool CanShowItemDropInUI()
    {
        return true;
    }
    public string GetConditionDescription()
    {
        return "Drops during Superhardmode";
    }
}

