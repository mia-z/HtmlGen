namespace HtmlGen.Core.Structs;

public readonly struct MarkupTag
{
    public MarkupTagName Tag { get; init; }

    public override string ToString()
    {
        return Tag.ToString().ToLower();
    }

    public static implicit operator string(MarkupTag tag)
    {
        return tag.ToString();
    }
    
    public static implicit operator MarkupTag(MarkupTagName tagName)
    {
        return new MarkupTag
        {
            Tag = tagName
        };
    }
}

public enum MarkupTagName
{
    Comment,
    Div,
    Span,
    Head,
    Body,
    Html,
    Header,
    Footer,
    Main,
    Section,
    Article,
    H1,
    H2,
    H3,
    H4,
    H5,
    H6,
    Table,
    THead,
    TBody,
    Th,
    Tr,
    Td,
    Sup,
    Sub,
    Strong,
    A,
    Ul,
    Ol,
    Li,
    Style,
    Meta,
    Title,
    P,
    U,
    I,
    Script,
    Br,
    Hr
}