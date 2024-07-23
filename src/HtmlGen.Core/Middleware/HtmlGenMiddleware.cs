using System.Diagnostics;
using HtmlGen.Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace HtmlGen.Core.Middleware;

internal sealed class HtmlGenMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IPageFactory _pageFactory;
    
    public HtmlGenMiddleware(RequestDelegate next, IPageFactory pageFactory)
    {
        _next = next;
        _pageFactory = pageFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var page = await _pageFactory.GeneratePage(context.Request.Path);
        await context.Response.WriteAsync(page);
        await _next(context);
    }
}