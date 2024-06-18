using HtmlGen.Core.Services;
using HtmlGen.Core.Structs;

namespace HtmlGen.Core.Interfaces;

public interface IPage : IRoutable, IAsyncRenderable
{
    internal ComponentResolver ComponentResolver { get; set; }
    internal Type? LayoutType { get; init; }
    internal ILayout? Layout { get; set; }
    internal Task<MarkupNode> RenderContent();
    StylesheetNode? ScopedStylesheet { get; init; }
}