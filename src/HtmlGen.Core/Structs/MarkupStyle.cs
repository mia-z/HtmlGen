namespace HtmlGen.Core.Structs;

public readonly struct MarkupStyle
{
    private readonly string _styleAttribute;
    private readonly string _styleValue;
    
    private MarkupStyle(string key, string value)
    {
        _styleAttribute = key;
        _styleValue = value;
    }

    public static implicit operator MarkupStyle((string styleAttribute, string styleValue) keyValueTuple)
    {
        return new MarkupStyle(keyValueTuple.styleAttribute, keyValueTuple.styleValue);
    }

    public override string ToString()
    {
        return $"{_styleAttribute}: {_styleValue}";
    }
}

public static class MarkupStyleExtensions
{
    public static string Serialize(this MarkupStyle[] attrs)
    {
        return $" style=\"{string.Join(";", attrs.Select(x => x.ToString()))}\"";
    }
}