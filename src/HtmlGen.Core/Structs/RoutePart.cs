using System.Text.RegularExpressions;

namespace HtmlGen.Core.Structs;

public readonly partial record struct RoutePart
{
    [GeneratedRegex(@"\{[^}]+\}")]
    private static partial Regex ParameterPattern();

    [GeneratedRegex(@"^(\{[a-zA-Z0-9]+\}|[a-zA-Z0-9]+)$")]
    private static partial Regex ValidityPattern();

    public bool IsParameter => ParameterPattern().IsMatch(_value);
    
    public bool IsValid => ValidityPattern().IsMatch(_value);
    
    public short Position { get; } = short.MinValue;
    private readonly string _value = String.Empty;
    public string Value
    {
        get
        {
            if (!IsParameter)
                return _value;

            return _value.Substring(1, _value.Length - 2);
        }
    }

    public string Template
    {
        get
        {
            if (!IsParameter)
                throw new InvalidOperationException("RoutePart is not a parameter");
            return _value;
        }
    }

    public RoutePart()
    {
        throw new InvalidOperationException("Dont construct route-part this way");
    }

    private RoutePart(string value, short position)
    {
        if (!ValidityPattern().IsMatch(value) && !value.Equals(string.Empty))
            throw new InvalidOperationException("Invalid format for parameter, must be a plain string or a string surrounded in curly braces");
        _value = value;
        Position = position;
    }

    public static implicit operator RoutePart((string value, short position) tuple) => 
        new(tuple.value, tuple.position);
}