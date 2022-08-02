using Avalon.Players;
using Avalon.Systems;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Avalon.DropConditions;

public class CloverPotionActive : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        if (info.npc.lastInteraction != -1)
        {
            return Main.player[info.npc.lastInteraction].GetModPlayer<ExxoBuffPlayer>().Lucky;
        }
        return false;
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
