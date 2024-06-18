using HtmlGen.Core.Interfaces;
using HtmlGen.Core.Structs;

namespace HtmlGen.Core.Abstractions;

public abstract class MainLayout : LayoutBase, IMainLayout
{
    public async Task<MarkupNode> RenderMainLayout(IPage page)
    {
        var layout = await RenderLayout();
        var content = await page.RenderAsync();
        if (page.ScopedStylesheet is not null)
            layout[MarkupTagName.Html][MarkupTagName.Head].Children.Add(style(page.ScopedStylesheet));
        layout[MarkupTagName.Html][MarkupTagName.Body].Children.Add(content);
        return layout;
    }

    public override async Task<MarkupNode> RenderPageContent()
    {
        return Marker("MAIN ENTRY")
            .WithAttributes(("render-id", Guid.NewGuid().ToString()));
    }
    
    protected MarkupNode meta(string nameKey, string value)
    {
        if (nameKey == "charset")
            return MarkupNode.Create(MarkupTagName.Meta)
                .WithAttributes(("charset", value)) with { IsVoid = true};
        return MarkupNode.Create(MarkupTagName.Meta)
            .WithAttributes(("name", nameKey), ("content", value)) with { IsVoid = true};
    }
    
    protected MarkupNode html(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.Html, markupNodes);
    }
    
    protected MarkupNode head(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.Head, markupNodes);
    }
    
    protected MarkupNode body(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.Body, markupNodes);
    }
    
    protected MarkupNode title(string title)
    {
        return MarkupNode.Create(MarkupTagName.Title, title);
    }
}