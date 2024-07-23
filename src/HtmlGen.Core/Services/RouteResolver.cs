using System.Collections.Immutable;
using System.Text.RegularExpressions;
using HtmlGen.Core.Interfaces;
using HtmlGen.Core.Structs;
using Microsoft.AspNetCore.Http;

namespace HtmlGen.Core.Services;

public class RouteResolver : IRouteResolver
{
    private readonly IRouteCollection _routeCollection;
    
    public RouteResolver(IRouteCollection routeCollection)
    {
        _routeCollection = routeCollection;
    }

    public Guid Resolve(PathString pathToMatch, out IEnumerable<KeyValuePair<string, string>> routeParameters)
    {
        routeParameters = [];
        
        var parts = pathToMatch
            .ToString()
            .Substring(1)
            .Split('/')
            .ToImmutableArray();

        var preliminaryRouteParameters = Enumerable.Empty<KeyValuePair<string, string>>();
        
        var match = _routeCollection.Routes.FirstOrDefault(pair =>
        {
            if (pair.Key.Match(parts, out var parameters))
            {
                preliminaryRouteParameters = parameters;
                return true;
            }
            return false;
        });

        routeParameters = preliminaryRouteParameters;
        return match.Value;
    }
}