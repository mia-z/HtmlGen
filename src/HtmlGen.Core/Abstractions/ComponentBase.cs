using HtmlGen.Core.Interfaces;
using HtmlGen.Core.Structs;

namespace HtmlGen.Core.Abstractions;

public abstract class ComponentBase : MarkupBuilderBase, IComponent
{
    public abstract MarkupNode RenderComponent();

    public MarkupNode Render()
    {
        return Fragment(
            Marker($"{GetType().Name}-{Guid.NewGuid().ToString()}"),
            RenderComponent()
        );
    }
}