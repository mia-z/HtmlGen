namespace HtmlGen.Core.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class RouteAttribute(string route) : Attribute
{
    public string Route => route;
}