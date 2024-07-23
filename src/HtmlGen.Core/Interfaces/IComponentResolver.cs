using HtmlGen.Core.Structs;

namespace HtmlGen.Core.Interfaces;

public interface IComponentResolver
{
    IComponent Resolve<TComponent>() where TComponent : IComponent;
    IComponent Resolve<TComponent>(params ComponentParameter[] parameters) where TComponent : IComponent;
}