using HtmlGen.Core.Interfaces;
using HtmlGen.Core.Services;
using HtmlGen.Core.Structs;

namespace HtmlGen.Core.Abstractions;

public abstract class PageBuilder : MarkupBuilder
{
    public IComponentResolver ComponentResolver { get; set; }
    
    protected MarkupNode Component<T>() where T : Component
    {
        var comp = ComponentResolver.Resolve<T>();
        return comp.Render();
    }

    protected MarkupNode Component<T>(params ComponentParameter[] parameters) where T : Component
    {
        var comp = ComponentResolver.Resolve<T>(parameters);
        
        return comp.Render();
    }

}