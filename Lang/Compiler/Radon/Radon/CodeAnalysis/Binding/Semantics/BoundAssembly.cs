using System.Collections.Immutable;
using Radon.CodeAnalysis.Binding.Semantics.Types;
using Radon.CodeAnalysis.Symbols;
using Radon.CodeAnalysis.Syntax.Nodes;

namespace Radon.CodeAnalysis.Binding.Semantics;

internal sealed class BoundAssembly : BoundNode
{
    public override BoundNodeKind Kind => BoundNodeKind.Assembly;
    public AssemblySymbol? Assembly { get; }
    public ImmutableArray<BoundType> Types { get; }
    public ImmutableArray<Diagnostic> Diagnostics { get; }
    public Scope? Scope { get; }
    internal BoundAssembly(SyntaxNode syntax, AssemblySymbol? assembly, ImmutableArray<BoundType> types, ImmutableArray<Diagnostic> diagnostics, Scope? scope) 
        : base(syntax)
    {
        Assembly = assembly;
        Types = types;
        Diagnostics = diagnostics;
        Scope = scope;
    }
}