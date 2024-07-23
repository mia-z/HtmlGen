namespace HtmlGen.Core.Structs;

public struct HyperscriptPartCollection
{
    private List<HyperscriptPart> _parts = [];

    public HyperscriptPartCollection()
    {
        
    }

    public static implicit operator string(HyperscriptPartCollection parts) => parts.ToString();
    
    public override string ToString()
    {
        return string.Join(" ", _parts);
    }

    public void Append(HyperscriptPart part)
    {
        _parts.Add(part);
    }
}