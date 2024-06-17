using Microsoft.AspNetCore.Http;

namespace HtmlGen.Core.Interfaces;

public interface IPageFactory
{
    public IPage? GeneratePage(PathString route);
}