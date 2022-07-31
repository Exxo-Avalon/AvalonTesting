using Avalon.Players;
using Avalon.Systems;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Avalon.DropConditions;

public class PostPhantasmHellcastleTokenDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return info.player.GetModPlayer<ExxoBiomePlayer>().ZoneHellcastle && NPC.downedMoonlord &&
               ModContent.GetInstance<DownedBossSystem>().DownedPhantasm;
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
