using HtmlGen.Core.Interfaces;
using HtmlGen.Core.Structs;
using Microsoft.AspNetCore.Http;

namespace HtmlGen.Core.Abstractions;

public abstract class PageBase : PageBuilder, IPage
{
    public PathString Route { get; init; }
    public Type? LayoutType { get; init; }
    public ILayout? Layout { get; set; }
    public Stylesheet? ScopedStylesheet { get; init; } = null;
    public string Title { get; set; } = "HTMLGen C# - Static web-page generator";
    public bool HasLayout => LayoutType is not null;
    public abstract Task<MarkupNode> RenderContent();
    
    public async Task<MarkupNode> RenderAsync()
    {
        //Idk if I like this, but it _works_ 
        if (Layout is not null)
        {
            Layout.Page = this;
            return await Layout.RenderAsync();
        }
        return await RenderContent();
    }
}