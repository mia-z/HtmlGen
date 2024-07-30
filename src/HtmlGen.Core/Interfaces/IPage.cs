using HtmlGen.Core.Services;
using HtmlGen.Core.Structs;

namespace HtmlGen.Core.Interfaces;

public interface IPage : IRoutable, IAsyncRenderable
{
    public IComponentResolver ComponentResolver { get; set; }
    internal Type? LayoutType { get; init; }
    internal ILayout? Layout { get; set; }
    internal bool HasLayout { get; }
    public Task<MarkupNode> RenderContent();
    Stylesheet? ScopedStylesheet { get; init; }
    string Title { get; set; }
}