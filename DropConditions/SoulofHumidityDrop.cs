using AvalonTesting.Players;
using AvalonTesting.Systems;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace AvalonTesting.DropConditions;

public class SoulofHumidityDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return (info.player.ZoneJungle || info.player.GetModPlayer<ExxoBiomePlayer>().ZoneTropics) &&
               ModContent.GetInstance<AvalonTestingWorld>().SuperHardmode && ModContent.GetInstance<DownedBossSytem>().DownedArmageddon;
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
