using System.Collections.Immutable;
using System.Reflection;
using System.Text.RegularExpressions;
using HtmlGen.Core.Abstractions;
using HtmlGen.Core.Attributes;
using HtmlGen.Core.DefaultLayouts;
using HtmlGen.Core.DefaultPages;
using HtmlGen.Core.Interfaces;
using HtmlGen.Core.Models;
using HtmlGen.Core.Services;
using HtmlGen.Core.Structs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace HtmlGen.Core.Extensions;

public static class WebApplicationBuilderExtensions
{
    private static ImmutableList<Type> assemblyTypes { get; set; }
    
    public static WebApplicationBuilder RegisterHtmlGen(this WebApplicationBuilder builder, HtmlGenConfiguration? config = null)
    {
        config = config is null
            ? new HtmlGenConfiguration
            {
                AssembliesToSearch = [ typeof(WebApplicationBuilderExtensions).Assembly, Assembly.GetCallingAssembly() ]
            }
            : config with
            {
                AssembliesToSearch = [ typeof(WebApplicationBuilderExtensions).Assembly, Assembly.GetCallingAssembly(), ..config.AssembliesToSearch ]
            };
        
        builder.Services.AddSingleton<IPageFactory, PageFactory>();
        
        builder.Services.AddSingleton<IComponentResolver, ComponentResolver>();
        builder.Services.AddSingleton<IRouteResolver, RouteResolver>();
        builder.Services.AddSingleton<IPageResolver, PageResolver>();
        builder.Services.AddSingleton<IRouteResolver, RouteResolver>();
        builder.Services.AddSingleton<ILayoutResolver, LayoutResolver>();
        
        assemblyTypes = config.AssembliesToSearch
            .SelectMany(x => x.GetTypes())
            .ToImmutableList();

        builder.RegisterPagesAndRoutes();
        builder.RegisterComponents();
        builder.RegisterLayouts();
        builder.RegisterMainLayout();
        builder.RegisterNotFoundPage();
        
        return builder;
    }

    private static WebApplicationBuilder RegisterMainLayout(this WebApplicationBuilder builder)
    {
        var mainLayout = assemblyTypes
            .Where(x => !x.IsAbstract && x.IsSubclassOf(typeof(MainLayout)) && x != typeof(DefaultMainLayout))
            .ToImmutableArray();

        if (mainLayout.Length > 1)
            throw new InvalidOperationException("Can only have one MainLayout");

        builder.Services.Add(mainLayout.Length == 0
            ? new ServiceDescriptor(typeof(IMainLayout), typeof(DefaultMainLayout), ServiceLifetime.Singleton)
            : new ServiceDescriptor(typeof(IMainLayout), mainLayout.First(), ServiceLifetime.Singleton));
        
        return builder;
    }
    
    private static WebApplicationBuilder RegisterNotFoundPage(this WebApplicationBuilder builder)
    {
        var notFoundPage = assemblyTypes
            .Where(x => !x.IsAbstract && x.IsSubclassOf(typeof(NotFoundPage)))
            .ToImmutableArray();

        if (notFoundPage.Length > 1)
            throw new InvalidOperationException("Can only have one NotFound page");

        builder.Services.Add(notFoundPage.Length == 0
            ? new ServiceDescriptor(typeof(NotFoundPage), typeof(DefaultNotFoundPage), ServiceLifetime.Singleton)
            : new ServiceDescriptor(typeof(NotFoundPage), notFoundPage.First(), ServiceLifetime.Singleton));
        
        return builder;
    }

    private static WebApplicationBuilder RegisterPagesAndRoutes(this WebApplicationBuilder builder)
    {
        var registeredPages = assemblyTypes
            .Where(x => x is { IsAbstract: false, BaseType: not null } && 
                x.BaseType.CustomAttributes
                    .All(y => y.AttributeType != typeof(FallbackPageAttribute)) && 
                x.IsSubclassOf(typeof(Page)))
            .ToImmutableArray();

        var routes = Enumerable.Empty<KeyValuePair<RoutePath, Guid>>();
        
        foreach (var page in registeredPages)
        {
            var attribute = page.GetCustomAttribute(typeof(RouteAttribute));
            if (attribute is RouteAttribute routeAttr)
            {
                var guid = Guid.NewGuid();
                var route = RoutePathBuilder.From(routeAttr.Route.ToLower());
                routes = routes.Append(new KeyValuePair<RoutePath, Guid>(route, guid));
                builder.Services.Add(new ServiceDescriptor(typeof(IPage), guid, page, ServiceLifetime.Scoped));
            }
            else
            {
                throw new InvalidOperationException($"Page {page.Name} has no [Route] attribute");
            }
        }

        builder.Services.AddSingleton<IRouteCollection, RouteCollection>(_ => new RouteCollection(routes));
        
        return builder;
    }
    
    private static WebApplicationBuilder RegisterComponents(this WebApplicationBuilder builder)
    {
        var registeredComponents = assemblyTypes
            .Where(x => !x.IsAbstract && x.IsSubclassOf(typeof(Component)))
            .ToImmutableArray();

        foreach (var component in registeredComponents)
            builder.Services.Add(new ServiceDescriptor(typeof(IComponent), component.UnderlyingSystemType.Name, component, ServiceLifetime.Scoped));
        
        return builder;
    }
    
    private static WebApplicationBuilder RegisterLayouts(this WebApplicationBuilder builder)
    {
        var registeredLayouts = assemblyTypes
            .Where(x => !x.IsAbstract && x.IsSubclassOf(typeof(Layout)))
            .ToImmutableArray();
        
        foreach(var layout in registeredLayouts)
            builder.Services.Add(new ServiceDescriptor(typeof(ILayout), layout.UnderlyingSystemType.Name, layout, ServiceLifetime.Scoped));
        
        return builder;
    }
}