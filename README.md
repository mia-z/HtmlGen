# HTML Gen

HTML Gen can be considered a framework for creating static sites using C# using the dotnet runtime.

It provides a way of creating html pages, page layouts and components in a declarative manner.

---

## This project is a WIP

Use this for testing or inspiration, not in a production environment!

---

<h3 align="center"> Full docs can <a href="https://htmlgen.miaz.dev">be found here</a></h3>

---

### Prerequisites

When using this in a dotnet app, register the core services using the extension method `.AddHtmlGen()` and then in the middleware pipeline using `.UseHtmlGen()`

<sub>Minimal API example
```c#
var builder = WebApplication.CreateBuilder(args);
builder.AddHtmlGen();

//Code omitted

var app = builder.Build();
app.UseHtmlGen();
```

---

### Pages and Routing

Pages will be the bread and butter of your site. To be functional, page must do two things:

- Use the Class Attribute `[Route(string)]`
- Inherit from Abstract Class `Page`
- Implement the abstract method `RenderContent()`

From there, your page will be reachable at the value provided in the Route Attribute.

```c#
[Route("/")]
public class Home : Page
{   
    public override async Task<MarkupNode> RenderContent() =>
        div("Hello world!");
}
```

---

### Layouts

They are two types of layout you can use.

#### MainLayout

The MainLayout is the page _outside_ of the `<body>` tag.
If you dont declare a main layout, default values will be used, which looks like this

```html
<!DOCTYPE html>
<html lang="en">
    <head>
        <title>HTMLGen C# - Static web-site framework</title>
        <meta charset="UTF-8">
        <meta viewport="width=device-width, initial-scale=1.0">
    </head>
    <body>
        <!-- layouts and pages rendered in here -->
    </body>
</html>
```

By inheriting from `MainLayoutBase`, you can customize the tags in the `<head>`.

```c#
public class MainLayout : MainLayoutBase
{
    public MainLayout()
    {
        UseHyperscript = true;
        UseTailwind = true;
    }

    protected override MarkupNode RenderHeadTags() =>
        Fragment(
            meta("description", "Framework for creating static sites"),
            meta("keywords", "C#, HTML, CSS, dotnet"),
            base.RenderMetaTags()
        );

    protected override async Task<MarkupNode> RenderLayout() => await RenderPageContent();
}
```

By overriding the `RenderHeadTags` function, you can provide additional tags that go in the head.

##### The `base.RenderHeadTags()` function contain the charset and responsive-viewport meta tags, so they're useful to include. 

##### Note: that you must wrap the meta tags in a Fragment as shown

Overriding `RenderLayout` is required as this is the entrypoint for the layout. If you don't wish to return any further inner content, you can just await the call to `RenderPageContent()`

Calling `RenderPageContent()` is where all the requested page's content will be rendered

```c#
protected override async Task<MarkupNode> RenderLayout() =>
    Fragment(
        header(
            div("Content for the header on every page")
        ),
        main(
            await RenderPageContent()
        )
    );
```

`MainLayoutBase` contains some other properties which you can configure to further customize the main layout.

- `bool NormalizeBaseCss` is a flag on whether to include a stylesheet which normalizes the base stylesheet for the browser. This includes setting global box sizing, and removing default user-agent margins.
##### defaults to false
- `bool UseTailwind` is a flag for on whether to include a `<link>` tag in the head providing a source to TailwindCSS's global stylesheet.
##### defaults to false
- `bool UseHyperscript` is a flag for on whether to include a `<link>` tag in the head providing the ability to use Hyperscript tags on elements.
##### defaults to true
- `string DocumentLanguage` value for the `<html>` attribute lang
##### defaults to 'en'

#### Common Layouts

The other type is a layout which can be used across pages to establish a common style, ie authenticated page, unauthenticated page, etc.

Creating one is similar to a `MainLayout`, in that you include the overridden method `RenderLayout()`, and then call `RenderPageContent` to render the page's content, except you inherit from `Layout`.

```c#
public class BodyLayout : Layout
{
    protected override async Task<MarkupNode> RenderLayout() =>
        main(
            await RenderPageContent()
        ).WithClasses("w-full h-screen flex flex-col bg-slate-900/80");
}
```

And then to use it in a `Page`, you apply the `[Layout]` Attribute to the `Page` Class.

```c#
[Route("/")]
[Layout(typeof(BodyLayout))]
public class Home : Page
{   
    public override async Task<MarkupNode> RenderContent() =>
        div("Hello world!");
}
```

---

### Components

Components are reusable sections of declared elements that can be used elsewhere in pages and layouts so you don't have to repeat yourself, and provide some method of composition to the framework.

You can create a component by inheriting from the `Component` Abstract Class, and then override the `RenderComponent()` method with your desired markup.

```c#
public class Header : Component
{
    public override MarkupNode RenderComponent() =>
        header(
            div("This is the header")
        ).WithClasses("h-32", "bg-zinc-700");
}
```

You can then use it in your pages and layouts by calling the `Component` helper and providing the Type Parameter of the component

```c#
public class BodyLayout : Layout
{    
    protected override async Task<MarkupNode> RenderLayout() =>
        main(
            Component<Header>(),
            await RenderPageContent()
        ).WithClasses("w-full h-screen flex flex-col bg-slate-900/80");
}
```

---

Those are all the basic building blocks of HTML Gen to get started, more features can be <a href="https://htmlgen.miaz.dev">found in the docs</a> 