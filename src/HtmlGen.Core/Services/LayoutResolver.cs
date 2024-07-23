using HtmlGen.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HtmlGen.Core.Services;

public class LayoutResolver : ILayoutResolver
{
    private readonly IComponentResolver _componentResolver;
    private readonly IServiceScopeFactory _scopeFactory;

    public LayoutResolver(IComponentResolver componentResolver, IServiceScopeFactory scopeFactory)
    {
        _componentResolver = componentResolver;
        _scopeFactory = scopeFactory;
    }

    public IPage Resolve(IPage page)
    {
        var services = _scopeFactory.CreateScope().ServiceProvider;
        
        page.Layout = services.GetRequiredKeyedService<ILayout>(page.LayoutType!.Name);
        page.Layout.ComponentResolver = _componentResolver;

        return page;
    }
}