using AvalonTesting.Players;
using AvalonTesting.Systems;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace AvalonTesting.DropConditions;

public class PostPhantasmHellcastleDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return info.player.GetModPlayer<ExxoBiomePlayer>().ZoneHellcastle && NPC.downedMoonlord &&
               DownedBossSystem.downedPhantasm;
    }

    public bool CanShowItemDropInUI()
    {
        return NPC.downedMoonlord;
    }

    public string GetConditionDescription()
    {
        return "Drops in the Hellcastle";
    }
}
