using HtmlGen.Core.Attributes;

namespace HtmlGen.Core.Abstractions;

public abstract class Page : PageBase
{
    protected Page()
    {
        var attribute = Attribute.GetCustomAttribute(GetType(), typeof(RouteAttribute));

        if (attribute is RouteAttribute attr)
        {
            Route = attr.Route;
        }
        else
        {
            throw new ArgumentNullException(nameof(Route), "Need to set Route attribute on page");
        }
    }
}