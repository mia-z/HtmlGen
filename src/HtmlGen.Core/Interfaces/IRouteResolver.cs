using Microsoft.AspNetCore.Http;

namespace HtmlGen.Core.Interfaces;

public interface IRouteResolver
{
    public Guid Resolve(PathString pathToMatch, out IEnumerable<KeyValuePair<string, string>> routeParameters);

}