using HtmlGen.Core.Structs;
using Microsoft.AspNetCore.Http;

namespace HtmlGen.Core.Interfaces;

public interface IPageFactory
{ 
    Task<MarkupNode> GeneratePage(PathString route);
}