using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using Avalon.Common;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Client)]
public class EvilAltar : ModHook
{
    protected override void Apply() => IL.Terraria.WorldGen.SmashAltar += WorldGen_SmashAltar;

    private void WorldGen_SmashAltar(ILContext il)
	{
        ILCursor c = new(il);
        int j = 0;
        while (c.TryGotoNext(i => i.MatchLdcI4(50),
            i => i.MatchLdcI4(255),
            i => i.MatchLdcI4(130)))
		{
            c.Index++;
            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldc_I4, j);
            c.EmitDelegate<Func<int, int>>((j) =>
            {
                int type = WorldGen.SavedOreTiers.Cobalt;
                if (j > 1 && j <= 3)
                    type = WorldGen.SavedOreTiers.Mythril;
                else if (j > 3 && j <= 5)
                    type = WorldGen.SavedOreTiers.Adamantite;
                return AvalonTesting.ReturnHardmodeColor(type).R;
            });

            c.Index++;
            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldc_I4, j);
            c.EmitDelegate<Func<int, int>>((j) =>
            {
                int type = WorldGen.SavedOreTiers.Cobalt;
                if (j > 1 && j <= 3)
                    type = WorldGen.SavedOreTiers.Mythril;
                else if (j > 3 && j <= 5)
                    type = WorldGen.SavedOreTiers.Adamantite;
                return AvalonTesting.ReturnHardmodeColor(type).G;
            });

            c.Index++;
            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldc_I4, j);
            c.EmitDelegate<Func<int, int>>((j) =>
            {
                int type = WorldGen.SavedOreTiers.Cobalt;
                if (j > 1 && j <= 3)
                    type = WorldGen.SavedOreTiers.Mythril;
                else if (j > 3 && j <= 5)
                    type = WorldGen.SavedOreTiers.Adamantite;
                return AvalonTesting.ReturnHardmodeColor(type).B;
            });

            j++;
		}
	}
}
