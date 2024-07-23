#nullable enable

using System.Collections.Immutable;

namespace HtmlGen.Core.Structs;

public readonly record struct RoutePath
{
    private readonly ImmutableArray<RoutePart> _parts = [];

    public int Length => _parts.Length;
    
    public RoutePath(IEnumerable<RoutePart> parts)
    {
        _parts = [..parts];
    }

    public RoutePart this[int index] => _parts[index];

    public bool Match(ImmutableArray<string> partsToMatch, out IEnumerable<KeyValuePair<string, string>> parameters)
    {
        parameters = [];
        
        if (Length != partsToMatch.Length)
        {
            return false;
        }

        for (var x = 0; x < Length; x++)
        {
            if ((!this[x].IsParameter && this[x].Value.Equals(partsToMatch[x].ToLower())) || (this[x].IsParameter && this[x].Position.Equals((short)x)))
            {
                if (this[x].IsParameter)
                    parameters = parameters.Append(new KeyValuePair<string, string>(this[x].Value, partsToMatch[x]));
                if (x.Equals(Length - 1))
                {
                    return true;
                }
                continue;
            }
            break;
        }
        
        return false;
    }
}