using Avalon.Players;
using Avalon.Systems;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Avalon.DropConditions;

public class SoulofHumidityDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return (info.player.ZoneJungle || info.player.GetModPlayer<ExxoBiomePlayer>().ZoneTropics) &&
               ModContent.GetInstance<AvalonWorld>().SuperHardmode && ModContent.GetInstance<DownedBossSystem>().DownedArmageddon;
    }

    public bool CanShowItemDropInUI()
    {
        return false;
    }

    public string GetConditionDescription()
    {
        return "Drops in the Jungle or Tropics in Superhardmode after the Armageddon Slime is defeated";
    }
}
