using HtmlGen.Core.Interfaces;
using HtmlGen.Core.Structs;

namespace HtmlGen.Core.Abstractions;

public abstract class LayoutBase : MarkupBuilder, ILayout
{
    protected IPage PageContent { get; set; }
    
    public abstract Task<MarkupNode> RenderLayout();

    public async Task<MarkupNode> RenderAsync()
    {
        return await RenderLayout();
    }
}