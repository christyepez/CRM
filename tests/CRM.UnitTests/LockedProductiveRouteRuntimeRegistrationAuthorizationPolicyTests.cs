using System.Text.Json;
using CRM.Api.ProductiveRoutes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Xunit;

namespace CRM.UnitTests;

public sealed class LockedProductiveRouteRuntimeRegistrationAuthorizationPolicyTests
{
    [Fact]
    public async Task LockedRoute_WithPolicyFlagDisabled_Returns423WithoutEvaluatingPolicy()
    {
        var app = CreateApp(lockedRegistrationEnabled: true, authorizationPolicyEnabled: false);
        app.TryMapLockedProductiveRoutes();

        var root = await InvokeAsync(app, "/api/crm/leads", "GET");

        Assert.False(root.GetProperty("authorizationPolicyEvaluated").GetBoolean());
        Assert.Equal("NotEvaluatedBecauseDisabled", root.GetProperty("authorizationDecision").GetString());
        Assert.False(root.GetProperty("tokenReadAttempted").GetBoolean());
        Assert.False(root.GetProperty("headerReadAttempted").GetBoolean());
        Assert.False(root.GetProperty("portalHttpCallAttempted").GetBoolean());
        Assert.False(root.GetProperty("sideEffectsAllowed").GetBoolean());
        Assert.False(root.GetProperty("productiveCrudEnabled").GetBoolean());
        Assert.False(root.GetProperty("deleteEndpointsEnabled").GetBoolean());
    }

    [Fact]
    public async Task LockedRoute_WithPolicyFlagEnabled_ReturnsSanitizedDecisionMetadata()
    {
        var app = CreateApp(lockedRegistrationEnabled: true, authorizationPolicyEnabled: true);
        app.TryMapLockedProductiveRoutes();

        var root = await InvokeAsync(app, "/api/crm/contacts", "PATCH");

        Assert.True(root.GetProperty("authorizationPolicyEvaluated").GetBoolean());
        Assert.Equal("BlockedBecauseRouteLocked", root.GetProperty("authorizationDecision").GetString());
        Assert.False(root.GetProperty("portalAuthRuntimeConnected").GetBoolean());
        Assert.False(root.GetProperty("tokenReadAttempted").GetBoolean());
        Assert.False(root.GetProperty("headerReadAttempted").GetBoolean());
        Assert.False(root.GetProperty("authorizationHeaderReadAttempted").GetBoolean());
        Assert.False(root.GetProperty("portalHttpCallAttempted").GetBoolean());
        Assert.False(root.GetProperty("sideEffectsAllowed").GetBoolean());
        Assert.False(root.GetProperty("productiveCrudEnabled").GetBoolean());
        Assert.Equal("Sprint8P6Sprint8GateDecision", root.GetProperty("nextGate").GetString());
    }

    [Fact]
    public void LockedRoute_DoesNotRegisterDelete()
    {
        var app = CreateApp(lockedRegistrationEnabled: true, authorizationPolicyEnabled: true);
        app.TryMapLockedProductiveRoutes();

        var methods = GetLockedRouteEndpoints(app)
            .Where(endpoint => endpoint.RoutePattern.RawText == "/api/crm/accounts")
            .SelectMany(endpoint => endpoint.Metadata.GetMetadata<HttpMethodMetadata>()?.HttpMethods ?? [])
            .ToArray();

        Assert.DoesNotContain("DELETE", methods);
    }

    private static WebApplication CreateApp(bool lockedRegistrationEnabled, bool authorizationPolicyEnabled)
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions { EnvironmentName = "Development" });
        builder.Configuration[LockedProductiveRouteRuntimeRegistration.FlagPath] = lockedRegistrationEnabled.ToString();
        builder.Configuration[LockedProductiveRouteRuntimeRegistration.AuthorizationPolicyFlagPath] = authorizationPolicyEnabled.ToString();
        return builder.Build();
    }

    private static async Task<JsonElement> InvokeAsync(WebApplication app, string route, string method)
    {
        var endpoint = GetLockedRouteEndpoints(app).Single(candidate => candidate.RoutePattern.RawText == route &&
            candidate.Metadata.GetMetadata<HttpMethodMetadata>()?.HttpMethods.Contains(method) == true);
        var context = new DefaultHttpContext();
        context.RequestServices = app.Services;
        context.Response.Body = new MemoryStream();

        await endpoint.RequestDelegate!(context);

        Assert.Equal(StatusCodes.Status423Locked, context.Response.StatusCode);
        context.Response.Body.Position = 0;
        using var document = await JsonDocument.ParseAsync(context.Response.Body);
        return document.RootElement.Clone();
    }

    private static IReadOnlyCollection<RouteEndpoint> GetLockedRouteEndpoints(WebApplication app) =>
        ((IEndpointRouteBuilder)app)
            .DataSources
            .SelectMany(source => source.Endpoints)
            .OfType<RouteEndpoint>()
            .Where(endpoint => endpoint.RoutePattern.RawText is "/api/crm/leads" or "/api/crm/accounts" or "/api/crm/contacts")
            .ToArray();
}
