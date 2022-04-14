using System;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Hooks;

public static class ExtraHealth
{
    public static void ILDrawLife(ILContext il)
    {
        var c = new ILCursor(il);

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

        c.Emit(OpCodes.Ldloc, 10);

        c.EmitDelegate<Func<Asset<Texture2D>, int, Asset<Texture2D>>>((sprite, index) =>
        {
            int crystalFruitCount = (Main.LocalPlayer.statLifeMax - 500) / 5;
            if (index - 1 < crystalFruitCount)
            {
                return ModContent.Request<Texture2D>($"{AvalonTesting.AssetPath}Textures/UI/Heart3");
            }

            return sprite;
        });
    }

    public static void ILLifeFillingDrawer(ILContext il)
    {
        var c = new ILCursor(il);

        FieldReference hpSegmentsCountField = null;

        if (!c.TryGotoNext(i => i.MatchLdarg(1)))
        {
            return;
        }

        if (!c.TryGotoNext(i => i.MatchLdarg(0)))
        {
            return;
        }

        if (!c.TryGotoNext(i => i.MatchLdfld(out hpSegmentsCountField)))
        {
            return;
        }

        if (!c.TryGotoNext(i => i.MatchBlt(out _)))
        {
            return;
        }

        if (!c.TryGotoNext(i => i.MatchStindRef()))
        {
            return;
        }

        c.Emit(OpCodes.Ldarg_1);
        c.Emit(OpCodes.Ldarg_0);
        c.Emit(OpCodes.Ldfld, hpSegmentsCountField);

        c.EmitDelegate<Func<Asset<Texture2D>, int, int, Asset<Texture2D>>>((sprite, elementIndex, hpSegmentsCount) =>
        {
            int crystalFruitSegments = (Main.LocalPlayer.statLifeMax - 500) / 5;
            if (elementIndex >= hpSegmentsCount - crystalFruitSegments)
            {
                return ModContent.Request<Texture2D>($"{AvalonTesting.AssetPath}Textures/UI/HP_Fill_Crystal");
            }

            return sprite;
        });
    }

    public static void ILHeartFillingDrawer(ILContext il)
    {
        var c = new ILCursor(il);

        if (!c.TryGotoNext(i => i.MatchBge(out _)))
        {
            return;
        }

        if (!c.TryGotoNext(i => i.MatchStindRef()))
        {
            return;
        }

        c.Emit(OpCodes.Ldarg_1);
        c.EmitDelegate<Func<Asset<Texture2D>, int, Asset<Texture2D>>>((sprite, elementIndex) =>
        {
            int crystalFruitCount = (Main.LocalPlayer.statLifeMax - 500) / 5;
            if (elementIndex < crystalFruitCount)
            {
                return ModContent.Request<Texture2D>($"{AvalonTesting.AssetPath}Textures/UI/FancyBlueHeart");
            }

            return sprite;
        });
    }
}
