using HtmlGen.Core.Services;
using HtmlGen.Core.Structs;

namespace HtmlGen.Core.Interfaces;

public interface ILayout : IAsyncRenderable
{
    IPage Page { get; set; }
    Task<MarkupNode> RenderPageContent();
    internal ComponentResolver ComponentResolver { get; set; }

}