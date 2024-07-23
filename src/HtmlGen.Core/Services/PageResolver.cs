using System.Collections.Immutable;
using HtmlGen.Core.Abstractions;
using HtmlGen.Core.Attributes;
using HtmlGen.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HtmlGen.Core.Services;

public class PageResolver : IPageResolver
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IComponentResolver _componentResolver;
    
    public PageResolver(IServiceScopeFactory scopeFactory, IComponentResolver componentResolver)
    {
        _scopeFactory = scopeFactory;
        _componentResolver = componentResolver;
    }

    public IPage Resolve(Guid guid, IEnumerable<KeyValuePair<string, string>> parameters)
    {
        var services = _scopeFactory.CreateScope().ServiceProvider;
        var page = guid == Guid.Empty
            ? services.GetRequiredService<NotFoundPage>()
            : services.GetKeyedService<IPage>(guid);

        if (page is null)
            throw new InvalidOperationException("Page is null");

        page.ComponentResolver = _componentResolver;

        if (!parameters.Equals(Enumerable.Empty<KeyValuePair<string, string>>()))
        {
            var propertiesWithAttribute = page.GetType().GetProperties()
                .Where(prop => prop.CustomAttributes.Any(y => y.AttributeType.Name == nameof(RouteParameterAttribute)))
                .ToImmutableArray();
        
            foreach (var (key, value) in parameters)
            {
                var matchingProperty = propertiesWithAttribute.FirstOrDefault(p => p.Name.ToLower().Equals(key.ToLower()));
                if (matchingProperty != null && matchingProperty.CanWrite)
                {
                    matchingProperty.SetValue(page, value);
                }
            }
        }
        
        return page;
    }
}