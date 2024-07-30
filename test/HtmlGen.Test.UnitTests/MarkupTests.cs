using HtmlGen.Core.Abstractions;
using HtmlGen.Core.Builders;

namespace HtmlGen.Test.UnitTests;

[TestClass]
public class MarkupTests
{
    [TestMethod]
    public void Generates_Valid_ParagraphTag()
    {
        var builder = new MarkupBuilder();
        var html = builder.p("Hello, world!");
        
        var htmlString = html.ToString();
        
        Assert.AreEqual("<p>Hello, world!</p>", htmlString);
    }
}