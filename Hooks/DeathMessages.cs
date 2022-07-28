using System;
using Avalon.Common;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Both)]
public class DeathMessages : ModHook
{
    protected override void Apply() => IL.Terraria.Lang.CreateDeathMessage += BlahBald;

    private void BlahBald(ILContext il)
    {
        ILCursor c = new(il);

        try
        {
            // lazy to properly document... so uh,

            c.GotoNext(i => i.MatchLdstr("DeathTextGeneric"))
                .GotoNext(i => i.MatchStloc(8));

            c.Index++;
            c.Emit(OpCodes.Ldloc, 8)
                .Emit(OpCodes.Ldarg, 1);
            c.EmitDelegate<Func<NetworkText, string, NetworkText>>((text, deadPlayerName) =>
            {
                if (Main.rand.NextBool(4))
                {
                    return Main.rand.Next(4) switch
                    {
                        3 => NetworkText.FromLiteral($"{deadPlayerName}.exe has stopped working. Program was closed"),
                        2 => NetworkText.FromLiteral($"{deadPlayerName} became a tombstone dispenser"),
                        1 => NetworkText.FromLiteral($"{deadPlayerName} was reduced to a fine paste"),
                        _ => NetworkText.FromLiteral($"{deadPlayerName} was tickled to smithereens"),
                    };
                }
                return text;
            });
            c.Emit(OpCodes.Stloc, 8)
                .GotoNext(i => i.MatchLdstr("DeathText.Fell_"))
                .GotoNext(i => i.MatchLdcI4(2));

            c.Index++;
            c.Emit(OpCodes.Pop);
            c.EmitDelegate(() => 9);
            c.GotoNext(i => i.MatchStloc(7));

            c.Index++;
            c.Emit(OpCodes.Ldloc, 7)
                .Emit(OpCodes.Ldarg, 1)
                .Emit(OpCodes.Ldloc, 9);
            c.EmitDelegate<Func<NetworkText, string, int, NetworkText>>((text, deadPlayerName, num) =>
            {
                if (num > 2)
                {
                    return (num - 3) switch
                    {
                        6 => NetworkText.FromLiteral($"{deadPlayerName} got splattered all over {Main.worldName}"),
                        5 => NetworkText.FromLiteral($"{deadPlayerName}'s parachute was just a backpack."),
                        4 => NetworkText.FromLiteral($"{deadPlayerName} is literally six feet under."),
                        3 => NetworkText.FromLiteral($"{deadPlayerName} dreamt of flying."),
                        2 => NetworkText.FromLiteral($"{deadPlayerName}, gravity sucks, y'know?"),
                        1 => NetworkText.FromLiteral($"{deadPlayerName} took a leap of faith..."),
                        _ => NetworkText.FromLiteral($"{deadPlayerName} made a satisfying SPLAT when they hit the ground."),
                    };
                }
                return text;
            });
            c.Emit(OpCodes.Stloc, 7)
                .GotoNext(i => i.MatchLdstr("DeathText.Drowned_"))
                .GotoNext(i => i.MatchLdcI4(4));

            c.Index++;
            c.Emit(OpCodes.Pop);
            c.EmitDelegate(() => 7);
            c.GotoNext(i => i.MatchStloc(7));

            c.Index++;
            c.Emit(OpCodes.Ldloc, 7)
                .Emit(OpCodes.Ldarg, 1)
                .Emit(OpCodes.Ldloc, 9);
            c.EmitDelegate<Func<NetworkText, string, int, NetworkText>>((text, deadPlayerName, num) =>
            {
                if (num > 4)
                {
                    return (num - 5) switch
                    {
                        2 => NetworkText.FromLiteral($"{deadPlayerName} blub blub blub..."),
                        1 => NetworkText.FromLiteral($"{deadPlayerName} thought they could breathe water."),
                        _ => NetworkText.FromLiteral($"{deadPlayerName} asphyxiated."),
                    };
                }
                return text;
            });
            c.Emit(OpCodes.Stloc, 7)
                .GotoNext(i => i.MatchLdstr("DeathText.Lava_"))
                .GotoNext(i => i.MatchLdcI4(4));

            c.Index++;
            c.Emit(OpCodes.Pop);
            c.EmitDelegate(() => 7);
            c.GotoNext(i => i.MatchStloc(7));

            c.Index++;
            c.Emit(OpCodes.Ldloc, 7)
                .Emit(OpCodes.Ldarg, 1)
                .Emit(OpCodes.Ldloc, 9);
            c.EmitDelegate<Func<NetworkText, string, int, NetworkText>>((text, deadPlayerName, num) =>
            {
                if (num > 4)
                {
                    return (num - 5) switch
                    {
                        2 => NetworkText.FromLiteral($"{deadPlayerName} took a bath in a lake of fire."),
                        1 => NetworkText.FromLiteral($"{deadPlayerName} became a charcoal briquette."),
                        _ => NetworkText.FromLiteral($"{deadPlayerName} is HOT HOT HOT."),
                    };
                }
                return text;
            });
            c.Emit(OpCodes.Stloc, 7)
                .GotoNext(i => i.MatchLdstr("DeathText.Petrified_"))
                .GotoNext(i => i.MatchLdcI4(4));

            c.Index++;
            c.Emit(OpCodes.Pop);
            c.EmitDelegate(() => 5);
            c.GotoNext(i => i.MatchStloc(7));

            c.Index++;
            c.Emit(OpCodes.Ldloc, 7)
                .Emit(OpCodes.Ldarg, 1)
                .Emit(OpCodes.Ldloc, 9);
            c.EmitDelegate<Func<NetworkText, string, int, NetworkText>>((text, deadPlayerName, num) =>
            {
                if (num > 4)
                {
                    return (num - 5) switch
                    {
                        _ => NetworkText.FromLiteral($"{deadPlayerName} was stoned."),
                    };
                }
                return text;
            });
            c.Emit(OpCodes.Stloc, 7)
                .GotoNext(i => i.MatchLdstr("DeathText.Electrocuted"))
                .GotoNext(i => i.MatchStloc(7));

            c.Index++;
            c.Emit(OpCodes.Ldloc, 7)
                .Emit(OpCodes.Ldarg, 1);
            c.EmitDelegate<Func<NetworkText, string, NetworkText>>((text, deadPlayerName) =>
            {
                int num = Main.rand.Next(3);
                if (num != 0)
                {
                    return (num - 1) switch
                    {
                        1 => NetworkText.FromLiteral($"{deadPlayerName} had an electrifying personality."),
                        _ => NetworkText.FromLiteral($"{deadPlayerName} got zzzzapped."),
                    };
                }
                return text;
            });
        }
        catch (Exception e)
        {
            Avalon.Mod.Logger.Error($"[Death Messages IL Error]\n{e.Message}\n{e.StackTrace}");
        }
    }
}
