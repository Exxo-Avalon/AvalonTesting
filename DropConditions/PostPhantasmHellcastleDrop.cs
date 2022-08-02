using Avalon.Players;
using Avalon.Systems;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Avalon.DropConditions;

public class PostPhantasmHellcastleDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return info.player.GetModPlayer<ExxoBiomePlayer>().ZoneHellcastle && NPC.downedMoonlord &&
               ModContent.GetInstance<DownedBossSystem>().DownedPhantasm && !info.IsInSimulation && info.npc.value > 0;
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
