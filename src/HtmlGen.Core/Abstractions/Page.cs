using HtmlGen.Core.Attributes;

namespace HtmlGen.Core.Abstractions;

public abstract class Page : PageBase
{
    protected Page()
    {
        var routeAttribute = Attribute.GetCustomAttribute(GetType(), typeof(RouteAttribute));
        if (routeAttribute is RouteAttribute routeAttr)
        {
            Route = routeAttr.Route;
        }
        else
        {
            throw new ArgumentNullException(nameof(Route), "Need to set Route attribute on page");
        }

        var layoutAttribute = Attribute.GetCustomAttribute(GetType(), typeof(LayoutAttribute));
        if (layoutAttribute is LayoutAttribute layoutAttr)
        {
            LayoutType = layoutAttr.Layout;
        }
    }
}