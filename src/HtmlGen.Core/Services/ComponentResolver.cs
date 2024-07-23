using System.Collections.Immutable;
using HtmlGen.Core.Attributes;
using HtmlGen.Core.Interfaces;
using HtmlGen.Core.Structs;
using Microsoft.Extensions.DependencyInjection;

namespace HtmlGen.Core.Services;

public class ComponentResolver : IComponentResolver
{
    private readonly IServiceScopeFactory _scopeFactory;
    
    public ComponentResolver(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public IComponent Resolve<TComponent>() where TComponent : IComponent
    {
        var services = _scopeFactory.CreateScope().ServiceProvider;
        return services.GetRequiredKeyedService<IComponent>(typeof(TComponent).Name);
    }
    
    public IComponent Resolve<TComponent>(params ComponentParameter[] parameters) where TComponent : IComponent
    {
        var services = _scopeFactory.CreateScope().ServiceProvider;
        
        var component = services.GetRequiredKeyedService<IComponent>(typeof(TComponent).Name);
        
        var propertiesWithAttribute = component.GetType().GetProperties()
            .Where(prop => prop.CustomAttributes.Any(y => y.AttributeType.Name == nameof(ParameterAttribute)))
            .ToImmutableArray();
        
        foreach (var (key, value) in parameters)
        {
            var matchingProperty = propertiesWithAttribute.FirstOrDefault(p => p.Name == key);
            if (matchingProperty != null && matchingProperty.CanWrite)
            {
                matchingProperty.SetValue(component, value);
            }
        }
        
        return component;
    }
}