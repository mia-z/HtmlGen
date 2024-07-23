using System.Reflection;

namespace HtmlGen.Core.Models;

public record HtmlGenConfiguration
{
    public Assembly[] AssembliesToSearch { get; set; } = [];
}