using System;
using System.Linq.Expressions;
using Mono.Cecil.Cil;
using MonoMod.Cil;

namespace AvalonTesting.Hooks;

public static class Utilities
{
    /// <summary>
    ///     Creates a delegate for reading instance property or field values that is faster than a reflection implementation,
    ///     this should only be used to create a cached expression as the compilation is expensive
    /// </summary>
    /// <param name="fieldName">The name of the property or field</param>
    /// <typeparam name="TInstance">The type of the instance that the field belongs to</typeparam>
    /// <typeparam name="TResult">The type of the property or field</typeparam>
    /// <returns>A delegate that provides the property or field value when supplied with an instance</returns>
    public static Func<TInstance, TResult> CreatePropertyOrFieldReaderDelegate<TInstance, TResult>(string fieldName)
    {
        ParameterExpression instanceParameter = Expression.Parameter(typeof(TInstance));
        return Expression
            .Lambda<Func<TInstance, TResult>>(Expression.PropertyOrField(instanceParameter, fieldName),
                instanceParameter).Compile();
    }

    public static void OutputIL(ILContext il)
    {
        var c = new ILCursor(il);
        foreach (Instruction instruction in c.Instrs)
        {
            object obj = instruction.Operand == null ? "" : instruction.Operand.ToString();
            AvalonTesting.Mod.Logger.Debug($"{instruction.Offset} | {instruction.OpCode} | {obj}");
        }
    }
}
