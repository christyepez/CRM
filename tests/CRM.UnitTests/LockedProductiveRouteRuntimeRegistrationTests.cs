using System.Text.Json;
using CRM.Api.ProductiveRoutes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Xunit;

namespace CRM.UnitTests;

public sealed class LockedProductiveRouteRuntimeRegistrationTests
{
    [Fact]
    public void TryMapLockedProductiveRoutes_DoesNotRegisterRoutesByDefault()
    {
        var app = CreateApp("Development", enabled: false);

        var registered = app.TryMapLockedProductiveRoutes();

        Assert.False(registered);
        Assert.Empty(GetLockedRouteEndpoints(app));
    }

    [Fact]
    public void TryMapLockedProductiveRoutes_DoesNotRegisterRoutesInProduction()
    {
        var app = CreateApp("Production", enabled: true);

        var registered = app.TryMapLockedProductiveRoutes();

        Assert.False(registered);
        Assert.Empty(GetLockedRouteEndpoints(app));
    }

    [Fact]
    public void TryMapLockedProductiveRoutes_RegistersOnlyLockedNonProductionMethods()
    {
        var app = CreateApp("Development", enabled: true);

        var registered = app.TryMapLockedProductiveRoutes();
        var endpoints = GetLockedRouteEndpoints(app);

        Assert.True(registered);
        foreach (var route in new[] { "/api/crm/leads", "/api/crm/accounts", "/api/crm/contacts" })
        {
            var methods = endpoints
                .Where(endpoint => endpoint.RoutePattern.RawText == route)
                .SelectMany(endpoint => endpoint.Metadata.GetMetadata<HttpMethodMetadata>()?.HttpMethods ?? [])
                .Order(StringComparer.Ordinal)
                .ToArray();

            Assert.Equal(["GET", "PATCH", "POST", "PUT"], methods);
            Assert.DoesNotContain("DELETE", methods);
        }
    }

    [Fact]
    public async Task LockedRouteHandler_ReturnsSanitized423WithoutSideEffects()
    {
        var app = CreateApp("Development", enabled: true);
        app.TryMapLockedProductiveRoutes();
        var endpoint = GetLockedRouteEndpoints(app).Single(route => route.RoutePattern.RawText == "/api/crm/leads" &&
            route.Metadata.GetMetadata<HttpMethodMetadata>()?.HttpMethods.Contains("GET") == true);
        var context = new DefaultHttpContext();
        context.RequestServices = app.Services;
        context.Response.Body = new MemoryStream();

        await endpoint.RequestDelegate!(context);

        context.Response.Body.Position = 0;
        var body = await JsonDocument.ParseAsync(context.Response.Body);
        var root = body.RootElement;

        Assert.Equal(StatusCodes.Status423Locked, context.Response.StatusCode);
        Assert.Equal("Locked", root.GetProperty("status").GetString());
        Assert.Equal("CRM_PRODUCTIVE_ROUTE_LOCKED", root.GetProperty("code").GetString());
        Assert.Equal("/api/crm/leads", root.GetProperty("route").GetString());
        Assert.Equal("GET", root.GetProperty("method").GetString());
        Assert.False(root.GetProperty("sideEffectsAllowed").GetBoolean());
        Assert.False(root.GetProperty("productiveCrudEnabled").GetBoolean());
        Assert.False(root.GetProperty("domainExecutionEnabled").GetBoolean());
        Assert.False(root.GetProperty("persistenceEnabled").GetBoolean());
        Assert.False(root.GetProperty("portalAuthRuntimeEnabled").GetBoolean());
        Assert.Equal("Sprint7P6Sprint7GateDecision", root.GetProperty("nextGate").GetString());
        Assert.DoesNotContain("token", root.GetRawText(), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("secret", root.GetRawText(), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("connection", root.GetRawText(), StringComparison.OrdinalIgnoreCase);
    }

    private static WebApplication CreateApp(string environment, bool enabled)
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            EnvironmentName = environment
        });
        builder.Configuration[LockedProductiveRouteRuntimeRegistration.FlagPath] = enabled.ToString();
        return builder.Build();
    }

    private static IReadOnlyCollection<RouteEndpoint> GetLockedRouteEndpoints(WebApplication app) =>
        ((IEndpointRouteBuilder)app)
            .DataSources
            .SelectMany(source => source.Endpoints)
            .OfType<RouteEndpoint>()
            .Where(endpoint => endpoint.RoutePattern.RawText is "/api/crm/leads" or "/api/crm/accounts" or "/api/crm/contacts")
            .ToArray();
}
