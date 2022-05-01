using System.Collections.Generic;
using System.Reflection;
using Mono.Cecil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour.HookGen;
using MonoMod.Utils;

namespace AvalonTesting.Hooks;

public static class GenPasses
{
    private static MethodBase ResetInfo;
    private static MethodBase ShiniesInfo;
    private static MethodBase AltarsInfo;

    public static event ILContext.Manipulator HookGenPassReset
    {
        add => HookEndpointManager.Modify(ResetInfo, value);
        remove => HookEndpointManager.Unmodify(ResetInfo, value);
    }

    public static event ILContext.Manipulator HookGenPassShinies
    {
        add => HookEndpointManager.Modify(ShiniesInfo, value);
        remove => HookEndpointManager.Unmodify(ShiniesInfo, value);
    }

    public static event ILContext.Manipulator HookGenPassAltars
    {
        add => HookEndpointManager.Modify(AltarsInfo, value);
        remove => HookEndpointManager.Unmodify(AltarsInfo, value);
    }

    public static void ILGenerateWorld(ILContext il)
    {
        ResetInfo = GetGenPassInfo(il, "Reset");
        ShiniesInfo = GetGenPassInfo(il, "Shinies");
        AltarsInfo = GetGenPassInfo(il, "Altars");
    }

    private static MethodBase GetGenPassInfo(ILContext il, string name)
    {
        try
        {
            var c = new ILCursor(il);
            MethodReference methodReference = null;

            c.GotoNext(i => i.MatchLdstr(name));
            c.TryGotoNext(i => i.MatchLdftn(out methodReference));

            return methodReference.ResolveReflection();
        }
        catch (KeyNotFoundException e)
        {
            AvalonTesting.Mod.Logger.Error($"Could not find GenPass with name {name}", e);
            return null;
        }
    }
}
