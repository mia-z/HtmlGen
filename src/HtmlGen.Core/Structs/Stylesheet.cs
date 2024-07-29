namespace HtmlGen.Core.Structs;

public readonly struct Stylesheet
{
    internal StylesheetNode[] Children { get; init; } = [];

    Stylesheet(StylesheetNode[] nodes)
    {
        Children = nodes;
    }

    public static Stylesheet Create(params StylesheetNode[] nodes)
    {
        return new Stylesheet(nodes);
    }

    public override string ToString()
    {
        return $"{string.Join("\n", Children)}";
    }
    
    public static implicit operator string(Stylesheet node)
    {
        return node.ToString();
    }
}