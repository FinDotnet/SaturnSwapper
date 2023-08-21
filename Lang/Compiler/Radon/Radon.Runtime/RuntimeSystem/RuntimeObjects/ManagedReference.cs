﻿using System;
using System.Text;
using Radon.CodeAnalysis.Emit;
using Radon.Runtime.Memory;

namespace Radon.Runtime.RuntimeSystem.RuntimeObjects;

internal sealed class ManagedReference : RuntimeObject
{
    public override RuntimeType Type { get; }
    public override int Size => sizeof(ulong);
    public override nuint Pointer { get; }

    public nuint Target { get; set; } // The pointer to the object this reference points to.

    public ManagedReference(RuntimeType type, nuint pointer, nuint target)
    {
        Type = type;
        Pointer = pointer;
        Target = target;
    }
    
    public override RuntimeObject ComputeOperation(OpCode operation, RuntimeObject? other, StackFrame stackFrame)
    {
        if (other is not ManagedReference otherReference)
        {
            throw new InvalidOperationException("Cannot perform an operation on a reference and a non-reference.");
        }
        
        switch (operation)
        {
            case OpCode.Ceq:
            {
                return stackFrame.AllocatePrimitive(ManagedRuntime.Boolean, Pointer == otherReference.Pointer);
            }
            case OpCode.Cne:
            {
                return stackFrame.AllocatePrimitive(ManagedRuntime.Boolean, Pointer != otherReference.Pointer);
            }
        }
        
        throw new InvalidOperationException($"Cannot perform operation {operation} on a reference.");
    }
    
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("ref ");
        sb.Append(Type);
        sb.Append(" -> ");
        // Get hex representation of the target.
        sb.Append("0x");
        sb.Append(Target.ToString("X"));
        return sb.ToString();
    }
}