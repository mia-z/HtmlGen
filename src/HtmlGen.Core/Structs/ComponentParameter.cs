namespace HtmlGen.Core.Structs;

public readonly struct ComponentParameter
{
    private readonly string _parameterName;
    private readonly string _parameterValue;
    
    private ComponentParameter(string key, string value)
    {
        _parameterName = key;
        _parameterValue = value;
    }
    
    public static implicit operator ComponentParameter((string parameterName, string parameterValue) keyValueTuple)
    {
        return new ComponentParameter(keyValueTuple.parameterName, keyValueTuple.parameterValue);
    }

    public static implicit operator (string key, string value)(ComponentParameter param)
    {
        return (param._parameterName, param._parameterValue);
    }

    public void Deconstruct(out string parameterName, out string parameterValue)
    {
        parameterName = _parameterName;
        parameterValue = _parameterValue;
    }
}