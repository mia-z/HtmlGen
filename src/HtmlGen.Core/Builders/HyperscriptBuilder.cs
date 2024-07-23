using HtmlGen.Core.Structs;

namespace HtmlGen.Core.Builders;

public static class HyperscriptBuilder
{
    public static HyperscriptAttribute _h
    {
        get
        {
            return new HyperscriptAttribute();
        }
    }
}