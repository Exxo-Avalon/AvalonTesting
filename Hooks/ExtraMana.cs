using System;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using On.Terraria.GameContent.UI.ResourceSets;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Hooks;

public static class ExtraMana
{
    public static void OnPlayerStatsSnapshotCtor(PlayerStatsSnapshot.orig_ctor orig,
                                                 ref Terraria.GameContent.UI.ResourceSets.PlayerStatsSnapshot self,
                                                 Player player)
    {
        orig(ref self, player);
        if (self.ManaMax > 400)
        {
            self.ManaPerSegment = self.ManaMax / 20f;
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
            if (Main.LocalPlayer.statManaMax2 > 400)
            {
                return Main.LocalPlayer.statManaMax2 / 20;
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
            int tier6ManaCount = (Main.player[Main.myPlayer].statManaMax2 - 2000) / 20;
            int tier5ManaCount = (Main.player[Main.myPlayer].statManaMax2 - 1600) / 20;
            int tier4ManaCount = (Main.player[Main.myPlayer].statManaMax2 - 1200) / 20;
            int tier3ManaCount = (Main.player[Main.myPlayer].statManaMax2 - 800) / 20;
            int tier2ManaCount = (Main.player[Main.myPlayer].statManaMax2 - 400) / 20;

            if (index - 1 < tier6ManaCount)
            {
                return ModContent.Request<Texture2D>($"{AvalonTesting.AssetPath}Textures/UI/Mana6");
            }

            if (index - 1 < tier5ManaCount)
            {
                return ModContent.Request<Texture2D>($"{AvalonTesting.AssetPath}Textures/UI/Mana5");
            }

            if (index - 1 < tier4ManaCount)
            {
                return ModContent.Request<Texture2D>($"{AvalonTesting.AssetPath}Textures/UI/Mana4");
            }

            if (index - 1 < tier3ManaCount)
            {
                return ModContent.Request<Texture2D>($"{AvalonTesting.AssetPath}Textures/UI/Mana3");
            }

            if (index - 1 < tier2ManaCount)
            {
                return ModContent.Request<Texture2D>($"{AvalonTesting.AssetPath}Textures/UI/Mana2");
            }

            return sprite;
        });
    }
}
