using HtmlGen.Core.Services;
using HtmlGen.Core.Structs;

namespace HtmlGen.Core.Interfaces;

public interface IPage : IRoutable, IAsyncRenderable
{
    internal ComponentResolver ComponentResolver { get; set; }
    StylesheetNode? ScopedStylesheet { get; init; }
}