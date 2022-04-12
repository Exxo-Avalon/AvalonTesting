using System;
using MonoMod.Cil;
using Terraria.ID;

namespace AvalonTesting.Hooks;

public static class DresserFix
{
    public static void ILIsInInteractionRangeToMultiTileHitbox(ILContext il)
    {
        var c = new ILCursor(il);

        // Don't apply if it's not present
        if (!c.TryGotoNext(i => i.MatchLdcI4(TileID.Dressers)))
        {
            return;
        }

        // Vanilla doesn't check the BasicDresser set... facepalm
        c.EmitDelegate<Func<ushort, ushort>>(val => TileID.Sets.BasicDresser[val] ? TileID.Dressers : val);
    }
}
