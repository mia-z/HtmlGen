using HtmlGen.Core.Middleware;
using Microsoft.AspNetCore.Builder;

namespace HtmlGen.Core.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseHtmlGen(this WebApplication app)
    {
        app.UseMiddleware<HtmlGenMiddleware>();
        
        return app;
    }
}