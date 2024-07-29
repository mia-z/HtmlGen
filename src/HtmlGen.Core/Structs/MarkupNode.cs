namespace HtmlGen.Core.Structs;

public readonly struct MarkupNode
{
    public List<MarkupNode> Children { get; private init; } = [];
    public MarkupTag MarkupTag { get; init; }
    public string StringContent { get; init; }
    public MarkupAttribute[] Attributes { get; init; } = [];
    public MarkupStyle[] Styles { get; init; } = [];
    public MarkupClass[] Classes { get; init; } = [];
    public bool IsSelfClosing { get; init; } = false;
    public bool IsVoid { get; init; } = false;
    public bool IsFragment { get; init; } = false;
    public bool IsJustContent { get; init; } = false;
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

    public MarkupNode? this[MarkupTagName tag, int at]
    {
        get
        {
            try
            {
                return Children.Where(x => x.MarkupTag.Tag == tag).ElementAt(at);
            }
            catch (Exception e)
            {
                return null;
            }
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
        this with { Attributes = [..Attributes, ..attrs] };

    public MarkupNode WithClasses(params MarkupClass[] classes) =>
        this with { Classes = classes };

    public MarkupNode WithTailwind(params TailwindUtilityClass[] tw) =>
        this with { Classes = [..Classes, ..tw] };

    public MarkupNode WithStyles(params MarkupStyle[] styleAttrs) =>
        this with { Styles = [..Styles, ..styleAttrs] };

    private string BuildMarkup() => this switch
    {
        { IsJustContent: true } => StringContent,
        { IsComment: true } => $"<!-- {StringContent} -->",
        { IsFragment: true } => string.Join(string.Empty, Children),
        { IsSelfClosing: true } => $"<{MarkupTag} {SerializeTagProperties()} />",
        { IsVoid: true } => $"<{MarkupTag} {SerializeTagProperties()}>",
        _ =>
            $"<{MarkupTag}{SerializeTagProperties()}>" +
                $"{string.Join(string.Empty, Children)}" + 
                $"{StringContent}" +
            $"</{MarkupTag}>"
    };

    private string SerializeTagProperties() =>
        $"{(Attributes.Length > 0 ? Attributes.Serialize() : string.Empty)}{(Styles.Length > 0 ? Styles.Serialize() : string.Empty)}{(Classes.Length > 0 ? Classes.Serialize() : string.Empty)}";
    
    public override string ToString() => BuildMarkup();
    
    public static implicit operator string(MarkupNode m) => m.ToString();

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