using HtmlGen.Core.Abstractions;
using HtmlGen.Core.Structs;

namespace HtmlGen.Core.DefaultPages;

internal sealed class DefaultMainLayout : MainLayout
{
    public override async Task<MarkupNode> RenderLayout()
    {
        return
            Fragment("<!DOCTYPE html>",
                html(
                    head(
                        title("C# Static Html Generator"),
                        meta("charset", "UTF-8"),
                        meta("viewport", "width=device-width, initial-scale=1.0"),
                        script("").WithAttributes(("src", "https://cdn.tailwindcss.com")),
                        style(
                            StylesheetNode.Create(
                                "html", 
                                ("line-height", "1.5"),
                                ("box-sizing", "border-box")
                            ),
                            StylesheetNode.Create(
                                "*, *::before, *::after",
                                ("box-sizing", "inherit")
                            ),
                            StylesheetNode.Create(
                                "body",
                                ("margin", "0")
                            )
                        )
                    ),
                    body(
                        await RenderPageContent()
                    )
                ).WithAttributes(("lang", "en"))
            );
    }
}