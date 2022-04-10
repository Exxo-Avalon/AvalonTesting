using AvalonTesting.Systems;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace AvalonTesting.DropConditions;
public class SoulofHumidityDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return (info.player.ZoneJungle || info.player.GetModPlayer<Players.ExxoBiomePlayer>().ZoneTropics) && ModContent.GetInstance<AvalonTestingWorld>().SuperHardmode && DownedBossSystem.stoppedArmageddon;
    }
    public bool CanShowItemDropInUI()
    {
        return true;
    }
    public string GetConditionDescription()
    {
        return "Drops in the Jungle or Tropics in Superhardmode after the Armageddon Slime is defeated";
    }
}

