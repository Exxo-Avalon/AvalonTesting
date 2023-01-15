using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Avalon.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public abstract class ExxoPrefix : ModPrefix
{
    public static readonly Dictionary<ExxoPrefixCategory, List<ExxoPrefix>> ExxoCategoryPrefixes = new();

    private static readonly Dictionary<DamageClass, string> DamageClassesToCheck = new()
    {
        { DamageClass.Generic, string.Empty },
        { DamageClass.Magic, "magic " },
        { DamageClass.Melee, "melee " },
        { DamageClass.Ranged, "ranged " },
        { DamageClass.Summon, "summon " },
        { DamageClass.Throwing, "throwing " },
    };

    private readonly Action<Player> cachedSetupPlayerAction = (Action<Player>)typeof(PlayerLoader)
        .GetMethod("SetupPlayer", BindingFlags.NonPublic | BindingFlags.Static)!
        .CreateDelegate(typeof(Action<Player>), null);

    public virtual ExxoPrefixCategory ExxoCategory => ExxoPrefixCategory.None;

    [NotNull] public ReadOnlyCollection<TooltipLine>? TooltipLines { get; private set; }

    public override void SetStaticDefaults()
    {
        if (!ExxoCategoryPrefixes.ContainsKey(ExxoCategory))
        {
            ExxoCategoryPrefixes[ExxoCategory] = new List<ExxoPrefix>();
        }

        ExxoCategoryPrefixes[ExxoCategory].Add(this);
        CacheToolTips();
    }

    public virtual void UpdateOwnerPlayer(Player player)
    {
    }

    public void CacheToolTips()
    {
        var lines = new List<TooltipLine>();

        var origPlayer = new Player();
        cachedSetupPlayerAction.Invoke(origPlayer);

        var modifiedPlayer = new Player();
        cachedSetupPlayerAction.Invoke(modifiedPlayer);

        float damageMult = 1f, knockbackMult = 1f, useTimeMult = 1f, scaleMult = 1f, shootSpeedMult = 1f, manaMult = 1f;
        int critBonus = 0;

        UpdateOwnerPlayer(modifiedPlayer);
        SetStats(ref damageMult, ref knockbackMult, ref useTimeMult, ref scaleMult, ref shootSpeedMult, ref manaMult,
            ref critBonus);

        damageMult--;
        knockbackMult--;
        useTimeMult--;
        scaleMult--;
        shootSpeedMult--;
        manaMult--;

        float critDamageDiff = modifiedPlayer.GetModPlayer<ExxoPlayer>().CritDamageMult -
                               origPlayer.GetModPlayer<ExxoPlayer>().CritDamageMult;

        BasicDifference(lines, "PrefixDamage", "damage", damageMult);
        BasicDifference(lines, "PrefixSize", "size", scaleMult);
        BasicDifference(lines, "PrefixKnockback", "knockback", knockbackMult);
        BasicDifference(lines, "PrefixSpeed", "speed", useTimeMult);
        BasicDifference(lines, "PrefixShootSpeed", "velocity", shootSpeedMult);
        BasicDifference(lines, "PrefixUseMana", "mana cost", manaMult, true);
        BasicDifference(lines, "PrefixCritBonus", "critical damage", critBonus);

        StatModifierDifference(lines, "PrefixAccDamage", "damage", origPlayer, modifiedPlayer,
            (player, damageClass) => player.GetDamage(damageClass).Additive);
        StatModifierDifference(lines, "PrefixAccAttackSpeed", "attack speed", origPlayer, modifiedPlayer,
            (player, damageClass) => player.GetAttackSpeed(damageClass));
        StatModifierDifference(lines, "PrefixAccKnockback", "knockback", origPlayer, modifiedPlayer,
            (player, damageClass) => player.GetKnockback(damageClass).Additive);
        StatModifierDifference(lines, "PrefixAccCritChance", "critical strike chance", origPlayer, modifiedPlayer,
            (player, damageClass) => player.GetCritChance(damageClass) / 100f);
        BasicDifference(lines, "PrefixAccArmorPen", "armor penetration", (int)(modifiedPlayer.GetArmorPenetration(DamageClass.Generic) - origPlayer.GetArmorPenetration(DamageClass.Generic)), percentage: false);

        BasicDifference(lines, "PrefixAccEndurance", "damage taken", -(modifiedPlayer.endurance - origPlayer.endurance),
            true);
        BasicDifference(lines, "PrefixAccMaxMana", "mana", modifiedPlayer.statManaMax2 - origPlayer.statManaMax2);
        BasicDifference(lines, "PrefixAccMoveSpeed", "movement speed", modifiedPlayer.moveSpeed - origPlayer.moveSpeed);
        BasicDifference(lines, "PrefixAccDefense", "defense", modifiedPlayer.statDefense - origPlayer.statDefense);
        BasicDifference(lines, "PrefixAccCritDamage", "critical strike damage", critDamageDiff);
        BasicDifference(lines, "PrefixAccBlockRange", "block placement range",
            modifiedPlayer.blockRange - origPlayer.blockRange);
        BasicDifference(lines, "PrefixAccTileSpeed", "tile placement speed",
            modifiedPlayer.tileSpeed - origPlayer.tileSpeed);
        BasicDifference(lines, "PrefixAccWallSpeed", "wall placement speed",
            modifiedPlayer.wallSpeed - origPlayer.wallSpeed);
        BasicDifference(lines, "PrefixAccIgnoreWater", "Free movement in liquids", modifiedPlayer.ignoreWater);
        BasicDifference(lines, "PrefixAccStinky", "You smell awful", modifiedPlayer.stinky, true);

        foreach (TooltipLine line in lines)
        {
            line.IsModifier = true;
        }

        TooltipLines = new ReadOnlyCollection<TooltipLine>(lines);
    }

    private void StatModifierDifference(ICollection<TooltipLine> list, string prefix, string identifier,
                                        Player origPlayer,
                                        Player modifiedPlayer, Func<Player, DamageClass, float> valueGetter)
    {
        foreach (DamageClass damageClass in DamageClassesToCheck.Keys)
        {
            BasicDifference(list, prefix, DamageClassesToCheck[damageClass] + identifier,
                valueGetter(modifiedPlayer, damageClass) - valueGetter(origPlayer, damageClass));
        }
    }

    private void BasicDifference(ICollection<TooltipLine> list, string prefix, string identifier, int value,
                                 bool inverted = false, bool percentage = false)
    {
        if (Math.Abs(value) > 0)
        {
            list.Add(new TooltipLine(Mod, prefix, $"{value:+#;-#;+0}{(percentage ? "%" : string.Empty)} {identifier}")
            {
                IsModifier = true, IsModifierBad = !inverted ? value < 0 : value >= 0,
            });
        }
    }

    private void BasicDifference(ICollection<TooltipLine> list, string prefix, string identifier, float value,
                                 bool inverted = false)
    {
        if (Math.Abs(value) > float.Epsilon)
        {
            list.Add(new TooltipLine(Mod, prefix, $"{value * 100:+#;-#;+0;n0}% {identifier}")
            {
                IsModifier = true, IsModifierBad = !inverted ? value < 0 : value >= 0,
            });
        }
    }

    private void BasicDifference(ICollection<TooltipLine> list, string prefix, string identifier, bool value,
                                 bool isBad = false)
    {
        if (value)
        {
            list.Add(new TooltipLine(Mod, prefix, identifier) { IsModifier = true, IsModifierBad = isBad });
        }
    }
}

public enum ExxoPrefixCategory
{
    None = 0,
    Armor = 1,
}
