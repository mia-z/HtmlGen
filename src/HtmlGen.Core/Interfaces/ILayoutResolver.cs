namespace HtmlGen.Core.Interfaces;

public interface ILayoutResolver
{
    IPage Resolve(IPage page);
}