using HtmlGen.Core.Abstractions;

namespace HtmlGen.Test.UnitTests;

[TestClass]
public class MarkupTests : MarkupBuilder
{
    [TestMethod]
    public void Generates_Valid_ParagraphTag()
    {
        var html = p("Hello, world!");
        
        var htmlString = html.ToString();
        
        Assert.AreEqual("<p>Hello, world!</p>", htmlString);
    }
}