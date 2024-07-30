using HtmlGen.Core.Structs;

namespace HtmlGen.Core.Interfaces;

public interface IAsyncRenderable
{
    protected internal Task<MarkupNode> RenderAsync();
}