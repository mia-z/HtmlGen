using HtmlGen.Core.Interfaces;
using HtmlGen.Core.Services;
using HtmlGen.Core.Structs;

namespace HtmlGen.Core.Abstractions;

public abstract class PageBase : PageBuilder, IPage
{
    public string Route { get; init; }
    public Type? LayoutType { get; init; }
    public ILayout? Layout { get; set; }
    public StylesheetNode? ScopedStylesheet { get; init; } = null;
    
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