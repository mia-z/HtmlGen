using HtmlGen.Core.Structs;

namespace HtmlGen.Core.Interfaces;

public interface IAsyncRenderable
{
    internal Task<MarkupNode> RenderAsync();
}