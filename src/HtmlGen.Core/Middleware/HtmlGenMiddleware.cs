using HtmlGen.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace HtmlGen.Core.Middleware;

internal sealed class HtmlGenMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IMainLayout _mainLayout;
    private readonly IPageFactory _pageFactory;
    
    public HtmlGenMiddleware(RequestDelegate next, IServiceProvider services, IPageFactory pageFactory)
    {
        _next = next;
        _pageFactory = pageFactory;

        var scopedServiceProvider = services.CreateScope();
        _mainLayout = scopedServiceProvider.ServiceProvider.GetRequiredService<IMainLayout>();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var page = _pageFactory.GeneratePage(context.Request.Path);
        if (page is not null)
        {
            var html = await _mainLayout.RenderLayout(page);
            await context.Response.WriteAsync(html);
        }

        await _next(context);
    }
}