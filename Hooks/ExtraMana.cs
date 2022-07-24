using System;
using Avalon.Common;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.ResourceSets;
using Terraria.ModLoader;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Client)]
public class ExtraMana : ModHook
{
    private const int LowManaTier = 2;
    private const int ManaPerCrystal = 20;
    private const int MaxManaCrystalsToDisplay = 10;
    private const int MaxManaToDisplay = MaxManaCrystalsToDisplay * ManaPerCrystal;
    private const int TopManaTier = 6;

    private static readonly Func<HorizontalBarsPlayerReosurcesDisplaySet, int> GetMPSegmentsCount =
        Utilities.CreateInstancePropertyOrFieldReaderDelegate<HorizontalBarsPlayerReosurcesDisplaySet, int>(
            "_mpSegmentsCount");

    protected override void Apply()
    {
        On.Terraria.GameContent.UI.ResourceSets.PlayerStatsSnapshot.ctor += OnPlayerStatsSnapshotCtor;
        IL.Terraria.GameContent.UI.ResourceSets.ClassicPlayerResourcesDisplaySet.DrawMana += ILClassicDrawMana;
        IL.Terraria.GameContent.UI.ResourceSets.FancyClassicPlayerResourcesDisplaySet.StarFillingDrawer +=
            ILStarFillingDrawer;
        IL.Terraria.GameContent.UI.ResourceSets.HorizontalBarsPlayerReosurcesDisplaySet.ManaFillingDrawer +=
            ILManaFillingDrawer;
    }

    private static void OnPlayerStatsSnapshotCtor(
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

    private static void ILClassicDrawMana(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(i => i.MatchLdcI4(20))
            .GotoNext(i => i.MatchStfld(out _));

        c.EmitDelegate<Func<int, int>>(val =>
        {
            if (Main.LocalPlayer.statManaMax2 > MaxManaToDisplay)
            {
                return Main.LocalPlayer.statManaMax2 / MaxManaCrystalsToDisplay;
            }

            return val;
        });

        c.GotoNext(i => i.MatchLdcR8(0.9))
            .GotoNext(i => i.MatchLdsfld(out _))
            .GotoNext(i => i.MatchCallvirt(out _))
            .Emit(OpCodes.Ldloc, 6);

        c.EmitDelegate<Func<Asset<Texture2D>, int, Asset<Texture2D>>>((sprite, index) =>
        {
            for (int i = TopManaTier; i >= LowManaTier; i--)
            {
                if (index - 1 < (Main.LocalPlayer.statManaMax2 - (MaxManaToDisplay * (i - 1))) / ManaPerCrystal)
                {
                    return Avalon.Mod.Assets.Request<Texture2D>($"{Avalon.TextureAssetsPath}/UI/Mana{i}");
                }
            }

            return sprite;
        });
    }

    private static void ILStarFillingDrawer(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(i => i.MatchLdfld(out _))
            .GotoNext(i => i.MatchStindRef())
            .Emit(OpCodes.Ldarg, 1);

        c.EmitDelegate<Func<Asset<Texture2D>, int, Asset<Texture2D>>>((sprite, index) =>
        {
            for (int i = TopManaTier; i >= LowManaTier; i--)
            {
                if (index < (Main.LocalPlayer.statManaMax2 - (MaxManaToDisplay * (i - 1))) / ManaPerCrystal)
                {
                    return Avalon.Mod.Assets.Request<Texture2D>(
                        $"{Avalon.TextureAssetsPath}/UI/FancyMana{i}");
                }
            }

            return sprite;
        });
    }

    private static void ILManaFillingDrawer(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(i => i.MatchLdfld(out _))
            .GotoNext(i => i.MatchStindRef())
            .Emit(OpCodes.Ldarg, 1)
            .Emit(OpCodes.Ldarg_0);

        c.EmitDelegate<Func<Asset<Texture2D>, int, HorizontalBarsPlayerReosurcesDisplaySet, Asset<Texture2D>>>(
            (sprite, index, self) =>
            {
                int mpSegmentsCount = GetMPSegmentsCount(self);
                for (int i = TopManaTier; i >= LowManaTier; i--)
                {
                    if (index >= mpSegmentsCount -
                        ((Main.LocalPlayer.statManaMax2 - (MaxManaToDisplay * (i - 1))) / ManaPerCrystal))
                    {
                        return Avalon.Mod.Assets.Request<Texture2D>(
                            $"{Avalon.TextureAssetsPath}/UI/BarMana{i}");
                    }
                }

                return sprite;
            });
    }
}
