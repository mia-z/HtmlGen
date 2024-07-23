using HtmlGen.Core.Structs;
using Microsoft.AspNetCore.Http;

namespace HtmlGen.Core.Services;

public static class RoutePathBuilder
{
    public static RoutePath From(string path)
    {
        IEnumerable<RoutePart> routeParts = [];

        var parts = path[(path.StartsWith('/') ? 1 : 0)..]
            .Split('/');

        for (var x = 0; x < parts.Length; x++)
            routeParts = routeParts.Append((parts[x], (short)x));

        return new RoutePath(routeParts);
    }
}