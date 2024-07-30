using HtmlGen.Core.Interfaces;
using HtmlGen.Core.Structs;

namespace HtmlGen.Core.Abstractions;

public abstract class LayoutBase : PageBuilder, ILayout
{
    public IPage Page { get; set; }
    
    public async Task<MarkupNode> RenderAsync()
    {
        return await RenderLayout();
    }
    
    protected abstract Task<MarkupNode> RenderLayout();

    public virtual async Task<MarkupNode> RenderPageContent()
    {
        return await Page.RenderContent();
    }
}