using System.Collections.Immutable;

namespace HtmlGen.Core.Structs;

public readonly struct StylesheetNode
{
    internal string Selectors { get; init; }
    internal bool IsNested { get; init; } = false;
    internal StylesheetNode[] Children { get; init; } = [];
    internal StylesheetNodeProperty[] Properties { get; init; } = [];
    
    public StylesheetNode()
    {
        
    }

    public static StylesheetNode Create(string selectorString, params StylesheetNodeProperty[] properties)
    {
        return new StylesheetNode
        {
            Selectors = selectorString,
            Properties = properties
        };
    }
    
    public StylesheetNode WithNested(params StylesheetNode[] nestedNode)
    {
        var nestedChildren = nestedNode.Select(x => x with { IsNested = true, Selectors = $"& {x.Selectors}"})
            .ToImmutableArray();
        return this with { Children = [ ..Children, ..nestedChildren ] };
    }

    public override string ToString()
    {
        return $@"{Selectors} {{
  {string.Join("\n", Properties)}
  {string.Join("\n", Children)}
}}";
    }

    public static implicit operator string(StylesheetNode node)
    {
        return node.ToString();
    }
    
    public static implicit operator StylesheetNode((string selectors, StylesheetNodeProperty[] properties) input)
    {
        return new StylesheetNode
        {
            Selectors = input.selectors,
            Properties = input.properties
        };
    }
}