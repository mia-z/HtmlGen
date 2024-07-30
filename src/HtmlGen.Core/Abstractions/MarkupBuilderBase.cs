using System.Collections.Immutable;
using HtmlGen.Core.Structs;

namespace HtmlGen.Core.Abstractions;

public abstract class MarkupBuilderBase
{
    public MarkupNode strong(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.Strong, markupNodes);
    }

    public MarkupNode header(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.Header, markupNodes);
    }
    
    public MarkupNode style(string stylesheetString)
    {
        return MarkupNode.Create(MarkupTagName.Style, stylesheetString);
    }
    
    public MarkupNode style(params StylesheetNode[] stylesheetNodes)
    {
        return MarkupNode.Create(stylesheetNodes);
    }
    
    public MarkupNode style(Stylesheet stylesheet)
    {
        return MarkupNode.Create(MarkupTagName.Style, stylesheet);
    }
    
    public MarkupNode h1(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.H1, markupNodes);
    }
    
    public MarkupNode h2(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.H2, markupNodes);
    }
    
    public MarkupNode h3(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.H3, markupNodes);
    }
    
    public MarkupNode h4(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.H4, markupNodes);
    }
    
    public MarkupNode h5(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.H5, markupNodes);
    }
    
    public MarkupNode h6(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.H6, markupNodes);
    }
    
    public MarkupNode div(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.Div, markupNodes);
    }
    
    public MarkupNode span(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.Span, markupNodes);
    }
    
    public MarkupNode a(string hrefValue, params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.A, markupNodes)
            .WithAttributes(("href", hrefValue));
    }

    public MarkupNode table(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.Table, markupNodes);
    }
    
    public MarkupNode th(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.Th, markupNodes);
    }
    
    public MarkupNode tr(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.Tr, markupNodes);
    }
    
    public MarkupNode td(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.Td, markupNodes);
    }
    
    public MarkupNode tbody(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.TBody, markupNodes);
    }
    
    public MarkupNode thead(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.THead, markupNodes);
    }

    public MarkupNode p(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.P, markupNodes);
    }
    
    public MarkupNode main(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.Main, markupNodes);
    }

    public MarkupNode script(string scriptContent)
    {
        return MarkupNode.Create(MarkupTagName.Script, scriptContent);
    }
    
    public MarkupNode script(string scriptContent, string scriptSrc)
    {
        return MarkupNode.Create(MarkupTagName.Script, scriptContent)
            .WithAttributes(("src", scriptSrc));
    }

    public MarkupNode button(string buttonContent)
    {
        return MarkupNode.Create(MarkupTagName.Button, buttonContent);
    }
    
    public MarkupNode br()
    {
        return MarkupNode.Create(MarkupTagName.Br)
            with { IsSelfClosing = true};
    }
    
    public MarkupNode hr()
    {
        return MarkupNode.Create(MarkupTagName.Hr)
            with { IsSelfClosing = true};
    }
    
    public MarkupNode label(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.Label, markupNodes);
    }
    
    public MarkupNode input(string type = "text", string? id = null, string? name = null)
    {
        var attributes = new List<MarkupAttribute>();
        attributes.Add(("type", type));
        if (id is not null)
            attributes.Add(("id", id));
        if (name is not null)
            attributes.Add(("name", name));
        
        return MarkupNode.Create(MarkupTagName.Input) with { IsVoid = true, Attributes = attributes.ToArray() };
    }
    
    public MarkupNode Repeating<T>(IEnumerable<T>? collection, Func<T, MarkupNode> iterFunc)
    {
        if (collection is null)
            return "";
        var markupNodes = new List<MarkupNode>();
        foreach (var item in collection)
        {
            var mi = iterFunc.Invoke(item);
            markupNodes.Add(mi);
        }
        return Fragment(markupNodes.ToArray());
    }
    
    public MarkupNode Fragment(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(markupNodes) with { IsFragment = true};
    }

    internal MarkupNode Marker(string id)
    {
        return MarkupNode.Create(MarkupTagName.Comment, id)
            with { IsComment = true };
    }
}