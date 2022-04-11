using AvalonTesting.Players;
using AvalonTesting.Systems;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace AvalonTesting.DropConditions;

public class PostPhantasmHellcastleDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return info.player.GetModPlayer<ExxoBiomePlayer>().ZoneHellcastle && NPC.downedMoonlord &&
               ModContent.GetInstance<DownedBossSystem>().DownedPhantasm;
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
