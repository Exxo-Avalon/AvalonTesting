using System;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.ResourceSets;

namespace AvalonTesting.Hooks;

public static class ExtraMana
{
    private const int MaxManaCrystalsToDisplay = 10;
    private const int ManaPerCrystal = 20;
    private const int MaxManaToDisplay = MaxManaCrystalsToDisplay * ManaPerCrystal;
    private const int TopManaTier = 6;
    private const int LowManaTier = 2;

    private static readonly Func<HorizontalBarsPlayerReosurcesDisplaySet, int> GetMPSegmentsCount =
        Utilities.CreateInstancePropertyOrFieldReaderDelegate<HorizontalBarsPlayerReosurcesDisplaySet, int>(
            "_mpSegmentsCount");

    public static void OnPlayerStatsSnapshotCtor(
        On.Terraria.GameContent.UI.ResourceSets.PlayerStatsSnapshot.orig_ctor orig,
        ref PlayerStatsSnapshot self,
        Player player)
    {
        orig(ref self, player);
        if (self.ManaMax > MaxManaToDisplay)
        {
            self.ManaPerSegment = self.ManaMax / (float)MaxManaCrystalsToDisplay;
        }
    }

    public static void ILClassicDrawMana(ILContext il)
    {
        var c = new ILCursor(il);

        if (!c.TryGotoNext(i => i.MatchLdcI4(20)))
        {
            return;
        }

        if (!c.TryGotoNext(i => i.MatchStfld(out _)))
        {
            return;
        }

        c.EmitDelegate<Func<int, int>>(val =>
        {
            if (Main.LocalPlayer.statManaMax2 > MaxManaToDisplay)
            {
                return Main.LocalPlayer.statManaMax2 / MaxManaCrystalsToDisplay;
            }

            return val;
        });

        if (!c.TryGotoNext(i => i.MatchLdcR8(0.9)))
        {
            return;
        }

        if (!c.TryGotoNext(i => i.MatchLdsfld(out _)))
        {
            return;
        }

        if (!c.TryGotoNext(i => i.MatchCallvirt(out _)))
        {
            return;
        }

        c.Emit(OpCodes.Ldloc, 6);

        c.EmitDelegate<Func<Asset<Texture2D>, int, Asset<Texture2D>>>((sprite, index) =>
        {
            for (int i = TopManaTier; i >= LowManaTier; i--)
            {
                if (index - 1 < (Main.LocalPlayer.statManaMax2 - (MaxManaToDisplay * (i - 1))) / ManaPerCrystal)
                {
                    return AvalonTesting.Mod.Assets.Request<Texture2D>($"{AvalonTesting.TextureAssetsPath}/UI/Mana{i}");
                }
            }

            return sprite;
        });
    }

    public static void ILStarFillingDrawer(ILContext il)
    {
        var c = new ILCursor(il);

        if (!c.TryGotoNext(i => i.MatchLdfld(out _)))
        {
            return;
        }

        if (!c.TryGotoNext(i => i.MatchStindRef()))
        {
            return;
        }

        c.Emit(OpCodes.Ldarg, 1);

        c.EmitDelegate<Func<Asset<Texture2D>, int, Asset<Texture2D>>>((sprite, index) =>
        {
            for (int i = TopManaTier; i >= LowManaTier; i--)
            {
                if (index < (Main.LocalPlayer.statManaMax2 - (MaxManaToDisplay * (i - 1))) / ManaPerCrystal)
                {
                    return AvalonTesting.Mod.Assets.Request<Texture2D>(
                        $"{AvalonTesting.TextureAssetsPath}/UI/FancyMana{i}");
                }
            }

            return sprite;
        });
    }

    public static void ILManaFillingDrawer(ILContext il)
    {
        var c = new ILCursor(il);

        if (!c.TryGotoNext(i => i.MatchLdfld(out _)))
        {
            return;
        }

        if (!c.TryGotoNext(i => i.MatchStindRef()))
        {
            return;
        }

        c.Emit(OpCodes.Ldarg, 1);
        c.Emit(OpCodes.Ldarg_0);

        c.EmitDelegate<Func<Asset<Texture2D>, int, HorizontalBarsPlayerReosurcesDisplaySet, Asset<Texture2D>>>(
            (sprite, index, self) =>
            {
                int mpSegmentsCount = GetMPSegmentsCount(self);
                for (int i = TopManaTier; i >= LowManaTier; i--)
                {
                    if (index >= mpSegmentsCount -
                        ((Main.LocalPlayer.statManaMax2 - (MaxManaToDisplay * (i - 1))) / ManaPerCrystal))
                    {
                        return AvalonTesting.Mod.Assets.Request<Texture2D>(
                            $"{AvalonTesting.TextureAssetsPath}/UI/BarMana{i}");
                    }
                }

                return sprite;
            });
    }
}
