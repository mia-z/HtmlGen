using System.Collections.Immutable;
using HtmlGen.Core.Interfaces;
using HtmlGen.Core.Services;
using HtmlGen.Core.Structs;

namespace HtmlGen.Test.UnitTests;

[TestClass]
public class RouteTests
{
    private const string PathRoutePart = "home";
    private const string ParameterRoutePart = "{userId}";
    private const string InvalidRoutePart = "{rief]";
    
    private const string RootUri = "/";
    private const string SimpleUri = "/home";
    private const string SingleNestedUri = "/home/pictures";
    private const string DoubleNestedUriWithParameter = "/home/pictures/{pictureId}";
    private const string DoubleNestedUriWithInnerParameter = "/home/{userId}/user";

    private static readonly List<KeyValuePair<RoutePath, Guid>> Routes =
    [
        new KeyValuePair<RoutePath, Guid>(RoutePathBuilder.From(RootUri), Guid.NewGuid()),
        new KeyValuePair<RoutePath, Guid>(RoutePathBuilder.From(SimpleUri), Guid.NewGuid()),
        new KeyValuePair<RoutePath, Guid>(RoutePathBuilder.From(SingleNestedUri), Guid.NewGuid()),
        new KeyValuePair<RoutePath, Guid>(RoutePathBuilder.From(DoubleNestedUriWithParameter), Guid.NewGuid()),
        new KeyValuePair<RoutePath, Guid>(RoutePathBuilder.From(DoubleNestedUriWithInnerParameter), Guid.NewGuid())
    ];
    
    private static readonly IRouteCollection _registeredRoutes = new RouteCollection(Routes);
    private readonly IRouteResolver _routeResolver = new RouteResolver(_registeredRoutes);
    
    [TestMethod]
    public void Valid_Path_RoutePart()
    {
        RoutePart routePart = (PathRoutePart, 0);

        Assert.AreEqual("home", routePart.Value);
        Assert.IsTrue(routePart.IsValid);
        Assert.IsFalse(routePart.IsParameter);
    }
    
    [TestMethod]
    public void Valid_Parameter_RoutePart()
    {
        RoutePart routePart = (ParameterRoutePart, 0);
        
        Assert.AreEqual("{userId}", routePart.Template);
        Assert.AreEqual("userId", routePart.Value);
        Assert.IsTrue(routePart.IsValid);
        Assert.IsTrue(routePart.IsParameter);
    }
    
    [TestMethod]
    public void Invalid_RoutePart_Throws()
    {
        void Act() { RoutePart _ = (InvalidRoutePart, 0); }

        Assert.ThrowsException<InvalidOperationException>(Act);
    }

    [TestMethod]
    public void RoutePart_PublicConstructor_Throws()
    {
        void Act() => new RoutePart();

        Assert.ThrowsException<InvalidOperationException>(Act);
    }

    [TestMethod]
    public void RoutePath_Parse_SimpleUri()
    {
        var rp = RoutePathBuilder.From(SimpleUri);
        
        Assert.AreEqual("home", rp[0].Value);
        Assert.IsTrue(rp[0].IsValid);
        Assert.IsFalse(rp[0].IsParameter);
    }
    
    [TestMethod]
    public void RoutePath_Parse_SingleNestedUri()
    {
        var rp = RoutePathBuilder.From(SingleNestedUri);
        
        Assert.AreEqual("home", rp[0].Value);
        Assert.IsTrue(rp[0].IsValid);
        Assert.IsFalse(rp[0].IsParameter);
        
        Assert.AreEqual("pictures", rp[1].Value);
        Assert.IsTrue(rp[1].IsValid);
        Assert.IsFalse(rp[1].IsParameter);
    }
    
    [TestMethod]
    public void RoutePath_Parse_DoubleNestedUriWithParameter()
    {
        var rp = RoutePathBuilder.From(DoubleNestedUriWithParameter);
        
        Assert.AreEqual("home", rp[0].Value);
        Assert.IsTrue(rp[0].IsValid);
        Assert.IsFalse(rp[0].IsParameter);
        
        Assert.AreEqual("pictures", rp[1].Value);
        Assert.IsTrue(rp[1].IsValid);
        Assert.IsFalse(rp[1].IsParameter);
        
        Assert.AreEqual("pictureId", rp[2].Value);
        Assert.AreEqual("{pictureId}", rp[2].Template);
        Assert.IsTrue(rp[2].IsValid);
        Assert.IsTrue(rp[2].IsParameter);
    }
    
    [TestMethod]
    public void RoutePath_Parse_DoubleNestedUriWithInnerParameter()
    {
        var rp = RoutePathBuilder.From(DoubleNestedUriWithInnerParameter);
        
        Assert.AreEqual("home", rp[0].Value);
        Assert.IsTrue(rp[0].IsValid);
        Assert.IsFalse(rp[0].IsParameter);
        
        Assert.AreEqual("userId", rp[1].Value);
        Assert.AreEqual("{userId}", rp[1].Template);
        Assert.IsTrue(rp[1].IsValid);
        Assert.IsTrue(rp[1].IsParameter);
        
        Assert.AreEqual("user", rp[2].Value);
        Assert.IsTrue(rp[2].IsValid);
        Assert.IsFalse(rp[2].IsParameter);
    }
    
    [TestMethod]
    public void Matches_RootUri()
    {
        var pageKey = _routeResolver.Resolve("/", out _);
        
        Assert.AreEqual(Routes[0].Value, pageKey);
    }
    
    [TestMethod]
    public void Matches_SimpleUri()
    {
        var pageKey = _routeResolver.Resolve("/home", out _);
        
        Assert.AreEqual(Routes[1].Value, pageKey);
    }
    
    [TestMethod]
    public void Matches_SimpleUriWithRouteParameter()
    {
        var pageKey = _routeResolver.Resolve("/home/pictures/behjfrckn", out var parameters);
        var dict = parameters.ToImmutableDictionary();
        
        Assert.AreEqual("behjfrckn", dict["pictureId"]);
        Assert.AreEqual(Routes[3].Value, pageKey);
    }
    
    [TestMethod]
    public void Matches_TripleNestedUriWithRouteParameterBetween()
    {
        var pageKey = _routeResolver.Resolve("/home/234/user", out var parameters);
        var dict = parameters.ToImmutableDictionary();
        
        Assert.AreEqual("234", dict["userId"]);
        Assert.AreEqual(Routes[4].Value, pageKey);
    }

    [TestMethod]
    public void DoesntMatch_Nonexistent_Page()
    {
        var pageKey = _routeResolver.Resolve("/hom", out _);
        
        Assert.AreEqual(Guid.Empty, pageKey);
    }
    
    [TestMethod]
    public void Matches_CaseInsensitive()
    {
        var pageKey = _routeResolver.Resolve("/HoMe", out _);
        
        Assert.AreEqual(Routes[1].Value, pageKey); 
    }
}