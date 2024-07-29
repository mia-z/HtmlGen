using System.Collections.Immutable;
using HtmlGen.Core.Structs;

namespace HtmlGen.Core.Abstractions;

public abstract class MarkupBuilder
{
    protected MarkupNode strong(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.Strong, markupNodes);
    }

    protected MarkupNode header(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.Header, markupNodes);
    }
    
    protected MarkupNode style(string stylesheetString)
    {
        return MarkupNode.Create(MarkupTagName.Style, stylesheetString);
    }
    
    protected MarkupNode style(params StylesheetNode[] stylesheetNodes)
    {
        return MarkupNode.Create(stylesheetNodes);
    }
    
    protected MarkupNode style(Stylesheet stylesheet)
    {
        return MarkupNode.Create(MarkupTagName.Style, stylesheet);
    }
    
    protected MarkupNode h1(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.H1, markupNodes);
    }
    
    protected MarkupNode h2(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.H2, markupNodes);
    }
    
    protected MarkupNode h3(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.H3, markupNodes);
    }
    
    protected MarkupNode h4(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.H4, markupNodes);
    }
    
    protected MarkupNode h5(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.H5, markupNodes);
    }
    
    protected MarkupNode h6(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.H6, markupNodes);
    }
    
    protected MarkupNode div(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.Div, markupNodes);
    }
    
    protected MarkupNode span(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.Span, markupNodes);
    }
    
    protected MarkupNode a(string hrefValue, params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.A, markupNodes)
            .WithAttributes(("href", hrefValue));
    }

    protected MarkupNode table(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.Table, markupNodes);
    }
    
    protected MarkupNode th(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.Th, markupNodes);
    }
    
    protected MarkupNode tr(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.Tr, markupNodes);
    }
    
    protected MarkupNode td(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.Td, markupNodes);
    }
    
    protected MarkupNode tbody(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.TBody, markupNodes);
    }
    
    protected MarkupNode thead(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.THead, markupNodes);
    }

    protected MarkupNode p(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.P, markupNodes);
    }
    
    protected MarkupNode main(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.Main, markupNodes);
    }

    protected MarkupNode script(string scriptContent)
    {
        return MarkupNode.Create(MarkupTagName.Script, scriptContent);
    }
    
    protected MarkupNode script(string scriptContent, string scriptSrc)
    {
        return MarkupNode.Create(MarkupTagName.Script, scriptContent)
            .WithAttributes(("src", scriptSrc));
    }

    protected MarkupNode button(string buttonContent)
    {
        return MarkupNode.Create(MarkupTagName.Button, buttonContent);
    }
    
    protected MarkupNode br()
    {
        return MarkupNode.Create(MarkupTagName.Br)
            with { IsSelfClosing = true};
    }
    
    protected MarkupNode hr()
    {
        return MarkupNode.Create(MarkupTagName.Hr)
            with { IsSelfClosing = true};
    }
    
    protected MarkupNode label(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(MarkupTagName.Label, markupNodes);
    }
    
    protected MarkupNode input(string type = "text", string? id = null, string? name = null)
    {
        var attributes = new List<MarkupAttribute>();
        attributes.Add(("type", type));
        if (id is not null)
            attributes.Add(("id", id));
        if (name is not null)
            attributes.Add(("name", name));
        
        return MarkupNode.Create(MarkupTagName.Input) with { IsVoid = true, Attributes = attributes.ToArray() };
    }
    
    protected MarkupNode Repeating<T>(IEnumerable<T>? collection, Func<T, MarkupNode> iterFunc)
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
    
    protected MarkupNode Fragment(params MarkupNode[] markupNodes)
    {
        return MarkupNode.Create(markupNodes) with { IsFragment = true};
    }

    internal MarkupNode Marker(string id)
    {
        return MarkupNode.Create(MarkupTagName.Comment, id)
            with { IsComment = true };
    }
}