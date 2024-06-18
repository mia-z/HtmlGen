namespace HtmlGen.Core.Structs;

public readonly struct MarkupClass
{
    private readonly string _class;

    public MarkupClass(string @class)
    {
        _class = @class;
    }

    public override string ToString()
    {
        return _class;
    }

    public static implicit operator string(MarkupClass @class)
    {
        return @class._class;
    }

    public static implicit operator MarkupClass(string @class)
    {
        return new MarkupClass(@class);
    }
}

public static class MarkupClassExtensions
{
    public static string Serialize(this MarkupClass[] classes)
    {
        return $" class=\"{string.Join(" ", classes)}\"";
    }
}