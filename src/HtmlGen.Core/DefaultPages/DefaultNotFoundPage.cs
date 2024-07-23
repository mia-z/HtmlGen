using HtmlGen.Core.Abstractions;
using HtmlGen.Core.Attributes;
using HtmlGen.Core.Structs;

namespace HtmlGen.Core.DefaultPages;

public class DefaultNotFoundPage : NotFoundPage
{
    public override async Task<MarkupNode> RenderContent() =>
        div("Not found!");
}