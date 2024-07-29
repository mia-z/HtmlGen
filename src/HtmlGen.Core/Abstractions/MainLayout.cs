using HtmlGen.Core.Interfaces;
using HtmlGen.Core.Structs;

namespace HtmlGen.Core.Abstractions;

public abstract class MainLayout : LayoutBase, IMainLayout
{
    public bool UseTailwind { get; set; } = false;
    public bool UseHyperscript { get; set; } = false;
    public bool NormalizeBaseCss { get; set; } = true;

    public string PageTitle => Page.Title;
    public string DocumentLanguage { get; set; } = "en";
    
    private MarkupNode RootLayout =>
        Fragment("<!DOCTYPE html>",
            html(
                head(
                    title(PageTitle),
                    RenderMetaTags(),
                    NormalizeBaseCss ? style(NormalizedStylesheet) : "",
                    UseHyperscript ? script(string.Empty, "https://unpkg.com/hyperscript.org@0.9.12") : "",
                    UseTailwind ? script(string.Empty, "https://cdn.tailwindcss.com") : ""
                ),
                body()
            ).WithAttributes(("lang", DocumentLanguage))
        );
    
    private Stylesheet NormalizedStylesheet =>
        Stylesheet.Create(
            StylesheetNode.Create(
                "html", 
                ("line-height", "1.5"),
                ("box-sizing", "border-box")
            ),
            StylesheetNode.Create(
                "*, *::before, *::after",
                ("box-sizing", "inherit")
            ),
            StylesheetNode.Create(
                "body",
                ("margin", "0")
            )
        );
    
    public async Task<MarkupNode> RenderMainLayout(IPage page)
    {
        Page = page;

        var root = RootLayout;
        
        if (page.ScopedStylesheet is not null)
            root[MarkupTagName.Html][MarkupTagName.Head].Children.Add(style(page.ScopedStylesheet));
        
        var layout = await RenderLayout();
        root[MarkupTagName.Html][MarkupTagName.Body].Children.Add(layout);
        
        return root;
    }

    protected virtual MarkupNode RenderMetaTags() =>
        Fragment(
            meta("charset", "UTF-8"),
            meta("viewport", "width=device-width, initial-scale=1.0")
        );
    
    public override async Task<MarkupNode> RenderPageContent()
    {
        return Fragment(
            Marker("MAIN ENTRY").WithAttributes(("render-id", Guid.NewGuid().ToString())),
            await Page.RenderAsync()
        );
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