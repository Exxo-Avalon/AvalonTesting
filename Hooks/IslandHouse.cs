using Avalon.Common;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MonoMod.Cil;
using System;
using Mono.Cecil.Cil;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Both)]
public class IslandHouse : ModHook
{
    protected override void Apply()
    {
        IL.Terraria.WorldGen.IslandHouse += BlahIsBad;
    }

    private void BlahIsBad(ILContext il)
    {
        ILCursor c = new(il);
        try
        {
            /*
                C# (L-6):
                    before:
                        byte type = 202;
                        byte wall = 82;
                    after:
                        byte type = 202;
                        type = SomeFunc(type);
                        byte wall = 82;
                        wall = SomeFunc2(wall);
                IL:
                    before:
                        IL_0000: ldc.i4 202
                        IL_0005: stloc.0
                        IL_0006: ldc.i4 82
                        IL_0008: stloc.1
                    after:
                        IL_0000: ldc.i4 202
                        IL_0005: stloc.0
                    [+] IL_0000: ldloc.0
                    [+] we have here our custom function
                    [+] IL_0000: stloc.0
                        IL_0006: ldc.i4 82
                        IL_0008: stloc.1
                    [+] IL_0000: ldloc.1
                    [+] we have here our custom function
                    [+] IL_0000: stloc.1
            */

            c.GotoNext(MoveType.After,
                i => i.MatchLdcI4(TileID.Sunplate),
                i => i.MatchStloc(0))
                .Emit(OpCodes.Ldloc, 0)
                .EmitDelegate<Func<int, int>>((i) =>
                {
                    if (WorldGen.SavedOreTiers.Gold == TileID.Platinum)
                        return ModContent.TileType<Tiles.MoonplateBlock>();
                    else if (WorldGen.SavedOreTiers.Gold == ModContent.TileType<Tiles.Ores.BismuthOre>())
                        return ModContent.TileType<Tiles.TwiliplateBlock>();
                    return i;
                });
            c.Emit(OpCodes.Stloc, 0);

            c.GotoNext(MoveType.After,
                i => i.MatchLdcI4(WallID.DiscWall),
                i => i.MatchStloc(1))
                .Emit(OpCodes.Ldloc, 1)
                .EmitDelegate<Func<int, int>>((i) =>
                {
                    if (WorldGen.SavedOreTiers.Gold == TileID.Platinum)
                        return ModContent.WallType<Walls.MoonWall>();
                    else if (WorldGen.SavedOreTiers.Gold == ModContent.TileType<Tiles.Ores.BismuthOre>())
                        return ModContent.WallType<Walls.TwilightWall>();
                    return i;
                });
            c.Emit(OpCodes.Stloc, 1);
        }
        catch (Exception e)
        {
            Avalon.Mod.Logger.Error($"[Island Plates IL Error]\n{e.Message}\n{e.StackTrace}");
        }
    }
}
