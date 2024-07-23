using System.Collections.Immutable;
using HtmlGen.Core.Structs;
using Microsoft.AspNetCore.Http;

namespace HtmlGen.Core.Interfaces;

public interface IRouteCollection
{
    ImmutableDictionary<RoutePath, Guid> Routes { get; }
}