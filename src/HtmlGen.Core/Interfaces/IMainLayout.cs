using HtmlGen.Core.Structs;

namespace HtmlGen.Core.Interfaces;

public interface IMainLayout : ILayout
{
    Task<MarkupNode> RenderMainLayout(IPage page);
}
