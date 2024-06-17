using HtmlGen.Core.Structs;

namespace HtmlGen.Core.Interfaces;

public interface IAsyncRenderable
{
    Task<MarkupNode> RenderAsync();
}