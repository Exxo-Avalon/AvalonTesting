using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using Mono.Cecil.Cil;
using MonoMod.Cil;

namespace Avalon.Hooks;

public static class Utilities
{
    /// <summary>
    ///     Creates a delegate for reading instance property or field values that is faster than a reflection implementation,
    ///     this should only be used to create a cached expression as the compilation is expensive.
    /// </summary>
    /// <param name="fieldName">The name of the property or field.</param>
    /// <typeparam name="TInstance">The type of the instance that the field belongs to.</typeparam>
    /// <typeparam name="TResult">The type of the property or field.</typeparam>
    /// <returns>A delegate that provides the property or field value when supplied with an instance.</returns>
    public static Func<TInstance, TResult> CreateInstancePropertyOrFieldReaderDelegate<TInstance, TResult>(
        string fieldName)
    {
        ParameterExpression instanceParameter = Expression.Parameter(typeof(TInstance));
        return Expression
            .Lambda<Func<TInstance, TResult>>(
                Expression.PropertyOrField(instanceParameter, fieldName),
                instanceParameter).Compile();
    }

    public static void OutputIL(ILContext il)
    {
        var c = new ILCursor(il);
        foreach (Instruction instruction in c.Instrs)
        {
            object obj = (instruction.Operand == null ? string.Empty : instruction.Operand.ToString()) ?? string.Empty;
            Avalon.Mod.Logger.Debug(
                $"{instruction.Offset.ToString(CultureInfo.InvariantCulture)} | {instruction.OpCode.ToString()} | {obj}");
        }
    }

    public static List<Instruction> FromCursorToInstruction(ILCursor c, Func<Instruction, bool> predicate)
    {
        List<Instruction> instructions = new();
        while (!predicate.Invoke(c.Next))
        {
            instructions.Add(c.Next);
            c.Index++;
        }

        return instructions;
    }

    public static void EmitInstructions(ILCursor c, IEnumerable<Instruction> instructions)
    {
        foreach (Instruction instruction in instructions)
        {
            c.Emit(instruction.OpCode, instruction.Operand);
        }
    }

    public static void RemoveUntilInstruction(ILCursor c, Func<Instruction, bool> predicate)
    {
        while (!predicate.Invoke(c.Next))
        {
            c.Remove();
        }
    }

    public static void AddAlternativeIdChecks(ILContext il, ushort origId, Func<ushort, bool> predicate)
    {
        var c = new ILCursor(il);

        while (c.TryGotoNext(i =>
                   (i.MatchBeq(out _) || i.MatchBneUn(out _)) && i.Offset != 0 && i.Previous.MatchLdcI4(origId)))
        {
            c.Index--;
            c.EmitDelegate<Func<ushort, ushort>>(id => predicate.Invoke(id) ? origId : id);
            c.Index += 2;
        }
    }

    public static void SoftReplaceAllMatchingInstructionsWithMethod(ILContext il, Instruction i1, MethodBase method)
    {
        var c = new ILCursor(il);

        while (c.TryGotoNext(i => i.OpCode == i1.OpCode && (i1.Operand == null || i.Operand == i1.Operand)))
        {
            // Ensure not replacing anything added by an IL hook
            if (c.Next.Offset != 0)
            {
                c.Index++;
                c.Emit(OpCodes.Pop);
                c.Emit(OpCodes.Call, method);
            }
        }
    }
}
