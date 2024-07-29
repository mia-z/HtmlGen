using HtmlGen.Core.Structs;
using HtmlGen.Hyperscript.Structs;

namespace HtmlGen.Hyperscript.Extensions;

public static class MarkupNodeExtensions
{
    public static MarkupNode WithHyperscript(this MarkupNode node, HyperscriptAttribute ha) =>
        node with { Attributes = [ ..node.Attributes, ha ] };
}