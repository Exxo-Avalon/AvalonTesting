using AvalonTesting.Systems;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace AvalonTesting.DropConditions;

public class DesertPostBeakDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return ModContent.GetInstance<DownedBossSytem>().DownedDesertBeak && info.player.ZoneDesert;
    }

    public bool CanShowItemDropInUI()
    {
        return ModContent.GetInstance<DownedBossSytem>().DownedDesertBeak;
    }

    public string GetConditionDescription()
    {
        return null;
    }
}
