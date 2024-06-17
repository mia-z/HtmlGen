using HtmlGen.Core.Interfaces;
using HtmlGen.Core.Services;
using HtmlGen.Core.Structs;

namespace HtmlGen.Core.Abstractions;

public abstract class PageBase : MarkupBuilder, IPage
{
    public ComponentResolver ComponentResolver { get; set; }
    public string Route { get; init; }
    public StylesheetNode? ScopedStylesheet { get; init; } = null;
    
    protected MarkupNode Component<T>() where T : notnull
    {
        var comp = ComponentResolver.Resolve<T>();
        return comp.Render();
    }

    protected MarkupNode Component<T>(params ComponentParameter[] parameters) where T : notnull
    {
        var comp = ComponentResolver.Resolve<T>(parameters);
        
        return comp.Render();
    }
    
    public abstract Task<MarkupNode> RenderContent();

    public async Task<MarkupNode> RenderAsync()
    {
        return await RenderContent();
    }
}