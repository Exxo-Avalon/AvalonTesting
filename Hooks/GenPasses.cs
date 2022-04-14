using System;
using System.Reflection;
using Mono.Cecil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour.HookGen;
using Terraria;

namespace AvalonTesting.Hooks;

public static class GenPasses
{
    private static Assembly assembly;
    private static MethodInfo resetInfo;
    private static MethodInfo dirtWallBackgroundsInfo;
    private static MethodInfo jungleInfo;
    private static MethodInfo cleanUpDirtInfo;
    private static MethodInfo wetJungleInfo;
    private static MethodInfo mudWallsInJungleInfo;
    private static MethodInfo wallVarietyInfo;
    private static MethodInfo iceWallsInfo;
    private static MethodInfo grassWallInfo;
    private static MethodInfo junglePlantsInfo;
    private static MethodInfo mudCavesToGrassInfo;
    private static MethodInfo potsInfo;
    private static MethodInfo pilesInfo;
    private static MethodInfo shiniesInfo;
    private static MethodInfo altarsInfo;

    public static event ILContext.Manipulator Hook_GenPassReset
    {
        add => HookEndpointManager.Modify(resetInfo, value);
        remove => HookEndpointManager.Unmodify(resetInfo, value);
    }

    public static event ILContext.Manipulator Hook_GenPassDirtWallBackgrounds
    {
        add => HookEndpointManager.Modify(dirtWallBackgroundsInfo, value);
        remove => HookEndpointManager.Unmodify(dirtWallBackgroundsInfo, value);
    }

    public static event ILContext.Manipulator Hook_GenPassJungle
    {
        add => HookEndpointManager.Modify(jungleInfo, value);
        remove => HookEndpointManager.Unmodify(jungleInfo, value);
    }

    public static event ILContext.Manipulator Hook_GenPassCleanUpDirt
    {
        add => HookEndpointManager.Modify(cleanUpDirtInfo, value);
        remove => HookEndpointManager.Unmodify(cleanUpDirtInfo, value);
    }

    public static event ILContext.Manipulator Hook_GenPassWetJungle
    {
        add => HookEndpointManager.Modify(wetJungleInfo, value);
        remove => HookEndpointManager.Unmodify(wetJungleInfo, value);
    }

    public static event ILContext.Manipulator Hook_GenPassMudWallsInJungle
    {
        add => HookEndpointManager.Modify(mudWallsInJungleInfo, value);
        remove => HookEndpointManager.Unmodify(mudWallsInJungleInfo, value);
    }

    public static event ILContext.Manipulator Hook_GenPassWallVariety
    {
        add => HookEndpointManager.Modify(wallVarietyInfo, value);
        remove => HookEndpointManager.Unmodify(wallVarietyInfo, value);
    }

    public static event ILContext.Manipulator Hook_GenPassIceWalls
    {
        add => HookEndpointManager.Modify(iceWallsInfo, value);
        remove => HookEndpointManager.Unmodify(iceWallsInfo, value);
    }

    public static event ILContext.Manipulator Hook_GenPassGrassWall
    {
        add => HookEndpointManager.Modify(grassWallInfo, value);
        remove => HookEndpointManager.Unmodify(grassWallInfo, value);
    }

    public static event ILContext.Manipulator Hook_GenPassJunglePlants
    {
        add => HookEndpointManager.Modify(junglePlantsInfo, value);
        remove => HookEndpointManager.Unmodify(junglePlantsInfo, value);
    }

    public static event ILContext.Manipulator Hook_GenPassMudCavesToGrass
    {
        add => HookEndpointManager.Modify(mudCavesToGrassInfo, value);
        remove => HookEndpointManager.Unmodify(mudCavesToGrassInfo, value);
    }

    public static event ILContext.Manipulator Hook_GenPassPots
    {
        add => HookEndpointManager.Modify(potsInfo, value);
        remove => HookEndpointManager.Unmodify(potsInfo, value);
    }

    public static event ILContext.Manipulator Hook_GenPassPiles
    {
        add => HookEndpointManager.Modify(pilesInfo, value);
        remove => HookEndpointManager.Unmodify(pilesInfo, value);
    }

    public static event ILContext.Manipulator Hook_GenPassShinies
    {
        add => HookEndpointManager.Modify(shiniesInfo, value);
        remove => HookEndpointManager.Unmodify(shiniesInfo, value);
    }

    public static event ILContext.Manipulator Hook_GenPassAltars
    {
        add => HookEndpointManager.Modify(altarsInfo, value);
        remove => HookEndpointManager.Unmodify(altarsInfo, value);
    }

    public static void ILGenerateWorld(ILContext il)
    {
        assembly = typeof(Main).Assembly;

        resetInfo = GetGenPassInfo(il, "Reset");
        shiniesInfo = GetGenPassInfo(il, "Shinies");
        altarsInfo = GetGenPassInfo(il, "Altars");
        // dirtWallBackgroundsInfo = GetGenPassInfo(il, "Dirt Wall Backgrounds");
        // jungleInfo = GetGenPassInfo(il, "Jungle");
        // cleanUpDirtInfo = GetGenPassInfo(il, "Clean Up Dirt");
        // wetJungleInfo = GetGenPassInfo(il, "Wet Jungle");
        // mudWallsInJungleInfo = GetGenPassInfo(il, "Muds Walls In Jungle");
        // wallVarietyInfo = GetGenPassInfo(il, "Wall Variety");
        // iceWallsInfo = GetGenPassInfo(il, "Ice Walls");
        // grassWallInfo = GetGenPassInfo(il, "Grass Wall");
        // junglePlantsInfo = GetGenPassInfo(il, "Jungle Plants");
        // mudCavesToGrassInfo = GetGenPassInfo(il, "Mud Caves To Grass");
        // potsInfo = GetGenPassInfo(il, "Pots");
        // pilesInfo = GetGenPassInfo(il, "Piles");
    }

    private static MethodInfo GetGenPassInfo(ILContext il, string name)
    {
        var c = new ILCursor(il);

        MethodReference methodReference = null;
        Type type;

        if (!c.TryGotoNext(i => i.MatchLdstr(name)))
        {
            return null;
        }

        if (!c.TryGotoNext(i => i.MatchLdftn(out methodReference)))
        {
            return null;
        }

        type = assembly.GetType("Terraria.WorldGen+" + methodReference.DeclaringType.Name);
        return type.GetMethod(methodReference.Name, BindingFlags.Instance | BindingFlags.NonPublic);
    }
}
