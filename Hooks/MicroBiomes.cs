using Avalon.Common;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria.ModLoader;

namespace Avalon.Hooks;
public class MicroBiomes : ModHook
{
    private static readonly ushort[] invalidWalls = new ushort[]
    {
        7, 94, 95, 8, 98, 99, 9, 96, 97, 3,
        83, 68, 62, 78, 87, 86, 42, 74, 27, 149,
        (ushort)ModContent.WallType<Walls.TuhrtlBrickWallUnsafe>(),
        (ushort)ModContent.WallType<Walls.ImperviousBrickWallUnsafe>()
    };
    // DOES NOT WORK RIP
    protected override void Apply()
    {
        //IL.Terraria.GameContent.Generation.TrackGenerator.IsLocationInvalid += ILTrackGeneratorTrackCanBePlaced;
    }

    public static void ILTrackGeneratorTrackCanBePlaced(ILContext il)
    {
        var c = new ILCursor(il);

        if (!c.TryGotoNext(i => i.MatchLdelemU1()))
            return;
        if (!c.TryGotoPrev(i => i.MatchLdsfld(out _)))
            return;

        Utilities.SoftReplaceAllMatchingInstructionsWithMethod(il, c.Next, typeof(MicroBiomes).GetMethod(nameof(ReturnInvalidWalls)));

        if (!c.TryGotoNext(i => i.MatchLdelemU1()))
            return;

        c.Remove();
        c.Emit(OpCodes.Ldelem_U2);
    }

    public static ushort[] ReturnInvalidWalls()
    {
        return invalidWalls;
    }
}
