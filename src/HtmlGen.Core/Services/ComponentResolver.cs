using System.Collections.Immutable;
using System.Reflection;
using HtmlGen.Core.Interfaces;
using HtmlGen.Core.Structs;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using IComponent = HtmlGen.Core.Interfaces.IComponent;

namespace HtmlGen.Core.Services;

public class ComponentResolver
{
    private readonly IServiceProvider _services;
    
    public ComponentResolver(IServiceProvider services)
    {
        _services = services;
    }

    protected IComponent Resolve<TComponent>() where TComponent : notnull
    {
        var scoped = _services.CreateScope().ServiceProvider;
        return (IComponent) scoped.GetRequiredKeyedService<TComponent>(typeof(TComponent).Name);
    }
    
    internal IComponent Resolve<TComponent>(params ComponentParameter[] parameters) where TComponent : notnull
    {
        var scoped = _services.CreateScope().ServiceProvider;
        var component = scoped.GetRequiredKeyedService<TComponent>(typeof(TComponent).Name);
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
        
        return (IComponent) component;
    }
}