namespace HtmlGen.Core.Structs;

public readonly struct MarkupNode
{
    internal List<MarkupNode> Children { get; private init; } = [];
    internal MarkupTag MarkupTag { get; init; }
    internal string StringContent { get; init; }
    internal MarkupAttribute[] Attributes { get; init; } = [];
    internal MarkupStyle[] Styles { get; init; } = [];
    internal MarkupClass[] Classes { get; init; } = [];
    internal bool IsSelfClosing { get; init; } = false;
    internal bool IsVoid { get; init; } = false;
    internal bool IsFragment { get; init; } = false;
    internal bool IsJustContent { get; init; } = false;
    internal bool IsComment { get; init; } = false;
    
    public MarkupNode()
    {
        
    }

    public MarkupNode this[MarkupTagName tag]
    {
        get
        {
            return Children.First(x => x.MarkupTag.Tag == tag);
        }
    }
    
    public static MarkupNode Create(params MarkupNode[] children)
    {
        return new MarkupNode
        {
            Children = children.ToList()
        };
    }
    
    public static MarkupNode Create(MarkupTagName tag, params MarkupNode[] children)
    {
        return new MarkupNode
        {
            MarkupTag = tag,
            Children = children.ToList()
        };
    }
    
    public static MarkupNode Create(MarkupTagName tag, string stringContent)
    {
        return new MarkupNode
        {
            MarkupTag = tag,
            StringContent = stringContent
        };
    }

    public static MarkupNode Create(MarkupTagName tag)
    {
        return new MarkupNode
        {
            MarkupTag = tag
        };
    }
    
    public static MarkupNode Create(params StylesheetNode[] stylesheetNodes)
    {
        return new MarkupNode
        {
            MarkupTag = MarkupTagName.Style,
            StringContent = string.Join(string.Empty, stylesheetNodes)
        };
    }
    public MarkupNode WithAttributes(params MarkupAttribute[] attrs) => 
        this with { Attributes = [ ..Attributes, ..attrs ] };

    public MarkupNode WithClasses(params MarkupClass[] classes) =>
        this with { Classes = classes };

    public MarkupNode WithStyles(params MarkupStyle[] styleAttrs) =>
        this with { Styles = [ ..Styles, ..styleAttrs ] };
    
    private string BuildMarkup()
    {
        if (IsJustContent)
            return StringContent;

        if (IsComment)
            return $"<!-- {StringContent} -->";
        
        if (IsFragment)
            return string.Join(string.Empty, Children);
        
        var markupString = $"<{MarkupTag}";

        if (Attributes.Length > 0)
            markupString += Attributes.Serialize();
        
        if (Styles.Length > 0)
            markupString += Styles.Serialize();
        
        if (Classes.Length > 0)
            markupString += Classes.Serialize();

        if (IsSelfClosing)
            return $"{markupString} />";

        if (IsVoid)
            return $"{markupString}>";
        
        markupString += ">";

        if (Children.Count > 0)
            markupString += string.Join(string.Empty, Children);

        markupString += $"{StringContent}";
        
        markupString += $"</{MarkupTag}>";
        
        return markupString;
    }

    public override string ToString() => BuildMarkup();
    
    public static implicit operator string(MarkupNode m)
    {
        return m.ToString();
    }

    public static implicit operator MarkupNode(DateTime input)
    {
        return new MarkupNode
        {
            StringContent = input.ToString("u"),
            IsJustContent = true
        };
    }
    
    public static implicit operator MarkupNode(float input)
    {
        return new MarkupNode
        {
            StringContent = input.ToString("f"),
            IsJustContent = true
        };
    }
    
    public static implicit operator MarkupNode(double input)
    {
        return new MarkupNode
        {
            StringContent = input.ToString("f"),
            IsJustContent = true
        };
    }
    
    public static implicit operator MarkupNode(int input)
    {
        return new MarkupNode
        {
            StringContent = input.ToString(),
            IsJustContent = true
        };
    }
    
    public static implicit operator MarkupNode(string input)
    {
        return new MarkupNode
        {
            StringContent = input,
            IsJustContent = true
        };
    }
    
    public static implicit operator MarkupNode(StylesheetNode input)
    {
        return new MarkupNode
        {
            StringContent = input.ToString(),
            IsJustContent = true
        };
    }
}