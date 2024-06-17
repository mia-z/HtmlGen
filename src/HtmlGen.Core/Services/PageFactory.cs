using HtmlGen.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace HtmlGen.Core.Services;

internal class PageFactory : IPageFactory
{
    private readonly ComponentResolver _componentResolver;
    private readonly IServiceProvider _serviceProvider;
    
    public PageFactory(ComponentResolver componentResolver, IServiceProvider serviceProvider)
    {
        _componentResolver = componentResolver;
        _serviceProvider = serviceProvider;
    }

    public IPage? GeneratePage(PathString route)
    {
        var scope = _serviceProvider.CreateScope().ServiceProvider;
        
        var page = scope.GetServices<IPage>().FirstOrDefault(x => x.Route == route);

        if (page is null)
            return null;
        
        page.ComponentResolver = _componentResolver;
        return page;
    }
}