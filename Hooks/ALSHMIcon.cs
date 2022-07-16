using System;
using System.Collections.Generic;
using System.Reflection;
using AvalonTesting.Common;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour.HookGen;
using Terraria.IO;
using Terraria.ModLoader;

namespace AvalonTesting.Hooks;

[Autoload(Side = ModSide.Client)]
public class ALSHMIcon : ModHook
{
    private Mod mod;

    private MethodInfo worldIcons;
    private MethodInfo drunkIcons;

    private event ILContext.Manipulator ModifyWorldIcons
    {
        add => HookEndpointManager.Modify(worldIcons, value);
        remove => HookEndpointManager.Unmodify(worldIcons, value);
    }

    private event ILContext.Manipulator ModifyDrunkIcons
    {
        add => HookEndpointManager.Modify(drunkIcons, value);
        remove => HookEndpointManager.Unmodify(drunkIcons, value);
    }

    private const string SHMPath = "AvalonTesting/Assets/WorldIcons/SuperHardmode";
    private Func<WorldFileData, bool> sHMCondition;

    protected override void Apply()
    {
        // always true
        this.mod = ModLoader.TryGetMod("AltLibrary", out Mod mod) ? mod : null;

        worldIcons = this.mod?.GetType()?.Assembly?.GetType("AltLibrary.Common.Hooks.WorldIcons")?.GetMethods(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)[5];
        drunkIcons = this.mod?.GetType()?.Assembly?.GetType("AltLibrary.Common.Hooks.WorldIcons")?.GetMethods(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)[3];

        sHMCondition = (d) => d.IsHardMode;

        ModifyWorldIcons += NormalSHM;
        ModifyDrunkIcons += DrunkSHM;
    }

    private void NormalSHM(ILContext il)
    {
        ILCursor c = new(il);
        try
        {
            c.GotoNext(MoveType.After,
                i => i.MatchStloc(1),
                i => i.MatchNop())
                .Emit(OpCodes.Ldloc, 1);
            c.EmitDelegate<Func<Dictionary<string, Func<WorldFileData, bool>>, Dictionary<string, Func<WorldFileData, bool>>>>((value) =>
            {
                value.Add("Terraria/AvalonTesting/SuperHardmode", sHMCondition);
                return value;
            });
            c.Emit(OpCodes.Stloc, 1)
                .GotoNext(i => i.MatchStloc(11))
                .GotoNext(i => i.MatchStloc(11))
                .GotoPrev(MoveType.After, i => i.MatchLdloca(10));

            c.Index++;

            ILLabel ifStart = c.DefineLabel();
            ILLabel ifEnd = c.DefineLabel();

            // if (...)
            c.EmitDelegate<Func<string, bool>>((v) => v == "Terraria/AvalonTesting/SuperHardmode");
            c.Emit(OpCodes.Brfalse_S, ifStart);

            // {

            c.EmitDelegate(() => SHMPath);
            c.Emit(OpCodes.Stloc, 11)
                .Emit(OpCodes.Br, ifEnd);

            // } else

            c.MarkLabel(ifStart);

            c.Emit(OpCodes.Ldloca, 10)
                .Emit(OpCodes.Call, typeof(KeyValuePair<string, object>).GetProperty(nameof(KeyValuePair<string, object>.Key), BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).GetMethod)
                .GotoNext(i => i.MatchNop());

            c.MarkLabel(ifEnd);
        }
        catch (Exception e)
        {
            Mod.Logger.Error("Normal SHM IL Error: " + e.Message + " " + e.StackTrace);
        }
    }

    private void DrunkSHM(ILContext il)
    {
        ILCursor c = new(il);
        try
        {
            c.GotoNext(MoveType.After,
                i => i.MatchStloc(17))
                .GotoPrev(i => i.MatchNop(),
                i => i.MatchNop());
            c.Index++;
            c.Emit(OpCodes.Ldloc, 1);
            c.EmitDelegate<Func<Dictionary<string, Func<WorldFileData, bool>>, Dictionary<string, Func<WorldFileData, bool>>>>((value) =>
            {
                value.Add("Terraria/AvalonTesting/SuperHardmode", sHMCondition);
                return value;
            });
            c.Emit(OpCodes.Stloc, 1)
                .GotoNext(i => i.MatchStloc(24))
                .GotoNext(i => i.MatchStloc(24))
                .GotoPrev(MoveType.After, i => i.MatchLdloca(23));

            c.Index++;

            ILLabel ifStart = c.DefineLabel();
            ILLabel ifEnd = c.DefineLabel();

            // if (...)
            c.EmitDelegate<Func<string, bool>>((v) => v == "Terraria/AvalonTesting/SuperHardmode");
            c.Emit(OpCodes.Brfalse_S, ifStart);

            // {

            c.EmitDelegate(() => SHMPath);
            c.Emit(OpCodes.Stloc, 24)
                .Emit(OpCodes.Br, ifEnd);

            // } else

            c.MarkLabel(ifStart);

            c.Emit(OpCodes.Ldloca, 23)
                .Emit(OpCodes.Call, typeof(KeyValuePair<string, object>).GetProperty(nameof(KeyValuePair<string, object>.Key), BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).GetMethod)
                .GotoNext(i => i.MatchNop());

            c.MarkLabel(ifEnd);
        }
        catch (Exception e)
        {
            Mod.Logger.Error("Drunk SHM IL Error: " + e.Message + " " + e.StackTrace);
        }
    }

    public override void Unload()
    {
        if (worldIcons != null)
        {
            ModifyWorldIcons -= NormalSHM;
        }

        if (drunkIcons != null)
        {
            ModifyDrunkIcons -= DrunkSHM;
        }

        sHMCondition = null;
        worldIcons = null;
        drunkIcons = null;
        mod = null;
    }
}
