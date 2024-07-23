using System.ComponentModel;
using System.Reflection;

namespace HtmlGen.Core.Structs;

public readonly record struct TailwindUtilityClass(string Class)
{
    public static implicit operator string(TailwindUtilityClass c) => c.Class;
    public static implicit operator TailwindUtilityClass(string s) => new (s);
    public static implicit operator MarkupClass(TailwindUtilityClass c) => c.Class;
}