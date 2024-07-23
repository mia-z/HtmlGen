using System.Collections.Immutable;
using HtmlGen.Core.Interfaces;
using HtmlGen.Core.Structs;
using Microsoft.AspNetCore.Http;

namespace HtmlGen.Core.Services;

public class RouteCollection : IRouteCollection
{
    private readonly ImmutableDictionary<RoutePath, Guid> _routes;
    public ImmutableDictionary<RoutePath, Guid> Routes => _routes;

    public RouteCollection(IEnumerable<KeyValuePair<RoutePath, Guid>> routes)
    {
        _routes = routes.ToImmutableDictionary();
    }
}