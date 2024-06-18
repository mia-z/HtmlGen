using System.Collections.Immutable;
using System.Reflection;
using HtmlGen.Core.Abstractions;
using HtmlGen.Core.DefaultPages;
using HtmlGen.Core.Interfaces;
using HtmlGen.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace HtmlGen.Core.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder RegisterHtmlGen(this WebApplicationBuilder builder)
    {
        var assemblyTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .ToImmutableList();
        
        builder.Services.AddSingleton<IPageFactory, PageFactory>();
        builder.Services.AddSingleton<ComponentResolver>();
        
        var registeredPages = assemblyTypes
            .Where(x => !x.IsAbstract && x.IsSubclassOf(typeof(Page)))
            .ToImmutableArray();

        foreach (var page in registeredPages)
            builder.Services.Add(new ServiceDescriptor(typeof(IPage), page, ServiceLifetime.Scoped));

        var registeredComponents = assemblyTypes
            .Where(x => !x.IsAbstract && x.IsSubclassOf(typeof(Component)))
            .ToImmutableArray();

        foreach (var component in registeredComponents)
            builder.Services.Add(new ServiceDescriptor(component.UnderlyingSystemType, component.UnderlyingSystemType.Name, component, ServiceLifetime.Scoped));

        var registeredLayouts = assemblyTypes
            .Where(x => !x.IsAbstract && x.IsSubclassOf(typeof(Layout)))
            .ToImmutableArray();
        
        foreach(var layout in registeredLayouts)
            builder.Services.Add(new ServiceDescriptor(typeof(ILayout), layout.UnderlyingSystemType.Name, layout, ServiceLifetime.Scoped));
        
        var mainLayout = assemblyTypes
            .Where(x => !x.IsAbstract && x.IsSubclassOf(typeof(MainLayout)))
            .ToImmutableArray();

        if (mainLayout.Length > 1)
            throw new InvalidOperationException("Can only have one MainLayout present");

        builder.Services.Add(mainLayout.Length == 0
            ? new ServiceDescriptor(typeof(IMainLayout), typeof(DefaultMainLayout), ServiceLifetime.Singleton)
            : new ServiceDescriptor(typeof(IMainLayout), mainLayout.First(), ServiceLifetime.Singleton));

        return builder;
    }
}