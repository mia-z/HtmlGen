using HtmlGen.Core.Interfaces;
using HtmlGen.Core.Structs;
using Microsoft.AspNetCore.Http;

namespace HtmlGen.Core.Services;

internal class PageFactory : IPageFactory
{
    private readonly IRouteResolver _routeResolver;
    private readonly IPageResolver _pageResolver;
    private readonly IMainLayout _mainLayout;
    private readonly ILayoutResolver _layoutResolver;
    
    public PageFactory(IMainLayout mainLayout, IRouteResolver routeResolver, IPageResolver pageResolver, ILayoutResolver layoutResolver)
    {
        _mainLayout = mainLayout;
        _routeResolver = routeResolver;
        _pageResolver = pageResolver;
        _layoutResolver = layoutResolver;
    }

    public async Task<MarkupNode> GeneratePage(PathString route)
    {
        var pageKey = _routeResolver.Resolve(route, out var routeParameters);
        var page = _pageResolver.Resolve(pageKey, routeParameters);
        if (page.HasLayout)
            page = _layoutResolver.Resolve(page);
        
        return await _mainLayout.RenderMainLayout(page);
    }
}