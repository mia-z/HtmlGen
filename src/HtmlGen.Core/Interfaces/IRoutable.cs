using Microsoft.AspNetCore.Http;

namespace HtmlGen.Core.Interfaces;

public interface IRoutable
{
    PathString Route { get; }
}