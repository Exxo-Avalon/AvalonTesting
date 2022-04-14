using System;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;

namespace AvalonTesting.Hooks;

public static class WorldGenEdits
{
    public static void ILGenPassShinies(ILContext il)
    {
        ContagionWorldGen.ILGenPassShinies(il);

        var instructions = new Instruction[4];
        var delegates = new Func<int>[4]
        {
            () => WorldGen.SavedOreTiers.Copper, () => WorldGen.SavedOreTiers.Iron,
            () => WorldGen.SavedOreTiers.Silver, () => WorldGen.SavedOreTiers.Gold
        };
        var c = new ILCursor(il);

        for (int i = 0; i < instructions.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (!c.TryGotoNext(i => i.MatchCall<WorldGen>(nameof(WorldGen.TileRunner))))
                {
                    return;
                }
            }

            if (!c.TryGotoPrev(i => i.MatchLdfld(out _)))
            {
                return;
            }

            instructions[i] = c.Next;
        }

        for (int i = 0; i < instructions.Length; i++)
        {
            SoftInterceptOreGen(il, instructions[i], delegates[i]);
        }
    }

    private static void SoftInterceptOreGen(ILContext il, Instruction i1, Func<int> del)
    {
        var c = new ILCursor(il);

        while (c.TryGotoNext(i => i.OpCode == i1.OpCode && i.Operand == i1.Operand))
        {
            c.Index++;
            c.Emit(OpCodes.Pop);
            c.EmitDelegate(del);
        }
    }
}
