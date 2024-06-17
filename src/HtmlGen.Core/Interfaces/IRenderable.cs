using HtmlGen.Core.Structs;

namespace HtmlGen.Core.Interfaces;

public interface IRenderable
{
    MarkupNode Render();
}