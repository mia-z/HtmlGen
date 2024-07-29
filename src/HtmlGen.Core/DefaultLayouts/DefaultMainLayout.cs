using HtmlGen.Core.Abstractions;
using HtmlGen.Core.Structs;

namespace HtmlGen.Core.DefaultLayouts;

internal sealed class DefaultMainLayout : MainLayout
{
    public override async Task<MarkupNode> RenderLayout() => await RenderPageContent();
}