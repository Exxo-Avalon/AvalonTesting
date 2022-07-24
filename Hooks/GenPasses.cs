using System.Collections.Generic;
using System.Reflection;
using Avalon.Common;
using IL.Terraria;
using Mono.Cecil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour.HookGen;
using MonoMod.Utils;
using Terraria.ModLoader;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Both)]
public class GenPasses : ModHook
{
    private MethodBase? altarsInfo;
    private MethodBase? resetInfo;
    private MethodBase? shiniesInfo;

    public event ILContext.Manipulator HookGenPassAltars
    {
        add => HookEndpointManager.Modify(altarsInfo, value);
        remove => HookEndpointManager.Unmodify(altarsInfo, value);
    }

    public event ILContext.Manipulator HookGenPassReset
    {
        add => HookEndpointManager.Modify(resetInfo, value);
        remove => HookEndpointManager.Unmodify(resetInfo, value);
    }

    public event ILContext.Manipulator HookGenPassShinies
    {
        add => HookEndpointManager.Modify(shiniesInfo, value);
        remove => HookEndpointManager.Unmodify(shiniesInfo, value);
    }

    public void ILGenerateWorld(ILContext il)
    {
        resetInfo = GetGenPassInfo(il, "Reset");
        shiniesInfo = GetGenPassInfo(il, "Shinies");
        altarsInfo = GetGenPassInfo(il, "Altars");
    }

    protected override void Apply() => WorldGen.GenerateWorld += ILGenerateWorld;

    private static MethodBase? GetGenPassInfo(ILContext il, string name)
    {
        try
        {
            var c = new ILCursor(il);
            MethodReference? methodReference = null;

            c.GotoNext(i => i.MatchLdstr(name))
                .GotoNext(i => i.MatchLdftn(out methodReference));

            return methodReference?.ResolveReflection();
        }
        catch (KeyNotFoundException e)
        {
            Avalon.Mod.Logger.Error($"Could not find GenPass with name {name}", e);
            return null;
        }
    }
}
