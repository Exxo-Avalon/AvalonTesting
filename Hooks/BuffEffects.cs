using AvalonTesting.Buffs.AdvancedBuffs;
using On.Terraria.GameContent.ItemDropRules;
using Terraria;
using Terraria.Utilities;

namespace AvalonTesting.Hooks;

public static class BuffEffects
{
    public static void OnFishingCheck_RollDropLevels(On.Terraria.Projectile.orig_FishingCheck_RollDropLevels orig,
                                                     Projectile self, int fishingLevel, out bool common,
                                                     out bool uncommon, out bool rare, out bool veryrare,
                                                     out bool legendary, out bool crate)
    {
        orig(self, fishingLevel, out common, out uncommon, out rare, out veryrare, out legendary, out crate);
        if (!crate && Main.player[self.owner].HasBuff<AdvCrate>() && Main.rand.NextFloat() < AdvCrate.Chance)
        {
            crate = true;
        }
    }

    public static void OnDropItemForEachInteractingPlayerOnThePlayer(
        CommonCode.orig_DropItemForEachInteractingPlayerOnThePlayer orig, NPC npc, int itemId, UnifiedRandom rng,
        int chanceNumerator, int chanceDenominator, int stack, bool interactionRequired)
    {
        if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].HasBuff<AdvLuck>())
        {
            chanceNumerator += (int)(chanceNumerator * AdvLuck.PercentIncrease);
        }

        orig(npc, itemId, rng, chanceNumerator, chanceDenominator, stack, interactionRequired);
    }
}
