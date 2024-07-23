using HtmlGen.Core.Enums;

namespace HtmlGen.Core.Structs;

public readonly struct HyperscriptAttribute
{
    private string HyperscriptCommand => ScriptParts;
    private HyperscriptPartCollection ScriptParts { get; init; }
    
    public HyperscriptAttribute()
    {
        ScriptParts = new HyperscriptPartCollection();
    }
    
    public static implicit operator string(HyperscriptAttribute hs) => hs.HyperscriptCommand;

    public static implicit operator MarkupAttribute(HyperscriptAttribute hs) => ("_", hs.HyperscriptCommand);
    
    public HyperscriptAttribute this[string variableName]
    {
        get
        {
            ScriptParts.Append($"{variableName}");
            return this;
        }
    }
    
    public HyperscriptAttribute set(string variableName)
    {
        ScriptParts.Append($"set {variableName}");
        return this;
    }

    public HyperscriptAttribute on(DomEvent eventName)
    {
        ScriptParts.Append($"on {Enum.GetName(eventName)!.ToLower()}");
        return this;
    }
    
    public HyperscriptAttribute on(string itemName)
    {
        ScriptParts.Append($"on {itemName}");
        return this;
    }
    
    public HyperscriptAttribute to(string variableValue)
    {
        ScriptParts.Append($"to {variableValue}");
        return this;
    }
    
    public HyperscriptAttribute then
    {
        get
        {
            ScriptParts.Append($"then");
            return this;
        }
    }
    
    public HyperscriptAttribute and
    {
        get
        {
            ScriptParts.Append($"and");
            return this;
        }
    }
    
    public HyperscriptAttribute log(string valueToLog)
    {
        ScriptParts.Append($"log {valueToLog}");
        return this;
    }
    
    public HyperscriptAttribute increment
    {
        get
        {
            ScriptParts.Append($"increment");
            return this;
        }
    }
    
    public HyperscriptAttribute put
    {
        get
        {
            ScriptParts.Append($"put");
            return this;
        }
    }
    
    public HyperscriptAttribute it
    {
        get
        {
            ScriptParts.Append($"it");
            return this;
        }
    }
    
    public HyperscriptAttribute its
    {
        get
        {
            ScriptParts.Append($"its");
            return this;
        }
    }
    
    public HyperscriptAttribute into
    {
        get
        {
            ScriptParts.Append($"into");
            return this;
        }
    }
    
    public HyperscriptAttribute the
    {
        get
        {
            ScriptParts.Append($"the");
            return this;
        }
    }
    
    public HyperscriptAttribute next(string targetOfNext)
    {
        ScriptParts.Append($"next {targetOfNext}");
        return this;
    }
    
    public HyperscriptAttribute result
    {
        get
        {
            ScriptParts.Append($"result");
            return this;
        }
    }
    
    public HyperscriptAttribute me
    {
        get
        {
            ScriptParts.Append($"me");
            return this;
        }
    }
    
    public HyperscriptAttribute my
    {
        get
        {
            ScriptParts.Append($"my");
            return this;
        }
    }
    
    public HyperscriptAttribute i
    {
        get
        {
            ScriptParts.Append($"i");
            return this;
        }
    }
    
    public HyperscriptAttribute body
    {
        get
        {
            ScriptParts.Append($"body");
            return this;
        }
    }
    
    public HyperscriptAttribute detail
    {
        get
        {
            ScriptParts.Append($"detail");
            return this;
        }
    }
    
    public HyperscriptAttribute sender
    {
        get
        {
            ScriptParts.Append($"sender");
            return this;
        }
    }
    
    public HyperscriptAttribute make
    {
        get
        {
            ScriptParts.Append($"make");
            return this;
        }
    }
    
    public HyperscriptAttribute a(string objectType)
    {
        ScriptParts.Append($"a {objectType}");
        return this;
    }
    
    public HyperscriptAttribute an(string objectType)
    {
        ScriptParts.Append($"an {objectType}");
        return this;
    }
    
    public HyperscriptAttribute called(string variableName)
    {
        ScriptParts.Append($"called {variableName}");
        return this;
    }
    
    public HyperscriptAttribute first
    {
        get
        {
            ScriptParts.Append($"first");
            return this;
        }
    }
    
    public HyperscriptAttribute of(string variableName)
    {
        ScriptParts.Append($"of {variableName}");
        return this;
    }
    
    public HyperscriptAttribute @if(string condition)
    {
        ScriptParts.Append($"if {condition}");
        return this;
    }
    
    public HyperscriptAttribute @else
    {
        get
        {
            ScriptParts.Append($"else");
            return this;
        }
    }
    
    public HyperscriptAttribute end
    {
        get
        {
            ScriptParts.Append($"end");
            return this;
        }
    }
    
    public HyperscriptAttribute otherwise
    {
        get
        {
            ScriptParts.Append($"otherwise");
            return this;
        }
    }
    
    public HyperscriptAttribute toggle(string target)
    {
        ScriptParts.Append($"toggle {target}");
        return this;
    }
    
    public HyperscriptAttribute @is
    {
        get
        {
            ScriptParts.Append($"is");
            return this;
        }
    }
    
    public HyperscriptAttribute not
    {
        get
        {
            ScriptParts.Append($"not");
            return this;
        }
    }
    
    public HyperscriptAttribute empty
    {
        get
        {
            ScriptParts.Append($"empty");
            return this;
        }
    }
}