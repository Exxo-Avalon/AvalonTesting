using AvalonTesting.Buffs.AdvancedBuffs;
using AvalonTesting.Common;
using AvalonTesting.Players;
using On.Terraria.GameContent.ItemDropRules;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AvalonTesting.Hooks;

[Autoload(Side = ModSide.Both)]
public class BuffEffects : ModHook
{
    protected override void Apply()
    {
        On.Terraria.Projectile.FishingCheck_RollDropLevels += OnFishingCheckRollDropLevels;
        CommonCode.DropItemForEachInteractingPlayerOnThePlayer += OnDropItemForEachInteractingPlayerOnThePlayer;
    }

    private static void OnFishingCheckRollDropLevels(
        On.Terraria.Projectile.orig_FishingCheck_RollDropLevels orig,
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

    private static void OnDropItemForEachInteractingPlayerOnThePlayer(
        CommonCode.orig_DropItemForEachInteractingPlayerOnThePlayer orig, NPC npc, int itemId, UnifiedRandom rng,
        int chanceNumerator, int chanceDenominator, int stack, bool interactionRequired)
    {
        if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<ExxoBuffPlayer>().Lucky)
        {
            chanceNumerator += (int)(chanceNumerator * AdvLuck.PercentIncrease);
        }

        orig(npc, itemId, rng, chanceNumerator, chanceDenominator, stack, interactionRequired);
    }
}
