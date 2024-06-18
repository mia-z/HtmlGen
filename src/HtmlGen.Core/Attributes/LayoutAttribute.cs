namespace HtmlGen.Core.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class LayoutAttribute(Type layout) : Attribute
{
    public Type Layout => layout;
}