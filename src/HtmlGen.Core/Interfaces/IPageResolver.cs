namespace HtmlGen.Core.Interfaces;

public interface IPageResolver
{
    IPage Resolve(Guid guid, IEnumerable<KeyValuePair<string, string>> parameters);
}