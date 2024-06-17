namespace HtmlGen.Core.Structs;

public readonly struct MarkupAttribute
{
    private readonly string _attributeKey;
    private readonly string _attributeValue;

    public string Attribute => _attributeKey;
    public string Value => _attributeValue;
    
    private MarkupAttribute(string key, string value)
    {
        _attributeKey = key;
        _attributeValue = value;
    }

    public static implicit operator MarkupAttribute((string attributeKey, string attributeValue) keyValueTuple)
    {
        return new MarkupAttribute(keyValueTuple.attributeKey, keyValueTuple.attributeValue);
    }

    public override string ToString()
    {
        return $"{_attributeKey}=\"{_attributeValue}\"";
    }
}

public static class MarkupAttributeExtensions
{
    public static string Serialize(this MarkupAttribute[] attrs)
    {
        return $" {string.Join(" ", attrs.Select(x => x.ToString()))}";
    }
}