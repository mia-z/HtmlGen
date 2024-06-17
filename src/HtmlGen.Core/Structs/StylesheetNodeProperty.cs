namespace HtmlGen.Core.Structs;

public readonly struct StylesheetNodeProperty
{
    internal string PropertyKey { get; init; }
    internal string PropertyValue { get; init; }

    public StylesheetNodeProperty()
    {
        
    }
    
    public static implicit operator StylesheetNodeProperty((string propertyKey, string propertyValue) keyValuePair)
    {
        return new StylesheetNodeProperty
        {
            PropertyKey = keyValuePair.propertyKey,
            PropertyValue = keyValuePair.propertyValue
        };
    }
    
    public override string ToString()
    {
        return $"{PropertyKey}: {PropertyValue};";
    }
}