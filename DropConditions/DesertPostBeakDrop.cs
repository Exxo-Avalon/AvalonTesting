using AvalonTesting.Systems;
using Terraria.GameContent.ItemDropRules;

namespace AvalonTesting.DropConditions;
public class DesertPostBeakDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return DownedBossSystem.downedDesertBeak && info.player.ZoneDesert;
    }
    public bool CanShowItemDropInUI()
    {
        return true;
    }
    public string GetConditionDescription()
    {
        return "Drops in the Desert after Desert Beak is defeated";
    }
}

