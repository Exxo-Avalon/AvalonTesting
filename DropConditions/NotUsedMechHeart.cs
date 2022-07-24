using Avalon.Players;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace Avalon.DropConditions;

public class NotUsedMechHeart :
    IItemDropRuleCondition,
    IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return !info.player.GetModPlayer<ExxoPlayer>().shmAcc;
    }

    public bool CanShowItemDropInUI()
    {
        return true;
    }

    public string GetConditionDescription()
    {
        return null;
    }
}
