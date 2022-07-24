using Avalon.Buffs.AdvancedBuffs;
using Avalon.Common;
using Avalon.Players;
using On.Terraria.GameContent.ItemDropRules;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Both)]
public class BuffEffects : ModHook
{
    protected override void Apply()
    {
        On.Terraria.Projectile.FishingCheck_RollDropLevels += OnFishingCheckRollDropLevels;
        CommonCode.DropItemForEachInteractingPlayerOnThePlayer += OnDropItemForEachInteractingPlayerOnThePlayer;
        On.Terraria.Player.AddBuff += OnAddBuff;
    }

    private static void OnAddBuff(On.Terraria.Player.orig_AddBuff orig, Player self, int type, int timeToAdd, bool quiet = true, bool foodHack = false)
    {
        for (int j = 0; j < 22; j++)
        {
            if (self.buffType[j] == type)
            {
                if (type == ModContent.BuffType<Buffs.StaminaDrain>())
                {
                    self.buffTime[j] += timeToAdd;
                    if (self.GetModPlayer<ExxoStaminaPlayer>().StaminaDrainStacks < 5)
                    {
                        self.GetModPlayer<ExxoStaminaPlayer>().StaminaDrainStacks++;
                    }
                    if (self.buffTime[j] > ExxoStaminaPlayer.StaminaDrainTime)
                    {
                        self.buffTime[j] = ExxoStaminaPlayer.StaminaDrainTime;
                        return;
                    }
                }
                else if (self.buffTime[j] < timeToAdd)
                {
                    self.buffTime[j] = timeToAdd;
                }
                if (type == ModContent.BuffType<Buffs.SkyBlessing>())
                {
                    self.buffTime[j] += timeToAdd;
                    if (self.GetModPlayer<ExxoBuffPlayer>().SkyStacks < 10)
                    {
                        self.GetModPlayer<ExxoBuffPlayer>().SkyStacks++;
                    }
                    else self.GetModPlayer<ExxoBuffPlayer>().SkyStacks = 10;
                    if (self.buffTime[j] > 60 * 7)
                    {
                        self.buffTime[j] = 60 * 7;
                        return;
                    }
                }
                else if (self.buffTime[j] < timeToAdd)
                {
                    self.buffTime[j] = timeToAdd;
                }
                if (type == ModContent.BuffType<Buffs.Reckoning>())
                {
                    self.buffTime[j] += timeToAdd;
                    if (self.GetModPlayer<ExxoPlayer>().reckoningLevel < 10)
                    {
                        self.GetModPlayer<ExxoPlayer>().reckoningLevel++;
                    }
                    else
                        self.GetModPlayer<ExxoPlayer>().reckoningLevel = 10;
                    if (self.buffTime[j] > 60 * 8)
                    {
                        self.buffTime[j] = 60 * 8;
                        return;
                    }
                }
                else if (self.buffTime[j] < timeToAdd)
                {
                    self.buffTime[j] = timeToAdd;
                }
                return;
            }
        }
        orig(self, type, timeToAdd, quiet, foodHack);
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
